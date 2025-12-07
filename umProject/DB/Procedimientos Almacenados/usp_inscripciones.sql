USE umDb
GO

/*
tbl_inscripciones
*/

CREATE OR ALTER PROC usp_inscripciones
(
    @Id_Inscripcion INT = NULL,
    @Codigo_Inscripcion VARCHAR(30) = NULL,
    @Id_Estudiante INT = NULL,
    @Id_Tipo_Inscripcion INT = NULL,
    @Id_Estado INT = NULL,
    @Fecha_Validacion DATETIME = NULL,
    @Fecha_Retiro DATETIME = NULL,
    @Motivo_Retiro NVARCHAR(500) = NULL,
    @Id_Usuario_Validador INT = NULL,
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
            /* FILTRAR POR ID INSCRIPCION */
            IF(@Id_Tipo_Transaccion = 112)
                BEGIN
                    IF ISNULL(@Id_Inscripcion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de inscripción!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_inscripciones(NOLOCK) WHERE Id_Inscripcion = @Id_Inscripcion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La inscripción no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    I.Id_Inscripcion, 
                                    I.Codigo_Inscripcion, 
                                    I.Id_Estudiante,
                                    I.Id_Tipo_Inscripcion, 
                                    I.Id_Estado, 
                                    I.Fecha_Validacion, 
                                    I.Fecha_Retiro,
                                    I.Motivo_Retiro, 
                                    I.Id_Usuario_Validador, 
                                    I.Fecha_Creacion, 
                                    I.Fecha_Modificacion, 
                                    I.Id_Creador, 
                                    I.Id_Modificador,
                                    I.Id_Transaccion, 
                                    I.RowVersion,
                                    -- Información relacionada para mostrar en la UI
                                    ESTU.Usuario AS Estudiante_Usuario,
                                    PEST.Primer_Nombre + ' ' + PEST.Primer_Apellido AS Estudiante_Nombre,
                                    TI.Nombre_Catalogo AS Tipo_Inscripcion_Nombre,
                                    EST.Nombre_Estado AS Estado_Nombre,
                                    VAL.Usuario AS Validador_Usuario
                                FROM tbl_inscripciones I (NOLOCK)
                                INNER JOIN tbl_usuarios ESTU (NOLOCK) ON I.Id_Estudiante = ESTU.Id_Usuario
                                INNER JOIN tbl_personas PEST (NOLOCK) ON ESTU.Id_Persona = PEST.Id_Persona
                                LEFT JOIN cls_catalogos TI (NOLOCK) ON I.Id_Tipo_Inscripcion = TI.Id_Catalogo
                                LEFT JOIN cls_estados EST (NOLOCK) ON I.Id_Estado = EST.Id_Estado
                                LEFT JOIN tbl_usuarios VAL (NOLOCK) ON I.Id_Usuario_Validador = VAL.Id_Usuario
                                WHERE I.Id_Inscripcion = @Id_Inscripcion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Inscripción encontrada!';
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
            /* FILTRAR POR ID ESTUDIANTE */
            ELSE IF(@Id_Tipo_Transaccion = 113)
                BEGIN
                    IF ISNULL(@Id_Estudiante, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado el estudiante para listar sus inscripciones!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    I.Id_Inscripcion, 
                                    I.Codigo_Inscripcion, 
                                    I.Id_Estudiante,
                                    I.Id_Tipo_Inscripcion, 
                                    I.Id_Estado, 
                                    I.Fecha_Validacion, 
                                    I.Fecha_Retiro,
                                    I.Motivo_Retiro, 
                                    I.Id_Usuario_Validador, 
                                    I.Fecha_Creacion, 
                                    I.Fecha_Modificacion, 
                                    I.Id_Creador, 
                                    I.Id_Modificador,
                                    I.Id_Transaccion, 
                                    I.RowVersion,
                                    -- Información relacionada para mostrar en la UI
                                    ESTU.Usuario AS Estudiante_Usuario,
                                    PEST.Primer_Nombre + ' ' + PEST.Primer_Apellido AS Estudiante_Nombre,
                                    TI.Nombre_Catalogo AS Tipo_Inscripcion_Nombre,
                                    EST.Nombre_Estado AS Estado_Nombre
                                FROM tbl_inscripciones I (NOLOCK)
                                INNER JOIN tbl_usuarios ESTU (NOLOCK) ON I.Id_Estudiante = ESTU.Id_Usuario
                                INNER JOIN tbl_personas PEST (NOLOCK) ON ESTU.Id_Persona = PEST.Id_Persona
                                LEFT JOIN cls_catalogos TI (NOLOCK) ON I.Id_Tipo_Inscripcion = TI.Id_Catalogo
                                LEFT JOIN cls_estados EST (NOLOCK) ON I.Id_Estado = EST.Id_Estado
                                WHERE I.Id_Estudiante = @Id_Estudiante
                                ORDER BY I.Fecha_Creacion DESC;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Inscripciones filtradas por estudiante!';
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
            /* FILTRAR POR ID SECCION - DEPRECADO: Ya no existe Id_Seccion en tbl_inscripciones */
            ELSE IF(@Id_Tipo_Transaccion = 114)
                BEGIN
                    SET @o_Num = -1;
                    SET @o_Msg = '¡Esta transacción ya no está disponible. Las inscripciones ya no están ligadas directamente a secciones.';
                END
            /* LISTAR TODAS LAS INSCRIPCIONES */
            ELSE IF(@Id_Tipo_Transaccion = 115)
                BEGIN
                    BEGIN TRY
                        SELECT 
                            I.Id_Inscripcion, 
                            I.Codigo_Inscripcion, 
                            I.Id_Estudiante,
                            I.Id_Tipo_Inscripcion, 
                            I.Id_Estado, 
                            I.Fecha_Validacion, 
                            I.Fecha_Retiro,
                            I.Motivo_Retiro, 
                            I.Id_Usuario_Validador, 
                            I.Fecha_Creacion, 
                            I.Fecha_Modificacion, 
                            I.Id_Creador, 
                            I.Id_Modificador,
                            I.Id_Transaccion, 
                            I.RowVersion,
                            -- Información relacionada para mostrar en la UI
                            ESTU.Usuario AS Estudiante_Usuario,
                            PEST.Primer_Nombre + ' ' + PEST.Primer_Apellido AS Estudiante_Nombre,
                            TI.Nombre_Catalogo AS Tipo_Inscripcion_Nombre,
                            EST.Nombre_Estado AS Estado_Nombre
                        FROM tbl_inscripciones I (NOLOCK)
                        INNER JOIN tbl_usuarios ESTU (NOLOCK) ON I.Id_Estudiante = ESTU.Id_Usuario
                        INNER JOIN tbl_personas PEST (NOLOCK) ON ESTU.Id_Persona = PEST.Id_Persona
                        LEFT JOIN cls_catalogos TI (NOLOCK) ON I.Id_Tipo_Inscripcion = TI.Id_Catalogo
                        LEFT JOIN cls_estados EST (NOLOCK) ON I.Id_Estado = EST.Id_Estado
                        ORDER BY I.Fecha_Creacion DESC, I.Codigo_Inscripcion;

                        SET @o_Num = 0;
                        SET @o_Msg = '¡Inscripciones listadas exitosamente!';
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
            /* LISTAR INSCRIPCIONES DISPONIBLES PARA GRUPOS (Estudiantes con rol estudiante y que tengan inscripción ACTIVA) */
            ELSE IF(@Id_Tipo_Transaccion = 116)
                BEGIN
                    BEGIN TRY
                        SELECT 
                            I.Id_Inscripcion, 
                            I.Codigo_Inscripcion, 
                            I.Id_Estudiante,
                            -- Información del estudiante
                            ESTU.Usuario AS Estudiante_Usuario,
                            PEST.Primer_Nombre + ' ' + ISNULL(PEST.Segundo_Nombre + ' ', '') + PEST.Primer_Apellido + ' ' + ISNULL(PEST.Segundo_Apellido, '') AS Estudiante_Nombre_Completo,
                            PEST.Valor_Documento AS Estudiante_Documento,
                            -- Información de la inscripción
                            TI.Nombre_Catalogo AS Tipo_Inscripcion_Nombre,
                            EST.Nombre_Estado AS Estado_Nombre
                        FROM tbl_inscripciones I (NOLOCK)
                        INNER JOIN tbl_usuarios ESTU (NOLOCK) ON I.Id_Estudiante = ESTU.Id_Usuario
                        INNER JOIN tbl_personas PEST (NOLOCK) ON ESTU.Id_Persona = PEST.Id_Persona
                        INNER JOIN cls_usuarios_roles UR (NOLOCK) ON ESTU.Id_Usuario = UR.Id_Usuario AND UR.Activo = 1
                        LEFT JOIN cls_catalogos TI (NOLOCK) ON I.Id_Tipo_Inscripcion = TI.Id_Catalogo
                        LEFT JOIN cls_estados EST (NOLOCK) ON I.Id_Estado = EST.Id_Estado
                        WHERE UR.Id_Rol = 2 -- Rol Estudiante
                            AND I.Id_Estado = 1 -- Solo inscripciones ACTIVAS
                            AND ESTU.Id_Estado = 1 -- Usuario ACTIVO
                        ORDER BY PEST.Primer_Apellido, PEST.Primer_Nombre, ESTU.Usuario;

                        SET @o_Num = 0;
                        SET @o_Msg = '¡Inscripciones disponibles listadas exitosamente!';
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
            /* AGREGAR INSCRIPCION */
            ELSE IF(@Id_Tipo_Transaccion = 110)
                BEGIN
                    IF ISNULL(@Id_Estudiante, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un estudiante!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_usuarios(NOLOCK) WHERE Id_Usuario = @Id_Estudiante)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El estudiante no existe!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM tbl_inscripciones(NOLOCK) WHERE Id_Estudiante = @Id_Estudiante)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El estudiante ya tiene una inscripción! Un estudiante solo puede tener una inscripción única.';
                        END
                    ELSE IF ISNULL(@Codigo_Inscripcion, '') <> '' AND EXISTS(SELECT 1 FROM tbl_inscripciones(NOLOCK) WHERE Codigo_Inscripcion = @Codigo_Inscripcion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe una inscripción con ese código!';
                        END
                    ELSE
                        BEGIN
                            -- Generar código de inscripción único si no se proporciona
                            IF ISNULL(@Codigo_Inscripcion, '') = ''
                                BEGIN
                                    DECLARE @Prefijo VARCHAR(10) = 'INS-';
                                    DECLARE @Anio VARCHAR(4) = CAST(YEAR(GETDATE()) AS VARCHAR);
                                    DECLARE @Contador INT;
                                    DECLARE @Patron VARCHAR(20) = @Prefijo + @Anio + '-%';
                                    
                                    -- Obtener el máximo contador de códigos que siguen el formato correcto
                                    -- Usar TRY_CAST para evitar errores con códigos antiguos que no siguen el formato
                                    SELECT @Contador = ISNULL(MAX(
                                        CASE 
                                            WHEN LEN(Codigo_Inscripcion) >= LEN(@Prefijo + @Anio + '-') + 1 
                                            THEN TRY_CAST(SUBSTRING(Codigo_Inscripcion, LEN(@Prefijo + @Anio + '-') + 1, LEN(Codigo_Inscripcion)) AS INT)
                                            ELSE NULL
                                        END
                                    ), 0) + 1
                                    FROM tbl_inscripciones(NOLOCK)
                                    WHERE Codigo_Inscripcion LIKE @Patron
                                    AND TRY_CAST(SUBSTRING(Codigo_Inscripcion, LEN(@Prefijo + @Anio + '-') + 1, LEN(Codigo_Inscripcion)) AS INT) IS NOT NULL;
                                    
                                    SET @Codigo_Inscripcion = @Prefijo + @Anio + '-' + RIGHT('000000' + CAST(@Contador AS VARCHAR), 6);
                                END

                            -- Estado inicial siempre EN REVISION (4)
                            SET @Id_Estado = 4;

                            SET @iConcepto = CONCAT('AGREGANDO INSCRIPCIÓN: ', @Codigo_Inscripcion);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_AgregarInscripcion
                            BEGIN TRY
                                INSERT INTO tbl_inscripciones(
                                    Codigo_Inscripcion, Id_Estudiante, Id_Tipo_Inscripcion,
                                    Id_Estado, Fecha_Validacion, Fecha_Retiro, Motivo_Retiro,
                                    Id_Usuario_Validador, Fecha_Creacion, Fecha_Modificacion,
                                    Id_Creador, Id_Modificador, Id_Transaccion
                                ) VALUES (
                                    @Codigo_Inscripcion, @Id_Estudiante, @Id_Tipo_Inscripcion,
                                    @Id_Estado, @Fecha_Validacion, @Fecha_Retiro, @Motivo_Retiro,
                                    @Id_Usuario_Validador, @Fecha_Creacion, @Fecha_Modificacion,
                                    @Id_Creador, @Id_Modificador, @Id_Transaccion
                                );

                                COMMIT TRAN trx_AgregarInscripcion;

                                SET @o_Num = SCOPE_IDENTITY();
                                SET @o_Msg = '¡Inscripción agregada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_AgregarInscripcion;

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
            /* ACTUALIZAR INSCRIPCION */
            ELSE IF(@Id_Tipo_Transaccion = 111)
                BEGIN
                    IF ISNULL(@Id_Inscripcion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de inscripción!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_inscripciones(NOLOCK) WHERE Id_Inscripcion = @Id_Inscripcion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La inscripción no existe!';
                        END
                    ELSE IF (@Codigo_Inscripcion IS NOT NULL AND EXISTS(SELECT 1 FROM tbl_inscripciones(NOLOCK) WHERE Codigo_Inscripcion = @Codigo_Inscripcion AND Id_Inscripcion <> @Id_Inscripcion))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe otra inscripción con ese código!';
                        END
                    ELSE IF (@Id_Estado IS NOT NULL AND @Id_Estado NOT IN (1, 2))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Solo se permiten estados ACTIVO (1) o INACTIVO (2) para actualizar!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('ACTUALIZANDO INSCRIPCIÓN ID: ', @Id_Inscripcion);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_ActualizarInscripcion
                            BEGIN TRY
                                UPDATE tbl_inscripciones
                                SET Codigo_Inscripcion = COALESCE(@Codigo_Inscripcion, Codigo_Inscripcion),
                                    Id_Estudiante = COALESCE(@Id_Estudiante, Id_Estudiante),
                                    Id_Tipo_Inscripcion = COALESCE(@Id_Tipo_Inscripcion, Id_Tipo_Inscripcion),
                                    Id_Estado = COALESCE(@Id_Estado, Id_Estado),
                                    Fecha_Validacion = COALESCE(@Fecha_Validacion, Fecha_Validacion),
                                    Fecha_Retiro = COALESCE(@Fecha_Retiro, Fecha_Retiro),
                                    Motivo_Retiro = COALESCE(@Motivo_Retiro, Motivo_Retiro),
                                    Id_Usuario_Validador = COALESCE(@Id_Usuario_Validador, Id_Usuario_Validador),
                                    Fecha_Modificacion = @Fecha_Modificacion,
                                    Id_Modificador = @Id_Modificador,
                                    Id_Transaccion = @Id_Transaccion
                                WHERE Id_Inscripcion = @Id_Inscripcion;

                                COMMIT TRAN trx_ActualizarInscripcion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Inscripcion actualizada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_ActualizarInscripcion;

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
        END
    ELSE
        BEGIN
            SET @o_Num = -1;
            SET @o_Msg = '¡Su usuario no tiene permiso para realizar este tipo de transacción!';
        END
END

