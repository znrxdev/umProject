USE umDb
GO

/*
cls_materias
*/

CREATE OR ALTER PROC usp_materias
(
    @Id_Materia INT = NULL,
    @Codigo_Materia VARCHAR(10) = NULL,
    @Nombre_Materia NVARCHAR(100) = NULL,
    @Activo BIT = NULL,
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
            /* FILTRAR POR ID MATERIA */
            IF(@Id_Tipo_Transaccion = 75)
                BEGIN
                    IF ISNULL(@Id_Materia, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de materia!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_materias(NOLOCK) WHERE Id_Materia = @Id_Materia)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La materia no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Materia,
                                    Codigo_Materia,
                                    Nombre_Materia,
                                    Fecha_Creacion,
                                    Fecha_Modificacion,
                                    Id_Creador,
                                    Id_Modificador,
                                    Id_Transaccion,
                                    Activo
                                FROM cls_materias(NOLOCK)
                                WHERE Id_Materia = @Id_Materia;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Materia encontrada!';
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
            /* FILTRAR POR CODIGO MATERIA */
            ELSE IF(@Id_Tipo_Transaccion = 76)
                BEGIN
                    IF ISNULL(@Codigo_Materia, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe ingresar un código de materia para buscar!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Materia,
                                    Codigo_Materia,
                                    Nombre_Materia,
                                    Fecha_Creacion,
                                    Fecha_Modificacion,
                                    Id_Creador,
                                    Id_Modificador,
                                    Id_Transaccion,
                                    Activo
                                FROM cls_materias(NOLOCK)
                                WHERE Codigo_Materia = @Codigo_Materia;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Materia encontrada!';
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
            /* FILTRAR POR NOMBRE MATERIA */
            ELSE IF(@Id_Tipo_Transaccion = 77)
                BEGIN
                    IF ISNULL(@Nombre_Materia, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe ingresar un nombre de materia para buscar!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Materia,
                                    Codigo_Materia,
                                    Nombre_Materia,
                                    Fecha_Creacion,
                                    Fecha_Modificacion,
                                    Id_Creador,
                                    Id_Modificador,
                                    Id_Transaccion,
                                    Activo
                                FROM cls_materias(NOLOCK)
                                WHERE Nombre_Materia LIKE '%' + @Nombre_Materia + '%'
                                AND Activo = 1
                                ORDER BY Nombre_Materia;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Materias encontradas!';
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
            /* AGREGAR MATERIA */
            ELSE IF(@Id_Tipo_Transaccion = 73)
                BEGIN
                    IF ISNULL(@Codigo_Materia, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El campo Código Materia no debe ir vacío!';
                        END
                    ELSE IF ISNULL(@Nombre_Materia, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El campo Nombre Materia no debe ir vacío!';
                        END
                    ELSE IF LEN(@Codigo_Materia) > 10
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El código de materia no puede exceder 10 caracteres!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM cls_materias(NOLOCK) WHERE Codigo_Materia = @Codigo_Materia)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe una materia con ese código!';
                        END
                    ELSE IF (@Activo) IS NULL
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe colocar un estado válido (activo/inactivo)!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('AGREGANDO MATERIA: ', @Nombre_Materia);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_AgregarMateria
                            BEGIN TRY
                                INSERT INTO cls_materias(
                                    Codigo_Materia,
                                    Nombre_Materia,
                                    Fecha_Creacion,
                                    Fecha_Modificacion,
                                    Id_Creador,
                                    Id_Modificador,
                                    Id_Transaccion,
                                    Activo
                                ) VALUES (
                                    @Codigo_Materia,
                                    @Nombre_Materia,
                                    @Fecha_Creacion,
                                    @Fecha_Modificacion,
                                    @Id_Creador,
                                    @Id_Modificador,
                                    @Id_Transaccion,
                                    @Activo
                                );

                                COMMIT TRAN trx_AgregarMateria;

                                SET @o_Num = SCOPE_IDENTITY();
                                SET @o_Msg = '¡Materia agregada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_AgregarMateria;

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
            /* ACTUALIZAR MATERIA */
            ELSE IF(@Id_Tipo_Transaccion = 74)
                BEGIN
                    IF ISNULL(@Id_Materia, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de materia!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_materias(NOLOCK) WHERE Id_Materia = @Id_Materia)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La materia no existe!';
                        END
                    ELSE IF (@Codigo_Materia IS NOT NULL AND LEN(@Codigo_Materia) > 10)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El código de materia no puede exceder 10 caracteres!';
                        END
                    ELSE IF (@Codigo_Materia IS NOT NULL AND EXISTS(SELECT 1 FROM cls_materias(NOLOCK) WHERE Codigo_Materia = @Codigo_Materia AND Id_Materia <> @Id_Materia))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe otra materia con ese código!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('ACTUALIZANDO MATERIA ID: ', @Id_Materia);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_ActualizarMateria
                            BEGIN TRY
                                UPDATE cls_materias
                                SET Codigo_Materia = COALESCE(@Codigo_Materia, Codigo_Materia),
                                    Nombre_Materia = COALESCE(@Nombre_Materia, Nombre_Materia),
                                    Fecha_Modificacion = @Fecha_Modificacion,
                                    Id_Modificador = @Id_Modificador,
                                    Id_Transaccion = @Id_Transaccion,
                                    Activo = COALESCE(@Activo, Activo)
                                WHERE Id_Materia = @Id_Materia;

                                COMMIT TRAN trx_ActualizarMateria;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Materia actualizada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_ActualizarMateria;

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

