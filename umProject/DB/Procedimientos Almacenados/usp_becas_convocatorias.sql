USE umDb
GO

/*
tbl_becas_convocatorias
*/

CREATE OR ALTER PROC usp_becas_convocatorias
(
    @Id_Convocatoria INT = NULL,
    @Codigo_Convocatoria VARCHAR(30) = NULL,
    @Id_Programa INT = NULL,
    @Id_Periodo INT = NULL,
    @Nombre_Convocatoria NVARCHAR(150) = NULL,
    @Descripcion NVARCHAR(500) = NULL,
    @Cupo_Total INT = NULL,
    @Cupo_Reservado INT = NULL,
    @Cupo_Asignado INT = NULL,
    @Fecha_Inicio DATE = NULL,
    @Fecha_Publicacion DATE = NULL,
    @Fecha_Fin DATE = NULL,
    @Fecha_Cierre DATE = NULL,
    @Requiere_Postulacion_Linea BIT = NULL,
    @Documentacion_Obligatoria NVARCHAR(500) = NULL,
    @Url_Convocatoria NVARCHAR(255) = NULL,
    @Observaciones NVARCHAR(500) = NULL,
    @Id_Estado INT = NULL,
    @Id_Estado_Publicacion INT = NULL,
    @Id_Usuario_Publicador INT = NULL,
    @Id_Usuario_Cierre INT = NULL,
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
            /* FILTRAR POR ID CONVOCATORIA */
            IF(@Id_Tipo_Transaccion = 84)
                BEGIN
                    IF ISNULL(@Id_Convocatoria, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de convocatoria!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_becas_convocatorias(NOLOCK) WHERE Id_Convocatoria = @Id_Convocatoria)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La convocatoria no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Convocatoria, Codigo_Convocatoria, Id_Programa, Id_Periodo,
                                    Nombre_Convocatoria, Descripcion, Cupo_Total, Cupo_Reservado,
                                    Cupo_Asignado, Fecha_Inicio, Fecha_Publicacion, Fecha_Fin,
                                    Fecha_Cierre, Requiere_Postulacion_Linea, Documentacion_Obligatoria,
                                    Url_Convocatoria, Observaciones, Id_Estado, Id_Estado_Publicacion,
                                    Id_Creador, Id_Modificador, Id_Usuario_Publicador, Id_Usuario_Cierre,
                                    Hash_Convocatoria, Codigo_Control, Fecha_Creacion, Fecha_Modificacion,
                                    RowVersion
                                FROM tbl_becas_convocatorias(NOLOCK)
                                WHERE Id_Convocatoria = @Id_Convocatoria;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Convocatoria encontrada!';
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
            /* FILTRAR POR ID PROGRAMA */
            ELSE IF(@Id_Tipo_Transaccion = 85)
                BEGIN
                    IF ISNULL(@Id_Programa, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado el programa para listar las convocatorias!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Convocatoria, Codigo_Convocatoria, Id_Programa, Id_Periodo,
                                    Nombre_Convocatoria, Descripcion, Cupo_Total, Cupo_Reservado,
                                    Cupo_Asignado, Fecha_Inicio, Fecha_Publicacion, Fecha_Fin,
                                    Fecha_Cierre, Requiere_Postulacion_Linea, Documentacion_Obligatoria,
                                    Url_Convocatoria, Observaciones, Id_Estado, Id_Estado_Publicacion,
                                    Id_Creador, Id_Modificador, Id_Usuario_Publicador, Id_Usuario_Cierre,
                                    Hash_Convocatoria, Codigo_Control, Fecha_Creacion, Fecha_Modificacion,
                                    RowVersion
                                FROM tbl_becas_convocatorias(NOLOCK)
                                WHERE Id_Programa = @Id_Programa
                                ORDER BY Fecha_Creacion DESC;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Convocatorias filtradas por programa!';
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
            /* FILTRAR POR ID PERIODO */
            ELSE IF(@Id_Tipo_Transaccion = 86)
                BEGIN
                    IF ISNULL(@Id_Periodo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado el período para listar las convocatorias!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Convocatoria, Codigo_Convocatoria, Id_Programa, Id_Periodo,
                                    Nombre_Convocatoria, Descripcion, Cupo_Total, Cupo_Reservado,
                                    Cupo_Asignado, Fecha_Inicio, Fecha_Publicacion, Fecha_Fin,
                                    Fecha_Cierre, Requiere_Postulacion_Linea, Documentacion_Obligatoria,
                                    Url_Convocatoria, Observaciones, Id_Estado, Id_Estado_Publicacion,
                                    Id_Creador, Id_Modificador, Id_Usuario_Publicador, Id_Usuario_Cierre,
                                    Hash_Convocatoria, Codigo_Control, Fecha_Creacion, Fecha_Modificacion,
                                    RowVersion
                                FROM tbl_becas_convocatorias(NOLOCK)
                                WHERE Id_Periodo = @Id_Periodo
                                ORDER BY Fecha_Creacion DESC;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Convocatorias filtradas por período!';
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
            /* AGREGAR CONVOCATORIA */
            ELSE IF(@Id_Tipo_Transaccion = 82)
                BEGIN
                    IF ISNULL(@Id_Programa, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un programa de beca!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_becas_programas(NOLOCK) WHERE Id_Beca_Programa = @Id_Programa)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El programa de beca no existe!';
                        END
                    ELSE IF ISNULL(@Id_Periodo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un período académico!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_periodos_academicos(NOLOCK) WHERE Id_Periodo = @Id_Periodo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El período académico no existe!';
                        END
                    ELSE IF ISNULL(@Nombre_Convocatoria, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El campo Nombre Convocatoria no debe ir vacío!';
                        END
                    ELSE IF ISNULL(@Cupo_Total, 0) = 0 OR @Cupo_Total < 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe especificar un cupo total válido!';
                        END
                    ELSE IF ISNULL(@Fecha_Inicio, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe especificar la fecha de inicio!';
                        END
                    ELSE IF ISNULL(@Fecha_Publicacion, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe especificar la fecha de publicación!';
                        END
                    ELSE IF ISNULL(@Fecha_Fin, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe especificar la fecha de fin!';
                        END
                    ELSE IF @Fecha_Fin < @Fecha_Inicio
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de fin debe ser mayor o igual que la fecha de inicio!';
                        END
                    ELSE IF (@Fecha_Cierre IS NOT NULL AND @Fecha_Cierre < @Fecha_Inicio)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de cierre no puede ser anterior a la fecha de inicio!';
                        END
                    ELSE IF ISNULL(@Id_Estado, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un estado a la convocatoria!';
                        END
                    ELSE IF ISNULL(@Id_Estado_Publicacion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un estado de publicación!';
                        END
                    ELSE IF (@Cupo_Reservado IS NOT NULL AND @Cupo_Reservado > @Cupo_Total)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El cupo reservado no puede ser mayor que el cupo total!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM tbl_becas_convocatorias(NOLOCK) WHERE Codigo_Convocatoria = @Codigo_Convocatoria)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe una convocatoria con ese código!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('AGREGANDO CONVOCATORIA DE BECA: ', @Nombre_Convocatoria);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_AgregarConvocatoria
                            BEGIN TRY
                                INSERT INTO tbl_becas_convocatorias(
                                    Codigo_Convocatoria, Id_Programa, Id_Periodo, Nombre_Convocatoria,
                                    Descripcion, Cupo_Total, Cupo_Reservado, Cupo_Asignado,
                                    Fecha_Inicio, Fecha_Publicacion, Fecha_Fin, Fecha_Cierre,
                                    Requiere_Postulacion_Linea, Documentacion_Obligatoria,
                                    Url_Convocatoria, Observaciones, Id_Estado, Id_Estado_Publicacion,
                                    Id_Creador, Id_Modificador, Id_Usuario_Publicador, Id_Usuario_Cierre,
                                    Id_Transaccion, Fecha_Creacion, Fecha_Modificacion
                                ) VALUES (
                                    @Codigo_Convocatoria, @Id_Programa, @Id_Periodo, @Nombre_Convocatoria,
                                    @Descripcion, @Cupo_Total, ISNULL(@Cupo_Reservado, 0), ISNULL(@Cupo_Asignado, 0),
                                    @Fecha_Inicio, @Fecha_Publicacion, @Fecha_Fin, @Fecha_Cierre,
                                    ISNULL(@Requiere_Postulacion_Linea, 1), @Documentacion_Obligatoria,
                                    @Url_Convocatoria, @Observaciones, @Id_Estado, @Id_Estado_Publicacion,
                                    @Id_Creador, @Id_Modificador, @Id_Usuario_Publicador, @Id_Usuario_Cierre,
                                    @Id_Transaccion, @Fecha_Creacion, @Fecha_Modificacion
                                );

                                COMMIT TRAN trx_AgregarConvocatoria;

                                SET @o_Num = SCOPE_IDENTITY();
                                SET @o_Msg = '¡Convocatoria agregada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_AgregarConvocatoria;

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
            /* ACTUALIZAR CONVOCATORIA */
            ELSE IF(@Id_Tipo_Transaccion = 83)
                BEGIN
                    IF ISNULL(@Id_Convocatoria, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de convocatoria!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_becas_convocatorias(NOLOCK) WHERE Id_Convocatoria = @Id_Convocatoria)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La convocatoria no existe!';
                        END
                    ELSE IF (@Fecha_Inicio IS NOT NULL AND @Fecha_Fin IS NOT NULL AND @Fecha_Fin < @Fecha_Inicio)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de fin debe ser mayor o igual que la fecha de inicio!';
                        END
                    ELSE IF (@Fecha_Cierre IS NOT NULL AND @Fecha_Inicio IS NOT NULL AND @Fecha_Cierre < @Fecha_Inicio)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de cierre no puede ser anterior a la fecha de inicio!';
                        END
                    ELSE IF (@Cupo_Reservado IS NOT NULL AND @Cupo_Total IS NOT NULL AND @Cupo_Reservado > @Cupo_Total)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El cupo reservado no puede ser mayor que el cupo total!';
                        END
                    ELSE IF (@Codigo_Convocatoria IS NOT NULL AND EXISTS(SELECT 1 FROM tbl_becas_convocatorias(NOLOCK) WHERE Codigo_Convocatoria = @Codigo_Convocatoria AND Id_Convocatoria <> @Id_Convocatoria))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe otra convocatoria con ese código!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('ACTUALIZANDO CONVOCATORIA ID: ', @Id_Convocatoria);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_ActualizarConvocatoria
                            BEGIN TRY
                                UPDATE tbl_becas_convocatorias
                                SET Codigo_Convocatoria = COALESCE(@Codigo_Convocatoria, Codigo_Convocatoria),
                                    Id_Programa = COALESCE(@Id_Programa, Id_Programa),
                                    Id_Periodo = COALESCE(@Id_Periodo, Id_Periodo),
                                    Nombre_Convocatoria = COALESCE(@Nombre_Convocatoria, Nombre_Convocatoria),
                                    Descripcion = COALESCE(@Descripcion, Descripcion),
                                    Cupo_Total = COALESCE(@Cupo_Total, Cupo_Total),
                                    Cupo_Reservado = COALESCE(@Cupo_Reservado, Cupo_Reservado),
                                    Cupo_Asignado = COALESCE(@Cupo_Asignado, Cupo_Asignado),
                                    Fecha_Inicio = COALESCE(@Fecha_Inicio, Fecha_Inicio),
                                    Fecha_Publicacion = COALESCE(@Fecha_Publicacion, Fecha_Publicacion),
                                    Fecha_Fin = COALESCE(@Fecha_Fin, Fecha_Fin),
                                    Fecha_Cierre = COALESCE(@Fecha_Cierre, Fecha_Cierre),
                                    Requiere_Postulacion_Linea = COALESCE(@Requiere_Postulacion_Linea, Requiere_Postulacion_Linea),
                                    Documentacion_Obligatoria = COALESCE(@Documentacion_Obligatoria, Documentacion_Obligatoria),
                                    Url_Convocatoria = COALESCE(@Url_Convocatoria, Url_Convocatoria),
                                    Observaciones = COALESCE(@Observaciones, Observaciones),
                                    Id_Estado = COALESCE(@Id_Estado, Id_Estado),
                                    Id_Estado_Publicacion = COALESCE(@Id_Estado_Publicacion, Id_Estado_Publicacion),
                                    Id_Usuario_Publicador = COALESCE(@Id_Usuario_Publicador, Id_Usuario_Publicador),
                                    Id_Usuario_Cierre = COALESCE(@Id_Usuario_Cierre, Id_Usuario_Cierre),
                                    Fecha_Modificacion = @Fecha_Modificacion,
                                    Id_Modificador = @Id_Modificador,
                                    Id_Transaccion = @Id_Transaccion
                                WHERE Id_Convocatoria = @Id_Convocatoria;

                                COMMIT TRAN trx_ActualizarConvocatoria;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Convocatoria actualizada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_ActualizarConvocatoria;

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

