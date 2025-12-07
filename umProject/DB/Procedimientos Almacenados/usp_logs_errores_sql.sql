USE umDb
GO
/*
usp_logs_errores_sql
Stored Procedure para gestionar los logs de errores SQL del sistema
*/

CREATE OR ALTER PROCEDURE usp_logs_errores_sql
(
    @Id_Error INT = NULL,
    @Origen_Error NVARCHAR(50) = NULL,
    @Linea_Error INT = NULL,
    @Numero_Error INT = NULL,
    @Mensaje_Error NVARCHAR(255) = NULL,
    @Fecha_Error DATETIME = NULL,
    @Fecha_Inicio DATETIME = NULL,
    @Fecha_Fin DATETIME = NULL,
    @Tipo_Transaccion INT,
    @Id_Sesion INT = NULL,
    @o_Msg NVARCHAR(255) = NULL OUTPUT,
    @o_Num INT = NULL OUTPUT
)
AS
BEGIN
    DECLARE @Permiso INT, @Linea_Error_Proc INT, @Numero_Error_Proc INT, @Mensaje_Error_Proc NVARCHAR(255), @Origen_Error_Proc NVARCHAR(50) = ERROR_PROCEDURE();
    
    SET @Permiso = dbo.fn_Validar_Permisos(@Id_Sesion, @Tipo_Transaccion);

    IF (@Permiso = 1)
    BEGIN
        /* LISTAR ERRORES SQL DEL SISTEMA */
        IF (@Tipo_Transaccion = 154)
        BEGIN
            BEGIN TRY
                SELECT 
                    Id_Error,
                    Origen_Error,
                    Linea_Error,
                    Numero_Error,
                    Mensaje_Error,
                    CONVERT(VARCHAR(19), Fecha_Error, 120) AS Fecha_Error
                FROM log_errores_sql (NOLOCK)
                WHERE (@Fecha_Inicio IS NULL OR Fecha_Error >= @Fecha_Inicio)
                    AND (@Fecha_Fin IS NULL OR Fecha_Error <= DATEADD(DAY, 1, CAST(@Fecha_Fin AS DATE)))
                    AND (@Origen_Error IS NULL OR Origen_Error LIKE '%' + @Origen_Error + '%')
                ORDER BY Fecha_Error DESC, Id_Error DESC;

                SET @o_Num = 0;
                SET @o_Msg = '¡Errores SQL listados exitosamente!';
            END TRY
            BEGIN CATCH
                SET @o_Num = -1;
                SET @o_Msg = '¡Error interno del servidor!';
                SET @Linea_Error_Proc = ERROR_LINE();
                SET @Numero_Error_Proc = ERROR_NUMBER();
                SET @Mensaje_Error_Proc = ERROR_MESSAGE();
                EXEC sp_logs_errores_sql
                @Modo = 'INS',
                @Origen_Error = @Origen_Error_Proc,
                @Linea_Error = @Linea_Error_Proc,
                @Numero_Error = @Numero_Error_Proc,
                @Mensaje_Error = @Mensaje_Error_Proc;
            END CATCH
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
        SET @o_Msg = '¡No tiene permisos para realizar esta acción!';
    END
END
GO

