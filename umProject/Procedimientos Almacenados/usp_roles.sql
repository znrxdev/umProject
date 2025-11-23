USE umDb
GO
/*

cls_roles
*/

CREATE OR ALTER PROC usp_roles
(
@Id_Rol INT = NULL,
@Nombre_Rol VARCHAR(80) = NULL,	
@Fecha_Creacion DATETIME = NULL,
@Fecha_Modificacion DATETIME = NULL,
@Id_Creador INT = NULL,
@Id_Modificador INT = NULL,
@Activo BIT = NULL,
@Id_Tipo_Transaccion INT,
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

    SET @Permiso = dbo.fn_Validar_Permisos(@Id_Sesion, @Id_Tipo_Transaccion);

	IF (@Permiso = 1)
		BEGIN
			/* LISTAR ROLES */
			IF (@Id_Tipo_Transaccion = 34)
				BEGIN
					BEGIN TRY
						SELECT TOP 10
							Id_Rol,
							Nombre_Rol,
							CONVERT(varchar(10), Fecha_Creacion, 103) AS Fecha_Creacion,
							CONVERT(varchar(10), Fecha_Modificacion, 103) AS Fecha_Modificacion,
							Id_Creador,
							Id_Modificador,
							Id_Transaccion,
							Activo
						FROM cls_roles (NOLOCK)
						WHERE Activo = 1
						ORDER BY Fecha_Modificacion DESC

						SET @o_Num = 0;
						SET @o_Msg = '¡Roles encontrados!';
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
			/* FILTRAR ROLES ID */
			ELSE IF (@Id_Tipo_Transaccion = 33)
				BEGIN
					IF ISNULL(@Id_Rol, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe seleccionar un rol!';
						END
					ELSE IF NOT EXISTS(SELECT 1 FROM cls_Roles(NOLOCK) WHERE Id_Rol = @Id_Rol)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Rol no existe!';
						END
					ELSE	
						BEGIN
							BEGIN TRY
								SELECT
									Id_Rol,
									Nombre_Rol,
									CONVERT(varchar(10), Fecha_Creacion, 103) AS Fecha_Creacion,
									CONVERT(varchar(10), Fecha_Modificacion, 103) AS Fecha_Modificacion,
									Id_Creador,
									Id_Modificador,
									Id_Transaccion,
									Activo
								FROM cls_Roles (NOLOCK)
								WHERE Id_Rol = @Id_Rol
								AND Activo = 1
								SET @o_Num = 0;
								SET @o_Msg = '¡Rol encontrado!';
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
			/* AGREGAR ROLES */
			ELSE IF (@Id_Tipo_Transaccion = 31)
				BEGIN
					IF ISNULL(@Nombre_Rol,  '') = '' 
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡El campo Nombre Rol no debe ir vacio!';
						END
					ELSE IF (@Activo) IS NULL
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe colocar un estado válido (activo/inactivo)!';						
						END
					ELSE IF EXISTS(SELECT 1 FROM cls_roles (NOLOCK) WHERE Nombre_Rol = @Nombre_Rol)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡El rol ya existe!';
						END
					ELSE 
						BEGIN
							SET @iConcepto = CONCAT('AGREGANDO ROL: ', @Nombre_Rol);
							EXEC sp_transacciones
							@Modo = 'INS',
							@Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
							@Id_Autor = @Id_Sesion,
							@Concepto = @iConcepto,
							@o_Num = @Id_Transaccion OUTPUT
							BEGIN TRAN trx_AgregarRol
							BEGIN TRY 
								INSERT INTO cls_roles (Nombre_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion,Activo) VALUES 
								(@Nombre_Rol, @Fecha_Creacion, @Fecha_Modificacion, @Id_Creador, @Id_Modificador, @Id_Transaccion,@Activo)

								COMMIT TRAN trx_AgregarRol
								SET @o_Num = SCOPE_IDENTITY()
								SET @o_Msg = '¡Rol agregado exitosamente!'
								EXEC sp_transacciones
								@Modo = 'UPD',
								@Id_Transaccion = @Id_Transaccion	

							END TRY 
							BEGIN CATCH 
								ROLLBACK TRAN trx_AgregarRol
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
			/* ACTUALIZAR ROLES */
			ELSE IF (@Id_Tipo_Transaccion = 32)
				BEGIN 
					IF ISNULL(@Id_Rol, 0) = 0	
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe seleccionar un ID rol!';
						END 
					ELSE IF NOT EXISTS(SELECT 1 FROM cls_roles(NOLOCK) WHERE Id_Rol = @Id_Rol)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Rol no existe!';
						END 
					ELSE IF EXISTS(SELECT 1 FROM cls_roles(NOLOCK) WHERE Nombre_Rol = @Nombre_Rol AND Id_Rol <> @Id_Rol)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Rol ya existe!';
						END 
					BEGIN
							SET @iConcepto = CONCAT('ACTUALIZANDO ROL N°: ', @Id_Rol);
							EXEC sp_transacciones
							@Modo = 'INS',
							@Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
							@Id_Autor = @Id_Sesion,
							@Concepto = @iConcepto,
							@o_Num = @Id_Transaccion OUTPUT;	
							BEGIN TRAN trx_ActualizarRol
							BEGIN TRY
								UPDATE cls_roles
								SET Nombre_Rol = COALESCE(@Nombre_Rol, @Nombre_Rol),
									Fecha_Modificacion = COALESCE(@Fecha_Modificacion, Fecha_Modificacion),
									Id_Modificador = COALESCE(@Id_Modificador, Id_Modificador),
									Id_Transaccion = COALESCE(@Id_Transaccion, Id_Transaccion),
									Activo = COALESCE(@Activo, Activo)
								WHERE Id_Rol = @Id_Rol;

								COMMIT TRAN trx_ActualizarRol;

								SET @o_Num = 0;
								SET @o_Msg = '¡Rol actualizado exitosamente!';

								EXEC sp_transacciones
								@Modo = 'UPD',
								@Id_Transaccion = @Id_Transaccion;
							END TRY
							BEGIN CATCH
								ROLLBACK TRAN trx_ActualizarRol
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