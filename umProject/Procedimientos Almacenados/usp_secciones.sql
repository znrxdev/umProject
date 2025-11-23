USE umDb
GO

/*
tbl_secciones
*/

CREATE OR ALTER PROC usp_secciones
(
    @Id_Seccion INT = NULL,
    @Codigo_Seccion VARCHAR(20) = NULL,
    @Id_Materia_Periodo INT = NULL,
    @Id_Docente INT = NULL,
    @Id_Tipo_Seccion INT = NULL,
    @Id_Aula INT = NULL,
    @Horario_Descripcion NVARCHAR(255) = NULL,
    @Modalidad NVARCHAR(50) = NULL,
    @Cupo_Maximo INT = NULL,
    @Requiere_Asistencia BIT = NULL,
    @Porcentaje_Asistencia_Minima DECIMAL(5,2) = NULL,
    @Id_Estado INT = NULL,
    @Id_Estado_Publicacion INT = NULL,
    @Fecha_Publicacion DATETIME = NULL,
    @Fecha_Cierre DATETIME = NULL,
    @Codigo_Firma NVARCHAR(100) = NULL,
    @Id_Usuario_Publicador INT = NULL,
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
            /* FILTRAR POR ID SECCION */
            IF(@Id_Tipo_Transaccion = 98)
                BEGIN
                    IF ISNULL(@Id_Seccion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de sección!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_secciones(NOLOCK) WHERE Id_Seccion = @Id_Seccion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La sección no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Seccion, Codigo_Seccion, Id_Materia_Periodo, Id_Docente,
                                    Id_Tipo_Seccion, Id_Aula, Horario_Descripcion, Modalidad,
                                    Cupo_Maximo, Requiere_Asistencia, Porcentaje_Asistencia_Minima,
                                    Id_Estado, Id_Estado_Publicacion, Fecha_Publicacion, Fecha_Cierre,
                                    Codigo_Firma, Id_Usuario_Publicador, Observaciones, Activo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM tbl_secciones(NOLOCK)
                                WHERE Id_Seccion = @Id_Seccion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Sección encontrada!';
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
            /* FILTRAR POR ID DOCENTE */
            ELSE IF(@Id_Tipo_Transaccion = 99)
                BEGIN
                    IF ISNULL(@Id_Docente, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado el docente para listar sus secciones!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Seccion, Codigo_Seccion, Id_Materia_Periodo, Id_Docente,
                                    Id_Tipo_Seccion, Id_Aula, Horario_Descripcion, Modalidad,
                                    Cupo_Maximo, Requiere_Asistencia, Porcentaje_Asistencia_Minima,
                                    Id_Estado, Id_Estado_Publicacion, Fecha_Publicacion, Fecha_Cierre,
                                    Codigo_Firma, Id_Usuario_Publicador, Observaciones, Activo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM tbl_secciones(NOLOCK)
                                WHERE Id_Docente = @Id_Docente AND Activo = 1
                                ORDER BY Fecha_Creacion DESC;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Secciones filtradas por docente!';
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
            /* FILTRAR POR ID MATERIA PERIODO */
            ELSE IF(@Id_Tipo_Transaccion = 100)
                BEGIN
                    IF ISNULL(@Id_Materia_Periodo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado la materia período para listar sus secciones!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Seccion, Codigo_Seccion, Id_Materia_Periodo, Id_Docente,
                                    Id_Tipo_Seccion, Id_Aula, Horario_Descripcion, Modalidad,
                                    Cupo_Maximo, Requiere_Asistencia, Porcentaje_Asistencia_Minima,
                                    Id_Estado, Id_Estado_Publicacion, Fecha_Publicacion, Fecha_Cierre,
                                    Codigo_Firma, Id_Usuario_Publicador, Observaciones, Activo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM tbl_secciones(NOLOCK)
                                WHERE Id_Materia_Periodo = @Id_Materia_Periodo AND Activo = 1
                                ORDER BY Codigo_Seccion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Secciones filtradas por materia período!';
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
            /* AGREGAR SECCION */
            ELSE IF(@Id_Tipo_Transaccion = 96)
                BEGIN
                    IF ISNULL(@Id_Materia_Periodo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar una materia período!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_materias_periodos(NOLOCK) WHERE Id_Materia_Periodo = @Id_Materia_Periodo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La materia período no existe!';
                        END
                    ELSE IF ISNULL(@Id_Docente, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un docente!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_usuarios(NOLOCK) WHERE Id_Usuario = @Id_Docente)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El docente no existe!';
                        END
                    ELSE IF ISNULL(@Id_Tipo_Seccion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un tipo de sección!';
                        END
                    ELSE IF ISNULL(@Id_Estado, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un estado!';
                        END
                    ELSE IF ISNULL(@Codigo_Seccion, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El campo Código Sección no debe ir vacío!';
                        END
                    ELSE IF (@Porcentaje_Asistencia_Minima IS NOT NULL AND (@Porcentaje_Asistencia_Minima < 0 OR @Porcentaje_Asistencia_Minima > 100))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El porcentaje de asistencia mínima debe estar entre 0 y 100!';
                        END
                    ELSE IF (@Cupo_Maximo IS NOT NULL AND @Cupo_Maximo <= 0)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El cupo máximo debe ser mayor que cero!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM tbl_secciones(NOLOCK) WHERE Codigo_Seccion = @Codigo_Seccion AND Id_Materia_Periodo = @Id_Materia_Periodo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe una sección con ese código para esa materia período!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('AGREGANDO SECCIÓN: ', @Codigo_Seccion);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_AgregarSeccion
                            BEGIN TRY
                                INSERT INTO tbl_secciones(
                                    Codigo_Seccion, Id_Materia_Periodo, Id_Docente, Id_Tipo_Seccion,
                                    Id_Aula, Horario_Descripcion, Modalidad, Cupo_Maximo,
                                    Requiere_Asistencia, Porcentaje_Asistencia_Minima, Id_Estado,
                                    Id_Estado_Publicacion, Fecha_Publicacion, Fecha_Cierre,
                                    Codigo_Firma, Id_Usuario_Publicador, Observaciones, Activo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion
                                ) VALUES (
                                    @Codigo_Seccion, @Id_Materia_Periodo, @Id_Docente, @Id_Tipo_Seccion,
                                    @Id_Aula, @Horario_Descripcion, @Modalidad, @Cupo_Maximo,
                                    ISNULL(@Requiere_Asistencia, 1), @Porcentaje_Asistencia_Minima, @Id_Estado,
                                    @Id_Estado_Publicacion, @Fecha_Publicacion, @Fecha_Cierre,
                                    @Codigo_Firma, @Id_Usuario_Publicador, @Observaciones, ISNULL(@Activo, 1),
                                    @Fecha_Creacion, @Fecha_Modificacion, @Id_Creador, @Id_Modificador,
                                    @Id_Transaccion
                                );

                                COMMIT TRAN trx_AgregarSeccion;

                                SET @o_Num = SCOPE_IDENTITY();
                                SET @o_Msg = '¡Sección agregada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_AgregarSeccion;

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
            /* ACTUALIZAR SECCION */
            ELSE IF(@Id_Tipo_Transaccion = 97)
                BEGIN
                    IF ISNULL(@Id_Seccion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de sección!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_secciones(NOLOCK) WHERE Id_Seccion = @Id_Seccion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La sección no existe!';
                        END
                    ELSE IF (@Porcentaje_Asistencia_Minima IS NOT NULL AND (@Porcentaje_Asistencia_Minima < 0 OR @Porcentaje_Asistencia_Minima > 100))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El porcentaje de asistencia mínima debe estar entre 0 y 100!';
                        END
                    ELSE IF (@Cupo_Maximo IS NOT NULL AND @Cupo_Maximo <= 0)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El cupo máximo debe ser mayor que cero!';
                        END
                    ELSE IF ((@Codigo_Seccion IS NOT NULL OR @Id_Materia_Periodo IS NOT NULL) AND
                             EXISTS(SELECT 1 FROM tbl_secciones(NOLOCK) 
                                    WHERE Codigo_Seccion = COALESCE(@Codigo_Seccion, (SELECT Codigo_Seccion FROM tbl_secciones WHERE Id_Seccion = @Id_Seccion))
                                      AND Id_Materia_Periodo = COALESCE(@Id_Materia_Periodo, (SELECT Id_Materia_Periodo FROM tbl_secciones WHERE Id_Seccion = @Id_Seccion))
                                      AND Id_Seccion <> @Id_Seccion))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe otra sección con ese código para esa materia período!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('ACTUALIZANDO SECCIÓN ID: ', @Id_Seccion);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_ActualizarSeccion
                            BEGIN TRY
                                UPDATE tbl_secciones
                                SET Codigo_Seccion = COALESCE(@Codigo_Seccion, Codigo_Seccion),
                                    Id_Materia_Periodo = COALESCE(@Id_Materia_Periodo, Id_Materia_Periodo),
                                    Id_Docente = COALESCE(@Id_Docente, Id_Docente),
                                    Id_Tipo_Seccion = COALESCE(@Id_Tipo_Seccion, Id_Tipo_Seccion),
                                    Id_Aula = COALESCE(@Id_Aula, Id_Aula),
                                    Horario_Descripcion = COALESCE(@Horario_Descripcion, Horario_Descripcion),
                                    Modalidad = COALESCE(@Modalidad, Modalidad),
                                    Cupo_Maximo = COALESCE(@Cupo_Maximo, Cupo_Maximo),
                                    Requiere_Asistencia = COALESCE(@Requiere_Asistencia, Requiere_Asistencia),
                                    Porcentaje_Asistencia_Minima = COALESCE(@Porcentaje_Asistencia_Minima, Porcentaje_Asistencia_Minima),
                                    Id_Estado = COALESCE(@Id_Estado, Id_Estado),
                                    Id_Estado_Publicacion = COALESCE(@Id_Estado_Publicacion, Id_Estado_Publicacion),
                                    Fecha_Publicacion = COALESCE(@Fecha_Publicacion, Fecha_Publicacion),
                                    Fecha_Cierre = COALESCE(@Fecha_Cierre, Fecha_Cierre),
                                    Codigo_Firma = COALESCE(@Codigo_Firma, Codigo_Firma),
                                    Id_Usuario_Publicador = COALESCE(@Id_Usuario_Publicador, Id_Usuario_Publicador),
                                    Observaciones = COALESCE(@Observaciones, Observaciones),
                                    Activo = COALESCE(@Activo, Activo),
                                    Fecha_Modificacion = @Fecha_Modificacion,
                                    Id_Modificador = @Id_Modificador,
                                    Id_Transaccion = @Id_Transaccion
                                WHERE Id_Seccion = @Id_Seccion;

                                COMMIT TRAN trx_ActualizarSeccion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Sección actualizada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_ActualizarSeccion;

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

