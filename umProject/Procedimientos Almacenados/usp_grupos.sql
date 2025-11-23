USE umDb
GO

/*
tbl_grupos
*/

CREATE OR ALTER PROC usp_grupos
(
    @Id_Grupo INT = NULL,
    @Codigo_Grupo VARCHAR(20) = NULL,
    @Nombre_Grupo NVARCHAR(100) = NULL,
    @Id_Periodo INT = NULL,
    @Id_Tipo_Grupo INT = NULL,
    @Id_Coordinador INT = NULL,
    @Id_Jornada INT = NULL,
    @Id_Estado INT = NULL,
    @Fecha_Cierre DATETIME = NULL,
    @Observaciones NVARCHAR(255) = NULL,
    @Codigo_Seguimiento VARCHAR(30) = NULL,
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
            /* FILTRAR POR ID GRUPO */
            IF(@Id_Tipo_Transaccion = 103)
                BEGIN
                    IF ISNULL(@Id_Grupo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de grupo!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_grupos(NOLOCK) WHERE Id_Grupo = @Id_Grupo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El grupo no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Grupo, Codigo_Grupo, Nombre_Grupo, Id_Periodo,
                                    Id_Tipo_Grupo, Id_Coordinador, Id_Jornada, Id_Estado,
                                    Fecha_Cierre, Observaciones, Codigo_Seguimiento, Activo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM tbl_grupos(NOLOCK)
                                WHERE Id_Grupo = @Id_Grupo;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Grupo encontrado!';
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
            ELSE IF(@Id_Tipo_Transaccion = 104)
                BEGIN
                    IF ISNULL(@Id_Periodo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado el período para listar los grupos!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Grupo, Codigo_Grupo, Nombre_Grupo, Id_Periodo,
                                    Id_Tipo_Grupo, Id_Coordinador, Id_Jornada, Id_Estado,
                                    Fecha_Cierre, Observaciones, Codigo_Seguimiento, Activo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM tbl_grupos(NOLOCK)
                                WHERE Id_Periodo = @Id_Periodo AND Activo = 1
                                ORDER BY Codigo_Grupo;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Grupos filtrados por período!';
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
            /* AGREGAR GRUPO */
            ELSE IF(@Id_Tipo_Transaccion = 101)
                BEGIN
                    IF ISNULL(@Id_Periodo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un período académico!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_periodos_academicos(NOLOCK) WHERE Id_Periodo = @Id_Periodo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El período académico no existe!';
                        END
                    ELSE IF ISNULL(@Id_Tipo_Grupo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un tipo de grupo!';
                        END
                    ELSE IF ISNULL(@Id_Estado, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un estado!';
                        END
                    ELSE IF ISNULL(@Codigo_Grupo, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El campo Código Grupo no debe ir vacío!';
                        END
                    ELSE IF ISNULL(@Nombre_Grupo, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El campo Nombre Grupo no debe ir vacío!';
                        END
                    ELSE IF ISNULL(@Codigo_Seguimiento, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El campo Código Seguimiento no debe ir vacío!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM tbl_grupos(NOLOCK) WHERE Codigo_Grupo = @Codigo_Grupo AND Id_Periodo = @Id_Periodo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe un grupo con ese código para ese período!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM tbl_grupos(NOLOCK) WHERE Codigo_Seguimiento = @Codigo_Seguimiento)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe un grupo con ese código de seguimiento!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('AGREGANDO GRUPO: ', @Nombre_Grupo);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_AgregarGrupo
                            BEGIN TRY
                                INSERT INTO tbl_grupos(
                                    Codigo_Grupo, Nombre_Grupo, Id_Periodo, Id_Tipo_Grupo,
                                    Id_Coordinador, Id_Jornada, Id_Estado, Fecha_Cierre,
                                    Observaciones, Codigo_Seguimiento, Activo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion
                                ) VALUES (
                                    @Codigo_Grupo, @Nombre_Grupo, @Id_Periodo, @Id_Tipo_Grupo,
                                    @Id_Coordinador, @Id_Jornada, @Id_Estado, @Fecha_Cierre,
                                    @Observaciones, @Codigo_Seguimiento, ISNULL(@Activo, 1),
                                    @Fecha_Creacion, @Fecha_Modificacion, @Id_Creador, @Id_Modificador,
                                    @Id_Transaccion
                                );

                                COMMIT TRAN trx_AgregarGrupo;

                                SET @o_Num = SCOPE_IDENTITY();
                                SET @o_Msg = '¡Grupo agregado exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_AgregarGrupo;

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
            /* ACTUALIZAR GRUPO */
            ELSE IF(@Id_Tipo_Transaccion = 102)
                BEGIN
                    IF ISNULL(@Id_Grupo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de grupo!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_grupos(NOLOCK) WHERE Id_Grupo = @Id_Grupo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El grupo no existe!';
                        END
                    ELSE IF ((@Codigo_Grupo IS NOT NULL OR @Id_Periodo IS NOT NULL) AND
                             EXISTS(SELECT 1 FROM tbl_grupos(NOLOCK) 
                                    WHERE Codigo_Grupo = COALESCE(@Codigo_Grupo, (SELECT Codigo_Grupo FROM tbl_grupos WHERE Id_Grupo = @Id_Grupo))
                                      AND Id_Periodo = COALESCE(@Id_Periodo, (SELECT Id_Periodo FROM tbl_grupos WHERE Id_Grupo = @Id_Grupo))
                                      AND Id_Grupo <> @Id_Grupo))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe otro grupo con ese código para ese período!';
                        END
                    ELSE IF (@Codigo_Seguimiento IS NOT NULL AND EXISTS(SELECT 1 FROM tbl_grupos(NOLOCK) WHERE Codigo_Seguimiento = @Codigo_Seguimiento AND Id_Grupo <> @Id_Grupo))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe otro grupo con ese código de seguimiento!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('ACTUALIZANDO GRUPO ID: ', @Id_Grupo);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_ActualizarGrupo
                            BEGIN TRY
                                UPDATE tbl_grupos
                                SET Codigo_Grupo = COALESCE(@Codigo_Grupo, Codigo_Grupo),
                                    Nombre_Grupo = COALESCE(@Nombre_Grupo, Nombre_Grupo),
                                    Id_Periodo = COALESCE(@Id_Periodo, Id_Periodo),
                                    Id_Tipo_Grupo = COALESCE(@Id_Tipo_Grupo, Id_Tipo_Grupo),
                                    Id_Coordinador = COALESCE(@Id_Coordinador, Id_Coordinador),
                                    Id_Jornada = COALESCE(@Id_Jornada, Id_Jornada),
                                    Id_Estado = COALESCE(@Id_Estado, Id_Estado),
                                    Fecha_Cierre = COALESCE(@Fecha_Cierre, Fecha_Cierre),
                                    Observaciones = COALESCE(@Observaciones, Observaciones),
                                    Codigo_Seguimiento = COALESCE(@Codigo_Seguimiento, Codigo_Seguimiento),
                                    Activo = COALESCE(@Activo, Activo),
                                    Fecha_Modificacion = @Fecha_Modificacion,
                                    Id_Modificador = @Id_Modificador,
                                    Id_Transaccion = @Id_Transaccion
                                WHERE Id_Grupo = @Id_Grupo;

                                COMMIT TRAN trx_ActualizarGrupo;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Grupo actualizado exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_ActualizarGrupo;

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

