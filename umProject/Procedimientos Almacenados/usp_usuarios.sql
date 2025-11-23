USE umDb
GO
/*
sp_usuarios
*/

CREATE OR ALTER PROCEDURE usp_usuarios
(
@Id_Usuario INT = NULL,
@Id_Persona INT = NULL,
@Id_Token INT = NULL,
@Usuario NVARCHAR(255) = NULL,
@Contrasena VARCHAR(100) = NULL,
@Valor_Documento VARCHAR(50) = NULL,
@Fecha_Creacion DATETIME = NULL,
@Fecha_Modificacion DATETIME = NULL,
@Ultima_Sesion DATETIME = NULL,
@Ultimo_Cambio_Contrasena DATETIME = NULL,
@Id_Creador INT = NULL,
@Id_Modificador INT = NULL,
@Id_Estado INT = NULL,
@Id_Tipo_Transaccion INT,
@Id_Sesion INT = NULL,
@Id_Transaccion INT = NULL,
@o_Msg NVARCHAR(255) = NULL OUTPUT,
@o_Num INT = NULL OUTPUT
)
AS
BEGIN
	DECLARE @Permiso INT, @iConcepto NVARCHAR(255), @msgtmp NVARCHAR(255), @Linea_Error INT, @Numero_Error INT, @Mensaje_Error NVARCHAR(255), @Origen_Error NVARCHAR(50) = ERROR_PROCEDURE();
	SET @Fecha_Creacion = GETDATE();
	SET @Fecha_Modificacion = GETDATE();
	IF(@Id_Tipo_Transaccion <> 19)
		BEGIN
			SET @Permiso = dbo.fn_Validar_Permisos(@Id_Sesion, @Id_Tipo_Transaccion)
		END
	SET @Id_Creador = @Id_Sesion;
	SET @Id_Modificador = @Id_Sesion;
	IF(@Permiso = 1 OR @Id_Tipo_Transaccion = 19)
		BEGIN
			/* INICIAR SESION */
			IF (@Id_Tipo_Transaccion = 19)
				BEGIN
					IF NOT EXISTS (SELECT 1 FROM tbl_usuarios U WITH (NOLOCK) WHERE U.Usuario = @Usuario)
						BEGIN
							SET @o_Num = 0;
							SET @o_Msg = 'Usuario y/o Contrase�a incorrecta.';
						END
					ELSE IF (SELECT u.Id_Estado FROM tbl_usuarios u WITH (NOLOCK) INNER JOIN tbl_personas p WITH (NOLOCK) ON u.Id_Persona = p.Id_Persona WHERE u.Usuario = @Usuario) <> 1
						BEGIN
							SET @o_Num = 0;
							SET @o_Msg = 'La persona relacionada a este usuario no esta activa.';
						END
					ELSE IF (SELECT u.Id_Estado FROM tbl_usuarios u WITH (NOLOCK) WHERE u.Usuario = @Usuario) <> 1
						BEGIN
							SET @o_Num = 0;
							SET @o_Msg = 'Usuario no esta activo.';
						END
					ELSE
						BEGIN
							SELECT 
								U.Id_Usuario,
								U.Usuario,
								U.Id_Persona,
								U.Contrasena
							FROM tbl_usuarios U WITH (NOLOCK)
							WHERE U.Usuario = @Usuario;

							SET @o_Num = 0;
							SET @o_Msg = '�Bienvenido!';
						END

				END

			/* AGREGAR USUARIOS */
			ELSE IF(@Id_Tipo_Transaccion = 20)
				BEGIN
					IF ISNULL(@Id_Persona, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '�Debe seleccionar a una persona!';
						END
					ELSE IF (SELECT Id_Estado FROM tbl_personas(NOLOCK) WHERE Id_Persona = @Id_Persona) NOT IN (1)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '�Esa persona no esta activa!'
						END
					ELSE IF EXISTS(SELECT 1 FROM tbl_usuarios(NOLOCK) WHERE Id_Persona = @Id_Persona)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '�Esa persona ya posee un usuario!'
						END
					ELSE IF ISNULL(@Usuario, '') = ''	
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '�Debe introducir un usuario!';
						END
					ELSE IF EXISTS(SELECT 1 FROM tbl_usuarios(NOLOCK) WHERE Usuario = @Usuario)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '�Ese usuario ya existe!'
						END
					ELSE IF ISNULL(CONVERT(VARCHAR(100),@Contrasena), '') = ''
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '�Debe digitar una contrase�a!';
						END
					ELSE IF ISNULL(@Id_Estado, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '�Debe seleccionar un estado!';
						END
					ELSE
						BEGIN
							SELECT @msgtmp = Valor_Documento FROM tbl_personas(NOLOCK) WHERE Id_Persona = @Id_Persona
							SET @iConcepto = CONCAT('AGREGANDO USUARIO A LA PERSONA CON DOCUMENTO: ', @msgtmp)
							EXEC sp_transacciones
							@Modo = 'INS',
							@Id_Autor = @Id_Sesion,
							@Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
							@Id_Persona = @Id_Persona,
							@Concepto = @iConcepto,
							@o_Num = @Id_Transaccion OUTPUT
							BEGIN TRAN trx_AgregarUsuario
							BEGIN TRY
									INSERT INTO tbl_usuarios (Id_Persona,Usuario, Contrasena, Id_Estado, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,Id_Transaccion)
									VALUES (@Id_Persona,@Usuario, @Contrasena, @Id_Estado, @Fecha_Creacion, @Fecha_Modificacion, @Id_Creador, @Id_Modificador,@Id_Transaccion);

								COMMIT TRAN trx_AgregarUsuario
								SET @o_Num = SCOPE_IDENTITY();
								SET @o_Msg = '�Usuario registrado correctamente!';

								SET @iConcepto = CONCAT('�Usuario agregado n�: ', @o_Num)
								EXEC sp_transacciones
								@Modo = 'UPD',
								@Concepto = @iConcepto,
								@Id_Transaccion = @Id_Transaccion,
								@Id_Usuario = @o_Num
							END TRY
							BEGIN CATCH
								ROLLBACK TRAN trx_AgregarUsuario
								SET @o_Num = -1;
								SET @o_Msg = '�Error interno del servidor!';
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
			/* ACTUALIZAR USUARIOS */
			ELSE IF(@Id_Tipo_Transaccion = 21)
				BEGIN
					IF ISNULL(@Id_Usuario, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '�Debe seleccionar un ID Usuario!';
						END
					ELSE IF NOT EXISTS(SELECT 1 FROM tbl_usuarios(NOLOCK) WHERE Id_Usuario = @Id_Usuario)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '�Ese usuario no existe!';
						END
					ELSE IF ISNULL(CONVERT(VARCHAR(100),@Contrasena), '') = ''
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '�La contrase�a no puede estar vac�a!';
						END
					ELSE IF LEN(@Contrasena) < 8
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '�La contrase�a debe ser mayor a 8 car�cteres!';
						END
					ELSE
						BEGIN
							SET @iConcepto = IIF(@Contrasena IS NULL, 'ACTUALIZANDO USUARIO', 'CONTRASE�A ACTUALIZADA')
							EXEC sp_transacciones
								@Modo = 'INS',
								@Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
								@Concepto = @iConcepto,
								@Id_Usuario = @Id_Usuario,
								@Id_Autor = @Id_Sesion,
								@o_Num = @Id_Transaccion OUTPUT
							DECLARE @ContrasenaActual VARCHAR(100)
							SELECT 
								@ContrasenaActual = Contrasena
							FROM tbl_usuarios(NOLOCK)
							WHERE Id_Usuario = @Id_Usuario

							BEGIN TRAN trx_ActualizarUsuario
							BEGIN TRY
								UPDATE tbl_usuarios
								SET 
									Contrasena = CASE 
										WHEN @Contrasena IS NOT NULL AND @Contrasena != '' AND @Contrasena != @ContrasenaActual 
										THEN @Contrasena
										ELSE Contrasena 
									END,
									Id_Estado = COALESCE(@Id_Estado, Id_Estado),
									Fecha_Modificacion = COALESCE(@Fecha_Modificacion, Fecha_Modificacion),
									Id_Modificador = COALESCE(@Id_Modificador, Id_Modificador),
									Id_Transaccion = COALESCE(@Id_Transaccion, Id_Transaccion),
									Ultimo_Cambio_Contrasena = CASE 
										WHEN @Contrasena IS NOT NULL AND @Contrasena != '' AND @Contrasena != @ContrasenaActual 
										THEN @Fecha_Modificacion 
										ELSE Ultimo_Cambio_Contrasena
									END
								WHERE Id_Usuario = @Id_Usuario

								COMMIT TRAN trx_ActualizarUsuario
								SET @o_Num = 0;
								SET @o_Msg = '�Usuario actualizado correctamente!';
								EXEC sp_transacciones
									@Modo = 'UPD',
									@Id_Transaccion = @Id_Transaccion
							END TRY
							BEGIN CATCH
								ROLLBACK TRAN trx_ActualizarUsuario
								SET @o_Num = -1;
								SET @o_Msg = '�Error interno del servidor!';
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
			/* LISTAR USUARIOS */
			ELSE IF (@Id_Tipo_Transaccion = 22)
				BEGIN
					BEGIN TRY
						SELECT TOP 20
							Id_Usuario,
							Id_Persona,
							Usuario,
							CONVERT(varchar(10), Ultima_Sesion, 103) AS Ultima_Sesion,
							CONVERT(varchar(10), Ultimo_Cambio_Contrasena, 103) AS Ultimo_Cambio_Contrasena,
							CONVERT(varchar(10), Fecha_Creacion, 103) AS Fecha_Creacion,
							CONVERT(varchar(10), Fecha_Modificacion, 103) AS Fecha_Modificacion,
							Id_Creador,
							Id_Modificador,
							Id_Transaccion,
							Id_Estado
						FROM tbl_usuarios(NOLOCK)
						WHERE Id_Estado = 1
						AND Id_Usuario <> 1
						ORDER BY Fecha_Modificacion DESC

						SET @o_Num = 0;
						SET @o_Msg = '�Usuarios listados!';
					END TRY
					BEGIN CATCH
						SET @o_Num = -1;
						SET @o_Msg = '�Error interno del servidor!';
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
			/* FILTRAR USUARIOS POR ID */
			ELSE IF (@Id_Tipo_Transaccion = 23)
				BEGIN
					BEGIN TRY
						SELECT 
							Id_Usuario,
							Id_Persona,
							Usuario,
							CONVERT(varchar(10), Ultima_Sesion, 103) AS Ultima_Sesion,
							CONVERT(varchar(10), Ultimo_Cambio_Contrasena, 103) AS Ultimo_Cambio_Contrasena,
							CONVERT(varchar(10), Fecha_Creacion, 103) AS Fecha_Creacion,
							CONVERT(varchar(10), Fecha_Modificacion, 103) AS Fecha_Modificacion,
							Id_Creador,
							Id_Modificador,
							Id_Transaccion,
							Id_Estado
						FROM tbl_usuarios (NOLOCK)
						WHERE Id_Usuario = @Id_Usuario
						SET @o_Num = 0;
						SET @o_Msg = '�Usuario filtrado!' 
					END TRY
					BEGIN CATCH
						SET @o_Num = -1;
						SET @o_Msg = '�Error interno del servidor!';
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
			/* FILTRAR USUARIOS POR USUARIO */
			ELSE IF (@Id_Tipo_Transaccion = 24)
				BEGIN
					IF NOT EXISTS(SELECT 1 FROM tbl_usuarios WITH (NOLOCK) WHERE Usuario LIKE '%' + @Usuario + '%')
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = 'Usuario no encontrado';
						END
					ELSE
						BEGIN
							BEGIN TRY
								SELECT 
									Id_Usuario,
									Id_Persona,
									Usuario,
									CONVERT(varchar(10), Ultima_Sesion, 103) AS Ultima_Sesion,
									CONVERT(varchar(10), Ultimo_Cambio_Contrasena, 103) AS Ultimo_Cambio_Contrasena,
									CONVERT(varchar(10), Fecha_Creacion, 103) AS Fecha_Creacion,
									CONVERT(varchar(10), Fecha_Modificacion, 103) AS Fecha_Modificacion,
									Id_Creador,
									Id_Modificador,
									Id_Transaccion,
									Id_Estado
								FROM tbl_usuarios (NOLOCK)
								WHERE Usuario LIKE '%' + @Usuario + '%'
								AND Id_Usuario <> 1
								SET @o_Num = 0;
								SET @o_Msg = '�Usuario/s filtrado!'
							END TRY
							BEGIN CATCH
								SET @o_Num = -1;
								SET @o_Msg = '�Error interno del servidor!';
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
			/* FILTRAR USUARIOS POR ID PERSONA */
			ELSE IF (@Id_Tipo_Transaccion = 25)
				BEGIN
					IF ISNULL(@Id_Persona, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '�Debe seleccionar a la persona para filtrar su usuario!';
						END
					
					ELSE IF NOT EXISTS(SELECT 1 FROM tbl_usuarios(NOLOCK) WHERE Id_Persona = @Id_Persona)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '�Usuario no existe!';
						END
					ELSE
						BEGIN
							BEGIN TRY
								SELECT 
									Id_Usuario,
									Id_Persona,
									Usuario,
									CONVERT(varchar(10), Ultima_Sesion, 103) AS Ultima_Sesion,
									CONVERT(varchar(10), Ultimo_Cambio_Contrasena, 103) AS Ultimo_Cambio_Contrasena,
									CONVERT(varchar(10), Fecha_Creacion, 103) AS Fecha_Creacion,
									CONVERT(varchar(10), Fecha_Modificacion, 103) AS Fecha_Modificacion,
									Id_Creador,
									Id_Modificador,
									Id_Transaccion,
									Id_Estado
								FROM tbl_usuarios (NOLOCK)
								WHERE Id_Persona = @Id_Persona
				
								SET @o_Num = 0;
								SET @o_Msg = '�Usuario filtrado!' 
							END TRY
							BEGIN CATCH
								SET @o_Num = -1;
								SET @o_Msg = '�Error interno del servidor!';
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
			/* FILTRAR USUARIOS POR ROL */
			ELSE IF (@Id_Tipo_Transaccion = 143)
				BEGIN
					IF ISNULL(@Id_Estado, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe seleccionar un rol para filtrar!';
						END
					ELSE IF NOT EXISTS(SELECT 1 FROM cls_roles(NOLOCK) WHERE Id_Rol = @Id_Estado)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡El rol seleccionado no existe!';
						END
					ELSE
						BEGIN
							BEGIN TRY
								SELECT DISTINCT
									u.Id_Usuario,
									u.Id_Persona,
									u.Usuario,
									CONVERT(varchar(10), u.Ultima_Sesion, 103) AS Ultima_Sesion,
									CONVERT(varchar(10), u.Ultimo_Cambio_Contrasena, 103) AS Ultimo_Cambio_Contrasena,
									CONVERT(varchar(10), u.Fecha_Creacion, 103) AS Fecha_Creacion,
									CONVERT(varchar(10), u.Fecha_Modificacion, 103) AS Fecha_Modificacion,
									u.Id_Creador,
									u.Id_Modificador,
									u.Id_Transaccion,
									u.Id_Estado
								FROM tbl_usuarios (NOLOCK) u
								INNER JOIN cls_usuarios_roles (NOLOCK) ur ON u.Id_Usuario = ur.Id_Usuario
								WHERE ur.Id_Rol = @Id_Estado
								AND ur.Activo = 1
								AND u.Id_Estado = 1
								AND u.Id_Usuario <> 1

								SET @o_Num = 0;
								SET @o_Msg = '¡Usuarios filtrados por rol!';
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
			/* LISTAR MENU POR ROL DE USUARIO */
			ELSE IF(@Id_Tipo_Transaccion = 26)
				BEGIN
					BEGIN TRY
						SELECT DISTINCT
						M.Id_Menu,
						M.Menu,
						M.Nombre_Boton
						FROM cls_menus_roles (NOLOCK) MR
						INNER JOIN cls_menus (NOLOCK) M ON MR.Id_Menu = M.Id_Menu AND M.Activo = 1 AND MR.Activo = 1
						INNER JOIN cls_Roles (NOLOCK) R ON MR.Id_Rol = R.Id_Rol AND R.Activo = 1
						INNER JOIN cls_usuarios_roles (NOLOCK) UR ON MR.Id_Rol = UR.Id_Rol AND UR.Activo = 1
						INNER JOIN tbl_usuarios (NOLOCK) U ON UR.Id_Usuario = U.Id_Usuario
						WHERE U.Id_Usuario = @Id_Sesion
						AND U.Id_Estado = 1
						ORDER BY Id_Menu ASC

						SET @o_Num = 0;
						SET @o_Msg = 'Menu Cargado';
					END TRY
					BEGIN CATCH
						SET @o_Num = -1;
						SET @o_Msg = '�Error interno del servidor!';
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
			/* OBTENER ESTUDIANTE POR NUMERO DE DOCUMENTO */
			ELSE IF(@Id_Tipo_Transaccion = 133)
				BEGIN
					IF ISNULL(@Valor_Documento, '') = ''
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe ingresar el número de documento!';
						END
					ELSE IF NOT EXISTS(SELECT 1 FROM tbl_personas(NOLOCK) WHERE Valor_Documento = @Valor_Documento)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡No existe una persona con ese número de documento!';
						END
					ELSE IF NOT EXISTS(
						SELECT 1 
						FROM tbl_usuarios U(NOLOCK)
						INNER JOIN tbl_personas P(NOLOCK) ON U.Id_Persona = P.Id_Persona
						WHERE P.Valor_Documento = @Valor_Documento
					)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡La persona con ese documento no tiene un usuario registrado!';
						END
					ELSE IF NOT EXISTS(
						SELECT 1 
						FROM tbl_usuarios U(NOLOCK)
						INNER JOIN tbl_personas P(NOLOCK) ON U.Id_Persona = P.Id_Persona
						INNER JOIN cls_usuarios_roles UR(NOLOCK) ON U.Id_Usuario = UR.Id_Usuario AND UR.Activo = 1
						WHERE P.Valor_Documento = @Valor_Documento
						AND UR.Id_Rol = 2
					)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡La persona con ese documento no es un estudiante!';
						END
					ELSE
						BEGIN
							BEGIN TRY
								SELECT 
									U.Id_Usuario,
									U.Id_Persona,
									U.Usuario,
									P.Valor_Documento,
									P.Primer_Nombre + ' ' + ISNULL(P.Segundo_Nombre, '') + ' ' + P.Primer_Apellido + ' ' + ISNULL(P.Segundo_Apellido, '') AS Nombre_Completo,
									CONVERT(varchar(10), U.Ultima_Sesion, 103) AS Ultima_Sesion,
									CONVERT(varchar(10), U.Ultimo_Cambio_Contrasena, 103) AS Ultimo_Cambio_Contrasena,
									CONVERT(varchar(10), U.Fecha_Creacion, 103) AS Fecha_Creacion,
									CONVERT(varchar(10), U.Fecha_Modificacion, 103) AS Fecha_Modificacion,
									U.Id_Creador,
									U.Id_Modificador,
									U.Id_Transaccion,
									U.Id_Estado
								FROM tbl_usuarios U(NOLOCK)
								INNER JOIN tbl_personas P(NOLOCK) ON U.Id_Persona = P.Id_Persona
								INNER JOIN cls_usuarios_roles UR(NOLOCK) ON U.Id_Usuario = UR.Id_Usuario AND UR.Activo = 1
								WHERE P.Valor_Documento = @Valor_Documento
								AND UR.Id_Rol = 2
								AND U.Id_Estado = 1

								SET @o_Num = 0;
								SET @o_Msg = '¡Estudiante encontrado!';
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
					SET @o_Msg = '�Tipo transacci�n invalida!';
				END
		END
	ELSE
		BEGIN
			SET @o_Num = -1;
			SET @o_Msg = '�Su usuario no tiene permiso para realizar este tipo de transacci�n!';
		END
END