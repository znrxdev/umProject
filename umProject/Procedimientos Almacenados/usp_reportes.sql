USE umDb
GO
/*
usp_reportes
Stored Procedure para generar reportes del sistema
*/

CREATE OR ALTER PROCEDURE usp_reportes
(
    @Id_Tipo_Transaccion INT,
    @Id_Sesion INT = NULL,
    @Fecha_Inicio DATETIME = NULL,
    @Fecha_Fin DATETIME = NULL,
    @o_Msg NVARCHAR(255) = NULL OUTPUT,
    @o_Num INT = NULL OUTPUT
)
AS
BEGIN
    DECLARE @Permiso INT, @Linea_Error INT, @Numero_Error INT, @Mensaje_Error NVARCHAR(255), @Origen_Error NVARCHAR(50) = ERROR_PROCEDURE();
    
    SET @Permiso = dbo.fn_Validar_Permisos(@Id_Sesion, @Id_Tipo_Transaccion);

    IF (@Permiso = 1)
    BEGIN
        /* REPORTE DE USUARIOS ACTIVOS */
        IF (@Id_Tipo_Transaccion = 151)
        BEGIN
            BEGIN TRY
                SELECT 
                    u.Id_Usuario,
                    u.Usuario,
                    u.Id_Persona,
                    p.Primer_Nombre,
                    p.Segundo_Nombre,
                    p.Primer_Apellido,
                    p.Segundo_Apellido,
                    p.Primer_Nombre + ' ' + ISNULL(p.Segundo_Nombre + ' ', '') + p.Primer_Apellido + ' ' + ISNULL(p.Segundo_Apellido, '') AS Nombre_Completo,
                    p.Valor_Documento,
                    td.Nombre_Catalogo AS Tipo_Documento,
                    p.Fecha_Nacimiento,
                    g.Nombre_Catalogo AS Genero,
                    n.Nombre_Catalogo AS Nacionalidad,
                    ec.Nombre_Catalogo AS Estado_Civil,
                    CONVERT(VARCHAR(19), u.Fecha_Creacion, 120) AS Fecha_Creacion_Usuario,
                    CONVERT(VARCHAR(19), u.Fecha_Modificacion, 120) AS Fecha_Modificacion_Usuario,
                    CONVERT(VARCHAR(19), u.Ultima_Sesion, 120) AS Ultima_Sesion,
                    CONVERT(VARCHAR(19), u.Ultimo_Cambio_Contrasena, 120) AS Ultimo_Cambio_Contrasena,
                    e.Nombre_Estado AS Estado_Usuario,
                    CONVERT(VARCHAR(19), p.Fecha_Creacion, 120) AS Fecha_Creacion_Persona,
                    CONVERT(VARCHAR(19), p.Fecha_Modificacion, 120) AS Fecha_Modificacion_Persona
                FROM tbl_usuarios (NOLOCK) u
                INNER JOIN tbl_personas (NOLOCK) p ON u.Id_Persona = p.Id_Persona
                LEFT JOIN cls_catalogos (NOLOCK) td ON p.Id_Tipo_Documento = td.Id_Catalogo
                LEFT JOIN cls_catalogos (NOLOCK) g ON p.Id_Genero_Persona = g.Id_Catalogo
                LEFT JOIN cls_catalogos (NOLOCK) n ON p.Id_Nacionalidad = n.Id_Catalogo
                LEFT JOIN cls_catalogos (NOLOCK) ec ON p.Id_Estado_Civil = ec.Id_Catalogo
                LEFT JOIN cls_estados (NOLOCK) e ON u.Id_Estado = e.Id_Estado
                WHERE u.Id_Estado = 1 -- ACTIVO
                    AND p.Id_Estado = 1 -- PERSONA ACTIVA
                    AND (@Fecha_Inicio IS NULL OR u.Fecha_Creacion >= @Fecha_Inicio)
                    AND (@Fecha_Fin IS NULL OR u.Fecha_Creacion <= DATEADD(DAY, 1, CAST(@Fecha_Fin AS DATE)))
                ORDER BY u.Fecha_Creacion DESC, u.Id_Usuario DESC;

                SET @o_Num = 0;
                SET @o_Msg = '¡Reporte de usuarios activos generado exitosamente!';
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
        /* REPORTE DE USUARIOS INACTIVOS */
        ELSE IF (@Id_Tipo_Transaccion = 152)
        BEGIN
            BEGIN TRY
                SELECT 
                    u.Id_Usuario,
                    u.Usuario,
                    u.Id_Persona,
                    p.Primer_Nombre,
                    p.Segundo_Nombre,
                    p.Primer_Apellido,
                    p.Segundo_Apellido,
                    p.Primer_Nombre + ' ' + ISNULL(p.Segundo_Nombre + ' ', '') + p.Primer_Apellido + ' ' + ISNULL(p.Segundo_Apellido, '') AS Nombre_Completo,
                    p.Valor_Documento,
                    td.Nombre_Catalogo AS Tipo_Documento,
                    p.Fecha_Nacimiento,
                    g.Nombre_Catalogo AS Genero,
                    n.Nombre_Catalogo AS Nacionalidad,
                    ec.Nombre_Catalogo AS Estado_Civil,
                    CONVERT(VARCHAR(19), u.Fecha_Creacion, 120) AS Fecha_Creacion_Usuario,
                    CONVERT(VARCHAR(19), u.Fecha_Modificacion, 120) AS Fecha_Modificacion_Usuario,
                    CONVERT(VARCHAR(19), u.Ultima_Sesion, 120) AS Ultima_Sesion,
                    CONVERT(VARCHAR(19), u.Ultimo_Cambio_Contrasena, 120) AS Ultimo_Cambio_Contrasena,
                    e.Nombre_Estado AS Estado_Usuario,
                    CONVERT(VARCHAR(19), p.Fecha_Creacion, 120) AS Fecha_Creacion_Persona,
                    CONVERT(VARCHAR(19), p.Fecha_Modificacion, 120) AS Fecha_Modificacion_Persona
                FROM tbl_usuarios (NOLOCK) u
                INNER JOIN tbl_personas (NOLOCK) p ON u.Id_Persona = p.Id_Persona
                LEFT JOIN cls_catalogos (NOLOCK) td ON p.Id_Tipo_Documento = td.Id_Catalogo
                LEFT JOIN cls_catalogos (NOLOCK) g ON p.Id_Genero_Persona = g.Id_Catalogo
                LEFT JOIN cls_catalogos (NOLOCK) n ON p.Id_Nacionalidad = n.Id_Catalogo
                LEFT JOIN cls_catalogos (NOLOCK) ec ON p.Id_Estado_Civil = ec.Id_Catalogo
                LEFT JOIN cls_estados (NOLOCK) e ON u.Id_Estado = e.Id_Estado
                WHERE u.Id_Estado <> 1 -- INACTIVO (diferente de ACTIVO)
                    AND (@Fecha_Inicio IS NULL OR u.Fecha_Creacion >= @Fecha_Inicio)
                    AND (@Fecha_Fin IS NULL OR u.Fecha_Creacion <= DATEADD(DAY, 1, CAST(@Fecha_Fin AS DATE)))
                ORDER BY u.Fecha_Creacion DESC, u.Id_Usuario DESC;

                SET @o_Num = 0;
                SET @o_Msg = '¡Reporte de usuarios inactivos generado exitosamente!';
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

