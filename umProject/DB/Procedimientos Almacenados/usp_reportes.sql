USE umDb
GO
/*
usp_reportes
Stored Procedure para generar reportes del sistema
*/

CREATE OR ALTER PROCEDURE usp_reportes
(
    @Id_Tipo_Transaccion INT,
    @Id_Sesion INT = NULL,
    @Fecha_Inicio DATETIME = NULL,
    @Fecha_Fin DATETIME = NULL,
    @o_Msg NVARCHAR(255) = NULL OUTPUT,
    @o_Num INT = NULL OUTPUT
)
AS
BEGIN
    DECLARE @Permiso INT, @Linea_Error INT, @Numero_Error INT, @Mensaje_Error NVARCHAR(255), @Origen_Error NVARCHAR(50) = ERROR_PROCEDURE();
    
    SET @Permiso = dbo.fn_Validar_Permisos(@Id_Sesion, @Id_Tipo_Transaccion);

    IF (@Permiso = 1)
    BEGIN
        /* REPORTE DE USUARIOS ACTIVOS */
        IF (@Id_Tipo_Transaccion = 151)
        BEGIN
            BEGIN TRY
                SELECT 
                    u.Id_Usuario,
                    u.Usuario,
                    u.Id_Persona,
                    p.Primer_Nombre,
                    p.Segundo_Nombre,
                    p.Primer_Apellido,
                    p.Segundo_Apellido,
                    p.Primer_Nombre + ' ' + ISNULL(p.Segundo_Nombre + ' ', '') + p.Primer_Apellido + ' ' + ISNULL(p.Segundo_Apellido, '') AS Nombre_Completo,
                    p.Valor_Documento,
                    td.Nombre_Catalogo AS Tipo_Documento,
                    p.Fecha_Nacimiento,
                    g.Nombre_Catalogo AS Genero,
                    n.Nombre_Catalogo AS Nacionalidad,
                    ec.Nombre_Catalogo AS Estado_Civil,
                    CONVERT(VARCHAR(19), u.Fecha_Creacion, 120) AS Fecha_Creacion_Usuario,
                    CONVERT(VARCHAR(19), u.Fecha_Modificacion, 120) AS Fecha_Modificacion_Usuario,
                    CONVERT(VARCHAR(19), u.Ultima_Sesion, 120) AS Ultima_Sesion,
                    CONVERT(VARCHAR(19), u.Ultimo_Cambio_Contrasena, 120) AS Ultimo_Cambio_Contrasena,
                    e.Nombre_Estado AS Estado_Usuario,
                    CONVERT(VARCHAR(19), p.Fecha_Creacion, 120) AS Fecha_Creacion_Persona,
                    CONVERT(VARCHAR(19), p.Fecha_Modificacion, 120) AS Fecha_Modificacion_Persona
                FROM tbl_usuarios (NOLOCK) u
                INNER JOIN tbl_personas (NOLOCK) p ON u.Id_Persona = p.Id_Persona
                LEFT JOIN cls_catalogos (NOLOCK) td ON p.Id_Tipo_Documento = td.Id_Catalogo
                LEFT JOIN cls_catalogos (NOLOCK) g ON p.Id_Genero_Persona = g.Id_Catalogo
                LEFT JOIN cls_catalogos (NOLOCK) n ON p.Id_Nacionalidad = n.Id_Catalogo
                LEFT JOIN cls_catalogos (NOLOCK) ec ON p.Id_Estado_Civil = ec.Id_Catalogo
                LEFT JOIN cls_estados (NOLOCK) e ON u.Id_Estado = e.Id_Estado
                WHERE u.Id_Estado = 1 -- ACTIVO
                    AND p.Id_Estado = 1 -- PERSONA ACTIVA
                    AND (@Fecha_Inicio IS NULL OR u.Fecha_Creacion >= @Fecha_Inicio)
                    AND (@Fecha_Fin IS NULL OR u.Fecha_Creacion <= DATEADD(DAY, 1, CAST(@Fecha_Fin AS DATE)))
                ORDER BY u.Fecha_Creacion DESC, u.Id_Usuario DESC;

                SET @o_Num = 0;
                SET @o_Msg = '¡Reporte de usuarios activos generado exitosamente!';
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
        /* REPORTE DE USUARIOS INACTIVOS */
        ELSE IF (@Id_Tipo_Transaccion = 152)
        BEGIN
            BEGIN TRY
                SELECT 
                    u.Id_Usuario,
                    u.Usuario,
                    u.Id_Persona,
                    p.Primer_Nombre,
                    p.Segundo_Nombre,
                    p.Primer_Apellido,
                    p.Segundo_Apellido,
                    p.Primer_Nombre + ' ' + ISNULL(p.Segundo_Nombre + ' ', '') + p.Primer_Apellido + ' ' + ISNULL(p.Segundo_Apellido, '') AS Nombre_Completo,
                    p.Valor_Documento,
                    td.Nombre_Catalogo AS Tipo_Documento,
                    p.Fecha_Nacimiento,
                    g.Nombre_Catalogo AS Genero,
                    n.Nombre_Catalogo AS Nacionalidad,
                    ec.Nombre_Catalogo AS Estado_Civil,
                    CONVERT(VARCHAR(19), u.Fecha_Creacion, 120) AS Fecha_Creacion_Usuario,
                    CONVERT(VARCHAR(19), u.Fecha_Modificacion, 120) AS Fecha_Modificacion_Usuario,
                    CONVERT(VARCHAR(19), u.Ultima_Sesion, 120) AS Ultima_Sesion,
                    CONVERT(VARCHAR(19), u.Ultimo_Cambio_Contrasena, 120) AS Ultimo_Cambio_Contrasena,
                    e.Nombre_Estado AS Estado_Usuario,
                    CONVERT(VARCHAR(19), p.Fecha_Creacion, 120) AS Fecha_Creacion_Persona,
                    CONVERT(VARCHAR(19), p.Fecha_Modificacion, 120) AS Fecha_Modificacion_Persona
                FROM tbl_usuarios (NOLOCK) u
                INNER JOIN tbl_personas (NOLOCK) p ON u.Id_Persona = p.Id_Persona
                LEFT JOIN cls_catalogos (NOLOCK) td ON p.Id_Tipo_Documento = td.Id_Catalogo
                LEFT JOIN cls_catalogos (NOLOCK) g ON p.Id_Genero_Persona = g.Id_Catalogo
                LEFT JOIN cls_catalogos (NOLOCK) n ON p.Id_Nacionalidad = n.Id_Catalogo
                LEFT JOIN cls_catalogos (NOLOCK) ec ON p.Id_Estado_Civil = ec.Id_Catalogo
                LEFT JOIN cls_estados (NOLOCK) e ON u.Id_Estado = e.Id_Estado
                WHERE u.Id_Estado <> 1 -- INACTIVO (diferente de ACTIVO)
                    AND (@Fecha_Inicio IS NULL OR u.Fecha_Creacion >= @Fecha_Inicio)
                    AND (@Fecha_Fin IS NULL OR u.Fecha_Creacion <= DATEADD(DAY, 1, CAST(@Fecha_Fin AS DATE)))
                ORDER BY u.Fecha_Creacion DESC, u.Id_Usuario DESC;

                SET @o_Num = 0;
                SET @o_Msg = '¡Reporte de usuarios inactivos generado exitosamente!';
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
        /* REPORTE DE PERSONAS REGISTRADAS */
        ELSE IF (@Id_Tipo_Transaccion = 155)
        BEGIN
            BEGIN TRY
                SELECT 
                    p.Id_Persona,
                    p.Primer_Nombre,
                    p.Segundo_Nombre,
                    p.Primer_Apellido,
                    p.Segundo_Apellido,
                    p.Primer_Nombre + ' ' + ISNULL(p.Segundo_Nombre + ' ', '') + p.Primer_Apellido + ' ' + ISNULL(p.Segundo_Apellido, '') AS Nombre_Completo,
                    p.Valor_Documento,
                    td.Nombre_Catalogo AS Tipo_Documento,
                    p.Fecha_Nacimiento,
                    g.Nombre_Catalogo AS Genero,
                    n.Nombre_Catalogo AS Nacionalidad,
                    ec.Nombre_Catalogo AS Estado_Civil,
                    e.Nombre_Estado AS Estado,
                    CONVERT(VARCHAR(19), p.Fecha_Creacion, 120) AS Fecha_Creacion,
                    CONVERT(VARCHAR(19), p.Fecha_Modificacion, 120) AS Fecha_Modificacion
                FROM tbl_personas (NOLOCK) p
                LEFT JOIN cls_catalogos (NOLOCK) td ON p.Id_Tipo_Documento = td.Id_Catalogo
                LEFT JOIN cls_catalogos (NOLOCK) g ON p.Id_Genero_Persona = g.Id_Catalogo
                LEFT JOIN cls_catalogos (NOLOCK) n ON p.Id_Nacionalidad = n.Id_Catalogo
                LEFT JOIN cls_catalogos (NOLOCK) ec ON p.Id_Estado_Civil = ec.Id_Catalogo
                LEFT JOIN cls_estados (NOLOCK) e ON p.Id_Estado = e.Id_Estado
                WHERE (@Fecha_Inicio IS NULL OR p.Fecha_Creacion >= @Fecha_Inicio)
                    AND (@Fecha_Fin IS NULL OR p.Fecha_Creacion <= DATEADD(DAY, 1, CAST(@Fecha_Fin AS DATE)))
                ORDER BY p.Fecha_Creacion DESC, p.Id_Persona DESC;

                SET @o_Num = 0;
                SET @o_Msg = '¡Reporte de personas registradas generado exitosamente!';
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
        /* REPORTE DE MATERIAS */
        ELSE IF (@Id_Tipo_Transaccion = 156)
        BEGIN
            BEGIN TRY
                SELECT 
                    m.Id_Materia,
                    m.Codigo_Materia,
                    m.Nombre_Materia,
                    CONVERT(VARCHAR(19), m.Fecha_Creacion, 120) AS Fecha_Creacion,
                    CONVERT(VARCHAR(19), m.Fecha_Modificacion, 120) AS Fecha_Modificacion,
                    CASE WHEN m.Activo = 1 THEN 'Activo' ELSE 'Inactivo' END AS Estado
                FROM cls_materias (NOLOCK) m
                WHERE (@Fecha_Inicio IS NULL OR m.Fecha_Creacion >= @Fecha_Inicio)
                    AND (@Fecha_Fin IS NULL OR m.Fecha_Creacion <= DATEADD(DAY, 1, CAST(@Fecha_Fin AS DATE)))
                ORDER BY m.Fecha_Creacion DESC, m.Id_Materia DESC;

                SET @o_Num = 0;
                SET @o_Msg = '¡Reporte de materias generado exitosamente!';
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
        /* REPORTE DE PERÍODOS ACADÉMICOS */
        ELSE IF (@Id_Tipo_Transaccion = 157)
        BEGIN
            BEGIN TRY
                SELECT 
                    pa.Id_Periodo,
                    pa.Codigo_Periodo,
                    pa.Nombre_Periodo,
                    tp.Nombre_Catalogo AS Tipo_Periodo,
                    CONVERT(VARCHAR(10), pa.Fecha_Inicio, 103) AS Fecha_Inicio,
                    CONVERT(VARCHAR(10), pa.Fecha_Fin, 103) AS Fecha_Fin,
                    CONVERT(VARCHAR(10), pa.Fecha_Cierre_Calificaciones, 103) AS Fecha_Cierre_Calificaciones,
                    CASE WHEN pa.Es_Periodo_Actual = 1 THEN 'Sí' ELSE 'No' END AS Es_Periodo_Actual,
                    e.Nombre_Estado AS Estado,
                    CONVERT(VARCHAR(19), pa.Fecha_Creacion, 120) AS Fecha_Creacion,
                    CONVERT(VARCHAR(19), pa.Fecha_Modificacion, 120) AS Fecha_Modificacion
                FROM tbl_periodos_academicos (NOLOCK) pa
                LEFT JOIN cls_catalogos (NOLOCK) tp ON pa.Id_Tipo_Periodo = tp.Id_Catalogo
                LEFT JOIN cls_estados (NOLOCK) e ON pa.Id_Estado = e.Id_Estado
                WHERE (@Fecha_Inicio IS NULL OR pa.Fecha_Creacion >= @Fecha_Inicio)
                    AND (@Fecha_Fin IS NULL OR pa.Fecha_Creacion <= DATEADD(DAY, 1, CAST(@Fecha_Fin AS DATE)))
                ORDER BY pa.Fecha_Creacion DESC, pa.Id_Periodo DESC;

                SET @o_Num = 0;
                SET @o_Msg = '¡Reporte de períodos académicos generado exitosamente!';
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
        /* REPORTE DE SECCIONES */
        ELSE IF (@Id_Tipo_Transaccion = 158)
        BEGIN
            BEGIN TRY
                SELECT 
                    s.Id_Seccion,
                    s.Codigo_Seccion,
                    m.Nombre_Materia,
                    mp.Codigo_Plan,
                    pa.Nombre_Periodo,
                    d.Usuario AS Docente,
                    ts.Nombre_Catalogo AS Tipo_Seccion,
                    a.Nombre_Catalogo AS Aula,
                    s.Horario_Descripcion,
                    s.Modalidad,
                    s.Cupo_Maximo,
                    e.Nombre_Estado AS Estado,
                    CONVERT(VARCHAR(19), s.Fecha_Creacion, 120) AS Fecha_Creacion,
                    CONVERT(VARCHAR(19), s.Fecha_Modificacion, 120) AS Fecha_Modificacion
                FROM tbl_secciones (NOLOCK) s
                INNER JOIN cls_materias_periodos (NOLOCK) mp ON s.Id_Materia_Periodo = mp.Id_Materia_Periodo
                INNER JOIN cls_materias (NOLOCK) m ON mp.Id_Materia = m.Id_Materia
                INNER JOIN tbl_periodos_academicos (NOLOCK) pa ON mp.Id_Periodo_Academico = pa.Id_Periodo
                INNER JOIN tbl_usuarios (NOLOCK) d ON s.Id_Docente = d.Id_Usuario
                LEFT JOIN cls_catalogos (NOLOCK) ts ON s.Id_Tipo_Seccion = ts.Id_Catalogo
                LEFT JOIN cls_catalogos (NOLOCK) a ON s.Id_Aula = a.Id_Catalogo
                LEFT JOIN cls_estados (NOLOCK) e ON s.Id_Estado = e.Id_Estado
                WHERE (@Fecha_Inicio IS NULL OR s.Fecha_Creacion >= @Fecha_Inicio)
                    AND (@Fecha_Fin IS NULL OR s.Fecha_Creacion <= DATEADD(DAY, 1, CAST(@Fecha_Fin AS DATE)))
                ORDER BY s.Fecha_Creacion DESC, s.Id_Seccion DESC;

                SET @o_Num = 0;
                SET @o_Msg = '¡Reporte de secciones generado exitosamente!';
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
        /* REPORTE DE GRUPOS */
        ELSE IF (@Id_Tipo_Transaccion = 159)
        BEGIN
            BEGIN TRY
                SELECT 
                    g.Id_Grupo,
                    g.Codigo_Grupo,
                    g.Nombre_Grupo,
                    pa.Nombre_Periodo,
                    tg.Nombre_Catalogo AS Tipo_Grupo,
                    c.Usuario AS Coordinador,
                    j.Nombre_Catalogo AS Jornada,
                    e.Nombre_Estado AS Estado,
                    CONVERT(VARCHAR(19), g.Fecha_Cierre, 120) AS Fecha_Cierre,
                    CONVERT(VARCHAR(19), g.Fecha_Creacion, 120) AS Fecha_Creacion,
                    CONVERT(VARCHAR(19), g.Fecha_Modificacion, 120) AS Fecha_Modificacion
                FROM tbl_grupos (NOLOCK) g
                INNER JOIN tbl_periodos_academicos (NOLOCK) pa ON g.Id_Periodo = pa.Id_Periodo
                LEFT JOIN cls_catalogos (NOLOCK) tg ON g.Id_Tipo_Grupo = tg.Id_Catalogo
                LEFT JOIN tbl_usuarios (NOLOCK) c ON g.Id_Coordinador = c.Id_Usuario
                LEFT JOIN cls_catalogos (NOLOCK) j ON g.Id_Jornada = j.Id_Catalogo
                LEFT JOIN cls_estados (NOLOCK) e ON g.Id_Estado = e.Id_Estado
                WHERE (@Fecha_Inicio IS NULL OR g.Fecha_Creacion >= @Fecha_Inicio)
                    AND (@Fecha_Fin IS NULL OR g.Fecha_Creacion <= DATEADD(DAY, 1, CAST(@Fecha_Fin AS DATE)))
                ORDER BY g.Fecha_Creacion DESC, g.Id_Grupo DESC;

                SET @o_Num = 0;
                SET @o_Msg = '¡Reporte de grupos generado exitosamente!';
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
        /* REPORTE DE INSCRIPCIONES */
        ELSE IF (@Id_Tipo_Transaccion = 160)
        BEGIN
            BEGIN TRY
                SELECT 
                    i.Id_Inscripcion,
                    i.Codigo_Inscripcion,
                    e.Usuario AS Estudiante,
                    pe.Primer_Nombre + ' ' + ISNULL(pe.Segundo_Nombre + ' ', '') + pe.Primer_Apellido + ' ' + ISNULL(pe.Segundo_Apellido, '') AS Nombre_Estudiante,
                    ti.Nombre_Catalogo AS Tipo_Inscripcion,
                    es.Nombre_Estado AS Estado,
                    CONVERT(VARCHAR(19), i.Fecha_Creacion, 120) AS Fecha_Creacion,
                    CONVERT(VARCHAR(19), i.Fecha_Validacion, 120) AS Fecha_Validacion,
                    CONVERT(VARCHAR(19), i.Fecha_Retiro, 120) AS Fecha_Retiro
                FROM tbl_inscripciones (NOLOCK) i
                INNER JOIN tbl_usuarios (NOLOCK) e ON i.Id_Estudiante = e.Id_Usuario
                INNER JOIN tbl_personas (NOLOCK) pe ON e.Id_Persona = pe.Id_Persona
                LEFT JOIN cls_catalogos (NOLOCK) ti ON i.Id_Tipo_Inscripcion = ti.Id_Catalogo
                LEFT JOIN cls_estados (NOLOCK) es ON i.Id_Estado = es.Id_Estado
                WHERE (@Fecha_Inicio IS NULL OR i.Fecha_Creacion >= @Fecha_Inicio)
                    AND (@Fecha_Fin IS NULL OR i.Fecha_Creacion <= DATEADD(DAY, 1, CAST(@Fecha_Fin AS DATE)))
                ORDER BY i.Fecha_Creacion DESC, i.Id_Inscripcion DESC;

                SET @o_Num = 0;
                SET @o_Msg = '¡Reporte de inscripciones generado exitosamente!';
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
        /* REPORTE DE EVALUACIONES */
        ELSE IF (@Id_Tipo_Transaccion = 161)
        BEGIN
            BEGIN TRY
                SELECT 
                    ea.Id_Evaluacion_Alumno,
                    ea.Codigo_Registro,
                    ei.Codigo_Instancia,
                    em.Nombre_Evaluacion,
                    m.Nombre_Materia,
                    e.Usuario AS Estudiante,
                    pe.Primer_Nombre + ' ' + ISNULL(pe.Segundo_Nombre + ' ', '') + pe.Primer_Apellido + ' ' + ISNULL(pe.Segundo_Apellido, '') AS Nombre_Estudiante,
                    ea.Puntaje_Obtenido,
                    ea.Porcentaje_Logrado,
                    es.Nombre_Estado AS Estado,
                    CONVERT(VARCHAR(19), ea.Fecha_Creacion, 120) AS Fecha_Creacion,
                    CONVERT(VARCHAR(19), ea.Fecha_Validacion, 120) AS Fecha_Validacion,
                    CONVERT(VARCHAR(19), ea.Fecha_Publicacion, 120) AS Fecha_Publicacion
                FROM tbl_evaluaciones_alumnos (NOLOCK) ea
                INNER JOIN tbl_evaluaciones_instancias (NOLOCK) ei ON ea.Id_Evaluacion_Instancia = ei.Id_Evaluacion_Instancia
                INNER JOIN cls_evaluaciones_modelos (NOLOCK) em ON ei.Id_Evaluacion_Modelo = em.Id_Evaluacion_Modelo
                INNER JOIN cls_materias (NOLOCK) m ON em.Id_Materia = m.Id_Materia
                INNER JOIN tbl_inscripciones (NOLOCK) i ON ea.Id_Inscripcion = i.Id_Inscripcion
                INNER JOIN tbl_usuarios (NOLOCK) e ON i.Id_Estudiante = e.Id_Usuario
                INNER JOIN tbl_personas (NOLOCK) pe ON e.Id_Persona = pe.Id_Persona
                LEFT JOIN cls_estados (NOLOCK) es ON ea.Id_Estado = es.Id_Estado
                WHERE (@Fecha_Inicio IS NULL OR ea.Fecha_Creacion >= @Fecha_Inicio)
                    AND (@Fecha_Fin IS NULL OR ea.Fecha_Creacion <= DATEADD(DAY, 1, CAST(@Fecha_Fin AS DATE)))
                ORDER BY ea.Fecha_Creacion DESC, ea.Id_Evaluacion_Alumno DESC;

                SET @o_Num = 0;
                SET @o_Msg = '¡Reporte de evaluaciones generado exitosamente!';
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
        /* REPORTE DE PROGRAMAS DE BECAS */
        ELSE IF (@Id_Tipo_Transaccion = 162)
        BEGIN
            BEGIN TRY
                SELECT 
                    bp.Id_Beca_Programa,
                    bp.Codigo_Programa,
                    bp.Nombre_Programa,
                    bp.Descripcion,
                    tp.Nombre_Catalogo AS Tipo_Programa,
                    mp.Nombre_Catalogo AS Modalidad_Programa,
                    bp.Promedio_Minimo,
                    ep.Nombre_Estado AS Estado_Programa,
                    CONVERT(VARCHAR(19), bp.Fecha_Creacion, 120) AS Fecha_Creacion,
                    CONVERT(VARCHAR(19), bp.Fecha_Modificacion, 120) AS Fecha_Modificacion
                FROM cls_becas_programas (NOLOCK) bp
                LEFT JOIN cls_catalogos (NOLOCK) tp ON bp.Id_Tipo_Programa = tp.Id_Catalogo
                LEFT JOIN cls_catalogos (NOLOCK) mp ON bp.Id_Modalidad_Programa = mp.Id_Catalogo
                LEFT JOIN cls_estados (NOLOCK) ep ON bp.Id_Estado_Programa = ep.Id_Estado
                WHERE (@Fecha_Inicio IS NULL OR bp.Fecha_Creacion >= @Fecha_Inicio)
                    AND (@Fecha_Fin IS NULL OR bp.Fecha_Creacion <= DATEADD(DAY, 1, CAST(@Fecha_Fin AS DATE)))
                ORDER BY bp.Fecha_Creacion DESC, bp.Id_Beca_Programa DESC;

                SET @o_Num = 0;
                SET @o_Msg = '¡Reporte de programas de becas generado exitosamente!';
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
        /* REPORTE DE CONVOCATORIAS DE BECAS */
        ELSE IF (@Id_Tipo_Transaccion = 163)
        BEGIN
            BEGIN TRY
                SELECT 
                    bc.Id_Convocatoria,
                    bc.Codigo_Convocatoria,
                    bc.Nombre_Convocatoria,
                    bp.Nombre_Programa,
                    pa.Nombre_Periodo,
                    bc.Cupo_Total,
                    bc.Cupo_Reservado,
                    bc.Cupo_Asignado,
                    CONVERT(VARCHAR(10), bc.Fecha_Inicio, 103) AS Fecha_Inicio,
                    CONVERT(VARCHAR(10), bc.Fecha_Publicacion, 103) AS Fecha_Publicacion,
                    CONVERT(VARCHAR(10), bc.Fecha_Fin, 103) AS Fecha_Fin,
                    e.Nombre_Estado AS Estado,
                    ep.Nombre_Estado AS Estado_Publicacion,
                    CONVERT(VARCHAR(19), bc.Fecha_Creacion, 120) AS Fecha_Creacion,
                    CONVERT(VARCHAR(19), bc.Fecha_Modificacion, 120) AS Fecha_Modificacion
                FROM tbl_becas_convocatorias (NOLOCK) bc
                INNER JOIN cls_becas_programas (NOLOCK) bp ON bc.Id_Programa = bp.Id_Beca_Programa
                INNER JOIN tbl_periodos_academicos (NOLOCK) pa ON bc.Id_Periodo = pa.Id_Periodo
                LEFT JOIN cls_estados (NOLOCK) e ON bc.Id_Estado = e.Id_Estado
                LEFT JOIN cls_estados (NOLOCK) ep ON bc.Id_Estado_Publicacion = ep.Id_Estado
                WHERE (@Fecha_Inicio IS NULL OR bc.Fecha_Creacion >= @Fecha_Inicio)
                    AND (@Fecha_Fin IS NULL OR bc.Fecha_Creacion <= DATEADD(DAY, 1, CAST(@Fecha_Fin AS DATE)))
                ORDER BY bc.Fecha_Creacion DESC, bc.Id_Convocatoria DESC;

                SET @o_Num = 0;
                SET @o_Msg = '¡Reporte de convocatorias de becas generado exitosamente!';
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
        /* REPORTE DE SOLICITUDES DE BECAS */
        ELSE IF (@Id_Tipo_Transaccion = 164)
        BEGIN
            BEGIN TRY
                SELECT 
                    sb.Id_Solicitud_Beca,
                    sb.Codigo_Seguimiento,
                    bp.Nombre_Programa,
                    bc.Nombre_Convocatoria,
                    e.Usuario AS Estudiante,
                    pe.Primer_Nombre + ' ' + ISNULL(pe.Segundo_Nombre + ' ', '') + pe.Primer_Apellido + ' ' + ISNULL(pe.Segundo_Apellido, '') AS Nombre_Estudiante,
                    sb.Promedio_Vigente,
                    sb.Total_Sanciones_Activas,
                    CASE WHEN sb.Cumple_Criterios = 1 THEN 'Sí' ELSE 'No' END AS Cumple_Criterios,
                    es.Nombre_Estado AS Estado,
                    CONVERT(VARCHAR(19), sb.Fecha_Solicitud, 120) AS Fecha_Solicitud,
                    CONVERT(VARCHAR(19), sb.Fecha_Ultima_Decision, 120) AS Fecha_Ultima_Decision,
                    CONVERT(VARCHAR(19), sb.Fecha_Cierre, 120) AS Fecha_Cierre
                FROM tbl_solicitudes_becas (NOLOCK) sb
                INNER JOIN cls_becas_programas (NOLOCK) bp ON sb.Id_Beca_Programa = bp.Id_Beca_Programa
                LEFT JOIN tbl_becas_convocatorias (NOLOCK) bc ON 1 = 0 -- columna eliminada
                INNER JOIN tbl_usuarios (NOLOCK) e ON sb.Id_Estudiante = e.Id_Usuario
                INNER JOIN tbl_personas (NOLOCK) pe ON e.Id_Persona = pe.Id_Persona
                LEFT JOIN cls_estados (NOLOCK) es ON sb.Id_Estado = es.Id_Estado
                WHERE (@Fecha_Inicio IS NULL OR sb.Fecha_Solicitud >= @Fecha_Inicio)
                    AND (@Fecha_Fin IS NULL OR sb.Fecha_Solicitud <= DATEADD(DAY, 1, CAST(@Fecha_Fin AS DATE)))
                ORDER BY sb.Fecha_Solicitud DESC, sb.Id_Solicitud_Beca DESC;

                SET @o_Num = 0;
                SET @o_Msg = '¡Reporte de solicitudes de becas generado exitosamente!';
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
        /* REPORTE DE SANCIONES ACADÉMICAS */
        ELSE IF (@Id_Tipo_Transaccion = 165)
        BEGIN
            BEGIN TRY
                SELECT 
                    sa.Id_Sancion,
                    sa.Codigo_Sancion,
                    e.Usuario AS Estudiante,
                    pe.Primer_Nombre + ' ' + ISNULL(pe.Segundo_Nombre + ' ', '') + pe.Primer_Apellido + ' ' + ISNULL(pe.Segundo_Apellido, '') AS Nombre_Estudiante,
                    ts.Nombre_Catalogo AS Tipo_Sancion,
                    tf.Nombre_Catalogo AS Tipo_Falta,
                    sv.Nombre_Catalogo AS Severidad,
                    es.Nombre_Estado AS Estado,
                    CONVERT(VARCHAR(19), sa.Fecha_Registro, 120) AS Fecha_Registro,
                    CONVERT(VARCHAR(19), sa.Fecha_Fin, 120) AS Fecha_Fin,
                    sa.Motivo,
                    CASE WHEN sa.Es_Apelable = 1 THEN 'Sí' ELSE 'No' END AS Es_Apelable,
                    CONVERT(VARCHAR(19), sa.Fecha_Apelacion, 120) AS Fecha_Apelacion,
                    sa.Resultado_Apelacion,
                    CONVERT(VARCHAR(19), sa.Fecha_Creacion, 120) AS Fecha_Creacion,
                    CONVERT(VARCHAR(19), sa.Fecha_Modificacion, 120) AS Fecha_Modificacion
                FROM tbl_sanciones_academicas (NOLOCK) sa
                INNER JOIN tbl_usuarios (NOLOCK) e ON sa.Id_Estudiante = e.Id_Usuario
                INNER JOIN tbl_personas (NOLOCK) pe ON e.Id_Persona = pe.Id_Persona
                LEFT JOIN cls_catalogos (NOLOCK) ts ON sa.Id_Tipo_Sancion = ts.Id_Catalogo
                LEFT JOIN cls_catalogos (NOLOCK) tf ON sa.Id_Tipo_Falta = tf.Id_Catalogo
                LEFT JOIN cls_catalogos (NOLOCK) sv ON sa.Id_Severidad = sv.Id_Catalogo
                LEFT JOIN cls_estados (NOLOCK) es ON sa.Id_Estado = es.Id_Estado
                WHERE (@Fecha_Inicio IS NULL OR sa.Fecha_Registro >= @Fecha_Inicio)
                    AND (@Fecha_Fin IS NULL OR sa.Fecha_Registro <= DATEADD(DAY, 1, CAST(@Fecha_Fin AS DATE)))
                ORDER BY sa.Fecha_Registro DESC, sa.Id_Sancion DESC;

                SET @o_Num = 0;
                SET @o_Msg = '¡Reporte de sanciones académicas generado exitosamente!';
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
        /* REPORTE DE TRANSACCIONES (AUDITORÍA) */
        ELSE IF (@Id_Tipo_Transaccion = 166)
        BEGIN
            BEGIN TRY
                SELECT 
                    t.Id_Transaccion,
                    tt.Nombre_Tipo_Transaccion,
                    t.Concepto,
                    CASE 
                        WHEN t.Id_Persona IS NOT NULL THEN 'Persona'
                        WHEN t.Id_Usuario IS NOT NULL THEN 'Usuario'
                        WHEN t.Id_Contacto IS NOT NULL THEN 'Contacto'
                        WHEN t.Id_Evaluacion IS NOT NULL THEN 'Evaluación'
                        WHEN t.Id_Solicitud_Beca IS NOT NULL THEN 'Solicitud Beca'
                        WHEN t.Id_Inscripcion IS NOT NULL THEN 'Inscripción'
                        ELSE 'Sistema'
                    END AS Tipo_Entidad,
                    ua.Usuario AS Autor,
                    CONVERT(VARCHAR(19), t.Fecha_Creacion, 120) AS Fecha_Creacion,
                    CASE WHEN t.Completado = 1 THEN 'Completado' ELSE 'Pendiente' END AS Estado
                FROM tbl_transacciones (NOLOCK) t
                LEFT JOIN cls_tipos_transacciones (NOLOCK) tt ON t.Id_Tipo_Transaccion = tt.Id_Tipo_Transaccion
                LEFT JOIN tbl_usuarios (NOLOCK) ua ON t.Id_Autor = ua.Id_Usuario
                WHERE (@Fecha_Inicio IS NULL OR t.Fecha_Creacion >= @Fecha_Inicio)
                    AND (@Fecha_Fin IS NULL OR t.Fecha_Creacion <= DATEADD(DAY, 1, CAST(@Fecha_Fin AS DATE)))
                ORDER BY t.Fecha_Creacion DESC, t.Id_Transaccion DESC;

                SET @o_Num = 0;
                SET @o_Msg = '¡Reporte de transacciones generado exitosamente!';
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
        ELSE
        BEGIN
            SET @o_Num = -1;
            SET @o_Msg = '¡Tipo de transacción no válido!';
        END
    END
    ELSE
    BEGIN
        SET @o_Num = -1;
        SET @o_Msg = '¡No tiene permisos para realizar esta acción!';
    END
END
GO

