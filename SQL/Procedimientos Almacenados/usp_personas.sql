USE umDb
GO
/*
tbl_personas
*/
CREATE OR ALTER PROC usp_personas
(
@Id_Persona INT = NULL,
@Primer_Nombre NVARCHAR(100) = NULL,
@Segundo_Nombre NVARCHAR(100) = NULL,
@Primer_Apellido NVARCHAR(100) = NULL,
@Segundo_Apellido NVARCHAR(100) = NULL,
@Id_Tipo_Documento INT = NULL,
@Valor_Documento NVARCHAR(100) = NULL,
@Id_Genero_Persona INT = NULL,
@Id_Nacionalidad INT = NULL,
@Id_Estado_Civil INT = NULL,
@Fecha_Creacion DATETIME = NULL,
@Fecha_Modificacion DATETIME = NULL,
@Id_Creador INT = NULL,
@Id_Modificador INT = NULL,
@Id_Sesion INT = NULL,
@Id_Tipo_Transaccion INT,
@Id_Transaccion INT = NULL,
@Id_Estado INT = NULL,
@o_Num INT = NULL OUTPUT,
@o_Msg NVARCHAR(255) = NULL OUTPUT
)
AS
BEGIN
	DECLARE @Permiso INT, @iConcepto NVARCHAR(255), @Linea_Error INT, @Numero_Error INT, @Mensaje_Error NVARCHAR(255), @Origen_Error NVARCHAR(50) = ERROR_PROCEDURE();
	SET @Id_Creador = @Id_Sesion;
	SET @Id_Modificador = @Id_Sesion;
	SET @Permiso = dbo.fn_Validar_Permisos(@Id_Sesion, @Id_Tipo_Transaccion)
	SET @Fecha_Creacion = GETDATE()
	SET @Fecha_Modificacion = GETDATE()
	SET @Valor_Documento = RTRIM(LTRIM(REPLACE(UPPER(@Valor_Documento), '-','')))
	SET @Primer_Nombre = LTRIM(RTRIM(UPPER(@Primer_Nombre)))
	SET @Segundo_Nombre = CASE WHEN @Segundo_Nombre IS NOT NULL THEN  LTRIM(RTRIM(UPPER(@Segundo_Nombre))) ELSE NULL END
	SET @Primer_Apellido = LTRIM(RTRIM(UPPER(@Primer_Apellido)))
	SET @Segundo_Apellido = CASE WHEN @Segundo_Apellido IS NOT NULL THEN  LTRIM(RTRIM(UPPER(@Segundo_Apellido))) ELSE NULL END
	
	IF(@Permiso = 1)
		BEGIN
			/* AGREGAR PERSONAS */
			IF(@Id_Tipo_Transaccion = 15)
				BEGIN
					IF ISNULL(@Primer_Nombre, '') = ''
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Primer nombre es obligatorio!';
						END
					ELSE IF ISNULL(@Primer_Apellido, '') = ''
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Primer apellido es obligatorio!';
						END
					ELSE IF ISNULL(@Id_Tipo_Documento, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Tipo de documento es obligatorio!';
						END
					ELSE IF ISNULL(@Valor_Documento, '') = ''
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Valor del documento es obligatorio!';						
						END
					ELSE IF (@Id_Tipo_Documento = 1 AND LEN(@Valor_Documento) < 14)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Logitud de numero documento no valida! ' + CAST(@Valor_Documento AS VARCHAR(20));
						END
					ELSE IF EXISTS (SELECT 1 FROM tbl_personas(NOLOCK) WHERE Valor_Documento = @Valor_Documento)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Ya existe una persona con este n° de documento!';
						END
					ELSE IF ISNULL(@Id_Genero_Persona, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Genero de la persona es requerido!';
						END
					ELSE IF ISNULL(@Id_Nacionalidad, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe seleccionar la nacionalidad!';
						END
					ELSE IF ISNULL(@Id_Estado_Civil, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe seleccionar el estado civil!';
						END
					ELSE IF ISNULL(@Id_Estado, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe seleccionar el estado!';
						END
					ELSE
						BEGIN
							SET @iConcepto = 'REGISTRANDO PERSONA';
							EXEC sp_transacciones
							@Modo = 'INS',
							@Id_Autor = @Id_Sesion,
							@Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
							@Concepto = @iConcepto,
							@o_Num = @Id_Transaccion OUTPUT
							BEGIN TRAN trx_AgregarPersona
							BEGIN TRY
								INSERT INTO tbl_personas(Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido, Id_Tipo_Documento, Valor_Documento, Id_Genero_Persona,Id_Nacionalidad, Id_Estado_Civil, Id_Estado, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion)
								VALUES (@Primer_Nombre, @Segundo_Nombre, @Primer_Apellido, @Segundo_Apellido, @Id_Tipo_Documento, @Valor_Documento, @Id_Genero_Persona, @Id_Nacionalidad, @Id_Estado_Civil, @Id_Estado, @Fecha_Creacion, @Fecha_Modificacion, @Id_Creador, @Id_Modificador, @Id_Transaccion)
								SET @o_Num = SCOPE_IDENTITY();
								SET @o_Msg = '¡Persona registrada correctamente!';
								COMMIT TRAN trx_AgregarPersona

								EXEC sp_transacciones
								@Modo = 'UPD',
								@Id_Transaccion = @Id_Transaccion,
								@Id_Persona = @o_Num
							END TRY
							BEGIN CATCH
								ROLLBACK TRAN trx_AgregarPersona
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
			/* ACTUALIZAR PERSONA */
			ELSE IF (@Id_Tipo_Transaccion = 16)
				BEGIN
					IF ISNULL(@Id_Persona, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe seleccionar a la persona!';
						END
					ELSE IF NOT EXISTS(SELECT 1 FROM tbl_personas(NOLOCK) WHERE Id_Persona = @Id_Persona)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Persona seleccionada no existe!';
						END
					ELSE IF EXISTS(SELECT 1 FROM tbl_personas(NOLOCK) WHERE Valor_Documento = @Valor_Documento AND Id_Persona <> @Id_Persona)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Ya existe una persona con ese n° de documento!';
						END

					ELSE
						BEGIN
							SET @iConcepto = 'ACTUALIZANDO PERSONA';
							EXEC sp_transacciones
							@Modo = 'INS',
							@Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
							@Concepto = @iConcepto,
							@Id_Autor = @Id_Sesion,
							@Id_Persona = @Id_Persona,
							@o_Num = @Id_Transaccion OUTPUT
							BEGIN TRAN trx_ActualizarPersona
							BEGIN TRY
								UPDATE tbl_personas
								SET Primer_Nombre = COALESCE(@Primer_Nombre, Primer_Nombre),
									Segundo_Nombre = COALESCE(@Segundo_Nombre, Segundo_Nombre),
									Primer_Apellido = COALESCE(@Primer_Apellido, Primer_Apellido),
									Segundo_Apellido = COALESCE(@Segundo_Apellido, Segundo_Apellido),
									Id_Tipo_Documento = COALESCE(@Id_Tipo_Documento, Id_Tipo_Documento),
									Valor_Documento = COALESCE(@Valor_Documento, Valor_Documento),
									Id_Genero_Persona = COALESCE(@Id_Genero_Persona, Id_Genero_Persona),
									Id_Nacionalidad = COALESCE(@Id_Nacionalidad, Id_Nacionalidad),
									Id_Estado_Civil = COALESCE(@Id_Estado_Civil, Id_Estado_Civil),
									Fecha_Modificacion = COALESCE(@Fecha_Modificacion, Fecha_Modificacion),
									Id_Modificador = COALESCE(@Id_Modificador, Id_Modificador),
									Id_Transaccion = COALESCE(@Id_Transaccion, Id_Transaccion),
									Id_Estado = COALESCE(@Id_Estado, Id_Estado)
								WHERE Id_Persona = @Id_Persona
								COMMIT TRAN trx_ActualizarPersona

								EXEC sp_transacciones
								@Modo = 'UPD',
								@Id_Transaccion = @Id_Transaccion

								SET @o_Num = 0
								SET @o_Msg = '¡Datos personales actualizados!';

							END TRY
							BEGIN CATCH
								ROLLBACK TRAN trx_ActualizarPersona
								EXEC sp_transacciones
								@Modo = 'RBK',
								@Id_Transaccion = @Id_Transaccion
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
			/* FILTRAR PERSONAS ID */
			ELSE IF (@Id_Tipo_Transaccion = 17)
				BEGIN
					BEGIN TRY
						SELECT
							Id_Persona,
							Primer_Nombre,
							Segundo_Nombre,
							Primer_Apellido,
							Segundo_Apellido,
							Id_Tipo_Documento,
							Valor_Documento,
							Id_Genero_Persona,
							Id_Nacionalidad,
							Id_Estado_Civil,
							Fecha_Creacion,
							Fecha_Modificacion,
							Id_Creador,
							Id_Modificador,
							Id_Transaccion,
							Id_Estado
						FROM tbl_personas(NOLOCK)
						WHERE Id_Persona = @Id_Persona

						SET @o_Num = 0;
						SET @o_Msg = '¡Persona filtrada!';
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
			/* FILTRAR PERSONAS POR NUMERO DOCUMENTO */
			ELSE IF (@Id_Tipo_Transaccion = 18)
				BEGIN
					IF ISNULL(@Valor_Documento, '') = ''
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe digitar el número del documento para filtrar!';
						END
					ELSE IF NOT EXISTS(SELECT 1 FROM tbl_personas(NOLOCK) WHERE Valor_Documento = @Valor_Documento)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡No existe una persona con este documento en el sistema!';
						END
					ELSE
						BEGIN
							BEGIN TRY
								SELECT
									Id_Persona,
									Primer_Nombre,
									Segundo_Nombre,
									Primer_Apellido,
									Segundo_Apellido,
									Id_Tipo_Documento,
									Valor_Documento,
									Id_Genero_Persona,
									Id_Nacionalidad,
									Id_Estado_Civil,
									Fecha_Creacion,
									Fecha_Modificacion,
									Id_Creador,
									Id_Modificador,
									Id_Transaccion,
									Id_Estado
								FROM tbl_personas(NOLOCK)
								WHERE Valor_Documento = @Valor_Documento

								SET @o_Num = 0;
								SET @o_Msg = '¡Persona encontrada!';
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
					SET @o_Msg = '¡Tipo de transacción no válido!';
				END
		END
	ELSE
		BEGIN
			SET @o_Num = -1;
			SET @o_Msg = '¡Su usuario no tiene permiso para realizar este tipo de transacción!';
		END
END