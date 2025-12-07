USE umDb
GO

/*
cls_grupos_secciones
*/

CREATE OR ALTER PROC usp_grupos_secciones
(
    @Id_Grupo_Seccion INT = NULL,
    @Id_Grupo INT = NULL,
    @Id_Seccion INT = NULL,
    @Id_Tipo_Vinculo INT = NULL,
    @Prioridad INT = NULL,
    @Fecha_Asignacion DATETIME = NULL,
    @Fecha_Desasignacion DATETIME = NULL,
    @Motivo_Desasignacion NVARCHAR(255) = NULL,
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
            /* FILTRAR POR ID GRUPO SECCION */
            IF(@Id_Tipo_Transaccion = 107)
                BEGIN
                    IF ISNULL(@Id_Grupo_Seccion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de grupo sección!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_grupos_secciones(NOLOCK) WHERE Id_Grupo_Seccion = @Id_Grupo_Seccion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El grupo sección no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Grupo_Seccion, Id_Grupo, Id_Seccion, Id_Tipo_Vinculo,
                                    Prioridad, Fecha_Asignacion, Fecha_Desasignacion, Motivo_Desasignacion,
                                    Activo, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM cls_grupos_secciones(NOLOCK)
                                WHERE Id_Grupo_Seccion = @Id_Grupo_Seccion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Grupo sección encontrado!';
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
            /* FILTRAR POR ID GRUPO */
            ELSE IF(@Id_Tipo_Transaccion = 108)
                BEGIN
                    IF ISNULL(@Id_Grupo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado el grupo para listar sus secciones!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Grupo_Seccion, Id_Grupo, Id_Seccion, Id_Tipo_Vinculo,
                                    Prioridad, Fecha_Asignacion, Fecha_Desasignacion, Motivo_Desasignacion,
                                    Activo, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM cls_grupos_secciones(NOLOCK)
                                WHERE Id_Grupo = @Id_Grupo AND Activo = 1
                                ORDER BY Prioridad, Fecha_Asignacion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Grupos secciones filtradas por grupo!';
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
            /* FILTRAR POR ID SECCION */
            ELSE IF(@Id_Tipo_Transaccion = 109)
                BEGIN
                    IF ISNULL(@Id_Seccion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado la sección para listar sus grupos!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Grupo_Seccion, Id_Grupo, Id_Seccion, Id_Tipo_Vinculo,
                                    Prioridad, Fecha_Asignacion, Fecha_Desasignacion, Motivo_Desasignacion,
                                    Activo, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM cls_grupos_secciones(NOLOCK)
                                WHERE Id_Seccion = @Id_Seccion AND Activo = 1
                                ORDER BY Prioridad, Fecha_Asignacion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Grupos secciones filtradas por sección!';
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
            /* AGREGAR GRUPO SECCION */
            ELSE IF(@Id_Tipo_Transaccion = 105)
                BEGIN
                    IF ISNULL(@Id_Grupo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un grupo!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_grupos(NOLOCK) WHERE Id_Grupo = @Id_Grupo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El grupo no existe!';
                        END
                    ELSE IF ISNULL(@Id_Seccion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar una sección!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_secciones(NOLOCK) WHERE Id_Seccion = @Id_Seccion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La sección no existe!';
                        END
                    ELSE IF ISNULL(@Id_Tipo_Vinculo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un tipo de vínculo!';
                        END
                    ELSE IF ISNULL(@Fecha_Asignacion, '') = ''
                        BEGIN
                            SET @Fecha_Asignacion = GETDATE();
                        END
                    ELSE IF (@Fecha_Desasignacion IS NOT NULL AND @Fecha_Desasignacion < @Fecha_Asignacion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de desasignación no puede ser anterior a la fecha de asignación!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM cls_grupos_secciones(NOLOCK) WHERE Id_Grupo = @Id_Grupo AND Id_Seccion = @Id_Seccion AND Activo = 1)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe un vínculo activo entre ese grupo y esa sección!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('AGREGANDO GRUPO SECCIÓN: Grupo ', @Id_Grupo, ' - Sección ', @Id_Seccion);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_AgregarGrupoSeccion
                            BEGIN TRY
                                INSERT INTO cls_grupos_secciones(
                                    Id_Grupo, Id_Seccion, Id_Tipo_Vinculo, Prioridad,
                                    Fecha_Asignacion, Fecha_Desasignacion, Motivo_Desasignacion,
                                    Activo, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion
                                ) VALUES (
                                    @Id_Grupo, @Id_Seccion, @Id_Tipo_Vinculo, ISNULL(@Prioridad, 1),
                                    @Fecha_Asignacion, @Fecha_Desasignacion, @Motivo_Desasignacion,
                                    ISNULL(@Activo, 1), @Fecha_Creacion, @Fecha_Modificacion, @Id_Creador, @Id_Modificador,
                                    @Id_Transaccion
                                );

                                COMMIT TRAN trx_AgregarGrupoSeccion;

                                SET @o_Num = SCOPE_IDENTITY();
                                SET @o_Msg = '¡Grupo sección agregado exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_AgregarGrupoSeccion;

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
            /* ACTUALIZAR GRUPO SECCION */
            ELSE IF(@Id_Tipo_Transaccion = 106)
                BEGIN
                    IF ISNULL(@Id_Grupo_Seccion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de grupo sección!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_grupos_secciones(NOLOCK) WHERE Id_Grupo_Seccion = @Id_Grupo_Seccion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El grupo sección no existe!';
                        END
                    ELSE IF (@Fecha_Desasignacion IS NOT NULL AND @Fecha_Asignacion IS NOT NULL AND @Fecha_Desasignacion < @Fecha_Asignacion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de desasignación no puede ser anterior a la fecha de asignación!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('ACTUALIZANDO GRUPO SECCIÓN ID: ', @Id_Grupo_Seccion);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_ActualizarGrupoSeccion
                            BEGIN TRY
                                UPDATE cls_grupos_secciones
                                SET Id_Grupo = COALESCE(@Id_Grupo, Id_Grupo),
                                    Id_Seccion = COALESCE(@Id_Seccion, Id_Seccion),
                                    Id_Tipo_Vinculo = COALESCE(@Id_Tipo_Vinculo, Id_Tipo_Vinculo),
                                    Prioridad = COALESCE(@Prioridad, Prioridad),
                                    Fecha_Asignacion = COALESCE(@Fecha_Asignacion, Fecha_Asignacion),
                                    Fecha_Desasignacion = COALESCE(@Fecha_Desasignacion, Fecha_Desasignacion),
                                    Motivo_Desasignacion = COALESCE(@Motivo_Desasignacion, Motivo_Desasignacion),
                                    Activo = COALESCE(@Activo, Activo),
                                    Fecha_Modificacion = @Fecha_Modificacion,
                                    Id_Modificador = @Id_Modificador,
                                    Id_Transaccion = @Id_Transaccion
                                WHERE Id_Grupo_Seccion = @Id_Grupo_Seccion;

                                COMMIT TRAN trx_ActualizarGrupoSeccion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Grupo sección actualizado exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_ActualizarGrupoSeccion;

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

