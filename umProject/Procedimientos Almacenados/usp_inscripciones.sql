USE umDb
GO

/*
tbl_inscripciones
*/

CREATE OR ALTER PROC usp_inscripciones
(
    @Id_Inscripcion INT = NULL,
    @Codigo_Inscripcion VARCHAR(30) = NULL,
    @Id_Seccion INT = NULL,
    @Id_Estudiante INT = NULL,
    @Id_Tipo_Inscripcion INT = NULL,
    @Id_Estado INT = NULL,
    @Fecha_Validacion DATETIME = NULL,
    @Fecha_Retiro DATETIME = NULL,
    @Motivo_Retiro NVARCHAR(500) = NULL,
    @Promedio_Acumulado DECIMAL(5,2) = NULL,
    @Total_Faltas INT = NULL,
    @Es_Repetidor BIT = NULL,
    @En_Riesgo_Academico BIT = NULL,
    @Id_Usuario_Validador INT = NULL,
    @Activo BIT = NULL,
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
                                    Id_Inscripcion, Codigo_Inscripcion, Id_Seccion, Id_Estudiante,
                                    Id_Tipo_Inscripcion, Id_Estado, Fecha_Validacion, Fecha_Retiro,
                                    Motivo_Retiro, Promedio_Acumulado, Total_Faltas, Es_Repetidor,
                                    En_Riesgo_Academico, Id_Usuario_Validador, Activo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM tbl_inscripciones(NOLOCK)
                                WHERE Id_Inscripcion = @Id_Inscripcion;

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
                                    Id_Inscripcion, Codigo_Inscripcion, Id_Seccion, Id_Estudiante,
                                    Id_Tipo_Inscripcion, Id_Estado, Fecha_Validacion, Fecha_Retiro,
                                    Motivo_Retiro, Promedio_Acumulado, Total_Faltas, Es_Repetidor,
                                    En_Riesgo_Academico, Id_Usuario_Validador, Activo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM tbl_inscripciones(NOLOCK)
                                WHERE Id_Estudiante = @Id_Estudiante AND Activo = 1
                                ORDER BY Fecha_Creacion DESC;

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
            /* FILTRAR POR ID SECCION */
            ELSE IF(@Id_Tipo_Transaccion = 114)
                BEGIN
                    IF ISNULL(@Id_Seccion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado la sección para listar sus inscripciones!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    i.Id_Inscripcion, 
                                    i.Codigo_Inscripcion, 
                                    i.Id_Seccion, 
                                    i.Id_Estudiante,
                                    i.Id_Tipo_Inscripcion, 
                                    i.Id_Estado, 
                                    i.Fecha_Validacion, 
                                    i.Fecha_Retiro,
                                    i.Motivo_Retiro, 
                                    i.Promedio_Acumulado, 
                                    i.Total_Faltas, 
                                    i.Es_Repetidor,
                                    i.En_Riesgo_Academico, 
                                    i.Id_Usuario_Validador, 
                                    i.Activo,
                                    i.Fecha_Creacion, 
                                    i.Fecha_Modificacion, 
                                    i.Id_Creador, 
                                    i.Id_Modificador,
                                    i.Id_Transaccion, 
                                    i.RowVersion,
                                    -- Información adicional del estudiante
                                    u.Usuario AS Usuario_Estudiante,
                                    p.Primer_Nombre + ' ' + p.Primer_Apellido AS Nombre_Estudiante
                                FROM tbl_inscripciones(NOLOCK) i
                                LEFT JOIN tbl_usuarios(NOLOCK) u ON i.Id_Estudiante = u.Id_Usuario
                                LEFT JOIN tbl_personas(NOLOCK) p ON u.Id_Persona = p.Id_Persona
                                WHERE i.Id_Seccion = @Id_Seccion AND i.Activo = 1
                                ORDER BY i.Fecha_Creacion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Inscripciones filtradas por sección!';
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
            /* AGREGAR INSCRIPCION */
            ELSE IF(@Id_Tipo_Transaccion = 110)
                BEGIN
                    IF ISNULL(@Id_Seccion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar una sección!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_secciones(NOLOCK) WHERE Id_Seccion = @Id_Seccion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La sección no existe!';
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
                            SET @o_Msg = '¡Debe asignar un estado!';
                        END
                    ELSE IF (@Promedio_Acumulado IS NOT NULL AND (@Promedio_Acumulado < 0 OR @Promedio_Acumulado > 100))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El promedio acumulado debe estar entre 0 y 100!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM tbl_inscripciones(NOLOCK) WHERE Id_Seccion = @Id_Seccion AND Id_Estudiante = @Id_Estudiante AND Activo = 1)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El estudiante ya está inscrito en esa sección!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM tbl_inscripciones(NOLOCK) WHERE Codigo_Inscripcion = @Codigo_Inscripcion)
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
                                    SELECT @Contador = ISNULL(MAX(CAST(SUBSTRING(Codigo_Inscripcion, LEN(@Prefijo + @Anio + '-') + 1, LEN(Codigo_Inscripcion)) AS INT)), 0) + 1
                                    FROM tbl_inscripciones(NOLOCK)
                                    WHERE Codigo_Inscripcion LIKE @Prefijo + @Anio + '-%';
                                    SET @Codigo_Inscripcion = @Prefijo + @Anio + '-' + RIGHT('000000' + CAST(@Contador AS VARCHAR), 6);
                                END

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
                                    Codigo_Inscripcion, Id_Seccion, Id_Estudiante, Id_Tipo_Inscripcion,
                                    Id_Estado, Fecha_Validacion, Fecha_Retiro, Motivo_Retiro,
                                    Promedio_Acumulado, Total_Faltas, Es_Repetidor, En_Riesgo_Academico,
                                    Id_Usuario_Validador, Activo, Fecha_Creacion, Fecha_Modificacion,
                                    Id_Creador, Id_Modificador, Id_Transaccion
                                ) VALUES (
                                    @Codigo_Inscripcion, @Id_Seccion, @Id_Estudiante, @Id_Tipo_Inscripcion,
                                    @Id_Estado, @Fecha_Validacion, @Fecha_Retiro, @Motivo_Retiro,
                                    @Promedio_Acumulado, ISNULL(@Total_Faltas, 0), ISNULL(@Es_Repetidor, 0),
                                    ISNULL(@En_Riesgo_Academico, 0), @Id_Usuario_Validador, ISNULL(@Activo, 1),
                                    @Fecha_Creacion, @Fecha_Modificacion, @Id_Creador, @Id_Modificador,
                                    @Id_Transaccion
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
                    ELSE IF (@Promedio_Acumulado IS NOT NULL AND (@Promedio_Acumulado < 0 OR @Promedio_Acumulado > 100))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El promedio acumulado debe estar entre 0 y 100!';
                        END
                    ELSE IF ((@Id_Seccion IS NOT NULL OR @Id_Estudiante IS NOT NULL) AND
                             EXISTS(SELECT 1 FROM tbl_inscripciones(NOLOCK) 
                                    WHERE Id_Seccion = COALESCE(@Id_Seccion, (SELECT Id_Seccion FROM tbl_inscripciones WHERE Id_Inscripcion = @Id_Inscripcion))
                                      AND Id_Estudiante = COALESCE(@Id_Estudiante, (SELECT Id_Estudiante FROM tbl_inscripciones WHERE Id_Inscripcion = @Id_Inscripcion))
                                      AND Id_Inscripcion <> @Id_Inscripcion
                                      AND Activo = 1))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe otra inscripción activa para ese estudiante en esa sección!';
                        END
                    ELSE IF (@Codigo_Inscripcion IS NOT NULL AND EXISTS(SELECT 1 FROM tbl_inscripciones(NOLOCK) WHERE Codigo_Inscripcion = @Codigo_Inscripcion AND Id_Inscripcion <> @Id_Inscripcion))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe otra inscripción con ese código!';
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
                                    Id_Seccion = COALESCE(@Id_Seccion, Id_Seccion),
                                    Id_Estudiante = COALESCE(@Id_Estudiante, Id_Estudiante),
                                    Id_Tipo_Inscripcion = COALESCE(@Id_Tipo_Inscripcion, Id_Tipo_Inscripcion),
                                    Id_Estado = COALESCE(@Id_Estado, Id_Estado),
                                    Fecha_Validacion = COALESCE(@Fecha_Validacion, Fecha_Validacion),
                                    Fecha_Retiro = COALESCE(@Fecha_Retiro, Fecha_Retiro),
                                    Motivo_Retiro = COALESCE(@Motivo_Retiro, Motivo_Retiro),
                                    Promedio_Acumulado = COALESCE(@Promedio_Acumulado, Promedio_Acumulado),
                                    Total_Faltas = COALESCE(@Total_Faltas, Total_Faltas),
                                    Es_Repetidor = COALESCE(@Es_Repetidor, Es_Repetidor),
                                    En_Riesgo_Academico = COALESCE(@En_Riesgo_Academico, En_Riesgo_Academico),
                                    Id_Usuario_Validador = COALESCE(@Id_Usuario_Validador, Id_Usuario_Validador),
                                    Activo = COALESCE(@Activo, Activo),
                                    Fecha_Modificacion = @Fecha_Modificacion,
                                    Id_Modificador = @Id_Modificador,
                                    Id_Transaccion = @Id_Transaccion
                                WHERE Id_Inscripcion = @Id_Inscripcion;

                                COMMIT TRAN trx_ActualizarInscripcion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Inscripción actualizada exitosamente!';

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

