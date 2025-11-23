USE umDb
GO
/*
sp_estados
cls_estados
*/

CREATE OR ALTER PROC usp_estados
(
@Id_Estado INT = NULL,
@Nombre_Estado NVARCHAR(50) = NULL,
@Fecha_Creacion DATETIME = NULL,
@Fecha_Modificacion DATETIME = NULL,
@Id_Creador INT = NULL,
@Id_Modificador INT = NULL,
@Id_Sesion INT = NULL,
@Activo BIT = NULL,
@Tipo_Transaccion INT,
@Id_Transaccion INT = NULL,
@Id_Tipo_Transaccion INT = NULL,
@o_Msg NVARCHAR(255) = NULL OUTPUT,
@o_Num INT = NULL OUTPUT
)
AS
BEGIN
	DECLARE @Permiso INT, @iConcepto NVARCHAR(255),@Linea_Error INT, @Numero_Error INT, @Mensaje_Error NVARCHAR(255), @Origen_Error NVARCHAR(50) = ERROR_PROCEDURE();
	SET @Fecha_Creacion = GETDATE();
	SET @Fecha_Modificacion = GETDATE();
	SET @Id_Creador = @Id_Sesion;
	SET @Id_Modificador = @Id_Sesion;
	SET @Permiso = dbo.fn_Validar_Permisos(@Id_Sesion, @Tipo_Transaccion)
	IF(@Permiso = 1)
		BEGIN
			/* FILTRAR ESTADOS ID */
			IF(@Tipo_Transaccion = 3)
				BEGIN
					IF ISNULL(@Id_Estado, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe seleccionar un estado a filtrar!';
						END
					ELSE IF NOT EXISTS (SELECT 1 FROM cls_estados (NOLOCK) WHERE Id_Estado = @Id_Estado)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Estado no existe!';						
						END
					ELSE
						BEGIN
							BEGIN TRY
								SELECT 
									Id_Estado,
									Nombre_Estado,
									Fecha_Creacion,
									Fecha_Modificacion,
									Id_Creador,
									Id_Modificador,
									Id_Transaccion,
									Activo
								FROM cls_estados(NOLOCK)
								WHERE Id_Estado = @Id_Estado
								AND Activo = 1
								SET @o_Num = 0;
								SET @o_Msg = '¡Estado filtrado!';
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
			/* FILTRAR ESTADOS POR TIPO TRANSACCION*/
			ELSE IF (@Tipo_Transaccion = 4)
				BEGIN
					BEGIN TRY
						SELECT
							E.Id_Estado,
							E.Nombre_Estado
						FROM cls_transacciones_estados (NOLOCK) TE
						INNER JOIN cls_tipos_transacciones (NOLOCK) TT ON TT.Id_Tipo_Transaccion = TE.Id_Tipo_Transaccion AND TT.Activo = 1
						INNER JOIN cls_estados (NOLOCK) E ON TE.Id_estado = E.Id_Estado AND E.Activo = 1
						WHERE TE.Activo = 1
						AND TE.Id_Tipo_Transaccion = @Id_Tipo_Transaccion

						SET @o_Num = 0;
						SET @o_Msg = '¡Estados listados!';
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
			/* LISTAR ULTIMOS 10 ESTADOS */
			ELSE IF(@Tipo_Transaccion = 5)
				BEGIN
					BEGIN TRY
						SELECT TOP 10
							Id_Estado,
							Nombre_Estado,
							Fecha_Creacion,
							Fecha_Modificacion,
							Id_Creador,
							Id_Modificador,
							Id_Transaccion,
							Activo
						FROM cls_estados WITH (NOLOCK)
						ORDER BY Id_Estado DESC;

						SET @o_Num = 0;
						SET @o_Msg = '¡Estados listados!';
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
			/* AGREGAR ESTADOS */
			ELSE IF(@Tipo_Transaccion = 1)
				BEGIN
					IF ISNULL(@Nombre_Estado, '') = ''	
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡El nombre del estado no puede estar vacío!';
						END
					ELSE IF EXISTS(SELECT 1 FROM cls_estados WITH (NOLOCK) WHERE Nombre_Estado = @Nombre_Estado)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Ya existe un estado con ese nombre!';
						END
					ELSE IF (@Activo) IS NULL
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe seleccionar un estado de actividad!';
						END
					ELSE
						BEGIN
							SET @iConcepto = 'REGISTRANDO NUEVO ESTADO';
							EXEC sp_transacciones
							@Modo = 'INS',
							@Id_Autor = @Id_Sesion,
							@Id_Tipo_Transaccion = @Tipo_Transaccion,
							@Concepto = @iConcepto,
							@o_Num = @Id_Transaccion OUTPUT
							BEGIN TRAN trx_AgregarEstados
							BEGIN TRY
								INSERT INTO cls_estados(Nombre_Estado, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion,Activo)
								VALUES (@Nombre_Estado, @Fecha_Creacion, @Fecha_Modificacion, @Id_Creador, @Id_Modificador, @Id_Transaccion,@Activo)

								SET @o_Num = SCOPE_IDENTITY();
								SET @o_Msg = '¡Estado registrado correctamente!';
								COMMIT TRAN trx_AgregarEstados
								EXEC sp_transacciones
								@Modo = 'UPD',
								@Id_Transaccion = @Id_Transaccion

							END TRY
							BEGIN CATCH
								ROLLBACK TRAN trx_AgregarEstados
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
			/* ACTUALIZAR ESTADOS */
			ELSE IF(@Tipo_Transaccion = 2)
				BEGIN
					IF ISNULL(@Id_Estado, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡No se ha mandado el estado!';
						END
					ELSE IF EXISTS(SELECT 1 FROM cls_estados WITH (NOLOCK) WHERE Nombre_Estado = @Nombre_Estado AND Id_Estado <> @Id_Estado)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Estado ya existe!';
						END
					ELSE
						BEGIN
							SET @iConcepto = 'ACTUALIZAR ESTADO'
							EXEC sp_transacciones
							@Modo = 'INS',
							@Id_Autor = @Id_Sesion,
							@Id_Tipo_Transaccion = @Tipo_Transaccion,
							@Concepto = @iConcepto,
							@o_Num = @Id_Transaccion OUTPUT
							BEGIN TRAN trx_ActualizarEstado
							BEGIN TRY
								UPDATE cls_estados
									SET Nombre_Estado = COALESCE(@Nombre_Estado, Nombre_Estado),
										Fecha_Modificacion = COALESCE(@Fecha_Modificacion, Fecha_Modificacion),
										Id_Modificador = COALESCE(@Id_Modificador, Id_Modificador),
										Id_Transaccion = COALESCE(@Id_Transaccion, Id_Transaccion),
										Activo = COALESCE(@Activo, Activo)
								WHERE Id_Estado = @Id_Estado
								COMMIT TRAN trx_ActualizarEstado

								SET @o_Num = 0;
								SET @o_Msg = '¡Estado actualizado!';

								EXEC sp_transacciones
								@Modo = 'UPD',
								@Id_Transaccion = @Id_Transaccion
							END TRY
							BEGIN CATCH
								ROLLBACK TRAN trx_ActualizarEstado
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
			SET @o_Msg = '¡Su usuario no tiene permiso para realizar esta acción!';
		END
END