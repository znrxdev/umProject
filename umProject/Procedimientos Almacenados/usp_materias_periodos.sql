USE umDb
GO

/*
cls_materias_periodos
*/

CREATE OR ALTER PROC usp_materias_periodos
(
    @Id_Materia_Periodo INT = NULL,
    @Id_Materia INT = NULL,
    @Id_Periodo_Academico INT = NULL,
    @Codigo_Plan VARCHAR(30) = NULL,
    @Id_Jornada INT = NULL,
    @Modalidad NVARCHAR(50) = NULL,
    @Horas_Teoricas INT = NULL,
    @Horas_Practicas INT = NULL,
    @Porcentaje_Asistencia_Minima DECIMAL(5,2) = NULL,
    @Id_Estado INT = NULL,
    @Fecha_Publicacion DATETIME = NULL,
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
            /* FILTRAR POR ID MATERIA PERIODO */
            IF(@Id_Tipo_Transaccion = 93)
                BEGIN
                    IF ISNULL(@Id_Materia_Periodo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de materia período!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_materias_periodos(NOLOCK) WHERE Id_Materia_Periodo = @Id_Materia_Periodo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La materia período no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Materia_Periodo, Id_Materia, Id_Periodo_Academico, Codigo_Plan,
                                    Id_Jornada, Modalidad, Horas_Teoricas, Horas_Practicas,
                                    Porcentaje_Asistencia_Minima, Id_Estado, Fecha_Publicacion,
                                    Id_Usuario_Publicador, Observaciones, Activo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM cls_materias_periodos(NOLOCK)
                                WHERE Id_Materia_Periodo = @Id_Materia_Periodo;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Materia período encontrada!';
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
            /* FILTRAR POR ID MATERIA */
            ELSE IF(@Id_Tipo_Transaccion = 94)
                BEGIN
                    IF ISNULL(@Id_Materia, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado la materia para listar sus períodos!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Materia_Periodo, Id_Materia, Id_Periodo_Academico, Codigo_Plan,
                                    Id_Jornada, Modalidad, Horas_Teoricas, Horas_Practicas,
                                    Porcentaje_Asistencia_Minima, Id_Estado, Fecha_Publicacion,
                                    Id_Usuario_Publicador, Observaciones, Activo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM cls_materias_periodos(NOLOCK)
                                WHERE Id_Materia = @Id_Materia AND Activo = 1
                                ORDER BY Id_Periodo_Academico DESC;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Materias períodos filtradas por materia!';
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
            ELSE IF(@Id_Tipo_Transaccion = 95)
                BEGIN
                    IF ISNULL(@Id_Periodo_Academico, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado el período para listar las materias!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Materia_Periodo, Id_Materia, Id_Periodo_Academico, Codigo_Plan,
                                    Id_Jornada, Modalidad, Horas_Teoricas, Horas_Practicas,
                                    Porcentaje_Asistencia_Minima, Id_Estado, Fecha_Publicacion,
                                    Id_Usuario_Publicador, Observaciones, Activo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM cls_materias_periodos(NOLOCK)
                                WHERE Id_Periodo_Academico = @Id_Periodo_Academico AND Activo = 1
                                ORDER BY Id_Materia;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Materias períodos filtradas por período!';
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
            /* LISTAR TODAS LAS MATERIAS PERÍODOS ACTIVAS */
            ELSE IF(@Id_Tipo_Transaccion = 96)
                BEGIN
                    BEGIN TRY
                        SELECT 
                            mp.Id_Materia_Periodo,
                            mp.Id_Materia,
                            mp.Id_Periodo_Academico,
                            mp.Codigo_Plan,
                            mp.Id_Jornada,
                            mp.Modalidad,
                            mp.Horas_Teoricas,
                            mp.Horas_Practicas,
                            mp.Porcentaje_Asistencia_Minima,
                            mp.Id_Estado,
                            mp.Fecha_Publicacion,
                            mp.Id_Usuario_Publicador,
                            mp.Observaciones,
                            mp.Activo,
                            mp.Fecha_Creacion,
                            mp.Fecha_Modificacion,
                            mp.Id_Creador,
                            mp.Id_Modificador,
                            mp.Id_Transaccion,
                            mp.RowVersion,
                            m.Nombre_Materia,
                            pa.Nombre_Periodo,
                            pa.Codigo_Periodo
                        FROM cls_materias_periodos(NOLOCK) mp
                        LEFT JOIN cls_materias(NOLOCK) m ON mp.Id_Materia = m.Id_Materia
                        LEFT JOIN tbl_periodos_academicos(NOLOCK) pa ON mp.Id_Periodo_Academico = pa.Id_Periodo
                        WHERE mp.Activo = 1
                        ORDER BY pa.Nombre_Periodo DESC, m.Nombre_Materia;

                        SET @o_Num = 0;
                        SET @o_Msg = '¡Materias períodos listadas exitosamente!';
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
            /* AGREGAR MATERIA PERIODO */
            ELSE IF(@Id_Tipo_Transaccion = 91)
                BEGIN
                    IF ISNULL(@Id_Materia, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar una materia!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_materias(NOLOCK) WHERE Id_Materia = @Id_Materia)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La materia no existe!';
                        END
                    ELSE IF ISNULL(@Id_Periodo_Academico, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un período académico!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_periodos_academicos(NOLOCK) WHERE Id_Periodo = @Id_Periodo_Academico)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El período académico no existe!';
                        END
                    ELSE IF ISNULL(@Codigo_Plan, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El campo Código Plan no debe ir vacío!';
                        END
                    ELSE IF ISNULL(@Id_Estado, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un estado!';
                        END
                    ELSE IF (@Porcentaje_Asistencia_Minima IS NOT NULL AND (@Porcentaje_Asistencia_Minima < 0 OR @Porcentaje_Asistencia_Minima > 100))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El porcentaje de asistencia mínima debe estar entre 0 y 100!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM cls_materias_periodos(NOLOCK) WHERE Id_Materia = @Id_Materia AND Id_Periodo_Academico = @Id_Periodo_Academico AND Codigo_Plan = @Codigo_Plan)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe una materia período con esa combinación de materia, período y código de plan!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('AGREGANDO MATERIA PERÍODO: Materia ', @Id_Materia, ' - Período ', @Id_Periodo_Academico);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_AgregarMateriaPeriodo
                            BEGIN TRY
                                INSERT INTO cls_materias_periodos(
                                    Id_Materia, Id_Periodo_Academico, Codigo_Plan, Id_Jornada,
                                    Modalidad, Horas_Teoricas, Horas_Practicas, Porcentaje_Asistencia_Minima,
                                    Id_Estado, Fecha_Publicacion, Id_Usuario_Publicador, Observaciones,
                                    Activo, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion
                                ) VALUES (
                                    @Id_Materia, @Id_Periodo_Academico, @Codigo_Plan, @Id_Jornada,
                                    @Modalidad, ISNULL(@Horas_Teoricas, 0), ISNULL(@Horas_Practicas, 0),
                                    @Porcentaje_Asistencia_Minima, @Id_Estado, @Fecha_Publicacion,
                                    @Id_Usuario_Publicador, @Observaciones, ISNULL(@Activo, 1),
                                    @Fecha_Creacion, @Fecha_Modificacion, @Id_Creador, @Id_Modificador,
                                    @Id_Transaccion
                                );

                                COMMIT TRAN trx_AgregarMateriaPeriodo;

                                SET @o_Num = SCOPE_IDENTITY();
                                SET @o_Msg = '¡Materia período agregada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_AgregarMateriaPeriodo;

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
            /* ACTUALIZAR MATERIA PERIODO */
            ELSE IF(@Id_Tipo_Transaccion = 92)
                BEGIN
                    IF ISNULL(@Id_Materia_Periodo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de materia período!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_materias_periodos(NOLOCK) WHERE Id_Materia_Periodo = @Id_Materia_Periodo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La materia período no existe!';
                        END
                    ELSE IF (@Porcentaje_Asistencia_Minima IS NOT NULL AND (@Porcentaje_Asistencia_Minima < 0 OR @Porcentaje_Asistencia_Minima > 100))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El porcentaje de asistencia mínima debe estar entre 0 y 100!';
                        END
                    ELSE IF ((@Id_Materia IS NOT NULL OR @Id_Periodo_Academico IS NOT NULL OR @Codigo_Plan IS NOT NULL) AND
                             EXISTS(SELECT 1 FROM cls_materias_periodos(NOLOCK) 
                                    WHERE Id_Materia = COALESCE(@Id_Materia, (SELECT Id_Materia FROM cls_materias_periodos WHERE Id_Materia_Periodo = @Id_Materia_Periodo))
                                      AND Id_Periodo_Academico = COALESCE(@Id_Periodo_Academico, (SELECT Id_Periodo_Academico FROM cls_materias_periodos WHERE Id_Materia_Periodo = @Id_Materia_Periodo))
                                      AND Codigo_Plan = COALESCE(@Codigo_Plan, (SELECT Codigo_Plan FROM cls_materias_periodos WHERE Id_Materia_Periodo = @Id_Materia_Periodo))
                                      AND Id_Materia_Periodo <> @Id_Materia_Periodo))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe otra materia período con esa combinación de materia, período y código de plan!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('ACTUALIZANDO MATERIA PERÍODO ID: ', @Id_Materia_Periodo);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_ActualizarMateriaPeriodo
                            BEGIN TRY
                                UPDATE cls_materias_periodos
                                SET Id_Materia = COALESCE(@Id_Materia, Id_Materia),
                                    Id_Periodo_Academico = COALESCE(@Id_Periodo_Academico, Id_Periodo_Academico),
                                    Codigo_Plan = COALESCE(@Codigo_Plan, Codigo_Plan),
                                    Id_Jornada = COALESCE(@Id_Jornada, Id_Jornada),
                                    Modalidad = COALESCE(@Modalidad, Modalidad),
                                    Horas_Teoricas = COALESCE(@Horas_Teoricas, Horas_Teoricas),
                                    Horas_Practicas = COALESCE(@Horas_Practicas, Horas_Practicas),
                                    Porcentaje_Asistencia_Minima = COALESCE(@Porcentaje_Asistencia_Minima, Porcentaje_Asistencia_Minima),
                                    Id_Estado = COALESCE(@Id_Estado, Id_Estado),
                                    Fecha_Publicacion = COALESCE(@Fecha_Publicacion, Fecha_Publicacion),
                                    Id_Usuario_Publicador = COALESCE(@Id_Usuario_Publicador, Id_Usuario_Publicador),
                                    Observaciones = COALESCE(@Observaciones, Observaciones),
                                    Activo = COALESCE(@Activo, Activo),
                                    Fecha_Modificacion = @Fecha_Modificacion,
                                    Id_Modificador = @Id_Modificador,
                                    Id_Transaccion = @Id_Transaccion
                                WHERE Id_Materia_Periodo = @Id_Materia_Periodo;

                                COMMIT TRAN trx_ActualizarMateriaPeriodo;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Materia período actualizada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_ActualizarMateriaPeriodo;

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

