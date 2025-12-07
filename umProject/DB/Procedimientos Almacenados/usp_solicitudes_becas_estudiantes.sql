USE umDb
GO

/* Flujo de solicitudes de becas para estudiantes */
CREATE OR ALTER PROC dbo.usp_solicitudes_becas_estudiantes
(
    @Id_Tipo_Transaccion INT,
    @Id_Beca_Programa INT = NULL,
    @Observaciones NVARCHAR(1000) = NULL,
    @Id_Solicitud_Beca INT = NULL,
    @Id_Sesion INT = NULL,
    @o_Num INT OUTPUT,
    @o_Msg NVARCHAR(255) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Linea_Error INT, @Numero_Error INT, @Mensaje_Error NVARCHAR(255), @Origen_Error NVARCHAR(50) = ERROR_PROCEDURE();

    IF ISNULL(@Id_Sesion,0)=0
    BEGIN
        SET @o_Num = -1;
        SET @o_Msg = N'¡No hay sesión activa!';
        RETURN;
    END

    /* =========================================================
       TX 188: LISTAR PROGRAMAS DE BECAS DISPONIBLES (ACTIVOS)
       ========================================================= */
    IF (@Id_Tipo_Transaccion = 188)
    BEGIN
        BEGIN TRY
            SELECT 
                bp.Id_Beca_Programa,
                bp.Codigo_Programa,
                bp.Nombre_Programa,
                bp.Promedio_Minimo,
                bp.Requiere_Sin_Sanciones,
                bp.Id_Estado_Programa,
                est.Nombre_Estado AS Nombre_Estado_Programa,
                STRING_AGG(CONCAT(c.Clave_Criterio, ' ', c.Operador_Comparacion, ' ', c.Valor_Criterio), ' | ') AS Criterios_Resumen
            FROM cls_becas_programas bp (NOLOCK)
            LEFT JOIN cls_estados est (NOLOCK) ON bp.Id_Estado_Programa = est.Id_Estado
            LEFT JOIN cls_becas_criterios c (NOLOCK) ON bp.Id_Beca_Programa = c.Id_Programa AND c.Activo = 1
            WHERE bp.Id_Estado_Programa = 1 -- ACTIVO
            GROUP BY bp.Id_Beca_Programa, bp.Codigo_Programa, bp.Nombre_Programa, bp.Promedio_Minimo, bp.Requiere_Sin_Sanciones,
                bp.Id_Estado_Programa, est.Nombre_Estado;

            SET @o_Num = 0;
            SET @o_Msg = N'¡Programas de becas obtenidos!';
        END TRY
        BEGIN CATCH
            SET @o_Num = -1;
            SET @o_Msg = N'Error al listar programas de becas.';
            SET @Linea_Error = ERROR_LINE(); SET @Numero_Error = ERROR_NUMBER(); SET @Mensaje_Error = ERROR_MESSAGE();
            EXEC sp_logs_errores_sql @Modo='INS', @Origen_Error=@Origen_Error, @Linea_Error=@Linea_Error, @Numero_Error=@Numero_Error, @Mensaje_Error=@Mensaje_Error;
        END CATCH
        RETURN;
    END

    /* =========================================================
       TX 189: APLICAR SOLICITUD DE BECA (ESTUDIANTE)
       ========================================================= */
    IF (@Id_Tipo_Transaccion = 189)
    BEGIN
        IF ISNULL(@Id_Beca_Programa,0)=0
        BEGIN
            SET @o_Num = -1; SET @o_Msg = N'¡Debe seleccionar un programa de beca!';
            RETURN;
        END

        IF NOT EXISTS(SELECT 1 FROM cls_becas_programas(NOLOCK) WHERE Id_Beca_Programa=@Id_Beca_Programa AND Id_Estado_Programa=1)
        BEGIN
            SET @o_Num = -1; SET @o_Msg = N'¡El programa de beca no está activo!';
            RETURN;
        END

        BEGIN TRY
            DECLARE @PromedioCalc DECIMAL(10,2), @MateriasAprobadas INT = 0, @TotalSanciones INT = 0, @Cumple BIT = 1, @CodigoSeguimiento VARCHAR(30), @IdNueva INT;

            -- Total de sanciones activas
            SELECT @TotalSanciones = COUNT(1)
            FROM tbl_sanciones_academicas(NOLOCK)
            WHERE Id_Estudiante = @Id_Sesion AND Id_Estado = 1;

            -- Cálculo de promedio general y materias aprobadas
            ;WITH EvalPorMateria AS (
                SELECT 
                    em.Id_Materia,
                    SUM((ea.Puntaje_Obtenido / NULLIF(ei.Calificacion_Maxima,0)) * 100.0) AS SumaPct,
                    COUNT(*) AS Conteo
                FROM tbl_evaluaciones_alumnos ea (NOLOCK)
                INNER JOIN tbl_evaluaciones_instancias ei (NOLOCK) ON ea.Id_Evaluacion_Instancia = ei.Id_Evaluacion_Instancia
                INNER JOIN cls_evaluaciones_modelos em (NOLOCK) ON ei.Id_Evaluacion_Modelo = em.Id_Evaluacion_Modelo
                INNER JOIN tbl_inscripciones ins (NOLOCK) ON ea.Id_Inscripcion = ins.Id_Inscripcion
                WHERE ins.Id_Estudiante = @Id_Sesion
                GROUP BY em.Id_Materia
            )
            SELECT 
                @PromedioCalc = CASE WHEN SUM(Conteo) > 0 THEN SUM(SumaPct) / SUM(Conteo) ELSE NULL END,
                @MateriasAprobadas = SUM(CASE WHEN SumaPct >= 60 THEN 1 ELSE 0 END)
            FROM EvalPorMateria;

            -- Validaciones de criterios configurados
            DECLARE @ReqProm DECIMAL(10,2) = NULL, @ReqMaterias INT = NULL, @ReqSinSanciones BIT = 0;

            -- Criterios por clave
            SELECT 
                @ReqProm = CASE WHEN Clave_Criterio = 'PROMEDIO_MIN' THEN TRY_CONVERT(DECIMAL(10,2), Valor_Criterio) ELSE @ReqProm END,
                @ReqMaterias = CASE WHEN Clave_Criterio = 'MATERIAS_APROBADAS' THEN TRY_CONVERT(INT, Valor_Criterio) ELSE @ReqMaterias END,
                @ReqSinSanciones = CASE WHEN Clave_Criterio = 'SIN_SANCIONES' THEN 1 ELSE @ReqSinSanciones END
            FROM cls_becas_criterios (NOLOCK)
            WHERE Id_Programa = @Id_Beca_Programa AND Activo = 1;

            -- Validar promedio
            IF (@ReqProm IS NOT NULL AND (@PromedioCalc IS NULL OR @PromedioCalc < @ReqProm))
                SET @Cumple = 0;

            -- Validar sanciones por criterio o bandera del programa
            DECLARE @ProgRequiereSinSanciones BIT = 0;
            SELECT @ProgRequiereSinSanciones = Requiere_Sin_Sanciones FROM cls_becas_programas (NOLOCK) WHERE Id_Beca_Programa=@Id_Beca_Programa;
            IF ((@ReqSinSanciones = 1 OR @ProgRequiereSinSanciones = 1) AND @TotalSanciones > 0)
                SET @Cumple = 0;

            -- Validar materias aprobadas
            IF (@ReqMaterias IS NOT NULL AND @MateriasAprobadas < @ReqMaterias)
                SET @Cumple = 0;

            -- Generar código de seguimiento
            DECLARE @Prefijo VARCHAR(10) = 'SOL-BEC-';
            DECLARE @Contador INT;
            SELECT @Contador = ISNULL(MAX(CAST(SUBSTRING(Codigo_Seguimiento, LEN(@Prefijo) + 1, LEN(Codigo_Seguimiento)) AS INT)), 0) + 1
            FROM tbl_solicitudes_becas(NOLOCK)
            WHERE Codigo_Seguimiento LIKE @Prefijo + '%';
            SET @CodigoSeguimiento = @Prefijo + RIGHT('000000' + CAST(@Contador AS VARCHAR), 6);

            BEGIN TRAN;

            IF (@Cumple = 1)
            BEGIN
                INSERT INTO tbl_solicitudes_becas
                (
                    Codigo_Seguimiento, Id_Beca_Programa, Id_Estudiante, Promedio_Vigente, Total_Sanciones_Activas,
                    Cumple_Criterios, Id_Tipo_Decision, Id_Estado, Fecha_Solicitud, Fecha_Ultima_Decision,
                    Fecha_Cierre, Motivo_Ultima_Decision, Observaciones, Es_Prioritaria,
                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador
                )
                VALUES
                (
                    @CodigoSeguimiento, @Id_Beca_Programa, @Id_Sesion, @PromedioCalc, @TotalSanciones,
                    1, NULL, 4, GETDATE(), NULL,
                    NULL, NULL, @Observaciones, 0,
                    GETDATE(), GETDATE(), @Id_Sesion, @Id_Sesion
                );

                SET @IdNueva = SCOPE_IDENTITY();

                INSERT INTO tbl_solicitudes_becas_historial
                (
                    Id_Solicitud_Beca, Id_Estado_Anterior, Id_Estado_Nuevo, Id_Usuario_Revisor, Fecha_Decision, Motivo_Decision, Observaciones
                )
                VALUES
                (
                    @IdNueva, NULL, 4, @Id_Sesion, GETDATE(), N'Solicitud creada por el estudiante.', @Observaciones
                );

                COMMIT TRAN;
                SET @o_Num = @IdNueva;
                SET @o_Msg = N'Solicitud registrada y enviada a revisión.';
            END
            ELSE
            BEGIN
                ROLLBACK TRAN;
                SET @o_Num = -1;
                SET @o_Msg = N'No cumple los criterios configurados para esta beca.';
            END
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0 ROLLBACK TRAN;
            SET @o_Num = -1;
            SET @Linea_Error = ERROR_LINE(); 
            SET @Numero_Error = ERROR_NUMBER(); 
            SET @Mensaje_Error = ERROR_MESSAGE();
            SET @o_Msg = CONCAT(N'Error al registrar la solicitud de beca. Detalle: ', @Mensaje_Error);
            EXEC sp_logs_errores_sql @Modo='INS', @Origen_Error=@Origen_Error, @Linea_Error=@Linea_Error, @Numero_Error=@Numero_Error, @Mensaje_Error=@Mensaje_Error;
        END CATCH
        RETURN;
    END

    /* =========================================================
       TX 190: OBTENER MIS SOLICITUDES
       ========================================================= */
    IF (@Id_Tipo_Transaccion = 190)
    BEGIN
        BEGIN TRY
            SELECT 
                sb.Id_Solicitud_Beca,
                sb.Codigo_Seguimiento,
                bp.Nombre_Programa AS Nombre_Programa,
                bp.Codigo_Programa AS Codigo_Programa,
                sb.Promedio_Vigente,
                sb.Total_Sanciones_Activas,
                sb.Cumple_Criterios,
                est.Nombre_Estado AS Estado_Solicitud,
                sb.Fecha_Solicitud,
                sb.Fecha_Ultima_Decision,
                sb.Fecha_Cierre,
                sb.Motivo_Ultima_Decision,
                sb.Observaciones
            FROM tbl_solicitudes_becas sb (NOLOCK)
            INNER JOIN cls_becas_programas bp (NOLOCK) ON sb.Id_Beca_Programa = bp.Id_Beca_Programa
            LEFT JOIN cls_estados est (NOLOCK) ON sb.Id_Estado = est.Id_Estado
            WHERE sb.Id_Estudiante = @Id_Sesion
            ORDER BY sb.Fecha_Solicitud DESC;

            SET @o_Num = 0;
            SET @o_Msg = N'¡Solicitudes obtenidas!';
        END TRY
        BEGIN CATCH
            SET @o_Num = -1;
            SET @o_Msg = N'Error al obtener solicitudes.';
            SET @Linea_Error = ERROR_LINE(); SET @Numero_Error = ERROR_NUMBER(); SET @Mensaje_Error = ERROR_MESSAGE();
            EXEC sp_logs_errores_sql @Modo='INS', @Origen_Error=@Origen_Error, @Linea_Error=@Linea_Error, @Numero_Error=@Numero_Error, @Mensaje_Error=@Mensaje_Error;
        END CATCH
        RETURN;
    END

    /* =========================================================
       TX 191: HISTORIAL DE MIS SOLICITUDES
       ========================================================= */
    IF (@Id_Tipo_Transaccion = 191)
    BEGIN
        BEGIN TRY
            SELECT 
                h.Id_Historial_Solicitud,
                h.Id_Solicitud_Beca,
                h.Id_Estado_Anterior,
                h.Id_Estado_Nuevo,
                est.Nombre_Estado AS Estado_Nuevo_Nombre,
                h.Id_Usuario_Revisor,
                u.Usuario AS Usuario_Revisor,
                h.Fecha_Decision,
                h.Motivo_Decision,
                h.Observaciones
            FROM tbl_solicitudes_becas_historial h (NOLOCK)
            INNER JOIN tbl_solicitudes_becas sb (NOLOCK) ON h.Id_Solicitud_Beca = sb.Id_Solicitud_Beca
            LEFT JOIN cls_estados est (NOLOCK) ON h.Id_Estado_Nuevo = est.Id_Estado
            LEFT JOIN tbl_usuarios u (NOLOCK) ON h.Id_Usuario_Revisor = u.Id_Usuario
            WHERE sb.Id_Estudiante = @Id_Sesion
            ORDER BY h.Fecha_Decision DESC;

            SET @o_Num = 0;
            SET @o_Msg = N'¡Historial obtenido!';
        END TRY
        BEGIN CATCH
            SET @o_Num = -1;
            SET @o_Msg = N'Error al obtener historial.';
            SET @Linea_Error = ERROR_LINE(); SET @Numero_Error = ERROR_NUMBER(); SET @Mensaje_Error = ERROR_MESSAGE();
            EXEC sp_logs_errores_sql @Modo='INS', @Origen_Error=@Origen_Error, @Linea_Error=@Linea_Error, @Numero_Error=@Numero_Error, @Mensaje_Error=@Mensaje_Error;
        END CATCH
        RETURN;
    END

    /* =========================================================
       TX 192: CRITERIOS DE UN PROGRAMA (VISIÓN ESTUDIANTE)
       ========================================================= */
    IF (@Id_Tipo_Transaccion = 192)
    BEGIN
        IF ISNULL(@Id_Beca_Programa,0)=0
        BEGIN
            SET @o_Num = -1; SET @o_Msg = N'¡Debe seleccionar un programa!';
            RETURN;
        END
        BEGIN TRY
            SELECT 
                Id_Beca_Criterio,
                Id_Programa,
                Clave_Criterio,
                Valor_Criterio,
                Tipo_Dato_Valor,
                Operador_Comparacion,
                Observaciones
            FROM cls_becas_criterios (NOLOCK)
            WHERE Id_Programa = @Id_Beca_Programa AND Activo = 1;

            SET @o_Num = 0;
            SET @o_Msg = N'Criterios obtenidos.';
        END TRY
        BEGIN CATCH
            SET @o_Num = -1;
            SET @o_Msg = N'Error al obtener criterios.';
            SET @Linea_Error = ERROR_LINE(); SET @Numero_Error = ERROR_NUMBER(); SET @Mensaje_Error = ERROR_MESSAGE();
            EXEC sp_logs_errores_sql @Modo='INS', @Origen_Error=@Origen_Error, @Linea_Error=@Linea_Error, @Numero_Error=@Numero_Error, @Mensaje_Error=@Mensaje_Error;
        END CATCH
        RETURN;
    END

    -- Transacción desconocida
    SET @o_Num = -1;
    SET @o_Msg = N'Tipo de transacción no soportado.';
END
GO

