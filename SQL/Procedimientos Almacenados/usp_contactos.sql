USE umDb
GO

-- tbl_contactos

CREATE OR ALTER PROC [dbo].[usp_contactos]
(
@Id_Contacto INT = NULL,
@Id_Persona INT = NULL,
@Id_Tipo_Contacto INT = NULL,
@Valor_Contacto NVARCHAR(MAX) = NULL,
@Principal BIT = NULL,
@Id_Sesion INT = NULL,
@Id_Creador INT = NULL,
@Id_Modificador INT = NULL,
@Id_Tipo_Transaccion INT = NULL,
@Id_Transaccion INT = NULL,
@Id_Estado INT = NULL,
@Fecha_Creacion DATETIME = NULL,
@Fecha_Modificacion DATETIME = NULL,
@o_Num INT = NULL OUTPUT,
@o_Msg NVARCHAR(255) = NULL OUTPUT
)
AS
BEGIN
    DECLARE @Permiso INT,@Linea_Error INT, @Numero_Error INT, @Mensaje_Error NVARCHAR(255), @Origen_Error NVARCHAR(50) = ERROR_PROCEDURE();
    SET @Fecha_Creacion = GETDATE();
	SET @Fecha_Modificacion = GETDATE();
	SET @Id_Creador = @Id_Sesion;
	SET @Id_Modificador = @Id_Sesion;
    SET @Valor_Contacto = UPPER(RTRIM(LTRIM(UPPER(REPLACE(@Valor_Contacto, '-', '')))));

    SET @Permiso = dbo.fn_Validar_Permisos(@Id_Sesion, @Id_Tipo_Transaccion);

    IF(@Permiso = 1)
		BEGIN
			/* FILTRAR CONTACTO POR PERSONA */
			IF (@Id_Tipo_Transaccion = 29)
				BEGIN
					BEGIN TRY
					SELECT
						C.Id_Contacto,
						C.Id_Persona,
						C.Id_Tipo_Contacto,
						C.Valor_Contacto,
						C.Principal,
						CONVERT(VARCHAR(10), C.Fecha_Creacion, 103) AS Fecha_Creacion,
						CONVERT(VARCHAR(10), C.Fecha_Modificacion, 103) AS Fecha_Modificacion,
						C.Id_Creador,
						C.Id_Modificador,
						C.Id_Transaccion,
						C.Id_Estado
					FROM tbl_Contactos (NOLOCK) C
					WHERE C.Id_Persona = @Id_Persona;

						SET @o_Num = 0;
						SET @o_Msg = 'Satisfactorio';
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
			/* FILTRAR CONTACTO POR ID CONTACTO */
			ELSE IF (@Id_Tipo_Transaccion = 30)
				BEGIN
					BEGIN TRY
						SELECT
							Id_Contacto,
							Id_Persona,
							Id_Tipo_Contacto,
							Valor_Contacto,
							Principal,
							CONVERT(VARCHAR(10), Fecha_Creacion, 103) AS Fecha_Creacion,
							CONVERT(VARCHAR(10), Fecha_Modificacion, 103) AS Fecha_Modificacion,
							Id_Creador,
							Id_Modificador,
							Id_Transaccion,
							Id_Estado
						FROM tbl_Contactos (NOLOCK)
						WHERE Id_Contacto = @Id_Contacto;

						SET @o_Num = 0;
						SET @o_Msg = 'Satisfactorio';
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
			/* AGREGAR CONTACTO */
			ELSE IF (@Id_Tipo_Transaccion = 27)
				BEGIN
					IF ISNULL(@Id_Persona, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe asignar una persona!';
						END
					ELSE IF ISNULL(@Id_Tipo_Contacto, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe asignar un tipo de contacto!';
						END
					ELSE IF ISNULL(@Valor_Contacto, '') = ''
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe asignar un valor al contacto!';
						END
					ELSE IF EXISTS (SELECT 1 FROM tbl_contactos (NOLOCK) WHERE Id_Persona = @Id_Persona AND Id_Tipo_Contacto = @Id_Tipo_Contacto AND Valor_Contacto = @Valor_Contacto)
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡El contacto ya existe!';
						END
					ELSE IF ISNULL(@Id_Estado, 0) = 0 
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = 'Estado es obligatorio';
						END
					ELSE
						BEGIN
							/* AGREGANDO REGISTRO BITACORA TRANSACCIONES */
							EXEC sp_transacciones
							@Modo = 'INS',
							@Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
							@Concepto = 'Nuevo Contacto',
							@Id_Persona =  @Id_Persona,
							@Id_Autor = @Id_Sesion,
							@o_Num = @Id_Transaccion OUTPUT

							BEGIN TRAN trx_InsertarContacto;
							BEGIN TRY
								INSERT INTO tbl_contactos (Id_Persona, Id_Tipo_Contacto, Valor_Contacto, Principal, Id_Estado, Id_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador)
								VALUES (@Id_Persona, @Id_Tipo_Contacto, @Valor_Contacto, @Principal, @Id_Estado, @Id_Transaccion, @Fecha_Creacion, @Fecha_Modificacion, @Id_Creador, @Id_Modificador);
								COMMIT TRAN trx_Contacto;
								SET @o_Num = SCOPE_IDENTITY();
								SET @o_Msg = '¡Contacto insertado correctamente!';

								EXEC sp_transacciones
								@Modo = 'UPD',
								@Id_Transaccion = @Id_Transaccion,
								@Id_Persona =  @Id_Persona,
								@Id_Contacto = @o_Num
							END TRY
							BEGIN CATCH
								ROLLBACK TRAN trx_InsertarContacto;

								/* REVERSANDO REGISTRO BITACORA TRANSACCIONES */
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
			/* ACTUALIZAR CONTACTO */
			ELSE IF(@Id_Tipo_Transaccion = 28)
				BEGIN 
					IF ISNULL(@Id_Contacto, 0) = 0
						BEGIN
							SET @o_Num = -1;
							SET @o_Msg = '¡Debe seleccionar un contacto!';
						END
					ELSE 
						BEGIN
							SELECT @Id_Persona = Id_Persona FROM dbo.tbl_contactos WITH(NOLOCK) WHERE Id_Contacto = @Id_Contacto
							/* AGREGANDO REGISTRO BITACORA TRANSACCIONES */
							EXEC sp_transacciones
							@Modo = 'INS',
							@Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
							@Concepto = 'Contacto actualizado',
							@Id_Persona = @Id_Persona,
							@Id_Contacto = @Id_Contacto,
							@Id_Autor = @Id_Sesion,
							@o_Num = @Id_Transaccion OUTPUT

							BEGIN TRAN trx_ActualizarContacto;
							BEGIN TRY
								/* ACTUALIZANDO REGISTRO TABLA CONTACTO */
								UPDATE tbl_contactos
								SET Id_Tipo_Contacto = COALESCE(@Id_Tipo_Contacto, Id_Tipo_Contacto),
									Valor_Contacto = COALESCE(@Valor_Contacto, Valor_Contacto),                       
									Principal = COALESCE(@Principal, Principal),             
									Id_Estado = COALESCE(@Id_Estado, Id_Estado),             
									Fecha_Modificacion = COALESCE(@Fecha_Modificacion, Fecha_Modificacion),
									Id_Modificador = COALESCE(@Id_Modificador, Id_Modificador)
								WHERE Id_Contacto = @Id_Contacto;

								COMMIT TRAN trx_ActualizarContacto;
								SET @o_Num = 0;
								SET @o_Msg = '¡Contacto actualizado correctamente!';

								EXEC sp_transacciones
								@Modo = 'UPD',
								@Id_Transaccion = @Id_Transaccion,
								@Id_Persona = @o_Num 

							END TRY
							BEGIN CATCH
								ROLLBACK TRAN trx_ActualizarContacto;

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
		END
	ELSE
		BEGIN
			SET @o_Num = -1;
			SET @o_Msg = '¡Su usuario no tiene permiso para realizar este tipo de transacción!';
		END
END