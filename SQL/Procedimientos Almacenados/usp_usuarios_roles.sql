USE umDb
GO
/*

cls_usuarios_roles
*/


CREATE OR ALTER PROC usp_usuarios_roles
(
@Id_Usuario_Rol INT = NULL,
@Id_Usuario INT = NULL,
@Id_Rol INT = NULL,
@Fecha_Creacion DATETIME = NULL,
@Fecha_Modificacion DATETIME = NULL,
@Activo BIT = NULL,
@Id_Tipo_Transaccion INT,
@Id_Sesion INT = NULL,
@Id_Creador INT = NULL,
@Id_Modificador INT = NULL,
@Id_Transaccion INT = NULL,
@o_Num INT = NULL OUTPUT,
@o_Msg NVARCHAR(255) = NULL OUTPUT
)
AS
BEGIN
    DECLARE @Permiso INT, @iConcepto NVARCHAR(255), @Linea_Error INT, @Numero_Error INT, @Mensaje_Error NVARCHAR(255), @Origen_Error NVARCHAR(50) = ERROR_PROCEDURE();
    SET @Fecha_Creacion = GETDATE();
	SET @Fecha_Modificacion = GETDATE();
	SET @Id_Modificador = @Id_Sesion;
	SET @Id_Creador = @Id_Sesion;
    SET @Permiso = dbo.fn_Validar_Permisos(@Id_Sesion, @Id_Tipo_Transaccion);

	IF (@Permiso = 1)
		BEGIN
			/* AGREGAR USUARIOS ROLES */
			IF (@Id_Tipo_Transaccion = 47)
				BEGIN
					IF ISNULL(@Id_Usuario, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe seleccionar al usuario!';
						END
					ELSE IF ISNULL(@Id_Rol, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe asignar un Rol!';
						END
					ELSE IF(@Activo) IS NULL
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe seleccionar el activo!';
						END
					IF EXISTS(SELECT 1 FROM cls_usuarios_roles (NOLOCK) WHERE Id_Usuario = @Id_Usuario AND Id_Rol = @Id_Rol)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡El usuario ya posee ese rol!';
						END
					ELSE
						BEGIN
							DECLARE @roltmp NVARCHAR(255)d
							SELECT @roltmp = Nombre_Rol FROM cls_roles(NOLOCK) WHERE Id_Rol = @Id_Rol
							SET @iConcepto = CONCAT('ASIGNANDO ROL: ', @roltmp)
							EXEC sp_transacciones
							@Modo = 'INS',
							@Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
							@Concepto = @iConcepto,
							@Id_Autor = @Id_Sesion,
							@Id_Usuario = @Id_Usuario,
							@o_Num = @Id_Transaccion OUTPUT
							BEGIN TRAN trx_AgregarUsuarioRol;
							BEGIN TRY
								INSERT INTO cls_usuarios_roles (Id_Usuario, Id_Rol, Fecha_Creacion, Fecha_Modificacion,Id_Creador,Id_Modificador, Id_Transaccion,Activo) 
								VALUES (@Id_Usuario, @Id_Rol, @Fecha_Creacion, @Fecha_Modificacion, @Id_Creador, @Id_Modificador,@Id_Transaccion,@Activo)


								SET @o_Num = SCOPE_IDENTITY();
								SET @o_Msg = '¡Rol asignado correctamente al usuario!';

								UPDATE dbo.tbl_usuarios
								SET Id_Transaccion = @Id_Transaccion
								WHERE Id_Usuario = @Id_Usuario

								COMMIT TRAN trx_AgregarUsuarioRol;

								EXEC sp_transacciones
								@Modo = 'UPD',
								@Id_Transaccion = @Id_Transaccion,
								@Id_Usuario = @Id_Usuario



							END TRY
							BEGIN CATCH
								ROLLBACK TRAN trx_AgregarUsuarioRol;

								SET @o_Num = -1;
								SET @o_Msg = '¡Error interno del servidor!';
								EXEC sp_transacciones
								@Modo = 'RBK',
								@Id_Transaccion = @Id_Transaccion

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
			/* ACTUALIZAR USUARIOS ROLES */
			ELSE IF (@Id_Tipo_Transaccion = 48)
				BEGIN
					IF ISNULL(@Id_Usuario_Rol, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe seleccionar al usuario rol!';
						END
					ELSE IF ISNULL(@Id_Usuario, 0) = 0	
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe seleccionar al usuario!';
						END
					ELSE IF ISNULL(@Id_Rol, 0) = 0	
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe seleccionar al usuario!';
						END
					ELSE IF EXISTS(SELECT 1 FROM cls_usuarios_roles (NOLOCK) WHERE Id_Usuario = @Id_Usuario AND Id_Rol = @Id_Rol AND Id_Usuario_Rol <> @Id_Usuario_Rol)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡La combinación de usuario rol ya existe!';
						END
					ELSE
						BEGIN
							EXEC sp_transacciones
							@Modo = 'INS',
							@Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
							@Concepto = 'ACTUALIZANDO ROL(es) DE USUARIO',
							@Id_Autor = @Id_Sesion,
							@Id_Usuario = @Id_Usuario,
							@o_Num = @Id_Transaccion OUTPUT
							BEGIN TRAN trx_ActualizarUsuarioRol;
							BEGIN TRY
								UPDATE cls_usuarios_roles
								SET Id_Rol = COALESCE (@Id_Rol, Id_Rol),
									Fecha_Modificacion = COALESCE(@Fecha_Modificacion, Fecha_Modificacion),
									Id_Modificador = COALESCE(@Id_Modificador, Id_Modificador),
									Id_Transaccion = COALESCE(@Id_Transaccion, Id_Transaccion),
									Activo = COALESCE (@Activo, Activo)
								WHERE  Id_Usuario_Rol = @Id_Usuario_Rol;

								SET @o_Num = 0;
								SET @o_Msg = '¡Rol actualizado correctamente para el usuario!';

								UPDATE dbo.tbl_usuarios
								SET Id_Transaccion = @Id_Transaccion
								WHERE Id_Usuario = @Id_Usuario


								COMMIT TRAN trx_ActualizarUsuarioRol
									EXEC sp_transacciones
									@Modo = 'UPD',
									@Id_Transaccion = @Id_Transaccion,
									@Id_Usuario = @Id_Usuario
							END TRY
							BEGIN CATCH
								ROLLBACK TRAN trx_ActualizarUsuarioRol;
								SET @o_Num = -1;
								SET @o_Msg = '¡Error interno del servidor!';
								EXEC sp_transacciones
								@Modo = 'RBK',
								@Id_Transaccion = @Id_Transaccion

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
			/* FILTRAR USUARIOS ROLES ID */
			ELSE IF (@Id_Tipo_Transaccion = 49)
				BEGIN
					BEGIN TRY
							SELECT TOP 30
								Id_Usuario_Rol,
								Id_Usuario,
								Id_Rol,
								Fecha_Creacion,
								Fecha_Modificacion,
								Id_Creador,
								Id_Modificador,
								Id_Transaccion,
								Activo
							FROM cls_usuarios_roles(NOLOCK)
							WHERE Id_Usuario_Rol = @Id_Usuario_Rol

							SET @o_Num = 0;
							SET @o_Msg = 'Roles encontrados correctamente';
					END TRY
					BEGIN CATCH
							SET @o_Num = -1;
							SET @o_Msg = '¡Error interno del servidor!';
					END CATCH
				END
			/* LISTAR ROLES DE USUARIO */
			ELSE IF (@Id_Tipo_Transaccion = 50)
				BEGIN
					BEGIN TRY
						SELECT
							Id_Usuario_Rol,
							Id_Usuario,
							Id_Rol,
							Fecha_Creacion,
							Fecha_Modificacion,
							Id_Creador,
							Id_Modificador,
							Id_Transaccion,
							Activo
						FROM cls_usuarios_roles(NOLOCK)
						WHERE Id_Usuario = @Id_Usuario

						SET @o_Num = 0;
						SET @o_Msg = 'Roles encontrados correctamente';
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
	ELSE
		BEGIN
			SET @o_Num = -1;
			SET @o_Msg = '¡Su usuario no tiene permiso para realizar este tipo de transacción!';
		END
END
