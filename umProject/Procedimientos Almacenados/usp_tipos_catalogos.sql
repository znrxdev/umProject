USE umDb
GO
/*
sp_tipos_catalogos
cls_tipos_catalogos
*/
CREATE OR ALTER PROC usp_tipos_catalogos
(
@Id_Tipo_Catalogo INT = NULL,
@Nombre_Tipo_Catalogo NVARCHAR(50) = NULL,
@Fecha_Creacion DATETIME = NULL,
@Fecha_Modificacion DATETIME = NULL,
@Id_Creador INT = NULL,
@Id_Modificador INT = NULL,
@Id_Sesion INT = NULL,
@Id_Tipo_Transaccion INT,
@Id_Transaccion INT = NULL,
@Activo BIT = NULL,
@o_Num INT = NULL OUTPUT,
@o_Msg NVARCHAR(255) = NULL OUTPUT
)
AS
BEGIN
	DECLARE @Permiso INT, @iConcepto NVARCHAR(255),@Linea_Error INT, @Numero_Error INT, @Mensaje_Error NVARCHAR(255), @Origen_Error NVARCHAR(50) = ERROR_PROCEDURE();
	SET @Fecha_Creacion = GETDATE();
	SET @Fecha_Modificacion = GETDATE();
	SET @Id_Creador = @Id_Sesion;
	SET @Id_Modificador = @Id_Sesion;
	SET @Permiso = dbo.fn_Validar_Permisos(@Id_Sesion, @Id_Tipo_Transaccion)
	IF (@Permiso = 1)
		BEGIN
			/* LISTAR TIPOS CATALOGOS */
			IF (@Id_Tipo_Transaccion = 9)
				BEGIN
					BEGIN TRY
						SELECT TOP 20
							Id_Tipo_Catalogo,
							Nombre_Tipo_Catalogo,
							Fecha_Creacion,
							Fecha_Modificacion,
							Id_Creador,
							Id_Modificador,
							Id_Transaccion,
							Activo
						FROM cls_tipos_catalogos(NOLOCK)
						WHERE Activo = 1
						ORDER BY Id_Tipo_Catalogo DESC

						SET @o_Num = 0;
						SET @o_Msg = '¡Tipos catálogos listados!';
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
			/* FILTRAR TIPOS CATALOGOS ID*/
			ELSE IF (@Id_Tipo_Transaccion = 8)
				BEGIN
					BEGIN TRY
						SELECT TOP 10
							Id_Tipo_Catalogo,
							Nombre_Tipo_Catalogo,
							Fecha_Creacion,
							Fecha_Modificacion,
							Id_Creador,
							Id_Modificador,
							Id_Transaccion,
							Activo
						FROM cls_tipos_catalogos(NOLOCK)
						WHERE Id_Tipo_Catalogo = @Id_Tipo_Catalogo
						AND Activo = 1

						SET @o_Num = 0;
						SET @o_Msg = '¡Tipo catálogo filtrado!';
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
			/* AGREGAR TIPOS CATALOGOS*/
			ELSE IF (@Id_Tipo_Transaccion = 6)
				BEGIN 
					IF ISNULL(@Nombre_Tipo_Catalogo,  '') = '' 
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡El campo Nombre Tipo Catalogo no debe ir vacio!';
						END
					ELSE IF (@Activo) IS NULL
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe colocar un estado válido (activo/inactivo)!';						
						END
					ELSE IF EXISTS(SELECT 1 FROM cls_tipos_catalogos(NOLOCK) WHERE Nombre_Tipo_Catalogo = @Nombre_Tipo_Catalogo)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡El tipo catalogo ya existe!';				
						END
					ELSE 
						BEGIN 
							SET @iConcepto = CONCAT('AGREGANDO TIPO CATALOGO: ', @Nombre_Tipo_Catalogo);
							EXEC sp_transacciones
							@Modo = 'INS',
							@Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
							@Id_Autor = @Id_Sesion,
							@Concepto = @iConcepto,
							@o_Num = @Id_Transaccion OUTPUT;
							BEGIN TRAN trx_AgregarTipoCatalogo
							BEGIN TRY 
								INSERT INTO cls_tipos_catalogos(Nombre_Tipo_Catalogo, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
								(@Nombre_Tipo_Catalogo, @Fecha_Creacion, @Fecha_Modificacion, @Id_Creador, @Id_Modificador, @Id_Transaccion, @Activo)

								COMMIT TRAN trx_AgregarTipoCatalogo
								SET @o_Num = SCOPE_IDENTITY()
								SET @o_Msg = '!Tipo catalogo agregado exitosamente!'
								EXEC sp_transacciones
								@Modo = 'UPD',
								@Id_Transaccion = @Id_Transaccion

							END TRY 
							BEGIN CATCH 
								ROLLBACK TRAN trx_AgregarTipoCatalogo
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
			/* ACTUALIZAR TIPOS CATALOGOS*/
			ELSE IF(@Id_Tipo_Transaccion = 7)
				BEGIN 
					IF ISNULL(@Id_Tipo_Catalogo, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe seleccionar un ID Tipo Catalogo!';
						END
					ELSE IF NOT EXISTS(SELECT 1 FROM cls_tipos_catalogos (NOLOCK) WHERE Id_Tipo_Catalogo = @Id_Tipo_Catalogo)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡El tipo catalogo no existe!';
						END
					ELSE IF EXISTS(SELECT 1 FROM cls_tipos_catalogos (NOLOCK) WHERE Nombre_Tipo_Catalogo = @Nombre_Tipo_Catalogo AND Id_Tipo_Catalogo <> @Id_Tipo_Catalogo)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡El tipo catalogo ya existe!';
						END
					ELSE 
						BEGIN
							SET @iConcepto = CONCAT('ACTUALIZANDO TIPO CATALOGO N°: ', @Id_Tipo_Catalogo);
							EXEC sp_transacciones
							@Modo = 'INS',
							@Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
							@Id_Autor = @Id_Sesion,
							@Concepto = @iConcepto,
							@o_Num = @Id_Transaccion OUTPUT;	
							BEGIN TRAN trx_ActualizarTipoCatalogo
							BEGIN TRY
								UPDATE cls_tipos_catalogos
								SET Nombre_Tipo_Catalogo = COALESCE(@Nombre_Tipo_Catalogo, @Nombre_Tipo_Catalogo),
									Fecha_Modificacion = COALESCE(@Fecha_Modificacion, Fecha_Modificacion),
									Id_Modificador = COALESCE(@Id_Modificador, Id_Modificador),
									Id_Transaccion = COALESCE(@Id_Transaccion, Id_Transaccion),
									Activo = COALESCE(@Activo, Activo)
								WHERE Id_Tipo_Catalogo = @Id_Tipo_Catalogo;

								COMMIT TRAN trx_ActualizarTipoCatalogo;

								SET @o_Num = 0;
								SET @o_Msg = '¡Tipo Catálogo actualizado exitosamente!';

								EXEC sp_transacciones
									@Modo = 'UPD',
									@Id_Transaccion = @Id_Transaccion;
							END TRY
							BEGIN CATCH
								ROLLBACK TRAN trx_ActualizarTipoCatalogo
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