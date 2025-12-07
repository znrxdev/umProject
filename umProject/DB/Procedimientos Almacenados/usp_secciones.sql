USE umDb
GO

/*
tbl_secciones
*/

CREATE OR ALTER PROC usp_secciones
(
    @Id_Seccion INT = NULL,
    @Codigo_Seccion VARCHAR(20) = NULL,
    @Id_Materia_Periodo INT = NULL,
    @Id_Periodo_Academico INT = NULL,
    @Id_Docente INT = NULL,
    @Id_Tipo_Seccion INT = NULL,
    @Id_Aula INT = NULL,
    @Horario_Descripcion NVARCHAR(255) = NULL,
    @Modalidad NVARCHAR(50) = NULL,
    @Cupo_Maximo INT = NULL,
    @Requiere_Asistencia BIT = NULL,
    @Porcentaje_Asistencia_Minima DECIMAL(5,2) = NULL,
    @Id_Estado INT = NULL,
    @Id_Estado_Publicacion INT = NULL,
    @Fecha_Publicacion DATETIME = NULL,
    @Fecha_Cierre DATETIME = NULL,
    @Codigo_Firma NVARCHAR(100) = NULL,
    @Id_Usuario_Publicador INT = NULL,
    @Observaciones NVARCHAR(255) = NULL,
    @Activo BIT = NULL,
    @Fecha_Creacion DATETIME = NULL,
    @Fecha_Modificacion DATETIME = NULL,
    @Id_Creador INT = NULL,
    @Id_Modificador INT = NULL,
    @Id_Tipo_Transaccion INT,
    @Id_Transaccion INT = NULL,
    @Id_Sesion INT = NULL,
    @o_Num INT = NULL OUTPUT,
    @o_Msg NVARCHAR(MAX) = NULL OUTPUT
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
            /* FILTRAR POR ID SECCION */
            IF(@Id_Tipo_Transaccion = 98)
                BEGIN
                    IF ISNULL(@Id_Seccion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de sección!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_secciones(NOLOCK) WHERE Id_Seccion = @Id_Seccion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La sección no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    S.Id_Seccion, 
                                    S.Codigo_Seccion, 
                                    S.Id_Materia_Periodo, 
                                    S.Id_Docente,
                                    S.Id_Tipo_Seccion, 
                                    S.Id_Aula, 
                                    S.Horario_Descripcion, 
                                    S.Modalidad,
                                    S.Cupo_Maximo, 
                                    S.Requiere_Asistencia, 
                                    S.Porcentaje_Asistencia_Minima,
                                    S.Id_Estado, 
                                    S.Id_Estado_Publicacion, 
                                    S.Fecha_Publicacion, 
                                    S.Fecha_Cierre,
                                    S.Codigo_Firma, 
                                    S.Id_Usuario_Publicador, 
                                    S.Observaciones, 
                                    S.Activo,
                                    S.Fecha_Creacion, 
                                    S.Fecha_Modificacion, 
                                    S.Id_Creador, 
                                    S.Id_Modificador,
                                    S.Id_Transaccion, 
                                    S.RowVersion,
                                    -- Información relacionada para mostrar en la UI
                                    M.Nombre_Materia,
                                    M.Codigo_Materia,
                                    PA.Nombre_Periodo,
                                    PA.Codigo_Periodo,
                                    DOC.Usuario AS Docente_Usuario,
                                    P.Primer_Nombre + ' ' + P.Primer_Apellido AS Docente_Nombre,
                                    TS.Nombre_Catalogo AS Tipo_Seccion_Nombre,
                                    AU.Nombre_Catalogo AS Aula_Nombre,
                                    EST.Nombre_Estado AS Estado_Nombre
                                FROM tbl_secciones S (NOLOCK)
                                INNER JOIN cls_materias_periodos MP (NOLOCK) ON S.Id_Materia_Periodo = MP.Id_Materia_Periodo
                                INNER JOIN cls_materias M (NOLOCK) ON MP.Id_Materia = M.Id_Materia
                                INNER JOIN tbl_periodos_academicos PA (NOLOCK) ON MP.Id_Periodo_Academico = PA.Id_Periodo
                                INNER JOIN tbl_usuarios DOC (NOLOCK) ON S.Id_Docente = DOC.Id_Usuario
                                INNER JOIN tbl_personas P (NOLOCK) ON DOC.Id_Persona = P.Id_Persona
                                LEFT JOIN cls_catalogos TS (NOLOCK) ON S.Id_Tipo_Seccion = TS.Id_Catalogo
                                LEFT JOIN cls_catalogos AU (NOLOCK) ON S.Id_Aula = AU.Id_Catalogo
                                LEFT JOIN cls_estados EST (NOLOCK) ON S.Id_Estado = EST.Id_Estado
                                WHERE S.Id_Seccion = @Id_Seccion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Sección encontrada!';
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
            /* FILTRAR POR ID DOCENTE */
            ELSE IF(@Id_Tipo_Transaccion = 99)
                BEGIN
                    IF ISNULL(@Id_Docente, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado el docente para listar sus secciones!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    S.Id_Seccion, 
                                    S.Codigo_Seccion, 
                                    S.Id_Materia_Periodo, 
                                    S.Id_Docente,
                                    S.Id_Tipo_Seccion, 
                                    S.Id_Aula, 
                                    S.Horario_Descripcion, 
                                    S.Modalidad,
                                    S.Cupo_Maximo, 
                                    S.Requiere_Asistencia, 
                                    S.Porcentaje_Asistencia_Minima,
                                    S.Id_Estado, 
                                    S.Id_Estado_Publicacion, 
                                    S.Fecha_Publicacion, 
                                    S.Fecha_Cierre,
                                    S.Codigo_Firma, 
                                    S.Id_Usuario_Publicador, 
                                    S.Observaciones, 
                                    S.Activo,
                                    S.Fecha_Creacion, 
                                    S.Fecha_Modificacion, 
                                    S.Id_Creador, 
                                    S.Id_Modificador,
                                    S.Id_Transaccion, 
                                    S.RowVersion,
                                    -- Información relacionada para mostrar en la UI
                                    M.Nombre_Materia,
                                    M.Codigo_Materia,
                                    PA.Nombre_Periodo,
                                    PA.Codigo_Periodo,
                                    DOC.Usuario AS Docente_Usuario,
                                    P.Primer_Nombre + ' ' + P.Primer_Apellido AS Docente_Nombre,
                                    TS.Nombre_Catalogo AS Tipo_Seccion_Nombre,
                                    AU.Nombre_Catalogo AS Aula_Nombre,
                                    EST.Nombre_Estado AS Estado_Nombre
                                FROM tbl_secciones S (NOLOCK)
                                INNER JOIN cls_materias_periodos MP (NOLOCK) ON S.Id_Materia_Periodo = MP.Id_Materia_Periodo
                                INNER JOIN cls_materias M (NOLOCK) ON MP.Id_Materia = M.Id_Materia
                                INNER JOIN tbl_periodos_academicos PA (NOLOCK) ON MP.Id_Periodo_Academico = PA.Id_Periodo
                                INNER JOIN tbl_usuarios DOC (NOLOCK) ON S.Id_Docente = DOC.Id_Usuario
                                INNER JOIN tbl_personas P (NOLOCK) ON DOC.Id_Persona = P.Id_Persona
                                LEFT JOIN cls_catalogos TS (NOLOCK) ON S.Id_Tipo_Seccion = TS.Id_Catalogo
                                LEFT JOIN cls_catalogos AU (NOLOCK) ON S.Id_Aula = AU.Id_Catalogo
                                LEFT JOIN cls_estados EST (NOLOCK) ON S.Id_Estado = EST.Id_Estado
                                WHERE S.Id_Docente = @Id_Docente AND S.Activo = 1
                                ORDER BY S.Fecha_Creacion DESC;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Secciones filtradas por docente!';
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
            /* LISTAR TODAS LAS SECCIONES / FILTRAR POR ID MATERIA PERIODO */
            ELSE IF(@Id_Tipo_Transaccion = 100)
                BEGIN
                    BEGIN TRY
                        -- Si @Id_Materia_Periodo es NULL o 0, listar todas las secciones activas
                        -- Si tiene un valor, filtrar por ese Id_Materia_Periodo específico
                        SELECT 
                            S.Id_Seccion, 
                            S.Codigo_Seccion, 
                            S.Id_Materia_Periodo, 
                            S.Id_Docente,
                            S.Id_Tipo_Seccion, 
                            S.Id_Aula, 
                            S.Horario_Descripcion, 
                            S.Modalidad,
                            S.Cupo_Maximo, 
                            S.Requiere_Asistencia, 
                            S.Porcentaje_Asistencia_Minima,
                            S.Id_Estado, 
                            S.Id_Estado_Publicacion, 
                            S.Fecha_Publicacion, 
                            S.Fecha_Cierre,
                            S.Codigo_Firma, 
                            S.Id_Usuario_Publicador, 
                            S.Observaciones, 
                            S.Activo,
                            S.Fecha_Creacion, 
                            S.Fecha_Modificacion, 
                            S.Id_Creador, 
                            S.Id_Modificador,
                            S.Id_Transaccion, 
                            S.RowVersion,
                            -- Información relacionada para mostrar en la UI
                            M.Nombre_Materia,
                            M.Codigo_Materia,
                            PA.Nombre_Periodo,
                            PA.Codigo_Periodo,
                            DOC.Usuario AS Docente_Usuario,
                            P.Primer_Nombre + ' ' + P.Primer_Apellido AS Docente_Nombre,
                            TS.Nombre_Catalogo AS Tipo_Seccion_Nombre,
                            AU.Nombre_Catalogo AS Aula_Nombre,
                            EST.Nombre_Estado AS Estado_Nombre
                        FROM tbl_secciones S (NOLOCK)
                        INNER JOIN cls_materias_periodos MP (NOLOCK) ON S.Id_Materia_Periodo = MP.Id_Materia_Periodo
                        INNER JOIN cls_materias M (NOLOCK) ON MP.Id_Materia = M.Id_Materia
                        INNER JOIN tbl_periodos_academicos PA (NOLOCK) ON MP.Id_Periodo_Academico = PA.Id_Periodo
                        INNER JOIN tbl_usuarios DOC (NOLOCK) ON S.Id_Docente = DOC.Id_Usuario
                        INNER JOIN tbl_personas P (NOLOCK) ON DOC.Id_Persona = P.Id_Persona
                        LEFT JOIN cls_catalogos TS (NOLOCK) ON S.Id_Tipo_Seccion = TS.Id_Catalogo
                        LEFT JOIN cls_catalogos AU (NOLOCK) ON S.Id_Aula = AU.Id_Catalogo
                        LEFT JOIN cls_estados EST (NOLOCK) ON S.Id_Estado = EST.Id_Estado
                        WHERE S.Activo = 1
                        AND (ISNULL(@Id_Materia_Periodo, 0) = 0 OR S.Id_Materia_Periodo = @Id_Materia_Periodo)
                        AND (ISNULL(@Id_Periodo_Academico, 0) = 0 OR MP.Id_Periodo_Academico = @Id_Periodo_Academico)
                        ORDER BY PA.Fecha_Inicio DESC, M.Codigo_Materia, S.Codigo_Seccion;

                        SET @o_Num = 0;
                        SET @o_Msg = CASE 
                            WHEN ISNULL(@Id_Materia_Periodo, 0) = 0 THEN '¡Secciones listadas exitosamente!'
                            ELSE '¡Secciones filtradas por materia período!'
                        END;
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
            /* AGREGAR SECCION */
            ELSE IF(@Id_Tipo_Transaccion = 96)
                BEGIN
                    IF ISNULL(@Id_Materia_Periodo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar una materia período!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_materias_periodos(NOLOCK) WHERE Id_Materia_Periodo = @Id_Materia_Periodo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La materia período no existe!';
                        END
                    ELSE IF ISNULL(@Id_Docente, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un docente!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_usuarios(NOLOCK) WHERE Id_Usuario = @Id_Docente)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El docente no existe!';
                        END
                    ELSE IF ISNULL(@Id_Tipo_Seccion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un tipo de sección!';
                        END
                    -- Forzar estado EN REVISION (4) al agregar
                    SET @Id_Estado = 4;
                    
                    -- Autogenerar código de sección si viene vacío o NULL
                    IF ISNULL(@Codigo_Seccion, '') = ''
                        BEGIN
                            DECLARE @Codigo_Materia VARCHAR(10);
                            DECLARE @Codigo_Periodo VARCHAR(20);
                            DECLARE @Aula_Nombre VARCHAR(50) = 'SIN-AULA';
                            DECLARE @Contador INT = 1;
                            DECLARE @Codigo_Generado VARCHAR(20);
                            
                            -- Obtener código de materia y código de período
                            SELECT 
                                @Codigo_Materia = M.Codigo_Materia,
                                @Codigo_Periodo = PA.Codigo_Periodo
                            FROM cls_materias_periodos MP (NOLOCK)
                            INNER JOIN cls_materias M (NOLOCK) ON MP.Id_Materia = M.Id_Materia
                            INNER JOIN tbl_periodos_academicos PA (NOLOCK) ON MP.Id_Periodo_Academico = PA.Id_Periodo
                            WHERE MP.Id_Materia_Periodo = @Id_Materia_Periodo;
                            
                            -- Obtener nombre del aula si está seleccionada
                            IF @Id_Aula IS NOT NULL
                                BEGIN
                                    SELECT @Aula_Nombre = Nombre_Catalogo
                                    FROM cls_catalogos(NOLOCK)
                                    WHERE Id_Catalogo = @Id_Aula;
                                    
                                    -- Limpiar el nombre del aula para usar en el código (solo letras y números)
                                    SET @Aula_Nombre = UPPER(REPLACE(REPLACE(REPLACE(@Aula_Nombre, ' ', ''), '-', ''), '_', ''));
                                    IF LEN(@Aula_Nombre) > 10
                                        SET @Aula_Nombre = SUBSTRING(@Aula_Nombre, 1, 10);
                                END
                            
                            -- Generar código base: {Codigo_Periodo}-{Codigo_Materia}-{Aula}
                            DECLARE @Codigo_Base VARCHAR(50) = CONCAT(@Codigo_Periodo, '-', @Codigo_Materia, '-', @Aula_Nombre);
                            
                            -- Buscar el siguiente contador para esta combinación
                            SELECT @Contador = ISNULL(MAX(
                                CASE 
                                    WHEN LEN(Codigo_Seccion) >= LEN(@Codigo_Base) + 2 
                                    THEN TRY_CAST(SUBSTRING(Codigo_Seccion, LEN(@Codigo_Base) + 2, LEN(Codigo_Seccion)) AS INT)
                                    ELSE NULL
                                END
                            ), 0) + 1
                            FROM tbl_secciones(NOLOCK)
                            WHERE Codigo_Seccion LIKE @Codigo_Base + '-%'
                            AND TRY_CAST(SUBSTRING(Codigo_Seccion, LEN(@Codigo_Base) + 2, LEN(Codigo_Seccion)) AS INT) IS NOT NULL;
                            
                            -- Generar código completo: {Codigo_Base}-{Contador}
                            SET @Codigo_Generado = CONCAT(@Codigo_Base, '-', CAST(@Contador AS VARCHAR));
                            
                            -- Asegurar que no exceda 20 caracteres
                            IF LEN(@Codigo_Generado) > 20
                                BEGIN
                                    -- Si excede, truncar el código base
                                    DECLARE @MaxLongitudBase INT = 20 - LEN(CAST(@Contador AS VARCHAR)) - 1; -- -1 para el guion
                                    SET @Codigo_Base = SUBSTRING(@Codigo_Base, 1, @MaxLongitudBase);
                                    SET @Codigo_Generado = CONCAT(@Codigo_Base, '-', CAST(@Contador AS VARCHAR));
                                END
                            
                            SET @Codigo_Seccion = @Codigo_Generado;
                        END
                    ELSE IF (@Porcentaje_Asistencia_Minima IS NOT NULL AND (@Porcentaje_Asistencia_Minima < 0 OR @Porcentaje_Asistencia_Minima > 100))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El porcentaje de asistencia mínima debe estar entre 0 y 100!';
                        END
                    ELSE IF (@Cupo_Maximo IS NOT NULL AND @Cupo_Maximo <= 0)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El cupo máximo debe ser mayor que cero!';
                        END
                    -- Validar que el código generado no exista ya para esta materia período
                    IF EXISTS(SELECT 1 FROM tbl_secciones(NOLOCK) WHERE Codigo_Seccion = @Codigo_Seccion AND Id_Materia_Periodo = @Id_Materia_Periodo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe una sección con ese código para esa materia período!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('AGREGANDO SECCIÓN: ', @Codigo_Seccion);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_AgregarSeccion
                            BEGIN TRY
                                INSERT INTO tbl_secciones(
                                    Codigo_Seccion, Id_Materia_Periodo, Id_Docente, Id_Tipo_Seccion,
                                    Id_Aula, Horario_Descripcion, Modalidad, Cupo_Maximo,
                                    Requiere_Asistencia, Porcentaje_Asistencia_Minima, Id_Estado,
                                    Id_Estado_Publicacion, Fecha_Publicacion, Fecha_Cierre,
                                    Codigo_Firma, Id_Usuario_Publicador, Observaciones, Activo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion
                                ) VALUES (
                                    @Codigo_Seccion, @Id_Materia_Periodo, @Id_Docente, @Id_Tipo_Seccion,
                                    @Id_Aula, @Horario_Descripcion, @Modalidad, @Cupo_Maximo,
                                    ISNULL(@Requiere_Asistencia, 1), @Porcentaje_Asistencia_Minima, @Id_Estado,
                                    @Id_Estado_Publicacion, @Fecha_Publicacion, @Fecha_Cierre,
                                    @Codigo_Firma, @Id_Usuario_Publicador, @Observaciones, ISNULL(@Activo, 1),
                                    @Fecha_Creacion, @Fecha_Modificacion, @Id_Creador, @Id_Modificador,
                                    @Id_Transaccion
                                );

                                COMMIT TRAN trx_AgregarSeccion;

                                SET @o_Num = SCOPE_IDENTITY();
                                SET @o_Msg = '¡Sección agregada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_AgregarSeccion;

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
            /* ACTUALIZAR SECCION */
            ELSE IF(@Id_Tipo_Transaccion = 97)
                BEGIN
                    SET @o_Num = 0;

                    IF ISNULL(@Id_Seccion, 0) = 0
                    BEGIN
                        SET @o_Num = -1;
                        SET @o_Msg = '¡Debe seleccionar un ID de sección!';
                    END

                    IF @o_Num = 0 AND NOT EXISTS(SELECT 1 FROM tbl_secciones(NOLOCK) WHERE Id_Seccion = @Id_Seccion)
                    BEGIN
                        SET @o_Num = -1;
                        SET @o_Msg = '¡La sección no existe!';
                    END

                    IF @o_Num = 0 AND (@Porcentaje_Asistencia_Minima IS NOT NULL AND (@Porcentaje_Asistencia_Minima < 0 OR @Porcentaje_Asistencia_Minima > 100))
                    BEGIN
                        SET @o_Num = -1;
                        SET @o_Msg = '¡El porcentaje de asistencia mínima debe estar entre 0 y 100!';
                    END

                    IF @o_Num = 0 AND (@Cupo_Maximo IS NOT NULL AND @Cupo_Maximo <= 0)
                    BEGIN
                        SET @o_Num = -1;
                        SET @o_Msg = '¡El cupo máximo debe ser mayor que cero!';
                    END

                    IF @o_Num = 0 AND ((@Codigo_Seccion IS NOT NULL OR @Id_Materia_Periodo IS NOT NULL) AND
                             EXISTS(SELECT 1 FROM tbl_secciones(NOLOCK) 
                                    WHERE Codigo_Seccion = COALESCE(@Codigo_Seccion, (SELECT Codigo_Seccion FROM tbl_secciones WHERE Id_Seccion = @Id_Seccion))
                                      AND Id_Materia_Periodo = COALESCE(@Id_Materia_Periodo, (SELECT Id_Materia_Periodo FROM tbl_secciones WHERE Id_Seccion = @Id_Seccion))
                                      AND Id_Seccion <> @Id_Seccion))
                    BEGIN
                        SET @o_Num = -1;
                        SET @o_Msg = '¡Ya existe otra sección con ese código para esa materia período!';
                    END

                    IF @o_Num = 0
                    BEGIN
                        DECLARE @EstadoFinalSeccion INT = COALESCE(@Id_Estado, (SELECT Id_Estado FROM tbl_secciones WHERE Id_Seccion = @Id_Seccion));
                        DECLARE @ActivoFinalSeccion BIT = COALESCE(@Activo, (SELECT Activo FROM tbl_secciones WHERE Id_Seccion = @Id_Seccion));
                        DECLARE @IdPeriodoSeccion INT;
                        SELECT @IdPeriodoSeccion = mp.Id_Periodo_Academico
                        FROM tbl_secciones s(NOLOCK)
                        INNER JOIN cls_materias_periodos mp(NOLOCK) ON s.Id_Materia_Periodo = mp.Id_Materia_Periodo
                        WHERE s.Id_Seccion = @Id_Seccion;

                        IF (@EstadoFinalSeccion = 2 OR @ActivoFinalSeccion = 0)
                           AND EXISTS(SELECT 1 FROM tbl_periodos_academicos(NOLOCK) WHERE Id_Periodo = @IdPeriodoSeccion AND Id_Estado = 1)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No se puede inactivar la sección porque su período académico está ACTIVO!';
                        END
                    END

                    IF @o_Num = 0
                        BEGIN
                            SET @iConcepto = CONCAT('ACTUALIZANDO SECCIÓN ID: ', @Id_Seccion);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_ActualizarSeccion
                            BEGIN TRY
                                UPDATE tbl_secciones
                                SET Codigo_Seccion = COALESCE(@Codigo_Seccion, Codigo_Seccion),
                                    Id_Materia_Periodo = COALESCE(@Id_Materia_Periodo, Id_Materia_Periodo),
                                    Id_Docente = COALESCE(@Id_Docente, Id_Docente),
                                    Id_Tipo_Seccion = COALESCE(@Id_Tipo_Seccion, Id_Tipo_Seccion),
                                    Id_Aula = COALESCE(@Id_Aula, Id_Aula),
                                    Horario_Descripcion = COALESCE(@Horario_Descripcion, Horario_Descripcion),
                                    Modalidad = COALESCE(@Modalidad, Modalidad),
                                    Cupo_Maximo = COALESCE(@Cupo_Maximo, Cupo_Maximo),
                                    Requiere_Asistencia = COALESCE(@Requiere_Asistencia, Requiere_Asistencia),
                                    Porcentaje_Asistencia_Minima = COALESCE(@Porcentaje_Asistencia_Minima, Porcentaje_Asistencia_Minima),
                                    Id_Estado = COALESCE(@Id_Estado, Id_Estado),
                                    Id_Estado_Publicacion = COALESCE(@Id_Estado_Publicacion, Id_Estado_Publicacion),
                                    Fecha_Publicacion = COALESCE(@Fecha_Publicacion, Fecha_Publicacion),
                                    Fecha_Cierre = COALESCE(@Fecha_Cierre, Fecha_Cierre),
                                    Codigo_Firma = COALESCE(@Codigo_Firma, Codigo_Firma),
                                    Id_Usuario_Publicador = COALESCE(@Id_Usuario_Publicador, Id_Usuario_Publicador),
                                    Observaciones = COALESCE(@Observaciones, Observaciones),
                                    Activo = COALESCE(@Activo, Activo),
                                    Fecha_Modificacion = @Fecha_Modificacion,
                                    Id_Modificador = @Id_Modificador,
                                    Id_Transaccion = @Id_Transaccion
                                WHERE Id_Seccion = @Id_Seccion;

                                COMMIT TRAN trx_ActualizarSeccion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Sección actualizada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_ActualizarSeccion;

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
            /* VALIDAR ACTIVACION SECCION */
            ELSE IF(@Id_Tipo_Transaccion = 186)
                BEGIN
                    IF ISNULL(@Id_Seccion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de sección!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_secciones(NOLOCK) WHERE Id_Seccion = @Id_Seccion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La sección no existe!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_secciones(NOLOCK) WHERE Id_Seccion = @Id_Seccion AND Id_Estado = 4)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Solo se puede validar la activación de una sección que esté en estado EN REVISION!';
                        END
                    ELSE
                        BEGIN
                            DECLARE @ErroresValidacion NVARCHAR(MAX) = '';
                            DECLARE @IdMateriaPeriodo INT;
                            DECLARE @IdPeriodoAcademico INT;
                            DECLARE @IdMateria INT;
                            DECLARE @CodigoSeccion VARCHAR(20);
                            DECLARE @FechaInicioPeriodo DATE;
                            DECLARE @FechaFinPeriodo DATE;
                            
                            -- Obtener información de la sección
                            SELECT 
                                @IdMateriaPeriodo = s.Id_Materia_Periodo,
                                @CodigoSeccion = s.Codigo_Seccion
                            FROM tbl_secciones(NOLOCK) s
                            WHERE s.Id_Seccion = @Id_Seccion;
                            
                            -- Obtener información del período académico y materia
                            SELECT 
                                @IdPeriodoAcademico = mp.Id_Periodo_Academico,
                                @IdMateria = mp.Id_Materia
                            FROM cls_materias_periodos(NOLOCK) mp
                            WHERE mp.Id_Materia_Periodo = @IdMateriaPeriodo;
                            
                            -- Obtener fechas del período académico
                            SELECT 
                                @FechaInicioPeriodo = p.Fecha_Inicio,
                                @FechaFinPeriodo = p.Fecha_Fin
                            FROM tbl_periodos_academicos(NOLOCK) p
                            WHERE p.Id_Periodo = @IdPeriodoAcademico;
                            
                            -- 1. Validar que el período académico esté EN REVISION (Id_Estado = 4)
                            IF NOT EXISTS(
                                SELECT 1 
                                FROM tbl_periodos_academicos(NOLOCK) 
                                WHERE Id_Periodo = @IdPeriodoAcademico 
                                AND Id_Estado = 4
                            )
                                BEGIN
                                    SET @ErroresValidacion = @ErroresValidacion + 'El período académico de la sección debe estar en estado EN REVISION antes de activar la sección. ';
                                END
                            
                            -- 2. Validar que la sección tenga al menos un grupo asignado
                            IF NOT EXISTS(
                                SELECT 1 
                                FROM cls_grupos_secciones(NOLOCK) 
                                WHERE Id_Seccion = @Id_Seccion 
                                AND Activo = 1
                            )
                                BEGIN
                                    SET @ErroresValidacion = @ErroresValidacion + 'La sección ' + @CodigoSeccion + ' no tiene grupos asignados. Debe asignar al menos un grupo antes de activar la sección. ';
                                END
                            ELSE
                                BEGIN
                                    -- 3. Validar grupos asignados
                                    DECLARE @IdGrupo INT;
                                    DECLARE @CodigoGrupo VARCHAR(20);
                                    DECLARE grupos_cursor CURSOR FOR 
                                        SELECT DISTINCT gs.Id_Grupo, g.Codigo_Grupo
                                        FROM cls_grupos_secciones gs(NOLOCK)
                                        INNER JOIN tbl_grupos(NOLOCK) g ON gs.Id_Grupo = g.Id_Grupo
                                        WHERE gs.Id_Seccion = @Id_Seccion AND gs.Activo = 1;
                                    OPEN grupos_cursor;
                                    FETCH NEXT FROM grupos_cursor INTO @IdGrupo, @CodigoGrupo;
                                    
                                    WHILE @@FETCH_STATUS = 0
                                    BEGIN
                                        -- 3.1. El grupo debe estar ACTIVO (Id_Estado = 1 y Activo = 1)
                                        IF NOT EXISTS(
                                            SELECT 1 
                                            FROM tbl_grupos(NOLOCK) 
                                            WHERE Id_Grupo = @IdGrupo 
                                            AND Id_Estado = 1 
                                            AND Activo = 1
                                        )
                                            BEGIN
                                                SET @ErroresValidacion = @ErroresValidacion + 'El grupo ' + @CodigoGrupo + ' asignado a la sección ' + @CodigoSeccion + ' no está ACTIVO. Todos los grupos deben estar ACTIVOS antes de activar la sección. ';
                                            END
                                        ELSE
                                            BEGIN
                                                -- 3.2. El grupo debe tener al menos una inscripción ACTIVA
                                                IF NOT EXISTS(
                                                    SELECT 1 
                                                    FROM tbl_grupos_inscripciones gi(NOLOCK)
                                                    INNER JOIN tbl_inscripciones i(NOLOCK) ON gi.Id_Inscripcion = i.Id_Inscripcion
                                                    WHERE gi.Id_Grupo = @IdGrupo 
                                                    AND gi.Activo = 1 
                                                    AND i.Id_Estado = 1 -- ACTIVA
                                                )
                                                    BEGIN
                                                        SET @ErroresValidacion = @ErroresValidacion + 'El grupo ' + @CodigoGrupo + ' asignado a la sección ' + @CodigoSeccion + ' no tiene inscripciones ACTIVAS. Todos los grupos deben tener al menos una inscripción ACTIVA antes de activar la sección. ';
                                                    END
                                            END
                                        
                                        FETCH NEXT FROM grupos_cursor INTO @IdGrupo, @CodigoGrupo;
                                    END
                                    CLOSE grupos_cursor;
                                    DEALLOCATE grupos_cursor;
                                    
                                    -- 4. Validar que la materia tenga al menos un modelo de evaluación
                                    IF NOT EXISTS(
                                        SELECT 1 
                                        FROM cls_evaluaciones_modelos(NOLOCK) 
                                        WHERE Id_Materia = @IdMateria 
                                        AND Activo = 1
                                    )
                                        BEGIN
                                            DECLARE @CodigoMateria VARCHAR(10);
                                            SELECT @CodigoMateria = Codigo_Materia FROM cls_materias(NOLOCK) WHERE Id_Materia = @IdMateria;
                                            SET @ErroresValidacion = @ErroresValidacion + 'La materia ' + ISNULL(@CodigoMateria, 'N/A') + ' de la sección ' + @CodigoSeccion + ' no tiene modelos de evaluación creados. Debe crear al menos un modelo de evaluación para la materia antes de activar la sección. ';
                                        END
                                    ELSE
                                        BEGIN
                                            -- 5. Validar que haya al menos una instancia de evaluación en estado PENDIENTE dentro del rango de fechas del período
                                            IF NOT EXISTS(
                                                SELECT 1 
                                                FROM tbl_evaluaciones_instancias ei(NOLOCK)
                                                INNER JOIN cls_evaluaciones_modelos(NOLOCK) em ON ei.Id_Evaluacion_Modelo = em.Id_Evaluacion_Modelo
                                                WHERE ei.Id_Seccion = @Id_Seccion 
                                                AND em.Id_Materia = @IdMateria
                                                AND ei.Id_Estado = 3 -- PENDIENTE
                                                AND ei.Fecha_Programada IS NOT NULL
                                                AND CAST(ei.Fecha_Programada AS DATE) >= @FechaInicioPeriodo
                                                AND CAST(ei.Fecha_Programada AS DATE) <= @FechaFinPeriodo
                                            )
                                                BEGIN
                                                    SET @ErroresValidacion = @ErroresValidacion + 'La sección ' + @CodigoSeccion + ' no tiene instancias de evaluación en estado PENDIENTE dentro del rango de fechas del período académico. Debe crear al menos una instancia de evaluación en estado PENDIENTE antes de activar la sección. ';
                                                END
                                            ELSE
                                                BEGIN
                                                    -- 6. Validar que la suma de calificación máxima sea exactamente 100
                                                    DECLARE @SumaCalificacionMax DECIMAL(10,2);
                                                    SELECT @SumaCalificacionMax = SUM(ISNULL(ei.Calificacion_Maxima, 0))
                                                    FROM tbl_evaluaciones_instancias ei(NOLOCK)
                                                    WHERE ei.Id_Seccion = @Id_Seccion
                                                    AND ei.Id_Estado = 3; -- PENDIENTE

                                                    IF ISNULL(@SumaCalificacionMax, 0) <> 100
                                                        BEGIN
                                                            SET @ErroresValidacion = @ErroresValidacion + 'La sección ' + @CodigoSeccion + ' tiene un total de calificación máxima de ' + CAST(ISNULL(@SumaCalificacionMax, 0) AS VARCHAR(20)) + ' puntos. Debe sumar exactamente 100 puntos antes de activar la sección. ';
                                                        END
                                                END
                                        END
                                END
                            
                            IF LEN(@ErroresValidacion) > 0
                                BEGIN
                                    SET @o_Num = -1;
                                    SET @o_Msg = @ErroresValidacion;
                                END
                            ELSE
                                BEGIN
                                    SET @o_Num = 0;
                                    SET @o_Msg = '¡Validación exitosa. La sección puede ser activada.';
                                END
                        END
                END
        END
    ELSE
        BEGIN
            SET @o_Num = -1;
            SET @o_Msg = '¡Su usuario no tiene permiso para realizar este tipo de transacción!';
        END
END

