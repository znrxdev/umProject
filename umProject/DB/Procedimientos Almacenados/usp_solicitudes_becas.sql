USE umDb
GO

/*
tbl_solicitudes_becas
*/

CREATE OR ALTER PROC usp_solicitudes_becas
(
    @Id_Solicitud_Beca INT = NULL,
    @Codigo_Seguimiento VARCHAR(30) = NULL,
    @Id_Beca_Programa INT = NULL,
    @Id_Estudiante INT = NULL,
    @Promedio_Vigente DECIMAL(5,2) = NULL,
    @Total_Sanciones_Activas INT = NULL,
    @Cumple_Criterios BIT = NULL,
    @Id_Tipo_Decision INT = NULL,
    @Id_Estado INT = NULL,
    @Fecha_Solicitud DATETIME = NULL,
    @Fecha_Ultima_Decision DATETIME = NULL,
    @Fecha_Cierre DATETIME = NULL,
    @Motivo_Ultima_Decision NVARCHAR(500) = NULL,
    @Observaciones NVARCHAR(1000) = NULL,
    @Es_Prioritaria BIT = NULL,
    @Fecha_Creacion DATETIME = NULL,
    @Fecha_Modificacion DATETIME = NULL,
    @Id_Creador INT = NULL,
    @Id_Modificador INT = NULL,
    @Id_Tipo_Transaccion INT,
    @Id_Transaccion INT = NULL,
    @Id_Sesion INT = NULL,
    @o_Num INT = NULL OUTPUT,
    @o_Msg NVARCHAR(255) = NULL OUTPUT
)
AS
BEGIN
    DECLARE @Permiso INT, @iConcepto NVARCHAR(255), @Linea_Error INT, @Numero_Error INT, @Mensaje_Error NVARCHAR(255), @Origen_Error NVARCHAR(50) = ERROR_PROCEDURE();
    SET @Fecha_Creacion = GETDATE();
    SET @Fecha_Modificacion = GETDATE();
    SET @Id_Creador = @Id_Sesion;
    SET @Id_Modificador = @Id_Sesion;
    SET @Permiso = dbo.fn_Validar_Permisos(@Id_Sesion, @Id_Tipo_Transaccion);

    IF(@Permiso = 1)
        BEGIN
            /* FILTRAR POR ID SOLICITUD BECA */
            IF(@Id_Tipo_Transaccion = 70)
                BEGIN
                    IF ISNULL(@Id_Solicitud_Beca, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de solicitud de beca!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_solicitudes_becas(NOLOCK) WHERE Id_Solicitud_Beca = @Id_Solicitud_Beca)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La solicitud de beca no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    sb.Id_Solicitud_Beca,
                                    sb.Codigo_Seguimiento,
                                    sb.Id_Beca_Programa,
                                    sb.Id_Estudiante,
                                    sb.Promedio_Vigente,
                                    sb.Total_Sanciones_Activas,
                                    sb.Cumple_Criterios,
                                    sb.Id_Tipo_Decision,
                                    sb.Id_Estado,
                                    sb.Fecha_Solicitud,
                                    sb.Fecha_Ultima_Decision,
                                    sb.Fecha_Cierre,
                                    sb.Motivo_Ultima_Decision,
                                    sb.Observaciones,
                                    sb.Es_Prioritaria,
                                    sb.Hash_Solicitud,
                                    sb.Codigo_Control,
                                    sb.Fecha_Creacion,
                                    sb.Fecha_Modificacion,
                                    sb.Id_Creador,
                                    sb.Id_Modificador,
                                    sb.Id_Transaccion,
                                    sb.RowVersion,
                                    -- Información relacionada para mostrar en la UI
                                    bp.Nombre_Programa AS Nombre_Programa_Beca,
                                    bp.Codigo_Programa AS Codigo_Programa_Beca,
                                    u.Usuario AS Usuario_Estudiante,
                                    p.Primer_Nombre + ' ' + p.Primer_Apellido AS Nombre_Estudiante,
                                    e.Nombre_Estado AS Estado_Nombre,
                                    cat.Nombre_Catalogo AS Tipo_Decision_Nombre
                                FROM tbl_solicitudes_becas sb (NOLOCK)
                                LEFT JOIN cls_becas_programas bp (NOLOCK) ON sb.Id_Beca_Programa = bp.Id_Beca_Programa
                                LEFT JOIN tbl_usuarios u (NOLOCK) ON sb.Id_Estudiante = u.Id_Usuario
                                LEFT JOIN tbl_personas p (NOLOCK) ON u.Id_Persona = p.Id_Persona
                                LEFT JOIN cls_estados e (NOLOCK) ON sb.Id_Estado = e.Id_Estado
                                LEFT JOIN cls_catalogos cat (NOLOCK) ON sb.Id_Tipo_Decision = cat.Id_Catalogo
                                WHERE sb.Id_Solicitud_Beca = @Id_Solicitud_Beca;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Solicitud de beca encontrada!';
                            END TRY
                            BEGIN CATCH
                                SET @o_Num = -1;
                                SET @o_Msg = '¡Error interno del servidor!';
                                SET @Linea_Error = ERROR_LINE();
                                SET @Numero_Error = ERROR_NUMBER();
                                SET @Mensaje_Error = ERROR_MESSAGE();
                                EXEC sp_logs_errores_sql
                                @Modo = 'INS',
                                @Origen_Error = @Origen_Error,
                                @Linea_Error = @Linea_Error,
                                @Numero_Error = @Numero_Error,
                                @Mensaje_Error = @Mensaje_Error;
                            END CATCH
                        END
                END
            /* LISTAR TODAS LAS SOLICITUDES DE BECAS */
            ELSE IF(@Id_Tipo_Transaccion = 151)
                BEGIN
                    BEGIN TRY
                        SELECT 
                            sb.Id_Solicitud_Beca,
                            sb.Codigo_Seguimiento,
                            sb.Id_Beca_Programa,
                            sb.Id_Estudiante,
                            sb.Promedio_Vigente,
                            sb.Total_Sanciones_Activas,
                            sb.Cumple_Criterios,
                            sb.Id_Tipo_Decision,
                            sb.Id_Estado,
                            sb.Fecha_Solicitud,
                            sb.Fecha_Ultima_Decision,
                            sb.Fecha_Cierre,
                            sb.Motivo_Ultima_Decision,
                            sb.Observaciones,
                            sb.Es_Prioritaria,
                            sb.Codigo_Control,
                            sb.Fecha_Creacion,
                            sb.Fecha_Modificacion,
                            sb.Id_Creador,
                            sb.Id_Modificador,
                            sb.Id_Transaccion,
                            -- Información adicional de JOINs
                            bp.Nombre_Programa AS Nombre_Programa_Beca,
                            bp.Codigo_Programa AS Codigo_Programa_Beca,
                            u.Usuario AS Usuario_Estudiante,
                            p.Primer_Nombre + ' ' + p.Primer_Apellido AS Nombre_Estudiante,
                            e.Nombre_Estado AS Estado_Nombre,
                            cat.Nombre_Catalogo AS Tipo_Decision_Nombre
                        FROM tbl_solicitudes_becas sb (NOLOCK)
                        LEFT JOIN cls_becas_programas bp (NOLOCK) ON sb.Id_Beca_Programa = bp.Id_Beca_Programa
                        LEFT JOIN tbl_usuarios u (NOLOCK) ON sb.Id_Estudiante = u.Id_Usuario
                        LEFT JOIN tbl_personas p (NOLOCK) ON u.Id_Persona = p.Id_Persona
                        LEFT JOIN cls_estados e (NOLOCK) ON sb.Id_Estado = e.Id_Estado
                        LEFT JOIN cls_catalogos cat (NOLOCK) ON sb.Id_Tipo_Decision = cat.Id_Catalogo
                        ORDER BY sb.Fecha_Solicitud DESC, bp.Nombre_Programa ASC;

                        SET @o_Num = 0;
                        SET @o_Msg = '¡Solicitudes de becas listadas exitosamente!';
                    END TRY
                    BEGIN CATCH
                        SET @o_Num = -1;
                        SET @o_Msg = '¡Error interno del servidor!';
                        SET @Linea_Error = ERROR_LINE();
                        SET @Numero_Error = ERROR_NUMBER();
                        SET @Mensaje_Error = ERROR_MESSAGE();
                        EXEC sp_logs_errores_sql
                        @Modo = 'INS',
                        @Origen_Error = @Origen_Error,
                        @Linea_Error = @Linea_Error,
                        @Numero_Error = @Numero_Error,
                        @Mensaje_Error = @Mensaje_Error;
                    END CATCH
                END
            /* FILTRAR POR ID BECA PROGRAMA */
            ELSE IF(@Id_Tipo_Transaccion = 71)
                BEGIN
                    IF ISNULL(@Id_Beca_Programa, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado el programa de beca para listar las solicitudes!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_becas_programas(NOLOCK) WHERE Id_Beca_Programa = @Id_Beca_Programa)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El programa de beca no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Solicitud_Beca,
                                    Codigo_Seguimiento,
                                    Id_Beca_Programa,
                                    Id_Estudiante,
                                    Promedio_Vigente,
                                    Total_Sanciones_Activas,
                                    Cumple_Criterios,
                                    Id_Tipo_Decision,
                                    Id_Estado,
                                    Fecha_Solicitud,
                                    Fecha_Ultima_Decision,
                                    Fecha_Cierre,
                                    Motivo_Ultima_Decision,
                                    Observaciones,
                                    Es_Prioritaria,
                                    Hash_Solicitud,
                                    Codigo_Control,
                                    Fecha_Creacion,
                                    Fecha_Modificacion,
                                    Id_Creador,
                                    Id_Modificador,
                                    Id_Transaccion,
                                    RowVersion
                                FROM tbl_solicitudes_becas(NOLOCK)
                                WHERE Id_Beca_Programa = @Id_Beca_Programa
                                ORDER BY Fecha_Solicitud DESC;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Solicitudes filtradas por programa!';
                            END TRY
                            BEGIN CATCH
                                SET @o_Num = -1;
                                SET @o_Msg = '¡Error interno del servidor!';
                                SET @Linea_Error = ERROR_LINE();
                                SET @Numero_Error = ERROR_NUMBER();
                                SET @Mensaje_Error = ERROR_MESSAGE();
                                EXEC sp_logs_errores_sql
                                @Modo = 'INS',
                                @Origen_Error = @Origen_Error,
                                @Linea_Error = @Linea_Error,
                                @Numero_Error = @Numero_Error,
                                @Mensaje_Error = @Mensaje_Error;
                            END CATCH
                        END
                END
            /* FILTRAR POR ESTUDIANTE */
            ELSE IF(@Id_Tipo_Transaccion = 72)
                BEGIN
                    IF ISNULL(@Id_Estudiante, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado el estudiante para listar sus solicitudes!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_usuarios(NOLOCK) WHERE Id_Usuario = @Id_Estudiante)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El estudiante no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Solicitud_Beca,
                                    Codigo_Seguimiento,
                                    Id_Beca_Programa,
                                    Id_Estudiante,
                                    Promedio_Vigente,
                                    Total_Sanciones_Activas,
                                    Cumple_Criterios,
                                    Id_Tipo_Decision,
                                    Id_Estado,
                                    Fecha_Solicitud,
                                    Fecha_Ultima_Decision,
                                    Fecha_Cierre,
                                    Motivo_Ultima_Decision,
                                    Observaciones,
                                    Es_Prioritaria,
                                    Hash_Solicitud,
                                    Codigo_Control,
                                    Fecha_Creacion,
                                    Fecha_Modificacion,
                                    Id_Creador,
                                    Id_Modificador,
                                    Id_Transaccion,
                                    RowVersion
                                FROM tbl_solicitudes_becas(NOLOCK)
                                WHERE Id_Estudiante = @Id_Estudiante
                                ORDER BY Fecha_Solicitud DESC;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Solicitudes filtradas por estudiante!';
                            END TRY
                            BEGIN CATCH
                                SET @o_Num = -1;
                                SET @o_Msg = '¡Error interno del servidor!';
                                SET @Linea_Error = ERROR_LINE();
                                SET @Numero_Error = ERROR_NUMBER();
                                SET @Mensaje_Error = ERROR_MESSAGE();
                                EXEC sp_logs_errores_sql
                                @Modo = 'INS',
                                @Origen_Error = @Origen_Error,
                                @Linea_Error = @Linea_Error,
                                @Numero_Error = @Numero_Error,
                                @Mensaje_Error = @Mensaje_Error;
                            END CATCH
                        END
                END
            /* OBTENER MIS SOLICITUDES DE BECAS (usando Id_Sesion) */
            ELSE IF(@Id_Tipo_Transaccion = 138)
                BEGIN
                    IF ISNULL(@Id_Sesion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No hay sesión activa!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Solicitud_Beca,
                                    Codigo_Seguimiento,
                                    Id_Beca_Programa,
                                    Id_Estudiante,
                                    Promedio_Vigente,
                                    Total_Sanciones_Activas,
                                    Cumple_Criterios,
                                    Id_Tipo_Decision,
                                    Id_Estado,
                                    Fecha_Solicitud,
                                    Fecha_Ultima_Decision,
                                    Fecha_Cierre,
                                    Motivo_Ultima_Decision,
                                    Observaciones,
                                    Es_Prioritaria,
                                    Hash_Solicitud,
                                    Codigo_Control,
                                    Fecha_Creacion,
                                    Fecha_Modificacion,
                                    Id_Creador,
                                    Id_Modificador,
                                    Id_Transaccion,
                                    RowVersion
                                FROM tbl_solicitudes_becas(NOLOCK)
                                WHERE Id_Estudiante = @Id_Sesion
                                ORDER BY Fecha_Solicitud DESC;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Solicitudes de becas obtenidas exitosamente!';
                            END TRY
                            BEGIN CATCH
                                SET @o_Num = -1;
                                SET @o_Msg = '¡Error interno del servidor!';
                                SET @Linea_Error = ERROR_LINE();
                                SET @Numero_Error = ERROR_NUMBER();
                                SET @Mensaje_Error = ERROR_MESSAGE();
                                EXEC sp_logs_errores_sql
                                @Modo = 'INS',
                                @Origen_Error = @Origen_Error,
                                @Linea_Error = @Linea_Error,
                                @Numero_Error = @Numero_Error,
                                @Mensaje_Error = @Mensaje_Error;
                            END CATCH
                        END
                END
            /* EVALUAR ELEGIBILIDAD BECA (sin crear solicitud) */
            ELSE IF(@Id_Tipo_Transaccion = 148)
                BEGIN
                    IF ISNULL(@Id_Beca_Programa, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un programa de beca para evaluar elegibilidad!';
                        END
                    ELSE IF ISNULL(@Id_Estudiante, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un estudiante para evaluar elegibilidad!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_becas_programas(NOLOCK) WHERE Id_Beca_Programa = @Id_Beca_Programa)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El programa de beca no existe!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_usuarios(NOLOCK) WHERE Id_Usuario = @Id_Estudiante)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El estudiante no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                DECLARE @PromedioCalc DECIMAL(5,2) = 0;
                                DECLARE @TotalSanciones INT = 0;
                                DECLARE @Cumple BIT = 1;

                                -- Promedio vigente: Ya no disponible en tbl_inscripciones (campo eliminado)
                                -- El promedio debe calcularse desde tbl_evaluaciones si es necesario
                                SET @PromedioCalc = NULL;

                                -- Total sanciones académicas activas
                                SELECT @TotalSanciones = COUNT(1)
                                FROM tbl_sanciones_academicas(NOLOCK)
                                WHERE Id_Estudiante = @Id_Estudiante
                                  AND Id_Estado = 1;

                                -- Evaluar criterios configurados para el programa
                                DECLARE curCriterios CURSOR LOCAL FAST_FORWARD FOR
                                SELECT Clave_Criterio,
                                       Valor_Criterio
                                FROM cls_becas_criterios(NOLOCK)
                                WHERE Id_Programa = @Id_Beca_Programa
                                  AND Activo = 1;

                                DECLARE @Clave NVARCHAR(100),
                                        @Valor NVARCHAR(255);

                                OPEN curCriterios;
                                FETCH NEXT FROM curCriterios INTO @Clave, @Valor;

                                WHILE @@FETCH_STATUS = 0
                                BEGIN
                                    -- Regla: PROMEDIO_MIN
                                    IF (@Clave = 'PROMEDIO_MIN')
                                    BEGIN
                                        DECLARE @UmbralProm DECIMAL(10,2);
                                        SET @UmbralProm = CASE WHEN ISNUMERIC(@Valor) = 1 THEN CONVERT(DECIMAL(10,2), @Valor) ELSE 0 END;
                                        IF (@PromedioCalc IS NULL OR @PromedioCalc < @UmbralProm)
                                        BEGIN
                                            SET @Cumple = 0;
                                        END
                                    END
                                    -- Regla: SIN_SANCIONES
                                    ELSE IF (@Clave = 'SIN_SANCIONES')
                                    BEGIN
                                        IF (@TotalSanciones > 0)
                                        BEGIN
                                            SET @Cumple = 0;
                                        END
                                    END

                                    FETCH NEXT FROM curCriterios INTO @Clave, @Valor;
                                END

                                CLOSE curCriterios;
                                DEALLOCATE curCriterios;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Evaluación de elegibilidad realizada exitosamente!';

                                SELECT 
                                    @Id_Beca_Programa AS Id_Beca_Programa,
                                    @Id_Estudiante AS Id_Estudiante,
                                    @PromedioCalc AS Promedio_Vigente,
                                    @TotalSanciones AS Total_Sanciones_Activas,
                                    @Cumple AS Cumple_Criterios;
                            END TRY
                            BEGIN CATCH
                                SET @o_Num = -1;
                                SET @o_Msg = '¡Error interno del servidor al evaluar elegibilidad de beca!';
                                SET @Linea_Error = ERROR_LINE();
                                SET @Numero_Error = ERROR_NUMBER();
                                SET @Mensaje_Error = ERROR_MESSAGE();
                                EXEC sp_logs_errores_sql
                                @Modo = 'INS',
                                @Origen_Error = @Origen_Error,
                                @Linea_Error = @Linea_Error,
                                @Numero_Error = @Numero_Error,
                                @Mensaje_Error = @Mensaje_Error;
                            END CATCH
                        END
                END
            /* LISTAR SOLICITUDES DE BECAS PENDIENTES POR NIVEL */
            ELSE IF(@Id_Tipo_Transaccion = 149)
                BEGIN
                    IF ISNULL(@Id_Beca_Programa, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un programa de beca para listar las solicitudes pendientes!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    sb.Id_Solicitud_Beca,
                                    sb.Codigo_Seguimiento,
                                    sb.Id_Beca_Programa,
                                    sb.Id_Estudiante,
                                    sb.Promedio_Vigente,
                                    sb.Total_Sanciones_Activas,
                                    sb.Cumple_Criterios,
                                    sb.Id_Tipo_Decision,
                                    sb.Id_Estado,
                                    sb.Fecha_Solicitud,
                                    sb.Fecha_Ultima_Decision,
                                    sb.Fecha_Cierre,
                                    sb.Motivo_Ultima_Decision,
                                    sb.Observaciones,
                                    sb.Es_Prioritaria,
                                    sb.Codigo_Control,
                                    sb.Fecha_Creacion,
                                    sb.Fecha_Modificacion,
                                    u.Usuario AS Usuario_Estudiante,
                                    p.Primer_Nombre + ' ' + p.Primer_Apellido AS Nombre_Estudiante,
                                    bp.Nombre_Programa
                                FROM tbl_solicitudes_becas(NOLOCK) sb
                                INNER JOIN cls_becas_programas(NOLOCK) bp ON sb.Id_Beca_Programa = bp.Id_Beca_Programa
                                INNER JOIN tbl_usuarios(NOLOCK) u ON sb.Id_Estudiante = u.Id_Usuario
                                INNER JOIN tbl_personas(NOLOCK) p ON u.Id_Persona = p.Id_Persona
                                WHERE sb.Id_Beca_Programa = @Id_Beca_Programa
                                  AND sb.Id_Estado IN (3,4) -- PENDIENTE / EN REVISION
                                ORDER BY sb.Fecha_Solicitud DESC;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Solicitudes pendientes listadas exitosamente!';
                            END TRY
                            BEGIN CATCH
                                SET @o_Num = -1;
                                SET @o_Msg = '¡Error interno del servidor al listar solicitudes de becas pendientes!';
                                SET @Linea_Error = ERROR_LINE();
                                SET @Numero_Error = ERROR_NUMBER();
                                SET @Mensaje_Error = ERROR_MESSAGE();
                                EXEC sp_logs_errores_sql
                                @Modo = 'INS',
                                @Origen_Error = @Origen_Error,
                                @Linea_Error = @Linea_Error,
                                @Numero_Error = @Numero_Error,
                                @Mensaje_Error = @Mensaje_Error;
                            END CATCH
                        END
                END
            /* AGREGAR SOLICITUD BECA */
            ELSE IF(@Id_Tipo_Transaccion = 68)
                BEGIN
                    IF ISNULL(@Id_Beca_Programa, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un programa de beca!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_becas_programas(NOLOCK) WHERE Id_Beca_Programa = @Id_Beca_Programa)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El programa de beca no existe!';
                        END
                    ELSE IF ISNULL(@Id_Estudiante, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un estudiante!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_usuarios(NOLOCK) WHERE Id_Usuario = @Id_Estudiante)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El estudiante no existe!';
                        END
                    ELSE IF ISNULL(@Id_Estado, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un estado a la solicitud!';
                        END
                    ELSE IF (@Promedio_Vigente IS NOT NULL AND (@Promedio_Vigente < 0 OR @Promedio_Vigente > 100))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El promedio vigente debe estar entre 0 y 100!';
                        END
                    ELSE IF (@Fecha_Cierre IS NOT NULL AND @Fecha_Solicitud IS NOT NULL AND @Fecha_Cierre < @Fecha_Solicitud)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de cierre no puede ser anterior a la fecha de solicitud!';
                        END
                    ELSE
                        BEGIN
                            -- Generar código de seguimiento único si no se proporciona
                            IF ISNULL(@Codigo_Seguimiento, '') = ''
                                BEGIN
                                    DECLARE @Prefijo VARCHAR(10) = 'SOL-BEC-';
                                    DECLARE @Contador INT;
                                    SELECT @Contador = ISNULL(MAX(CAST(SUBSTRING(Codigo_Seguimiento, LEN(@Prefijo) + 1, LEN(Codigo_Seguimiento)) AS INT)), 0) + 1
                                    FROM tbl_solicitudes_becas(NOLOCK)
                                    WHERE Codigo_Seguimiento LIKE @Prefijo + '%';
                                    SET @Codigo_Seguimiento = @Prefijo + RIGHT('000000' + CAST(@Contador AS VARCHAR), 6);
                                END
                            ELSE IF EXISTS(SELECT 1 FROM tbl_solicitudes_becas(NOLOCK) WHERE Codigo_Seguimiento = @Codigo_Seguimiento)
                                BEGIN
                                    SET @o_Num = -1;
                                    SET @o_Msg = '¡Ya existe una solicitud con ese código de seguimiento!';
                                    RETURN;
                                END

                            SET @iConcepto = CONCAT('AGREGANDO SOLICITUD DE BECA: ', @Codigo_Seguimiento);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_AgregarSolicitudBeca
                            BEGIN TRY
                                INSERT INTO tbl_solicitudes_becas(
                                    Codigo_Seguimiento,
                                    Id_Beca_Programa,
                                    Id_Estudiante,
                                    Promedio_Vigente,
                                    Total_Sanciones_Activas,
                                    Cumple_Criterios,
                                    Id_Tipo_Decision,
                                    Id_Estado,
                                    Fecha_Solicitud,
                                    Fecha_Ultima_Decision,
                                    Fecha_Cierre,
                                    Motivo_Ultima_Decision,
                                    Observaciones,
                                    Es_Prioritaria,
                                    Fecha_Creacion,
                                    Fecha_Modificacion,
                                    Id_Creador,
                                    Id_Modificador,
                                    Id_Transaccion
                                ) VALUES (
                                    @Codigo_Seguimiento,
                                    @Id_Beca_Programa,
                                    @Id_Estudiante,
                                    @Promedio_Vigente,
                                    ISNULL(@Total_Sanciones_Activas, 0),
                                    ISNULL(@Cumple_Criterios, 0),
                                    @Id_Tipo_Decision,
                                    @Id_Estado,
                                    ISNULL(@Fecha_Solicitud, GETDATE()),
                                    @Fecha_Ultima_Decision,
                                    @Fecha_Cierre,
                                    @Motivo_Ultima_Decision,
                                    @Observaciones,
                                    ISNULL(@Es_Prioritaria, 0),
                                    @Fecha_Creacion,
                                    @Fecha_Modificacion,
                                    @Id_Creador,
                                    @Id_Modificador,
                                    @Id_Transaccion
                                );

                                COMMIT TRAN trx_AgregarSolicitudBeca;

                                SET @o_Num = SCOPE_IDENTITY();
                                SET @o_Msg = '¡Solicitud de beca agregada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_AgregarSolicitudBeca;

                                SET @o_Num = -1;
                                SET @o_Msg = '¡Error interno del servidor!';
                                SET @Linea_Error = ERROR_LINE();
                                SET @Numero_Error = ERROR_NUMBER();
                                SET @Mensaje_Error = ERROR_MESSAGE();
                                EXEC sp_logs_errores_sql
                                @Modo = 'INS',
                                @Origen_Error = @Origen_Error,
                                @Linea_Error = @Linea_Error,
                                @Numero_Error = @Numero_Error,
                                @Mensaje_Error = @Mensaje_Error;
                                EXEC sp_transacciones
                                @Modo = 'RBK',
                                @Id_Transaccion = @Id_Transaccion;
                            END CATCH
                        END
                END
            /* ACTUALIZAR SOLICITUD BECA */
            ELSE IF(@Id_Tipo_Transaccion = 69)
                BEGIN
                    IF ISNULL(@Id_Solicitud_Beca, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de solicitud de beca!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_solicitudes_becas(NOLOCK) WHERE Id_Solicitud_Beca = @Id_Solicitud_Beca)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La solicitud de beca no existe!';
                        END
                    ELSE IF (@Codigo_Seguimiento IS NOT NULL AND EXISTS(SELECT 1 FROM tbl_solicitudes_becas(NOLOCK) WHERE Codigo_Seguimiento = @Codigo_Seguimiento AND Id_Solicitud_Beca <> @Id_Solicitud_Beca))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe otra solicitud con ese código de seguimiento!';
                        END
                    ELSE IF (@Promedio_Vigente IS NOT NULL AND (@Promedio_Vigente < 0 OR @Promedio_Vigente > 100))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El promedio vigente debe estar entre 0 y 100!';
                        END
                    ELSE IF (@Fecha_Cierre IS NOT NULL AND EXISTS(SELECT 1 FROM tbl_solicitudes_becas(NOLOCK) WHERE Id_Solicitud_Beca = @Id_Solicitud_Beca AND Fecha_Cierre IS NOT NULL AND @Fecha_Cierre < Fecha_Solicitud))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de cierre no puede ser anterior a la fecha de solicitud!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('ACTUALIZANDO SOLICITUD DE BECA ID: ', @Id_Solicitud_Beca);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_ActualizarSolicitudBeca
                            BEGIN TRY
                                UPDATE tbl_solicitudes_becas
                                SET Codigo_Seguimiento = COALESCE(@Codigo_Seguimiento, Codigo_Seguimiento),
                                    Id_Beca_Programa = COALESCE(@Id_Beca_Programa, Id_Beca_Programa),
                                    Id_Estudiante = COALESCE(@Id_Estudiante, Id_Estudiante),
                                    Promedio_Vigente = COALESCE(@Promedio_Vigente, Promedio_Vigente),
                                    Total_Sanciones_Activas = COALESCE(@Total_Sanciones_Activas, Total_Sanciones_Activas),
                                    Cumple_Criterios = COALESCE(@Cumple_Criterios, Cumple_Criterios),
                                    Id_Tipo_Decision = COALESCE(@Id_Tipo_Decision, Id_Tipo_Decision),
                                    Id_Estado = COALESCE(@Id_Estado, Id_Estado),
                                    Fecha_Solicitud = COALESCE(@Fecha_Solicitud, Fecha_Solicitud),
                                    Fecha_Ultima_Decision = COALESCE(@Fecha_Ultima_Decision, Fecha_Ultima_Decision),
                                    Fecha_Cierre = COALESCE(@Fecha_Cierre, Fecha_Cierre),
                                    Motivo_Ultima_Decision = COALESCE(@Motivo_Ultima_Decision, Motivo_Ultima_Decision),
                                    Observaciones = COALESCE(@Observaciones, Observaciones),
                                    Es_Prioritaria = COALESCE(@Es_Prioritaria, Es_Prioritaria),
                                    Fecha_Modificacion = @Fecha_Modificacion,
                                    Id_Modificador = @Id_Modificador,
                                    Id_Transaccion = @Id_Transaccion
                                WHERE Id_Solicitud_Beca = @Id_Solicitud_Beca;

                                COMMIT TRAN trx_ActualizarSolicitudBeca;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Solicitud de beca actualizada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_ActualizarSolicitudBeca;

                                SET @o_Num = -1;
                                SET @o_Msg = '¡Error interno del servidor!';
                                SET @Linea_Error = ERROR_LINE();
                                SET @Numero_Error = ERROR_NUMBER();
                                SET @Mensaje_Error = ERROR_MESSAGE();
                                EXEC sp_logs_errores_sql
                                @Modo = 'INS',
                                @Origen_Error = @Origen_Error,
                                @Linea_Error = @Linea_Error,
                                @Numero_Error = @Numero_Error,
                                @Mensaje_Error = @Mensaje_Error;
                                EXEC sp_transacciones
                                @Modo = 'RBK',
                                @Id_Transaccion = @Id_Transaccion;
                            END CATCH
                        END
                END
            /* REGISTRAR DECISIÓN SOBRE SOLICITUD DE BECA */
            ELSE IF(@Id_Tipo_Transaccion = 150)
                BEGIN
                    IF ISNULL(@Id_Solicitud_Beca, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar una solicitud de beca para registrar la decisión!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_solicitudes_becas(NOLOCK) WHERE Id_Solicitud_Beca = @Id_Solicitud_Beca)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La solicitud de beca no existe!';
                        END
                    ELSE IF ISNULL(@Id_Tipo_Decision, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe indicar el tipo de decisión (APROBADA / RECHAZADA / PENDIENTE)!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                DECLARE @EstadoAnterior INT,
                                        @EstadoNuevo INT,
                                        @NombreDecision NVARCHAR(50);

                                SELECT 
                                    @EstadoAnterior = Id_Estado
                                FROM tbl_solicitudes_becas(NOLOCK)
                                WHERE Id_Solicitud_Beca = @Id_Solicitud_Beca;

                                SELECT @NombreDecision = Nombre_Catalogo
                                FROM cls_catalogos(NOLOCK)
                                WHERE Id_Catalogo = @Id_Tipo_Decision;

                                -- Determinar nuevo estado según la decisión
                                IF(@NombreDecision = 'APROBADA')
                                    BEGIN
                                        SET @EstadoNuevo = 5; -- APROBADA
                                        SET @Fecha_Cierre = GETDATE();
                                    END
                                ELSE IF(@NombreDecision = 'RECHAZADA')
                                    BEGIN
                                        SET @EstadoNuevo = 6; -- RECHAZADA
                                        SET @Fecha_Cierre = GETDATE();
                                    END
                                ELSE
                                    BEGIN
                                        -- Otras decisiones (por ejemplo, mantener en revisión)
                                        SET @EstadoNuevo = 4; -- EN REVISION
                                        SET @Fecha_Cierre = NULL;
                                    END

                                SET @Fecha_Ultima_Decision = GETDATE();

                                SET @iConcepto = CONCAT('REGISTRANDO DECISIÓN SOBRE SOLICITUD DE BECA ID: ', @Id_Solicitud_Beca);
                                EXEC sp_transacciones
                                @Modo = 'INS',
                                @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                                @Id_Autor = @Id_Sesion,
                                @Concepto = @iConcepto,
                                @o_Num = @Id_Transaccion OUTPUT;

                                BEGIN TRAN trx_RegistrarDecisionSolBec
                                BEGIN TRY
                                    -- Actualizar solicitud principal
                                    UPDATE tbl_solicitudes_becas
                                    SET Id_Tipo_Decision = @Id_Tipo_Decision,
                                        Id_Estado = @EstadoNuevo,
                                        Fecha_Ultima_Decision = @Fecha_Ultima_Decision,
                                        Fecha_Cierre = COALESCE(@Fecha_Cierre, Fecha_Cierre),
                                        Motivo_Ultima_Decision = COALESCE(@Motivo_Ultima_Decision, Motivo_Ultima_Decision),
                                        Observaciones = COALESCE(@Observaciones, Observaciones),
                                        Fecha_Modificacion = @Fecha_Modificacion,
                                        Id_Modificador = @Id_Modificador,
                                        Id_Transaccion = @Id_Transaccion
                                    WHERE Id_Solicitud_Beca = @Id_Solicitud_Beca;

                                    -- Registrar historial de la decisión
                                    INSERT INTO tbl_solicitudes_becas_historial(
                                        Id_Solicitud_Beca,
                                        Id_Estado_Anterior,
                                        Id_Estado_Nuevo,
                                        Id_Usuario_Revisor,
                                        Fecha_Decision,
                                        Motivo_Decision,
                                        Observaciones
                                    ) VALUES (
                                        @Id_Solicitud_Beca,
                                        @EstadoAnterior,
                                        @EstadoNuevo,
                                        @Id_Sesion,
                                        @Fecha_Ultima_Decision,
                                        @Motivo_Ultima_Decision,
                                        @Observaciones
                                    );

                                    COMMIT TRAN trx_RegistrarDecisionSolBec;

                                    SET @o_Num = 0;
                                    SET @o_Msg = '¡Decisión registrada exitosamente para la solicitud de beca!';

                                    EXEC sp_transacciones
                                    @Modo = 'UPD',
                                    @Id_Transaccion = @Id_Transaccion;
                                END TRY
                                BEGIN CATCH
                                    ROLLBACK TRAN trx_RegistrarDecisionSolBec;

                                    SET @o_Num = -1;
                                    SET @o_Msg = '¡Error interno del servidor al registrar la decisión de beca!';
                                    SET @Linea_Error = ERROR_LINE();
                                    SET @Numero_Error = ERROR_NUMBER();
                                    SET @Mensaje_Error = ERROR_MESSAGE();
                                    EXEC sp_logs_errores_sql
                                    @Modo = 'INS',
                                    @Origen_Error = @Origen_Error,
                                    @Linea_Error = @Linea_Error,
                                    @Numero_Error = @Numero_Error,
                                    @Mensaje_Error = @Mensaje_Error;
                                    EXEC sp_transacciones
                                    @Modo = 'RBK',
                                    @Id_Transaccion = @Id_Transaccion;
                                END CATCH
                            END TRY
                            BEGIN CATCH
                                SET @o_Num = -1;
                                SET @o_Msg = '¡Error interno del servidor al preparar la decisión de beca!';
                                SET @Linea_Error = ERROR_LINE();
                                SET @Numero_Error = ERROR_NUMBER();
                                SET @Mensaje_Error = ERROR_MESSAGE();
                                EXEC sp_logs_errores_sql
                                @Modo = 'INS',
                                @Origen_Error = @Origen_Error,
                                @Linea_Error = @Linea_Error,
                                @Numero_Error = @Numero_Error,
                                @Mensaje_Error = @Mensaje_Error;
                            END CATCH
                        END
                END
        END
    ELSE
        BEGIN
            SET @o_Num = -1;
            SET @o_Msg = '¡Su usuario no tiene permiso para realizar este tipo de transacción!';
        END
END

