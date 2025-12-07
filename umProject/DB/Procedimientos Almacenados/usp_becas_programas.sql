USE umDb
GO

/*
cls_becas_programas
*/

CREATE OR ALTER PROC usp_becas_programas
(
    @Id_Beca_Programa INT = NULL,
    @Codigo_Programa VARCHAR(30) = NULL,
    @Nombre_Programa NVARCHAR(150) = NULL,
    @Descripcion NVARCHAR(500) = NULL,
    @Id_Tipo_Programa INT = NULL,
    @Id_Modalidad_Programa INT = NULL,
    @Promedio_Minimo DECIMAL(5,2) = NULL,
    @Requiere_Sin_Sanciones BIT = NULL,
    @Id_Estado_Programa INT = NULL,
    @Fecha_Creacion DATETIME = NULL,
    @Fecha_Modificacion DATETIME = NULL,
    @Id_Creador INT = NULL,
    @Id_Modificador INT = NULL,
    @Id_Tipo_Transaccion INT,
    @Id_Transaccion INT = NULL,
    @Id_Sesion INT = NULL,
    @o_Num INT = NULL OUTPUT,
    @o_Msg NVARCHAR(255) = NULL OUTPUT
)
AS
BEGIN
    DECLARE @Permiso INT, @iConcepto NVARCHAR(255), @Linea_Error INT, @Numero_Error INT, @Mensaje_Error NVARCHAR(255), @Origen_Error NVARCHAR(50) = ERROR_PROCEDURE();
    SET @Fecha_Creacion = GETDATE();
    SET @Fecha_Modificacion = GETDATE();
    SET @Id_Creador = @Id_Sesion;
    SET @Id_Modificador = @Id_Sesion;
    SET @Permiso = dbo.fn_Validar_Permisos(@Id_Sesion, @Id_Tipo_Transaccion);

    IF(@Permiso = 1)
        BEGIN
            IF(ISNULL(@Codigo_Programa, '') = '')
                SET @Codigo_Programa = CONCAT('BP-', FORMAT(GETDATE(), 'yyyyMMddHHmmss'), '-', RIGHT(CONVERT(VARCHAR(8), ABS(CHECKSUM(NEWID()))), 4));

            /* LISTAR TODOS LOS PROGRAMAS DE BECAS */
            IF(@Id_Tipo_Transaccion = 184)
                BEGIN
                    BEGIN TRY
                        SELECT 
                            bp.Id_Beca_Programa,
                            bp.Codigo_Programa,
                            bp.Nombre_Programa,
                            bp.Descripcion,
                            bp.Id_Tipo_Programa,
                            bp.Id_Modalidad_Programa,
                            bp.Promedio_Minimo,
                            bp.Requiere_Sin_Sanciones,
                            bp.Id_Estado_Programa,
                            bp.Fecha_Creacion,
                            bp.Fecha_Modificacion,
                            bp.Id_Creador,
                            bp.Id_Modificador,
                            bp.Id_Transaccion,
                            bp.Codigo_Control,
                            -- Información relacionada para mostrar en la UI
                            tp.Nombre_Catalogo AS Nombre_Tipo_Programa,
                            mp.Nombre_Catalogo AS Nombre_Modalidad_Programa,
                            est.Nombre_Estado AS Nombre_Estado_Programa
                        FROM cls_becas_programas bp (NOLOCK)
                        LEFT JOIN cls_catalogos tp (NOLOCK) ON bp.Id_Tipo_Programa = tp.Id_Catalogo
                        LEFT JOIN cls_catalogos mp (NOLOCK) ON bp.Id_Modalidad_Programa = mp.Id_Catalogo
                        LEFT JOIN cls_estados est (NOLOCK) ON bp.Id_Estado_Programa = est.Id_Estado
                        ORDER BY bp.Fecha_Creacion DESC;

                        SET @o_Num = 0;
                        SET @o_Msg = '¡Programas de becas listados exitosamente!';
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
            /* FILTRAR POR ID TIPO PROGRAMA */
            ELSE IF(@Id_Tipo_Transaccion = 61)
                BEGIN
                    IF ISNULL(@Id_Tipo_Programa, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado el tipo de programa para listar los programas!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Beca_Programa,
                                    Codigo_Programa,
                                    Nombre_Programa,
                                    Descripcion,
                                    Id_Tipo_Programa,
                                    Id_Modalidad_Programa,
                                    Promedio_Minimo,
                                    Requiere_Sin_Sanciones,
                                    Id_Estado_Programa,
                                    Fecha_Creacion,
                                    Fecha_Modificacion,
                                    Id_Creador,
                                    Id_Modificador,
                                    Id_Transaccion,
                                    Codigo_Control,
                                    RowVersion
                                FROM cls_becas_programas(NOLOCK)
                                WHERE Id_Tipo_Programa = @Id_Tipo_Programa
                                ORDER BY Fecha_Creacion DESC;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Programas filtrados por tipo!';
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
            /* FILTRAR POR ID BECA PROGRAMA */
            ELSE IF(@Id_Tipo_Transaccion = 62)
                BEGIN
                    IF ISNULL(@Id_Beca_Programa, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de programa de beca!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_becas_programas(NOLOCK) WHERE Id_Beca_Programa = @Id_Beca_Programa)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El programa de beca no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    bp.Id_Beca_Programa,
                                    bp.Codigo_Programa,
                                    bp.Nombre_Programa,
                                    bp.Descripcion,
                                    bp.Id_Tipo_Programa,
                                    bp.Id_Modalidad_Programa,
                                    bp.Promedio_Minimo,
                                    bp.Requiere_Sin_Sanciones,
                                    bp.Id_Estado_Programa,
                                    bp.Fecha_Creacion,
                                    bp.Fecha_Modificacion,
                                    bp.Id_Creador,
                                    bp.Id_Modificador,
                                    bp.Id_Transaccion,
                                    bp.Codigo_Control,
                                    -- Información relacionada para mostrar en la UI
                                    tp.Nombre_Catalogo AS Nombre_Tipo_Programa,
                                    mp.Nombre_Catalogo AS Nombre_Modalidad_Programa,
                                    est.Nombre_Estado AS Nombre_Estado_Programa
                                FROM cls_becas_programas bp (NOLOCK)
                                LEFT JOIN cls_catalogos tp (NOLOCK) ON bp.Id_Tipo_Programa = tp.Id_Catalogo
                                LEFT JOIN cls_catalogos mp (NOLOCK) ON bp.Id_Modalidad_Programa = mp.Id_Catalogo
                                LEFT JOIN cls_estados est (NOLOCK) ON bp.Id_Estado_Programa = est.Id_Estado
                                WHERE bp.Id_Beca_Programa = @Id_Beca_Programa;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Programa de beca encontrado!';
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
            /* FILTRAR POR NOMBRE */
            ELSE IF(@Id_Tipo_Transaccion = 63)
                BEGIN
                    IF ISNULL(@Nombre_Programa, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe ingresar un nombre de programa para buscar!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Beca_Programa,
                                    Codigo_Programa,
                                    Nombre_Programa,
                                    Descripcion,
                                    Id_Tipo_Programa,
                                    Id_Modalidad_Programa,
                                    Promedio_Minimo,
                                    Requiere_Sin_Sanciones,
                                    Id_Estado_Programa,
                                    Fecha_Creacion,
                                    Fecha_Modificacion,
                                    Id_Creador,
                                    Id_Modificador,
                                    Id_Transaccion,
                                    Codigo_Control,
                                    RowVersion
                                FROM cls_becas_programas(NOLOCK)
                                WHERE Nombre_Programa LIKE '%' + @Nombre_Programa + '%'
                                ORDER BY Nombre_Programa;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Programas encontrados!';
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
            /* AGREGAR PROGRAMA DE BECA */
            ELSE IF(@Id_Tipo_Transaccion = 59)
                BEGIN
                    IF ISNULL(@Nombre_Programa, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El campo Nombre Programa no debe ir vacío!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM cls_becas_programas(NOLOCK) WHERE Codigo_Programa = @Codigo_Programa)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe un programa con ese código!';
                        END
                    ELSE IF (@Promedio_Minimo IS NOT NULL AND (@Promedio_Minimo < 0 OR @Promedio_Minimo > 100))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El promedio mínimo debe estar entre 0 y 100!';
                        END
                    ELSE
                        BEGIN
                            IF(ISNULL(@Id_Estado_Programa, 0) = 0)
                                SET @Id_Estado_Programa = 4; -- EN REVISION al crear

                            SET @iConcepto = CONCAT('AGREGANDO PROGRAMA DE BECA: ', @Nombre_Programa);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_AgregarBecaPrograma
                            BEGIN TRY
                                INSERT INTO cls_becas_programas(
                                    Codigo_Programa,
                                    Nombre_Programa,
                                    Descripcion,
                                    Id_Tipo_Programa,
                                    Id_Modalidad_Programa,
                                    Promedio_Minimo,
                                    Requiere_Sin_Sanciones,
                                    Id_Estado_Programa,
                                    Fecha_Creacion,
                                    Fecha_Modificacion,
                                    Id_Creador,
                                    Id_Modificador,
                                    Id_Transaccion
                                ) VALUES (
                                    @Codigo_Programa,
                                    @Nombre_Programa,
                                    @Descripcion,
                                    @Id_Tipo_Programa,
                                    @Id_Modalidad_Programa,
                                    @Promedio_Minimo,
                                    ISNULL(@Requiere_Sin_Sanciones, 1),
                                    @Id_Estado_Programa,
                                    @Fecha_Creacion,
                                    @Fecha_Modificacion,
                                    @Id_Creador,
                                    @Id_Modificador,
                                    @Id_Transaccion
                                );

                                COMMIT TRAN trx_AgregarBecaPrograma;

                                SET @o_Num = SCOPE_IDENTITY();
                                SET @o_Msg = '¡Programa de beca agregado exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_AgregarBecaPrograma;

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
            /* ACTUALIZAR PROGRAMA DE BECA */
            ELSE IF(@Id_Tipo_Transaccion = 60)
                BEGIN
                    IF ISNULL(@Id_Beca_Programa, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de programa de beca!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_becas_programas(NOLOCK) WHERE Id_Beca_Programa = @Id_Beca_Programa)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El programa de beca no existe!';
                        END
                    ELSE IF (@Codigo_Programa IS NOT NULL AND EXISTS(SELECT 1 FROM cls_becas_programas(NOLOCK) WHERE Codigo_Programa = @Codigo_Programa AND Id_Beca_Programa <> @Id_Beca_Programa))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe otro programa con ese código!';
                        END
                    ELSE IF (@Promedio_Minimo IS NOT NULL AND (@Promedio_Minimo < 0 OR @Promedio_Minimo > 100))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El promedio mínimo debe estar entre 0 y 100!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('ACTUALIZANDO PROGRAMA DE BECA ID: ', @Id_Beca_Programa);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_ActualizarBecaPrograma
                            BEGIN TRY
                                UPDATE cls_becas_programas
                                SET Codigo_Programa = COALESCE(@Codigo_Programa, Codigo_Programa),
                                    Nombre_Programa = COALESCE(@Nombre_Programa, Nombre_Programa),
                                    Descripcion = COALESCE(@Descripcion, Descripcion),
                                    Id_Tipo_Programa = COALESCE(@Id_Tipo_Programa, Id_Tipo_Programa),
                                    Id_Modalidad_Programa = COALESCE(@Id_Modalidad_Programa, Id_Modalidad_Programa),
                                    Promedio_Minimo = COALESCE(@Promedio_Minimo, Promedio_Minimo),
                                    Requiere_Sin_Sanciones = COALESCE(@Requiere_Sin_Sanciones, Requiere_Sin_Sanciones),
                                    Id_Estado_Programa = COALESCE(@Id_Estado_Programa, Id_Estado_Programa),
                                    Fecha_Modificacion = @Fecha_Modificacion,
                                    Id_Modificador = @Id_Modificador,
                                    Id_Transaccion = @Id_Transaccion
                                WHERE Id_Beca_Programa = @Id_Beca_Programa;

                                COMMIT TRAN trx_ActualizarBecaPrograma;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Programa de beca actualizado exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_ActualizarBecaPrograma;

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

