USE umDb
GO
/* 
sp_transacciones
tbl_transacciones
*/
CREATE OR ALTER PROC sp_transacciones
(
@Id_Transaccion INT = NULL,
@Id_Tipo_Transaccion INT = NULL,
@Concepto NVARCHAR(100) = NULL,
@Id_Persona INT = NULL,
@Id_Usuario INT = NULL,
@Id_Contacto INT = NULL,
@Id_Evaluacion INT = NULL,
@Id_Solicitud_Beca INT = NULL,
@Id_Inscripcion INT = NULL,
@Id_Autor INT = NULL,
@Completado BIT = NULL,
@Fecha_Creacion DATETIME = NULL,
@Modo CHAR(5) = NULL,
@o_Num INT = NULL OUTPUT,
@o_Msg NVARCHAR(255) = NULL OUTPUT
)
AS
BEGIN
	DECLARE @Linea_Error INT, @Numero_Error INT, @Mensaje_Error NVARCHAR(255), @Origen_Error NVARCHAR(50) = ERROR_PROCEDURE();
	SET @Fecha_Creacion = GETDATE();
	IF(@Modo = 'INS')
		BEGIN
			IF ISNULL(@Id_Autor, 0) = 0
				BEGIN
					SET @o_Num = -1;
					SET @o_Msg = '¡Debe mandar al usuario creador de la transacción!';
				END
			ELSE IF ISNULL(@Id_Tipo_Transaccion, 0) = 0
				BEGIN
					SET @o_Num = -1;
					SET @o_Msg = '¡Debe mandar el tipo de transacción!';
				END
			ELSE IF ISNULL(@Concepto, '') = ''
				BEGIN
					SET @o_Num = -1;
					SET @o_Msg = '¡Debe digitar la referencia!';
				END
			ELSE
				BEGIN
					BEGIN TRAN trx_AgregarTransaccion
					BEGIN TRY
						SET @Completado = ISNULL(@Completado, 0)
						INSERT INTO tbl_transacciones(Id_Tipo_Transaccion, Concepto, Id_Persona, Id_Usuario, [Id_Contacto], [Id_Evaluacion], [Id_Solicitud_Beca],[Id_Inscripcion],Id_Autor,Fecha_Creacion, Completado)
						VALUES	(@Id_Tipo_Transaccion, @Concepto, @Id_Persona, @Id_Usuario, @Id_Contacto, @Id_Evaluacion,@Id_Solicitud_Beca,@Id_Inscripcion,@Id_Autor, @Fecha_Creacion, @Completado)
						COMMIT TRAN trx_AgregarTransaccion

						SET @Id_Transaccion = SCOPE_IDENTITY();
						SET @o_Num = @Id_Transaccion
						SET @o_Msg = '¡Transaccion registrada!';
					END TRY
					BEGIN CATCH
						ROLLBACK TRAN trx_AgregarTransaccion
						SET @o_Num = -1;
						SET @o_Msg = '¡Error interno del servidor!'

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
	ELSE IF(@Modo = 'UPD')
		BEGIN
			IF ISNULL(@Id_Transaccion, 0) = 0
				BEGIN
					SET @o_Num = -1;
					SET @o_Msg = '¡Debe mandar el ID Transaccion!';
				END
			ELSE
				BEGIN
					BEGIN TRAN trx_ActualizarTransaccion
					BEGIN TRY
						SET @Completado = 1
						UPDATE tbl_transacciones
						SET Id_Persona = COALESCE(@Id_Persona, Id_Persona),
							Id_Usuario = COALESCE(@Id_Usuario, Id_Usuario),
							Id_Contacto = COALESCE(@Id_Contacto, Id_Contacto),
							Id_Evaluacion = COALESCE(@Id_Evaluacion, Id_Evaluacion),
							Id_Solicitud_Beca = COALESCE(@Id_Solicitud_Beca, Id_Solicitud_Beca),
							Id_Inscripcion = COALESCE(@Id_Inscripcion, Id_Inscripcion),
							Completado = COALESCE(@Completado, Completado)
						WHERE Id_Transaccion = @Id_Transaccion

						COMMIT TRAN trx_ActualizarTransaccion
						SET @o_Num = 0;
						SET @o_Msg = '¡Actualización realizada!';
					END TRY
					BEGIN CATCH
						ROLLBACK TRAN trx_ActualizarTransaccion
						SET @o_Num = -1;
						SET @o_Msg = '¡Error interno del servidor!'

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
	ELSE IF (@Modo = 'RBK')
		BEGIN
			IF ISNULL(@Id_Transaccion, 0) = 0
				BEGIN
					SET @o_Num = -1;
					SET @o_Msg = '¡Debe mandar el ID Transaccion!';
				END
			ELSE
				BEGIN
					BEGIN TRAN trx_ActualizarTransaccion
					BEGIN TRY
						SET @Completado = 0
						UPDATE tbl_transacciones
						SET Concepto = COALESCE(@Concepto, Concepto),
							Completado = COALESCE(@Completado, Completado)
						WHERE Id_Transaccion = @Id_Transaccion

						COMMIT TRAN trx_ActualizarTransaccion
						SET @o_Num = 0;
						SET @o_Msg = '¡Rollback realizado!';
					END TRY
					BEGIN CATCH
						ROLLBACK TRAN trx_ActualizarTransaccion
						SET @o_Num = -1;
						SET @o_Msg = '¡Error interno del servidor!'

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
	ELSE
		BEGIN
			SET @o_Num = -1;
			SET @o_Msg = '¡Modo invalido!';
		END
END