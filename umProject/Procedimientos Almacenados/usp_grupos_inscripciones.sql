USE umDb
GO

/*
tbl_grupos_inscripciones
*/

CREATE OR ALTER PROC usp_grupos_inscripciones
(
    @Id_Grupo_Inscripcion INT = NULL,
    @Id_Grupo INT = NULL,
    @Id_Inscripcion INT = NULL,
    @Id_Rol_Grupo INT = NULL,
    @Id_Estado INT = NULL,
    @Fecha_Asignacion DATETIME = NULL,
    @Fecha_Baja DATETIME = NULL,
    @Motivo_Baja NVARCHAR(255) = NULL,
    @Es_Delegado BIT = NULL,
    @Observaciones NVARCHAR(255) = NULL,
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
            /* FILTRAR POR ID GRUPO INSCRIPCION */
            IF(@Id_Tipo_Transaccion = 117)
                BEGIN
                    IF ISNULL(@Id_Grupo_Inscripcion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de grupo inscripción!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_grupos_inscripciones(NOLOCK) WHERE Id_Grupo_Inscripcion = @Id_Grupo_Inscripcion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El grupo inscripción no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Grupo_Inscripcion, Id_Grupo, Id_Inscripcion, Id_Rol_Grupo,
                                    Id_Estado, Fecha_Asignacion, Fecha_Baja, Motivo_Baja,
                                    Es_Delegado, Observaciones, Activo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM tbl_grupos_inscripciones(NOLOCK)
                                WHERE Id_Grupo_Inscripcion = @Id_Grupo_Inscripcion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Grupo inscripción encontrado!';
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
            ELSE IF(@Id_Tipo_Transaccion = 118)
                BEGIN
                    IF ISNULL(@Id_Grupo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado el grupo para listar sus inscripciones!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Grupo_Inscripcion, Id_Grupo, Id_Inscripcion, Id_Rol_Grupo,
                                    Id_Estado, Fecha_Asignacion, Fecha_Baja, Motivo_Baja,
                                    Es_Delegado, Observaciones, Activo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM tbl_grupos_inscripciones(NOLOCK)
                                WHERE Id_Grupo = @Id_Grupo AND Activo = 1
                                ORDER BY Fecha_Asignacion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Grupos inscripciones filtradas por grupo!';
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
            /* FILTRAR POR ID INSCRIPCION */
            ELSE IF(@Id_Tipo_Transaccion = 119)
                BEGIN
                    IF ISNULL(@Id_Inscripcion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado la inscripción para listar sus grupos!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Grupo_Inscripcion, Id_Grupo, Id_Inscripcion, Id_Rol_Grupo,
                                    Id_Estado, Fecha_Asignacion, Fecha_Baja, Motivo_Baja,
                                    Es_Delegado, Observaciones, Activo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM tbl_grupos_inscripciones(NOLOCK)
                                WHERE Id_Inscripcion = @Id_Inscripcion AND Activo = 1
                                ORDER BY Fecha_Asignacion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Grupos inscripciones filtradas por inscripción!';
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
            /* AGREGAR GRUPO INSCRIPCION */
            ELSE IF(@Id_Tipo_Transaccion = 115)
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
                    ELSE IF ISNULL(@Id_Inscripcion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar una inscripción!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_inscripciones(NOLOCK) WHERE Id_Inscripcion = @Id_Inscripcion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La inscripción no existe!';
                        END
                    ELSE IF ISNULL(@Id_Estado, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un estado!';
                        END
                    ELSE IF ISNULL(@Fecha_Asignacion, '') = ''
                        BEGIN
                            SET @Fecha_Asignacion = GETDATE();
                        END
                    ELSE IF (@Fecha_Baja IS NOT NULL AND @Fecha_Baja < @Fecha_Asignacion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de baja no puede ser anterior a la fecha de asignación!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM tbl_grupos_inscripciones(NOLOCK) WHERE Id_Grupo = @Id_Grupo AND Id_Inscripcion = @Id_Inscripcion AND Activo = 1)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe un vínculo activo entre ese grupo y esa inscripción!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('AGREGANDO GRUPO INSCRIPCIÓN: Grupo ', @Id_Grupo, ' - Inscripción ', @Id_Inscripcion);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_AgregarGrupoInscripcion
                            BEGIN TRY
                                INSERT INTO tbl_grupos_inscripciones(
                                    Id_Grupo, Id_Inscripcion, Id_Rol_Grupo, Id_Estado,
                                    Fecha_Asignacion, Fecha_Baja, Motivo_Baja, Es_Delegado,
                                    Observaciones, Activo, Fecha_Creacion, Fecha_Modificacion,
                                    Id_Creador, Id_Modificador, Id_Transaccion
                                ) VALUES (
                                    @Id_Grupo, @Id_Inscripcion, @Id_Rol_Grupo, @Id_Estado,
                                    @Fecha_Asignacion, @Fecha_Baja, @Motivo_Baja, ISNULL(@Es_Delegado, 0),
                                    @Observaciones, ISNULL(@Activo, 1), @Fecha_Creacion, @Fecha_Modificacion,
                                    @Id_Creador, @Id_Modificador, @Id_Transaccion
                                );

                                COMMIT TRAN trx_AgregarGrupoInscripcion;

                                SET @o_Num = SCOPE_IDENTITY();
                                SET @o_Msg = '¡Grupo inscripción agregado exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_AgregarGrupoInscripcion;

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
            /* ACTUALIZAR GRUPO INSCRIPCION */
            ELSE IF(@Id_Tipo_Transaccion = 116)
                BEGIN
                    IF ISNULL(@Id_Grupo_Inscripcion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de grupo inscripción!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_grupos_inscripciones(NOLOCK) WHERE Id_Grupo_Inscripcion = @Id_Grupo_Inscripcion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El grupo inscripción no existe!';
                        END
                    ELSE IF (@Fecha_Baja IS NOT NULL AND @Fecha_Asignacion IS NOT NULL AND @Fecha_Baja < @Fecha_Asignacion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de baja no puede ser anterior a la fecha de asignación!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('ACTUALIZANDO GRUPO INSCRIPCIÓN ID: ', @Id_Grupo_Inscripcion);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_ActualizarGrupoInscripcion
                            BEGIN TRY
                                UPDATE tbl_grupos_inscripciones
                                SET Id_Grupo = COALESCE(@Id_Grupo, Id_Grupo),
                                    Id_Inscripcion = COALESCE(@Id_Inscripcion, Id_Inscripcion),
                                    Id_Rol_Grupo = COALESCE(@Id_Rol_Grupo, Id_Rol_Grupo),
                                    Id_Estado = COALESCE(@Id_Estado, Id_Estado),
                                    Fecha_Asignacion = COALESCE(@Fecha_Asignacion, Fecha_Asignacion),
                                    Fecha_Baja = COALESCE(@Fecha_Baja, Fecha_Baja),
                                    Motivo_Baja = COALESCE(@Motivo_Baja, Motivo_Baja),
                                    Es_Delegado = COALESCE(@Es_Delegado, Es_Delegado),
                                    Observaciones = COALESCE(@Observaciones, Observaciones),
                                    Activo = COALESCE(@Activo, Activo),
                                    Fecha_Modificacion = @Fecha_Modificacion,
                                    Id_Modificador = @Id_Modificador,
                                    Id_Transaccion = @Id_Transaccion
                                WHERE Id_Grupo_Inscripcion = @Id_Grupo_Inscripcion;

                                COMMIT TRAN trx_ActualizarGrupoInscripcion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Grupo inscripción actualizado exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_ActualizarGrupoInscripcion;

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

