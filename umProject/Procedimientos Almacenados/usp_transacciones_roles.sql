USE umDb
GO

/*
cls_transacciones_roles
*/

CREATE OR ALTER PROC [dbo].[usp_transacciones_roles]
(
@Id_Transaccion_Rol INT = NULL,
@Id_Tipo_Transaccion INT = NULL,
@Id_Rol INT = NULL,
@Fecha_Creacion DATETIME = NULL,
@Fecha_Modificacion DATETIME = NULL,
@Id_Creador INT = NULL,
@Id_Modificador INT = NULL,
@Activo BIT = NULL,
@Tipo_Transaccion INT,
@Id_Transaccion INT = NULL,
@Id_Sesion INT = NULL,
@o_Num INT = NULL OUTPUT,
@o_Msg NVARCHAR(255) = NULL OUTPUT
)
AS
BEGIN
    DECLARE @Permiso INT, @iConcepto VARCHAR(255),@Linea_Error INT, @Numero_Error INT, @Mensaje_Error NVARCHAR(255), @Origen_Error NVARCHAR(50) = ERROR_PROCEDURE();
    SET @Fecha_Creacion = GETDATE();
	SET @Fecha_Modificacion = GETDATE();
	SET @Id_Creador = @Id_Sesion;
	SET @Id_Modificador = @Id_Sesion;

    SET @Permiso = dbo.fn_Validar_Permisos(@Id_Sesion, @Tipo_Transaccion);

	IF (@Permiso = 1)
		BEGIN
			/* LISTAR TRANSACCIONES ROLES */
			IF (@Tipo_Transaccion = 42)
				BEGIN
					BEGIN TRY
						SELECT TOP 10
							Id_Transaccion_Rol,
							Id_Tipo_Transaccion,
							Id_Rol,
							Fecha_Creacion,
							Fecha_Modificacion,
							Id_Creador,
							Id_Modificador,
							Id_Transaccion,
							Activo
						FROM cls_transacciones_roles (NOLOCK)
						WHERE Activo = 1

						SET @o_Num = 0;
						SET @o_Msg = '¡Transacciones roles encontrados!';
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
			/* FILTRAR TRANSACCIONES ROLES ID */
			ELSE IF (@Tipo_Transaccion = 41)
				BEGIN
					IF ISNULL(@Id_Transaccion_Rol, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe seleccionar una transaccion rol a filtrar!';
						END
					ELSE IF NOT EXISTS(SELECT 1 FROM cls_transacciones_roles(NOLOCK) WHERE Id_Transaccion_Rol = @Id_Transaccion_Rol)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Transaccion rol no existe!';
						END
					ELSE	
						BEGIN
							BEGIN TRY
								SELECT
									Id_Transaccion_Rol,
									Id_Tipo_Transaccion,
									Id_Rol,
									Fecha_Creacion,
									Fecha_Modificacion,
									Id_Creador,
									Id_Modificador,
									Id_Transaccion,
									Activo
								FROM cls_transacciones_roles (NOLOCK)
								WHERE Id_Transaccion_Rol = @Id_Transaccion_Rol
								AND Activo = 1
								SET @o_Num = 0;
								SET @o_Msg = '¡Transaccion rol encontrado!';
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
			/* AGREGAR TRANSACCIONES ROLES */
			ELSE IF(@Tipo_Transaccion = 39)
				BEGIN
					IF ISNULL(@Id_Tipo_Transaccion, 0) = 0
						BEGIN
							SET @o_NUm = -1;
							SET @o_Msg = '¡No se ha seleccionado el tipo de transacción!';
						END
					ELSE IF ISNULL(@Id_Rol, 0) = 0
						BEGIN
							SET @o_NUm = -1;
							SET @o_Msg = '¡No se ha seleccionado el rol!';
						END
					ELSE IF (@Activo) IS NULL
						BEGIN
							SET @o_NUm = -1;
							SET @o_Msg = '¡Activo no ha sido enviado!';
						END
					ELSE IF EXISTS(SELECT 1 FROM cls_transacciones_roles WITH (NOLOCK) WHERE Id_Tipo_Transaccion = @Id_Tipo_Transaccion AND Id_Rol = @Id_Rol)
						BEGIN
							SET @o_NUm = -1;
							SET @o_Msg = '¡Ese rol ya tiene acceso a ese tipo de transacción!';
						END
					ELSE
						BEGIN
							 SET @iConcepto = 'AGREGANDO ACCESO DE ROL A TIPO DE TRANSACCION'
							 EXEC sp_transacciones
							 @Modo = 'INS',
							 @Id_Tipo_Transaccion = @Tipo_Transaccion,
							 @Id_Autor = @Id_Sesion,
							 @Concepto = @iConcepto,
							 @o_Num = @Id_Transaccion OUTPUT
							 BEGIN TRAN trx_AgregarTransaccionRol
							 BEGIN TRY
								INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo)
								VALUES (@Id_Tipo_Transaccion, @Id_Rol, @Fecha_Creacion, @Fecha_Modificacion, @Id_Creador, @Id_Modificador, @Id_Transaccion, @Activo)

								SET @o_Num = SCOPE_IDENTITY();
								SET @o_Msg = '¡Se ha registrado correctamente!';
								COMMIT TRAN trx_AgregarTransaccionRol

								EXEC sp_transacciones
								@Modo = 'UPD',
								@Id_Transaccion = @Id_Transaccion
							 END TRY
							 BEGIN CATCH
								ROLLBACK TRAN trx_AgregarTransaccionRol
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
								@Id_Transaccion = @Id_Transaccion
							 END CATCH
						END
				END
			/* ACTUALZIAR TRANSACCIONES ROLES */
			ELSE IF(@Tipo_Transaccion = 40)
				BEGIN
					IF ISNULL(@Id_Transaccion_Rol, 0) = 0
						BEGIN
							SET @o_NUm = -1;
							SET @o_Msg = '¡No se ha seleccionado el registro a actualizar!';
						END
					ELSE
						BEGIN
							 SET @iConcepto = 'ACTUALIZANDO REGISTRO'
							 EXEC sp_transacciones
							 @Modo = 'INS',
							 @Id_Tipo_Transaccion = @Tipo_Transaccion,
							 @Id_Autor = @Id_Sesion,
							 @Concepto = @iConcepto,
							 @o_Num = @Id_Transaccion OUTPUT
							 BEGIN TRAN trx_ActualizarTransaccionRol
							 BEGIN TRY
								UPDATE cls_transacciones_roles
								SET Activo = COALESCE(@Activo, Activo),
									Id_Transaccion = COALESCE(@Id_Transaccion, Id_Transaccion),
									Fecha_Modificacion = COALESCE(@Fecha_Modificacion, Fecha_Modificacion),
									Id_Modificador = COALESCE(@Id_Modificador, Id_Modificador)
								WHERE Id_Transaccion_Rol = @Id_Transaccion_Rol

								SET @o_Num = 0;
								SET @o_Msg = '¡Se ha actualizado correctamente!';
								COMMIT TRAN trx_ActualizarTransaccionRol

								EXEC sp_transacciones
								@Modo = 'UPD',
								@Id_Transaccion = @Id_Transaccion
							 END TRY
							 BEGIN CATCH
								ROLLBACK TRAN trx_ActualizarTransaccionRol
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
								@Id_Transaccion = @Id_Transaccion
							 END CATCH
						END
				END
			ELSE
				BEGIN
					SET @o_Num = -1;
					SET @o_Msg = '¡Tipo de transacción no válido!';
				END
		END
	ELSE
		BEGIN
		   SET @o_Num = -1;
		   SET @o_Msg = '¡Su usuario no tiene permiso para realizar este tipo de transacción!';
		END
END