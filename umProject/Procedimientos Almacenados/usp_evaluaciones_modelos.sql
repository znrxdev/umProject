USE umDb
GO

/*
cls_evaluaciones_modelos
*/

CREATE OR ALTER PROC usp_evaluaciones_modelos
(
    @Id_Evaluacion_Modelo INT = NULL,
    @Id_Materia_Periodo INT = NULL,
    @Id_Tipo_Evaluacion INT = NULL,
    @Codigo_Modelo VARCHAR(30) = NULL,
    @Nombre_Evaluacion NVARCHAR(100) = NULL,
    @Concepto NVARCHAR(255) = NULL,
    @Calificacion_Maxima DECIMAL(6,2) = NULL,
    @Peso_Porcentual DECIMAL(6,2) = NULL,
    @Orden INT = NULL,
    @Requiere_Aprobacion BIT = NULL,
    @Version_Configuracion INT = NULL,
    @Id_Metodo_Calculo INT = NULL,
    @Rubrica_Detalle NVARCHAR(MAX) = NULL,
    @Porcentaje_Minimo_Aprobacion DECIMAL(5,2) = NULL,
    @Niveles_Revision TINYINT = NULL,
    @Id_Rol_Aprobador INT = NULL,
    @Permite_Recalculo BIT = NULL,
    @Tiempo_Maximo_Carga INT = NULL,
    @Fecha_Inicio DATETIME = NULL,
    @Fecha_Fin DATETIME = NULL,
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
            /* FILTRAR POR ID EVALUACION MODELO */
            IF(@Id_Tipo_Transaccion = 122)
                BEGIN
                    IF ISNULL(@Id_Evaluacion_Modelo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de evaluación modelo!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_evaluaciones_modelos(NOLOCK) WHERE Id_Evaluacion_Modelo = @Id_Evaluacion_Modelo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La evaluación modelo no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Evaluacion_Modelo, Id_Materia_Periodo, Id_Tipo_Evaluacion, Codigo_Modelo,
                                    Nombre_Evaluacion, Concepto, Calificacion_Maxima, Peso_Porcentual,
                                    Orden, Requiere_Aprobacion, Version_Configuracion, Id_Metodo_Calculo,
                                    Rubrica_Detalle, Porcentaje_Minimo_Aprobacion, Niveles_Revision,
                                    Id_Rol_Aprobador, Permite_Recalculo, Tiempo_Maximo_Carga,
                                    Fecha_Inicio, Fecha_Fin, Activo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM cls_evaluaciones_modelos(NOLOCK)
                                WHERE Id_Evaluacion_Modelo = @Id_Evaluacion_Modelo;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Evaluación modelo encontrada!';
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
            ELSE IF(@Id_Tipo_Transaccion = 123)
                BEGIN
                    IF ISNULL(@Id_Materia_Periodo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado la materia período para listar sus modelos de evaluación!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Evaluacion_Modelo, Id_Materia_Periodo, Id_Tipo_Evaluacion, Codigo_Modelo,
                                    Nombre_Evaluacion, Concepto, Calificacion_Maxima, Peso_Porcentual,
                                    Orden, Requiere_Aprobacion, Version_Configuracion, Id_Metodo_Calculo,
                                    Rubrica_Detalle, Porcentaje_Minimo_Aprobacion, Niveles_Revision,
                                    Id_Rol_Aprobador, Permite_Recalculo, Tiempo_Maximo_Carga,
                                    Fecha_Inicio, Fecha_Fin, Activo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM cls_evaluaciones_modelos(NOLOCK)
                                WHERE Id_Materia_Periodo = @Id_Materia_Periodo AND Activo = 1
                                ORDER BY Orden, Codigo_Modelo;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Evaluaciones modelos filtradas por materia período!';
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
            /* LISTAR TODOS LOS MODELOS DE EVALUACIÓN */
            ELSE IF(@Id_Tipo_Transaccion = 142)
                BEGIN
                    BEGIN TRY
                        SELECT 
                            em.Id_Evaluacion_Modelo, em.Id_Materia_Periodo, em.Id_Tipo_Evaluacion, em.Codigo_Modelo,
                            em.Nombre_Evaluacion, em.Concepto, em.Calificacion_Maxima, em.Peso_Porcentual,
                            em.Orden, em.Requiere_Aprobacion, em.Version_Configuracion, em.Id_Metodo_Calculo,
                            em.Rubrica_Detalle, em.Porcentaje_Minimo_Aprobacion, em.Niveles_Revision,
                            em.Id_Rol_Aprobador, em.Permite_Recalculo, em.Tiempo_Maximo_Carga,
                            em.Fecha_Inicio, em.Fecha_Fin, em.Activo,
                            em.Fecha_Creacion, em.Fecha_Modificacion, em.Id_Creador, em.Id_Modificador,
                            em.Id_Transaccion, em.RowVersion,
                            m.Nombre_Materia,
                            pa.Nombre_Periodo,
                            c.Nombre_Catalogo AS Nombre_Tipo_Evaluacion
                        FROM cls_evaluaciones_modelos(NOLOCK) em
                        LEFT JOIN cls_materias_periodos(NOLOCK) mp ON em.Id_Materia_Periodo = mp.Id_Materia_Periodo
                        LEFT JOIN cls_materias(NOLOCK) m ON mp.Id_Materia = m.Id_Materia
                        LEFT JOIN cls_periodos_academicos(NOLOCK) pa ON mp.Id_Periodo_Academico = pa.Id_Periodo_Academico
                        LEFT JOIN cls_catalogos(NOLOCK) c ON em.Id_Tipo_Evaluacion = c.Id_Catalogo
                        WHERE em.Activo = 1
                        ORDER BY em.Fecha_Creacion DESC, em.Codigo_Modelo;

                        SET @o_Num = 0;
                        SET @o_Msg = '¡Modelos de evaluación listados exitosamente!';
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
            /* AGREGAR EVALUACION MODELO */
            ELSE IF(@Id_Tipo_Transaccion = 120)
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
                    ELSE IF ISNULL(@Id_Tipo_Evaluacion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un tipo de evaluación!';
                        END
                    ELSE IF ISNULL(@Codigo_Modelo, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El campo Código Modelo no debe ir vacío!';
                        END
                    ELSE IF ISNULL(@Nombre_Evaluacion, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El campo Nombre Evaluación no debe ir vacío!';
                        END
                    ELSE IF ISNULL(@Calificacion_Maxima, 0) = 0 OR @Calificacion_Maxima <= 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe especificar una calificación máxima válida!';
                        END
                    ELSE IF ISNULL(@Peso_Porcentual, 0) = 0 OR @Peso_Porcentual < 0 OR @Peso_Porcentual > 100
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El peso porcentual debe estar entre 0 y 100!';
                        END
                    ELSE IF (@Porcentaje_Minimo_Aprobacion IS NOT NULL AND (@Porcentaje_Minimo_Aprobacion < 0 OR @Porcentaje_Minimo_Aprobacion > 100))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El porcentaje mínimo de aprobación debe estar entre 0 y 100!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM cls_evaluaciones_modelos(NOLOCK) 
                                   WHERE Id_Materia_Periodo = @Id_Materia_Periodo 
                                     AND Codigo_Modelo = @Codigo_Modelo 
                                     AND Version_Configuracion = ISNULL(@Version_Configuracion, 1))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe un modelo de evaluación con ese código y versión para esa materia período!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('AGREGANDO EVALUACIÓN MODELO: ', @Nombre_Evaluacion);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_AgregarEvaluacionModelo
                            BEGIN TRY
                                INSERT INTO cls_evaluaciones_modelos(
                                    Id_Materia_Periodo, Id_Tipo_Evaluacion, Codigo_Modelo, Nombre_Evaluacion,
                                    Concepto, Calificacion_Maxima, Peso_Porcentual, Orden,
                                    Requiere_Aprobacion, Version_Configuracion, Id_Metodo_Calculo,
                                    Rubrica_Detalle, Porcentaje_Minimo_Aprobacion, Niveles_Revision,
                                    Id_Rol_Aprobador, Permite_Recalculo, Tiempo_Maximo_Carga,
                                    Fecha_Inicio, Fecha_Fin, Activo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion
                                ) VALUES (
                                    @Id_Materia_Periodo, @Id_Tipo_Evaluacion, @Codigo_Modelo, @Nombre_Evaluacion,
                                    @Concepto, @Calificacion_Maxima, @Peso_Porcentual, ISNULL(@Orden, 1),
                                    ISNULL(@Requiere_Aprobacion, 0), ISNULL(@Version_Configuracion, 1), @Id_Metodo_Calculo,
                                    @Rubrica_Detalle, @Porcentaje_Minimo_Aprobacion, ISNULL(@Niveles_Revision, 1),
                                    @Id_Rol_Aprobador, ISNULL(@Permite_Recalculo, 0), @Tiempo_Maximo_Carga,
                                    @Fecha_Inicio, @Fecha_Fin, ISNULL(@Activo, 1),
                                    @Fecha_Creacion, @Fecha_Modificacion, @Id_Creador, @Id_Modificador,
                                    @Id_Transaccion
                                );

                                COMMIT TRAN trx_AgregarEvaluacionModelo;

                                SET @o_Num = SCOPE_IDENTITY();
                                SET @o_Msg = '¡Evaluación modelo agregada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_AgregarEvaluacionModelo;

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
            /* ACTUALIZAR EVALUACION MODELO */
            ELSE IF(@Id_Tipo_Transaccion = 121)
                BEGIN
                    IF ISNULL(@Id_Evaluacion_Modelo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de evaluación modelo!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_evaluaciones_modelos(NOLOCK) WHERE Id_Evaluacion_Modelo = @Id_Evaluacion_Modelo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La evaluación modelo no existe!';
                        END
                    ELSE IF (@Calificacion_Maxima IS NOT NULL AND @Calificacion_Maxima <= 0)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La calificación máxima debe ser mayor que cero!';
                        END
                    ELSE IF (@Peso_Porcentual IS NOT NULL AND (@Peso_Porcentual < 0 OR @Peso_Porcentual > 100))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El peso porcentual debe estar entre 0 y 100!';
                        END
                    ELSE IF (@Porcentaje_Minimo_Aprobacion IS NOT NULL AND (@Porcentaje_Minimo_Aprobacion < 0 OR @Porcentaje_Minimo_Aprobacion > 100))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El porcentaje mínimo de aprobación debe estar entre 0 y 100!';
                        END
                    ELSE IF ((@Id_Materia_Periodo IS NOT NULL OR @Codigo_Modelo IS NOT NULL OR @Version_Configuracion IS NOT NULL) AND
                             EXISTS(SELECT 1 FROM cls_evaluaciones_modelos(NOLOCK) 
                                    WHERE Id_Materia_Periodo = COALESCE(@Id_Materia_Periodo, (SELECT Id_Materia_Periodo FROM cls_evaluaciones_modelos WHERE Id_Evaluacion_Modelo = @Id_Evaluacion_Modelo))
                                      AND Codigo_Modelo = COALESCE(@Codigo_Modelo, (SELECT Codigo_Modelo FROM cls_evaluaciones_modelos WHERE Id_Evaluacion_Modelo = @Id_Evaluacion_Modelo))
                                      AND Version_Configuracion = COALESCE(@Version_Configuracion, (SELECT Version_Configuracion FROM cls_evaluaciones_modelos WHERE Id_Evaluacion_Modelo = @Id_Evaluacion_Modelo))
                                      AND Id_Evaluacion_Modelo <> @Id_Evaluacion_Modelo))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe otro modelo de evaluación con esa combinación de materia período, código y versión!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('ACTUALIZANDO EVALUACIÓN MODELO ID: ', @Id_Evaluacion_Modelo);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_ActualizarEvaluacionModelo
                            BEGIN TRY
                                UPDATE cls_evaluaciones_modelos
                                SET Id_Materia_Periodo = COALESCE(@Id_Materia_Periodo, Id_Materia_Periodo),
                                    Id_Tipo_Evaluacion = COALESCE(@Id_Tipo_Evaluacion, Id_Tipo_Evaluacion),
                                    Codigo_Modelo = COALESCE(@Codigo_Modelo, Codigo_Modelo),
                                    Nombre_Evaluacion = COALESCE(@Nombre_Evaluacion, Nombre_Evaluacion),
                                    Concepto = COALESCE(@Concepto, Concepto),
                                    Calificacion_Maxima = COALESCE(@Calificacion_Maxima, Calificacion_Maxima),
                                    Peso_Porcentual = COALESCE(@Peso_Porcentual, Peso_Porcentual),
                                    Orden = COALESCE(@Orden, Orden),
                                    Requiere_Aprobacion = COALESCE(@Requiere_Aprobacion, Requiere_Aprobacion),
                                    Version_Configuracion = COALESCE(@Version_Configuracion, Version_Configuracion),
                                    Id_Metodo_Calculo = COALESCE(@Id_Metodo_Calculo, Id_Metodo_Calculo),
                                    Rubrica_Detalle = COALESCE(@Rubrica_Detalle, Rubrica_Detalle),
                                    Porcentaje_Minimo_Aprobacion = COALESCE(@Porcentaje_Minimo_Aprobacion, Porcentaje_Minimo_Aprobacion),
                                    Niveles_Revision = COALESCE(@Niveles_Revision, Niveles_Revision),
                                    Id_Rol_Aprobador = COALESCE(@Id_Rol_Aprobador, Id_Rol_Aprobador),
                                    Permite_Recalculo = COALESCE(@Permite_Recalculo, Permite_Recalculo),
                                    Tiempo_Maximo_Carga = COALESCE(@Tiempo_Maximo_Carga, Tiempo_Maximo_Carga),
                                    Fecha_Inicio = COALESCE(@Fecha_Inicio, Fecha_Inicio),
                                    Fecha_Fin = COALESCE(@Fecha_Fin, Fecha_Fin),
                                    Activo = COALESCE(@Activo, Activo),
                                    Fecha_Modificacion = @Fecha_Modificacion,
                                    Id_Modificador = @Id_Modificador,
                                    Id_Transaccion = @Id_Transaccion
                                WHERE Id_Evaluacion_Modelo = @Id_Evaluacion_Modelo;

                                COMMIT TRAN trx_ActualizarEvaluacionModelo;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Evaluación modelo actualizada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_ActualizarEvaluacionModelo;

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

