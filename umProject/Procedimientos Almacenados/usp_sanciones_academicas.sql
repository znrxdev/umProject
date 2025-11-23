USE umDb
GO

/*
tbl_sanciones_academicas
*/

CREATE OR ALTER PROC usp_sanciones_academicas
(
    @Id_Sancion INT = NULL,
    @Codigo_Sancion VARCHAR(30) = NULL,
    @Id_Estudiante INT = NULL,
    @Id_Tipo_Sancion INT = NULL,
    @Id_Tipo_Falta INT = NULL,
    @Id_Severidad INT = NULL,
    @Id_Estado INT = NULL,
    @Fecha_Registro DATETIME = NULL,
    @Fecha_Fin DATETIME = NULL,
    @Motivo NVARCHAR(300) = NULL,
    @Es_Apelable BIT = NULL,
    @Fecha_Apelacion DATETIME = NULL,
    @Resultado_Apelacion NVARCHAR(200) = NULL,
    @Observaciones_Apelacion NVARCHAR(500) = NULL,
    @Documento_Resolucion NVARCHAR(255) = NULL,
    @Id_Usuario_Resolucion INT = NULL,
    @Fecha_Resolucion DATETIME = NULL,
    @Id_Sancion_Origen INT = NULL,
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
            /* FILTRAR POR ID SANCION */
            IF(@Id_Tipo_Transaccion = 88)
                BEGIN
                    IF ISNULL(@Id_Sancion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de sanción!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_sanciones_academicas(NOLOCK) WHERE Id_Sancion = @Id_Sancion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La sanción no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Sancion, Codigo_Sancion, Id_Estudiante, Id_Tipo_Sancion,
                                    Id_Tipo_Falta, Id_Severidad, Id_Estado, Fecha_Registro,
                                    Fecha_Fin, Motivo, Es_Apelable, Fecha_Apelacion,
                                    Resultado_Apelacion, Observaciones_Apelacion, Documento_Resolucion,
                                    Id_Usuario_Resolucion, Fecha_Resolucion, Id_Sancion_Origen,
                                    Hash_Sancion, Codigo_Control, Fecha_Creacion, Fecha_Modificacion,
                                    Id_Creador, Id_Modificador, Id_Transaccion, RowVersion
                                FROM tbl_sanciones_academicas(NOLOCK)
                                WHERE Id_Sancion = @Id_Sancion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Sanción encontrada!';
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
            ELSE IF(@Id_Tipo_Transaccion = 89)
                BEGIN
                    IF ISNULL(@Id_Estudiante, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado el estudiante para listar sus sanciones!';
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
                                    Id_Sancion, Codigo_Sancion, Id_Estudiante, Id_Tipo_Sancion,
                                    Id_Tipo_Falta, Id_Severidad, Id_Estado, Fecha_Registro,
                                    Fecha_Fin, Motivo, Es_Apelable, Fecha_Apelacion,
                                    Resultado_Apelacion, Observaciones_Apelacion, Documento_Resolucion,
                                    Id_Usuario_Resolucion, Fecha_Resolucion, Id_Sancion_Origen,
                                    Hash_Sancion, Codigo_Control, Fecha_Creacion, Fecha_Modificacion,
                                    Id_Creador, Id_Modificador, Id_Transaccion, RowVersion
                                FROM tbl_sanciones_academicas(NOLOCK)
                                WHERE Id_Estudiante = @Id_Estudiante
                                ORDER BY Fecha_Registro DESC;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Sanciones filtradas por estudiante!';
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
            /* FILTRAR POR ID ESTUDIANTE Y ESTADO */
            ELSE IF(@Id_Tipo_Transaccion = 135)
                BEGIN
                    IF ISNULL(@Id_Estudiante, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado el estudiante para listar sus sanciones!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_usuarios(NOLOCK) WHERE Id_Usuario = @Id_Estudiante)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El estudiante no existe!';
                        END
                    ELSE IF ISNULL(@Id_Estado, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado un estado para filtrar las sanciones!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Sancion, Codigo_Sancion, Id_Estudiante, Id_Tipo_Sancion,
                                    Id_Tipo_Falta, Id_Severidad, Id_Estado, Fecha_Registro,
                                    Fecha_Fin, Motivo, Es_Apelable, Fecha_Apelacion,
                                    Resultado_Apelacion, Observaciones_Apelacion, Documento_Resolucion,
                                    Id_Usuario_Resolucion, Fecha_Resolucion, Id_Sancion_Origen,
                                    Hash_Sancion, Codigo_Control, Fecha_Creacion, Fecha_Modificacion,
                                    Id_Creador, Id_Modificador, Id_Transaccion, RowVersion
                                FROM tbl_sanciones_academicas(NOLOCK)
                                WHERE Id_Estudiante = @Id_Estudiante
                                AND Id_Estado = @Id_Estado
                                ORDER BY Fecha_Registro DESC;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Sanciones filtradas por estudiante y estado!';
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
            /* OBTENER MIS SANCIONES ACADÉMICAS (usando Id_Sesion) */
            ELSE IF(@Id_Tipo_Transaccion = 136)
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
                                    Id_Sancion, Codigo_Sancion, Id_Estudiante, Id_Tipo_Sancion,
                                    Id_Tipo_Falta, Id_Severidad, Id_Estado, Fecha_Registro,
                                    Fecha_Fin, Motivo, Es_Apelable, Fecha_Apelacion,
                                    Resultado_Apelacion, Observaciones_Apelacion, Documento_Resolucion,
                                    Id_Usuario_Resolucion, Fecha_Resolucion, Id_Sancion_Origen,
                                    Hash_Sancion, Codigo_Control, Fecha_Creacion, Fecha_Modificacion,
                                    Id_Creador, Id_Modificador, Id_Transaccion, RowVersion
                                FROM tbl_sanciones_academicas(NOLOCK)
                                WHERE Id_Estudiante = @Id_Sesion
                                ORDER BY Fecha_Registro DESC;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Sanciones académicas obtenidas exitosamente!';
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
            /* APELAR SANCIÓN ACADÉMICA (solo para estudiantes, actualiza Fecha_Apelacion y Observaciones_Apelacion) */
            ELSE IF(@Id_Tipo_Transaccion = 139)
                BEGIN
                    IF ISNULL(@Id_Sancion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de sanción!';
                        END
                    ELSE IF ISNULL(@Id_Sesion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No hay sesión activa!';
                        END
                    ELSE IF ISNULL(@Observaciones_Apelacion, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe ingresar los comentarios de apelación!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_sanciones_academicas(NOLOCK) WHERE Id_Sancion = @Id_Sancion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La sanción no existe!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_sanciones_academicas(NOLOCK) WHERE Id_Sancion = @Id_Sancion AND Id_Estudiante = @Id_Sesion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No tiene permiso para apelar esta sanción!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_sanciones_academicas(NOLOCK) WHERE Id_Sancion = @Id_Sancion AND Id_Estado = 1)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Solo se pueden apelar sanciones activas!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_sanciones_academicas(NOLOCK) WHERE Id_Sancion = @Id_Sancion AND Es_Apelable = 1)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Esta sanción no es apelable!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM tbl_sanciones_academicas(NOLOCK) WHERE Id_Sancion = @Id_Sancion AND Fecha_Apelacion IS NOT NULL)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Esta sanción ya fue apelada anteriormente!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('APELANDO SANCIÓN ACADÉMICA ID: ', @Id_Sancion);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_ApelarSancion
                            BEGIN TRY
                                UPDATE tbl_sanciones_academicas
                                SET Fecha_Apelacion = GETDATE(),
                                    Observaciones_Apelacion = @Observaciones_Apelacion,
                                    Id_Estado = 4, -- Cambiar a EN REVISION cuando se apela
                                    Fecha_Modificacion = @Fecha_Modificacion,
                                    Id_Modificador = @Id_Modificador,
                                    Id_Transaccion = @Id_Transaccion
                                WHERE Id_Sancion = @Id_Sancion
                                AND Id_Estudiante = @Id_Sesion
                                AND Id_Estado = 1
                                AND Es_Apelable = 1
                                AND Fecha_Apelacion IS NULL;

                                IF @@ROWCOUNT = 0
                                    BEGIN
                                        SET @o_Num = -1;
                                        SET @o_Msg = '¡No se pudo procesar la apelación. Verifique que la sanción cumpla con todos los requisitos!';
                                        ROLLBACK TRAN trx_ApelarSancion;
                                    END
                                ELSE
                                    BEGIN
                                        COMMIT TRAN trx_ApelarSancion;

                                        SET @o_Num = 0;
                                        SET @o_Msg = '¡Apelación registrada exitosamente!';

                                        EXEC sp_transacciones
                                        @Modo = 'UPD',
                                        @Id_Transaccion = @Id_Transaccion;
                                    END
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_ApelarSancion;
                                SET @o_Num = -1;
                                SET @o_Msg = '¡Error interno del servidor al procesar la apelación!';
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
            /* AGREGAR SANCION */
            ELSE IF(@Id_Tipo_Transaccion = 87)
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
                    ELSE IF ISNULL(@Id_Tipo_Sancion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un tipo de sanción!';
                        END
                    ELSE IF ISNULL(@Id_Severidad, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar una severidad!';
                        END
                    ELSE IF ISNULL(@Id_Estado, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un estado a la sanción!';
                        END
                    ELSE IF ISNULL(@Fecha_Registro, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe especificar la fecha de registro!';
                        END
                    ELSE IF (@Fecha_Fin IS NOT NULL AND @Fecha_Fin < @Fecha_Registro)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de fin no puede ser anterior a la fecha de registro!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM tbl_sanciones_academicas(NOLOCK) WHERE Codigo_Sancion = @Codigo_Sancion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe una sanción con ese código!';
                        END
                    ELSE
                        BEGIN
                            -- Generar código de sanción único si no se proporciona
                            IF ISNULL(@Codigo_Sancion, '') = ''
                                BEGIN
                                    DECLARE @Prefijo VARCHAR(10) = 'SAN-';
                                    DECLARE @Anio VARCHAR(4) = CAST(YEAR(GETDATE()) AS VARCHAR);
                                    DECLARE @Contador INT;
                                    SELECT @Contador = ISNULL(MAX(CAST(SUBSTRING(Codigo_Sancion, LEN(@Prefijo + @Anio + '-') + 1, LEN(Codigo_Sancion)) AS INT)), 0) + 1
                                    FROM tbl_sanciones_academicas(NOLOCK)
                                    WHERE Codigo_Sancion LIKE @Prefijo + @Anio + '-%';
                                    SET @Codigo_Sancion = @Prefijo + @Anio + '-' + RIGHT('000000' + CAST(@Contador AS VARCHAR), 6);
                                END

                            SET @iConcepto = CONCAT('AGREGANDO SANCIÓN ACADÉMICA: ', @Codigo_Sancion);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_AgregarSancion
                            BEGIN TRY
                                INSERT INTO tbl_sanciones_academicas(
                                    Codigo_Sancion, Id_Estudiante, Id_Tipo_Sancion, Id_Tipo_Falta,
                                    Id_Severidad, Id_Estado, Fecha_Registro, Fecha_Fin, Motivo,
                                    Es_Apelable, Fecha_Apelacion, Resultado_Apelacion,
                                    Observaciones_Apelacion, Documento_Resolucion, Id_Usuario_Resolucion,
                                    Fecha_Resolucion, Id_Sancion_Origen, Fecha_Creacion, Fecha_Modificacion,
                                    Id_Creador, Id_Modificador, Id_Transaccion
                                ) VALUES (
                                    @Codigo_Sancion, @Id_Estudiante, @Id_Tipo_Sancion, @Id_Tipo_Falta,
                                    @Id_Severidad, @Id_Estado, @Fecha_Registro, @Fecha_Fin, @Motivo,
                                    ISNULL(@Es_Apelable, 0), @Fecha_Apelacion, @Resultado_Apelacion,
                                    @Observaciones_Apelacion, @Documento_Resolucion, @Id_Usuario_Resolucion,
                                    @Fecha_Resolucion, @Id_Sancion_Origen, @Fecha_Creacion, @Fecha_Modificacion,
                                    @Id_Creador, @Id_Modificador, @Id_Transaccion
                                );

                                COMMIT TRAN trx_AgregarSancion;

                                SET @o_Num = SCOPE_IDENTITY();
                                SET @o_Msg = '¡Sanción académica agregada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_AgregarSancion;

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
            /* ACTUALIZAR SANCION */
            ELSE IF(@Id_Tipo_Transaccion = 90)
                BEGIN
                    IF ISNULL(@Id_Sancion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de sanción!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_sanciones_academicas(NOLOCK) WHERE Id_Sancion = @Id_Sancion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La sanción no existe!';
                        END
                    ELSE IF (@Fecha_Fin IS NOT NULL AND @Fecha_Registro IS NOT NULL AND @Fecha_Fin < @Fecha_Registro)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de fin no puede ser anterior a la fecha de registro!';
                        END
                    ELSE IF (@Codigo_Sancion IS NOT NULL AND EXISTS(SELECT 1 FROM tbl_sanciones_academicas(NOLOCK) WHERE Codigo_Sancion = @Codigo_Sancion AND Id_Sancion <> @Id_Sancion))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe otra sanción con ese código!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('ACTUALIZANDO SANCIÓN ACADÉMICA ID: ', @Id_Sancion);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_ActualizarSancion
                            BEGIN TRY
                                UPDATE tbl_sanciones_academicas
                                SET Codigo_Sancion = COALESCE(@Codigo_Sancion, Codigo_Sancion),
                                    Id_Estudiante = COALESCE(@Id_Estudiante, Id_Estudiante),
                                    Id_Tipo_Sancion = COALESCE(@Id_Tipo_Sancion, Id_Tipo_Sancion),
                                    Id_Tipo_Falta = COALESCE(@Id_Tipo_Falta, Id_Tipo_Falta),
                                    Id_Severidad = COALESCE(@Id_Severidad, Id_Severidad),
                                    Id_Estado = COALESCE(@Id_Estado, Id_Estado),
                                    Fecha_Registro = COALESCE(@Fecha_Registro, Fecha_Registro),
                                    Fecha_Fin = COALESCE(@Fecha_Fin, Fecha_Fin),
                                    Motivo = COALESCE(@Motivo, Motivo),
                                    Es_Apelable = COALESCE(@Es_Apelable, Es_Apelable),
                                    Fecha_Apelacion = COALESCE(@Fecha_Apelacion, Fecha_Apelacion),
                                    Resultado_Apelacion = COALESCE(@Resultado_Apelacion, Resultado_Apelacion),
                                    Observaciones_Apelacion = COALESCE(@Observaciones_Apelacion, Observaciones_Apelacion),
                                    Documento_Resolucion = COALESCE(@Documento_Resolucion, Documento_Resolucion),
                                    Id_Usuario_Resolucion = COALESCE(@Id_Usuario_Resolucion, Id_Usuario_Resolucion),
                                    Fecha_Resolucion = COALESCE(@Fecha_Resolucion, Fecha_Resolucion),
                                    Id_Sancion_Origen = COALESCE(@Id_Sancion_Origen, Id_Sancion_Origen),
                                    Fecha_Modificacion = @Fecha_Modificacion,
                                    Id_Modificador = @Id_Modificador,
                                    Id_Transaccion = @Id_Transaccion
                                WHERE Id_Sancion = @Id_Sancion;

                                COMMIT TRAN trx_ActualizarSancion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Sanción académica actualizada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_ActualizarSancion;

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
            /* LISTAR ESTUDIANTES CON SANCIONES */
            ELSE IF(@Id_Tipo_Transaccion = 134)
                BEGIN
                    BEGIN TRY
                        SELECT DISTINCT
                            U.Id_Usuario,
                            U.Id_Persona,
                            U.Usuario,
                            P.Valor_Documento,
                            P.Primer_Nombre + ' ' + ISNULL(P.Segundo_Nombre, '') + ' ' + P.Primer_Apellido + ' ' + ISNULL(P.Segundo_Apellido, '') AS Nombre_Completo,
                            COUNT(SA.Id_Sancion) AS Total_Sanciones
                        FROM tbl_sanciones_academicas SA(NOLOCK)
                        INNER JOIN tbl_usuarios U(NOLOCK) ON SA.Id_Estudiante = U.Id_Usuario
                        INNER JOIN tbl_personas P(NOLOCK) ON U.Id_Persona = P.Id_Persona
                        INNER JOIN cls_usuarios_roles UR(NOLOCK) ON U.Id_Usuario = UR.Id_Usuario AND UR.Activo = 1
                        WHERE UR.Id_Rol = 2
                        AND U.Id_Estado = 1
                        GROUP BY U.Id_Usuario, U.Id_Persona, U.Usuario, P.Valor_Documento, P.Primer_Nombre, P.Segundo_Nombre, P.Primer_Apellido, P.Segundo_Apellido
                        ORDER BY Nombre_Completo ASC;

                        SET @o_Num = 0;
                        SET @o_Msg = '¡Estudiantes con sanciones listados exitosamente!';
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
            /* OBTENER SANCIONES EN ESPERA DE APELACIÓN */
            ELSE IF(@Id_Tipo_Transaccion = 140)
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
                                    Id_Sancion, Codigo_Sancion, Id_Estudiante, Id_Tipo_Sancion,
                                    Id_Tipo_Falta, Id_Severidad, Id_Estado, Fecha_Registro,
                                    Fecha_Fin, Motivo, Es_Apelable, Fecha_Apelacion,
                                    Resultado_Apelacion, Observaciones_Apelacion, Documento_Resolucion,
                                    Id_Usuario_Resolucion, Fecha_Resolucion, Id_Sancion_Origen,
                                    Hash_Sancion, Codigo_Control, Fecha_Creacion, Fecha_Modificacion,
                                    Id_Creador, Id_Modificador, Id_Transaccion, RowVersion
                                FROM tbl_sanciones_academicas(NOLOCK)
                                WHERE Fecha_Apelacion IS NOT NULL
                                AND Id_Estado = 4 -- EN REVISION
                                AND Resultado_Apelacion IS NULL -- Aún no se ha respondido
                                ORDER BY Fecha_Apelacion ASC;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Sanciones en espera de apelación obtenidas exitosamente!';
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
            /* RESPONDER APELACIÓN (aprobar o rechazar) */
            ELSE IF(@Id_Tipo_Transaccion = 141)
                BEGIN
                    IF ISNULL(@Id_Sancion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de sanción!';
                        END
                    ELSE IF ISNULL(@Id_Sesion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No hay sesión activa!';
                        END
                    ELSE IF ISNULL(@Id_Estado, 0) = 0 OR (@Id_Estado <> 5 AND @Id_Estado <> 6)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un estado válido (APROBADA o RECHAZADA)!';
                        END
                    ELSE IF ISNULL(@Resultado_Apelacion, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe ingresar el resultado de la apelación!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_sanciones_academicas(NOLOCK) WHERE Id_Sancion = @Id_Sancion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La sanción no existe!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_sanciones_academicas(NOLOCK) WHERE Id_Sancion = @Id_Sancion AND Fecha_Apelacion IS NOT NULL)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Esta sanción no tiene una apelación registrada!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_sanciones_academicas(NOLOCK) WHERE Id_Sancion = @Id_Sancion AND Id_Estado = 4)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Esta sanción no está en estado de revisión!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM tbl_sanciones_academicas(NOLOCK) WHERE Id_Sancion = @Id_Sancion AND Resultado_Apelacion IS NOT NULL)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Esta apelación ya fue respondida anteriormente!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('RESPONDIENDO APELACIÓN DE SANCIÓN ACADÉMICA ID: ', @Id_Sancion);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_ResponderApelacion
                            BEGIN TRY
                                UPDATE tbl_sanciones_academicas
                                SET Id_Estado = @Id_Estado, -- 5 = APROBADA, 6 = RECHAZADA
                                    Resultado_Apelacion = @Resultado_Apelacion,
                                    Id_Usuario_Resolucion = @Id_Sesion,
                                    Fecha_Resolucion = GETDATE(),
                                    Fecha_Modificacion = @Fecha_Modificacion,
                                    Id_Modificador = @Id_Modificador,
                                    Id_Transaccion = @Id_Transaccion
                                WHERE Id_Sancion = @Id_Sancion
                                AND Fecha_Apelacion IS NOT NULL
                                AND Id_Estado = 4
                                AND Resultado_Apelacion IS NULL;

                                IF @@ROWCOUNT = 0
                                    BEGIN
                                        SET @o_Num = -1;
                                        SET @o_Msg = '¡No se pudo procesar la respuesta de la apelación. Verifique que la sanción cumpla con todos los requisitos!';
                                        ROLLBACK TRAN trx_ResponderApelacion;
                                    END
                                ELSE
                                    BEGIN
                                        COMMIT TRAN trx_ResponderApelacion;

                                        SET @o_Num = 0;
                                        SET @o_Msg = '¡Respuesta de apelación registrada exitosamente!';

                                        EXEC sp_transacciones
                                        @Modo = 'UPD',
                                        @Id_Transaccion = @Id_Transaccion;
                                    END
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_ResponderApelacion;
                                SET @o_Num = -1;
                                SET @o_Msg = '¡Error interno del servidor al procesar la respuesta de la apelación!';
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

