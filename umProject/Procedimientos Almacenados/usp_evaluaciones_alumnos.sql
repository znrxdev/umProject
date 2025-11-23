USE umDb
GO

/*
tbl_evaluaciones_alumnos
*/

CREATE OR ALTER PROC usp_evaluaciones_alumnos
(
    @Id_Evaluacion_Alumno INT = NULL,
    @Codigo_Registro VARCHAR(30) = NULL,
    @Id_Evaluacion_Instancia INT = NULL,
    @Id_Inscripcion INT = NULL,
    @Puntaje_Obtenido DECIMAL(8,2) = NULL,
    @Porcentaje_Logrado DECIMAL(6,2) = NULL,
    @Puntaje_Normalizado DECIMAL(6,4) = NULL,
    @Es_Recalculo BIT = NULL,
    @Numero_Recalculo INT = NULL,
    @Motivo_Ajuste NVARCHAR(500) = NULL,
    @Observaciones NVARCHAR(255) = NULL,
    @Id_Usuario_Evaluador INT = NULL,
    @Id_Usuario_Validador INT = NULL,
    @Fecha_Validacion DATETIME = NULL,
    @Id_Estado INT = NULL,
    @Id_Estado_Publicacion INT = NULL,
    @Id_Evaluacion_Reemplazada INT = NULL,
    @Firmado_Por_Estudiante BIT = NULL,
    @Firma_Digital NVARCHAR(255) = NULL,
    @Fecha_Notificacion DATETIME = NULL,
    @Fecha_Publicacion DATETIME = NULL,
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
            /* FILTRAR POR ID EVALUACION ALUMNO */
            IF(@Id_Tipo_Transaccion = 130)
                BEGIN
                    IF ISNULL(@Id_Evaluacion_Alumno, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de evaluación alumno!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_evaluaciones_alumnos(NOLOCK) WHERE Id_Evaluacion_Alumno = @Id_Evaluacion_Alumno)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La evaluación alumno no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Evaluacion_Alumno, Codigo_Registro, Id_Evaluacion_Instancia, Id_Inscripcion,
                                    Puntaje_Obtenido, Porcentaje_Logrado, Puntaje_Normalizado, Es_Recalculo,
                                    Numero_Recalculo, Motivo_Ajuste, Observaciones, Id_Usuario_Evaluador,
                                    Id_Usuario_Validador, Fecha_Validacion, Id_Estado, Id_Estado_Publicacion,
                                    Hash_Resultado, Id_Evaluacion_Reemplazada, Firmado_Por_Estudiante,
                                    Firma_Digital, Fecha_Notificacion, Fecha_Publicacion,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM tbl_evaluaciones_alumnos(NOLOCK)
                                WHERE Id_Evaluacion_Alumno = @Id_Evaluacion_Alumno;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Evaluación alumno encontrada!';
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
            /* FILTRAR POR ID ESTUDIANTE (a través de inscripción) */
            ELSE IF(@Id_Tipo_Transaccion = 131)
                BEGIN
                    IF ISNULL(@Id_Inscripcion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado la inscripción para listar las evaluaciones del estudiante!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    ea.Id_Evaluacion_Alumno, ea.Codigo_Registro, ea.Id_Evaluacion_Instancia, ea.Id_Inscripcion,
                                    ea.Puntaje_Obtenido, ea.Porcentaje_Logrado, ea.Puntaje_Normalizado, ea.Es_Recalculo,
                                    ea.Numero_Recalculo, ea.Motivo_Ajuste, ea.Observaciones, ea.Id_Usuario_Evaluador,
                                    ea.Id_Usuario_Validador, ea.Fecha_Validacion, ea.Id_Estado, ea.Id_Estado_Publicacion,
                                    ea.Hash_Resultado, ea.Id_Evaluacion_Reemplazada, ea.Firmado_Por_Estudiante,
                                    ea.Firma_Digital, ea.Fecha_Notificacion, ea.Fecha_Publicacion,
                                    ea.Fecha_Creacion, ea.Fecha_Modificacion, ea.Id_Creador, ea.Id_Modificador,
                                    ea.Id_Transaccion, ea.RowVersion
                                FROM tbl_evaluaciones_alumnos(NOLOCK) ea
                                INNER JOIN tbl_inscripciones(NOLOCK) i ON ea.Id_Inscripcion = i.Id_Inscripcion
                                WHERE i.Id_Estudiante = @Id_Inscripcion
                                ORDER BY ea.Fecha_Creacion DESC;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Evaluaciones alumnos filtradas por estudiante!';
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
            /* FILTRAR POR ID INSTANCIA */
            ELSE IF(@Id_Tipo_Transaccion = 132)
                BEGIN
                    IF ISNULL(@Id_Evaluacion_Instancia, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado la instancia de evaluación para listar las evaluaciones de alumnos!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Evaluacion_Alumno, Codigo_Registro, Id_Evaluacion_Instancia, Id_Inscripcion,
                                    Puntaje_Obtenido, Porcentaje_Logrado, Puntaje_Normalizado, Es_Recalculo,
                                    Numero_Recalculo, Motivo_Ajuste, Observaciones, Id_Usuario_Evaluador,
                                    Id_Usuario_Validador, Fecha_Validacion, Id_Estado, Id_Estado_Publicacion,
                                    Hash_Resultado, Id_Evaluacion_Reemplazada, Firmado_Por_Estudiante,
                                    Firma_Digital, Fecha_Notificacion, Fecha_Publicacion,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM tbl_evaluaciones_alumnos(NOLOCK)
                                WHERE Id_Evaluacion_Instancia = @Id_Evaluacion_Instancia
                                ORDER BY Fecha_Creacion;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Evaluaciones alumnos filtradas por instancia!';
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
            /* OBTENER MIS EVALUACIONES PUBLICADAS (usando Id_Sesion, ordenadas por período) */
            ELSE IF(@Id_Tipo_Transaccion = 137)
                BEGIN
                    IF ISNULL(@Id_Sesion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No hay sesión activa!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    ea.Id_Evaluacion_Alumno, 
                                    ea.Codigo_Registro, 
                                    ea.Id_Evaluacion_Instancia, 
                                    ea.Id_Inscripcion,
                                    ea.Puntaje_Obtenido, 
                                    ea.Porcentaje_Logrado, 
                                    ea.Puntaje_Normalizado, 
                                    ea.Es_Recalculo,
                                    ea.Numero_Recalculo, 
                                    ea.Motivo_Ajuste, 
                                    ea.Observaciones, 
                                    ea.Id_Usuario_Evaluador,
                                    ea.Id_Usuario_Validador, 
                                    ea.Fecha_Validacion, 
                                    ea.Id_Estado, 
                                    ea.Id_Estado_Publicacion,
                                    ea.Hash_Resultado, 
                                    ea.Id_Evaluacion_Reemplazada, 
                                    ea.Firmado_Por_Estudiante,
                                    ea.Firma_Digital, 
                                    ea.Fecha_Notificacion, 
                                    ea.Fecha_Publicacion,
                                    ea.Fecha_Creacion, 
                                    ea.Fecha_Modificacion, 
                                    ea.Id_Creador, 
                                    ea.Id_Modificador,
                                    ea.Id_Transaccion, 
                                    ea.RowVersion,
                                    -- Información del período académico
                                    p.Id_Periodo,
                                    p.Codigo_Periodo,
                                    p.Nombre_Periodo,
                                    -- Información de la materia
                                    m.Codigo_Materia,
                                    m.Nombre_Materia,
                                    -- Información de la sección
                                    s.Codigo_Seccion,
                                    -- Información del modelo de evaluación
                                    em.Nombre_Evaluacion AS Nombre_Modelo_Evaluacion
                                FROM tbl_evaluaciones_alumnos(NOLOCK) ea
                                INNER JOIN tbl_inscripciones(NOLOCK) i ON ea.Id_Inscripcion = i.Id_Inscripcion
                                INNER JOIN tbl_secciones(NOLOCK) s ON i.Id_Seccion = s.Id_Seccion
                                INNER JOIN cls_materias_periodos(NOLOCK) mp ON s.Id_Materia_Periodo = mp.Id_Materia_Periodo
                                INNER JOIN tbl_periodos_academicos(NOLOCK) p ON mp.Id_Periodo_Academico = p.Id_Periodo
                                INNER JOIN cls_materias(NOLOCK) m ON mp.Id_Materia = m.Id_Materia
                                INNER JOIN tbl_evaluaciones_instancias(NOLOCK) ei ON ea.Id_Evaluacion_Instancia = ei.Id_Evaluacion_Instancia
                                INNER JOIN tbl_evaluaciones_modelos(NOLOCK) em ON ei.Id_Evaluacion_Modelo = em.Id_Evaluacion_Modelo
                                WHERE i.Id_Estudiante = @Id_Sesion
                                AND ea.Fecha_Publicacion IS NOT NULL -- Solo evaluaciones que han sido publicadas
                                ORDER BY p.Fecha_Inicio DESC, m.Nombre_Materia ASC, ea.Fecha_Publicacion DESC;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Evaluaciones publicadas obtenidas exitosamente!';
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
            /* AGREGAR EVALUACION ALUMNO */
            ELSE IF(@Id_Tipo_Transaccion = 128)
                BEGIN
                    IF ISNULL(@Id_Evaluacion_Instancia, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar una instancia de evaluación!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_evaluaciones_instancias(NOLOCK) WHERE Id_Evaluacion_Instancia = @Id_Evaluacion_Instancia)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La instancia de evaluación no existe!';
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
                    ELSE IF (@Puntaje_Obtenido IS NOT NULL AND @Puntaje_Obtenido < 0)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El puntaje obtenido no puede ser negativo!';
                        END
                    ELSE IF (@Porcentaje_Logrado IS NOT NULL AND (@Porcentaje_Logrado < 0 OR @Porcentaje_Logrado > 100))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El porcentaje logrado debe estar entre 0 y 100!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM tbl_evaluaciones_alumnos(NOLOCK) WHERE Codigo_Registro = @Codigo_Registro)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe una evaluación alumno con ese código de registro!';
                        END
                    ELSE
                        BEGIN
                            -- Generar código de registro único si no se proporciona
                            IF ISNULL(@Codigo_Registro, '') = ''
                                BEGIN
                                    DECLARE @Prefijo VARCHAR(10) = 'EVA-';
                                    DECLARE @Anio VARCHAR(4) = CAST(YEAR(GETDATE()) AS VARCHAR);
                                    DECLARE @Contador INT;
                                    SELECT @Contador = ISNULL(MAX(CAST(SUBSTRING(Codigo_Registro, LEN(@Prefijo + @Anio + '-') + 1, LEN(Codigo_Registro)) AS INT)), 0) + 1
                                    FROM tbl_evaluaciones_alumnos(NOLOCK)
                                    WHERE Codigo_Registro LIKE @Prefijo + @Anio + '-%';
                                    SET @Codigo_Registro = @Prefijo + @Anio + '-' + RIGHT('000000' + CAST(@Contador AS VARCHAR), 6);
                                END

                            SET @iConcepto = CONCAT('AGREGANDO EVALUACIÓN ALUMNO: ', @Codigo_Registro);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_AgregarEvaluacionAlumno
                            BEGIN TRY
                                INSERT INTO tbl_evaluaciones_alumnos(
                                    Codigo_Registro, Id_Evaluacion_Instancia, Id_Inscripcion, Puntaje_Obtenido,
                                    Porcentaje_Logrado, Puntaje_Normalizado, Es_Recalculo, Numero_Recalculo,
                                    Motivo_Ajuste, Observaciones, Id_Usuario_Evaluador, Id_Usuario_Validador,
                                    Fecha_Validacion, Id_Estado, Id_Estado_Publicacion, Id_Evaluacion_Reemplazada,
                                    Firmado_Por_Estudiante, Firma_Digital, Fecha_Notificacion, Fecha_Publicacion,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion
                                ) VALUES (
                                    @Codigo_Registro, @Id_Evaluacion_Instancia, @Id_Inscripcion, ISNULL(@Puntaje_Obtenido, 0),
                                    @Porcentaje_Logrado, @Puntaje_Normalizado, ISNULL(@Es_Recalculo, 0), ISNULL(@Numero_Recalculo, 0),
                                    @Motivo_Ajuste, @Observaciones, @Id_Usuario_Evaluador, @Id_Usuario_Validador,
                                    @Fecha_Validacion, @Id_Estado, @Id_Estado_Publicacion, @Id_Evaluacion_Reemplazada,
                                    ISNULL(@Firmado_Por_Estudiante, 0), @Firma_Digital, @Fecha_Notificacion, @Fecha_Publicacion,
                                    @Fecha_Creacion, @Fecha_Modificacion, @Id_Creador, @Id_Modificador,
                                    @Id_Transaccion
                                );

                                COMMIT TRAN trx_AgregarEvaluacionAlumno;

                                SET @o_Num = SCOPE_IDENTITY();
                                SET @o_Msg = '¡Evaluación alumno agregada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_AgregarEvaluacionAlumno;

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
            /* LISTAR TODAS LAS CALIFICACIONES DE ALUMNOS */
            ELSE IF(@Id_Tipo_Transaccion = 147)
                BEGIN
                    BEGIN TRY
                        SELECT 
                            ea.Id_Evaluacion_Alumno,
                            ea.Codigo_Registro,
                            ea.Id_Evaluacion_Instancia,
                            ea.Id_Inscripcion,
                            ea.Puntaje_Obtenido,
                            ea.Porcentaje_Logrado,
                            ea.Puntaje_Normalizado,
                            ea.Es_Recalculo,
                            ea.Numero_Recalculo,
                            ea.Motivo_Ajuste,
                            ea.Observaciones,
                            ea.Id_Usuario_Evaluador,
                            ea.Id_Usuario_Validador,
                            ea.Fecha_Validacion,
                            ea.Id_Estado,
                            ea.Id_Estado_Publicacion,
                            ea.Id_Evaluacion_Reemplazada,
                            ea.Firmado_Por_Estudiante,
                            ea.Firma_Digital,
                            ea.Fecha_Notificacion,
                            ea.Fecha_Publicacion,
                            ea.Fecha_Creacion,
                            ea.Fecha_Modificacion,
                            ea.Id_Creador,
                            ea.Id_Modificador,
                            ea.Id_Transaccion,
                            -- Información adicional de JOINs
                            ei.Codigo_Instancia,
                            s.Codigo_Seccion,
                            m.Nombre_Materia,
                            em.Nombre_Evaluacion AS Nombre_Modelo_Evaluacion,
                            pa.Nombre_Periodo,
                            pa.Codigo_Periodo,
                            i.Codigo_Inscripcion,
                            u.Usuario AS Usuario_Estudiante,
                            p.Primer_Nombre + ' ' + p.Primer_Apellido AS Nombre_Estudiante,
                            e.Nombre_Estado,
                            ep.Nombre_Estado AS Nombre_Estado_Publicacion
                        FROM tbl_evaluaciones_alumnos(NOLOCK) ea
                        LEFT JOIN tbl_evaluaciones_instancias(NOLOCK) ei ON ea.Id_Evaluacion_Instancia = ei.Id_Evaluacion_Instancia
                        LEFT JOIN tbl_inscripciones(NOLOCK) i ON ea.Id_Inscripcion = i.Id_Inscripcion
                        LEFT JOIN tbl_secciones(NOLOCK) s ON i.Id_Seccion = s.Id_Seccion
                        LEFT JOIN cls_materias_periodos(NOLOCK) mp ON s.Id_Materia_Periodo = mp.Id_Materia_Periodo
                        LEFT JOIN cls_materias(NOLOCK) m ON mp.Id_Materia = m.Id_Materia
                        LEFT JOIN cls_evaluaciones_modelos(NOLOCK) em ON ei.Id_Evaluacion_Modelo = em.Id_Evaluacion_Modelo
                        LEFT JOIN tbl_periodos_academicos(NOLOCK) pa ON mp.Id_Periodo_Academico = pa.Id_Periodo
                        LEFT JOIN tbl_usuarios(NOLOCK) u ON i.Id_Estudiante = u.Id_Usuario
                        LEFT JOIN tbl_personas(NOLOCK) p ON u.Id_Persona = p.Id_Persona
                        LEFT JOIN cls_estados(NOLOCK) e ON ea.Id_Estado = e.Id_Estado
                        LEFT JOIN cls_estados(NOLOCK) ep ON ea.Id_Estado_Publicacion = ep.Id_Estado
                        ORDER BY pa.Fecha_Inicio DESC, m.Nombre_Materia ASC, ea.Fecha_Creacion DESC;

                        SET @o_Num = 0;
                        SET @o_Msg = '¡Calificaciones de alumnos listadas exitosamente!';
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
            /* ACTUALIZAR EVALUACION ALUMNO */
            ELSE IF(@Id_Tipo_Transaccion = 129)
                BEGIN
                    IF ISNULL(@Id_Evaluacion_Alumno, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de evaluación alumno!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_evaluaciones_alumnos(NOLOCK) WHERE Id_Evaluacion_Alumno = @Id_Evaluacion_Alumno)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La evaluación alumno no existe!';
                        END
                    ELSE IF (@Puntaje_Obtenido IS NOT NULL AND @Puntaje_Obtenido < 0)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El puntaje obtenido no puede ser negativo!';
                        END
                    ELSE IF (@Porcentaje_Logrado IS NOT NULL AND (@Porcentaje_Logrado < 0 OR @Porcentaje_Logrado > 100))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El porcentaje logrado debe estar entre 0 y 100!';
                        END
                    ELSE IF (@Codigo_Registro IS NOT NULL AND EXISTS(SELECT 1 FROM tbl_evaluaciones_alumnos(NOLOCK) WHERE Codigo_Registro = @Codigo_Registro AND Id_Evaluacion_Alumno <> @Id_Evaluacion_Alumno))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe otra evaluación alumno con ese código de registro!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('ACTUALIZANDO EVALUACIÓN ALUMNO ID: ', @Id_Evaluacion_Alumno);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_ActualizarEvaluacionAlumno
                            BEGIN TRY
                                UPDATE tbl_evaluaciones_alumnos
                                SET Codigo_Registro = COALESCE(@Codigo_Registro, Codigo_Registro),
                                    Id_Evaluacion_Instancia = COALESCE(@Id_Evaluacion_Instancia, Id_Evaluacion_Instancia),
                                    Id_Inscripcion = COALESCE(@Id_Inscripcion, Id_Inscripcion),
                                    Puntaje_Obtenido = COALESCE(@Puntaje_Obtenido, Puntaje_Obtenido),
                                    Porcentaje_Logrado = COALESCE(@Porcentaje_Logrado, Porcentaje_Logrado),
                                    Puntaje_Normalizado = COALESCE(@Puntaje_Normalizado, Puntaje_Normalizado),
                                    Es_Recalculo = COALESCE(@Es_Recalculo, Es_Recalculo),
                                    Numero_Recalculo = COALESCE(@Numero_Recalculo, Numero_Recalculo),
                                    Motivo_Ajuste = COALESCE(@Motivo_Ajuste, Motivo_Ajuste),
                                    Observaciones = COALESCE(@Observaciones, Observaciones),
                                    Id_Usuario_Evaluador = COALESCE(@Id_Usuario_Evaluador, Id_Usuario_Evaluador),
                                    Id_Usuario_Validador = COALESCE(@Id_Usuario_Validador, Id_Usuario_Validador),
                                    Fecha_Validacion = COALESCE(@Fecha_Validacion, Fecha_Validacion),
                                    Id_Estado = COALESCE(@Id_Estado, Id_Estado),
                                    Id_Estado_Publicacion = COALESCE(@Id_Estado_Publicacion, Id_Estado_Publicacion),
                                    Id_Evaluacion_Reemplazada = COALESCE(@Id_Evaluacion_Reemplazada, Id_Evaluacion_Reemplazada),
                                    Firmado_Por_Estudiante = COALESCE(@Firmado_Por_Estudiante, Firmado_Por_Estudiante),
                                    Firma_Digital = COALESCE(@Firma_Digital, Firma_Digital),
                                    Fecha_Notificacion = COALESCE(@Fecha_Notificacion, Fecha_Notificacion),
                                    Fecha_Publicacion = COALESCE(@Fecha_Publicacion, Fecha_Publicacion),
                                    Fecha_Modificacion = @Fecha_Modificacion,
                                    Id_Modificador = @Id_Modificador,
                                    Id_Transaccion = @Id_Transaccion
                                WHERE Id_Evaluacion_Alumno = @Id_Evaluacion_Alumno;

                                COMMIT TRAN trx_ActualizarEvaluacionAlumno;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Evaluación alumno actualizada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_ActualizarEvaluacionAlumno;

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

