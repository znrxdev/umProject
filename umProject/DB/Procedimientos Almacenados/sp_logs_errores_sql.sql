USE umDb
GO

CREATE OR ALTER PROC sp_logs_errores_sql
(
@Origen_Error NVARCHAR(50) NULL,
@Linea_Error INT NULL,
@Numero_Error INT NULL,
@Mensaje_Error NVARCHAR(255) NULL,
@Fecha_Error DATETIME = NULL,
@Modo CHAR(3) = NULL
)
AS
BEGIN
	SET @Fecha_Error = GETDATE();

	/* Si el parametro de entrada @Modo tiene de valor 'INS' que entre en este bloque de instrucciones. */
	IF(@Modo = 'INS')
		BEGIN
			BEGIN TRAN trx_AgregarLogSqlError
			BEGIN TRY
				INSERT INTO log_errores_sql(Origen_Error,Linea_Error,Numero_Error,Mensaje_Error,Fecha_Error)
				VALUES (@Origen_Error, @Linea_Error, @Numero_Error,@Mensaje_Error, @Fecha_Error)
				COMMIT TRAN trx_AgregarLogSqlError
			END TRY
			BEGIN CATCH
				ROLLBACK TRAN trx_AgregarLogSqlError
			END CATCH
		END
END