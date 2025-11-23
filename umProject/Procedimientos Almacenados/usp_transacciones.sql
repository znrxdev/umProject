USE umDb
GO
/*
usp_transacciones
Stored Procedure para gestionar las transacciones del sistema (auditoría)
*/

CREATE OR ALTER PROCEDURE usp_transacciones
(
    @Id_Transaccion INT = NULL,
    @Id_Tipo_Transaccion INT = NULL,
    @Concepto NVARCHAR(255) = NULL,
    @Id_Persona INT = NULL,
    @Id_Usuario INT = NULL,
    @Id_Contacto INT = NULL,
    @Id_Evaluacion INT = NULL,
    @Id_Solicitud_Beca INT = NULL,
    @Id_Inscripcion INT = NULL,
    @Id_Autor INT = NULL,
    @Fecha_Creacion DATETIME = NULL,
    @Completado BIT = NULL,
    @Tipo_Transaccion INT,
    @Id_Sesion INT = NULL,
    @Fecha_Inicio DATETIME = NULL,
    @Fecha_Fin DATETIME = NULL,
    @o_Msg NVARCHAR(255) = NULL OUTPUT,
    @o_Num INT = NULL OUTPUT
)
AS
BEGIN
    DECLARE @Permiso INT, @Linea_Error INT, @Numero_Error INT, @Mensaje_Error NVARCHAR(255), @Origen_Error NVARCHAR(50) = ERROR_PROCEDURE();
    
    SET @Permiso = dbo.fn_Validar_Permisos(@Id_Sesion, @Tipo_Transaccion);

    IF (@Permiso = 1)
    BEGIN
        /* LISTAR AUDITORÍA DEL SISTEMA */
        IF (@Tipo_Transaccion = 144 OR @Tipo_Transaccion = 153)
        BEGIN
            BEGIN TRY
                SELECT 
                    t.Id_Transaccion,
                    t.Id_Tipo_Transaccion,
                    tt.Nombre_Tipo_Transaccion,
                    t.Concepto,
                    t.Id_Persona,
                    p.Primer_Nombre + ' ' + ISNULL(p.Segundo_Nombre, '') + ' ' + p.Primer_Apellido + ' ' + ISNULL(p.Segundo_Apellido, '') AS Nombre_Persona,
                    t.Id_Usuario,
                    u.Usuario AS Nombre_Usuario,
                    t.Id_Contacto,
                    t.Id_Evaluacion,
                    t.Id_Solicitud_Beca,
                    t.Id_Inscripcion,
                    t.Id_Autor,
                    ua.Usuario AS Nombre_Autor,
                    CONVERT(VARCHAR(19), t.Fecha_Creacion, 120) AS Fecha_Creacion,
                    t.Completado,
                    CASE 
                        WHEN t.Id_Persona IS NOT NULL THEN 'Persona'
                        WHEN t.Id_Usuario IS NOT NULL THEN 'Usuario'
                        WHEN t.Id_Contacto IS NOT NULL THEN 'Contacto'
                        WHEN t.Id_Evaluacion IS NOT NULL THEN 'Evaluación'
                        WHEN t.Id_Solicitud_Beca IS NOT NULL THEN 'Solicitud Beca'
                        WHEN t.Id_Inscripcion IS NOT NULL THEN 'Inscripción'
                        ELSE 'Sistema'
                    END AS Tipo_Entidad
                FROM tbl_transacciones (NOLOCK) t
                LEFT JOIN cls_tipos_transacciones (NOLOCK) tt ON t.Id_Tipo_Transaccion = tt.Id_Tipo_Transaccion
                LEFT JOIN tbl_personas (NOLOCK) p ON t.Id_Persona = p.Id_Persona
                LEFT JOIN tbl_usuarios (NOLOCK) u ON t.Id_Usuario = u.Id_Usuario
                LEFT JOIN tbl_usuarios (NOLOCK) ua ON t.Id_Autor = ua.Id_Usuario
                WHERE (@Fecha_Inicio IS NULL OR t.Fecha_Creacion >= @Fecha_Inicio)
                    AND (@Fecha_Fin IS NULL OR t.Fecha_Creacion <= DATEADD(DAY, 1, CAST(@Fecha_Fin AS DATE)))
                ORDER BY t.Fecha_Creacion DESC, t.Id_Transaccion DESC;

                SET @o_Num = 0;
                SET @o_Msg = '¡Auditoría del sistema listada exitosamente!';
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

