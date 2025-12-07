USE umDb
GO
/*
usp_estudiantes
Stored Procedure para gestionar operaciones del módulo de Estudiantes
*/

CREATE OR ALTER PROCEDURE usp_estudiantes
(
    @Id_Tipo_Transaccion INT,
    @Id_Sesion INT = NULL,
    @Id_Usuario INT = NULL,
    @Solo_Actuales BIT = NULL,
    @Solo_Activas BIT = NULL,
    @o_Msg NVARCHAR(255) = NULL OUTPUT,
    @o_Num INT = NULL OUTPUT
)
AS
BEGIN
    DECLARE @Permiso INT, @Linea_Error INT, @Numero_Error INT, @Mensaje_Error NVARCHAR(255), @Origen_Error NVARCHAR(50) = ERROR_PROCEDURE();
    
    SET @Permiso = dbo.fn_Validar_Permisos(@Id_Sesion, @Id_Tipo_Transaccion);

    IF (@Permiso = 1)
    BEGIN
        /* LISTAR ESTUDIANTES (Id_Tipo_Transaccion = 167) */
        IF (@Id_Tipo_Transaccion = 167)
        BEGIN
            BEGIN TRY
                SELECT DISTINCT
                    U.Id_Usuario,
                    U.Usuario,
                    U.Id_Persona,
                    P.Primer_Nombre + ' ' + ISNULL(P.Segundo_Nombre + ' ', '') + P.Primer_Apellido + ' ' + ISNULL(P.Segundo_Apellido, '') AS Nombre_Completo,
                    P.Valor_Documento,
                    P.Fecha_Nacimiento,
                    U.Id_Estado AS Id_Estado_Usuario,
                    E.Nombre_Estado AS Estado_Usuario,
                    U.Ultima_Sesion,
                    U.Fecha_Creacion AS Fecha_Creacion_Usuario
                FROM tbl_usuarios U (NOLOCK)
                INNER JOIN cls_usuarios_roles UR (NOLOCK) ON U.Id_Usuario = UR.Id_Usuario AND UR.Activo = 1
                INNER JOIN cls_roles R (NOLOCK) ON UR.Id_Rol = R.Id_Rol AND R.Activo = 1
                INNER JOIN tbl_personas P (NOLOCK) ON U.Id_Persona = P.Id_Persona
                INNER JOIN cls_estados E (NOLOCK) ON U.Id_Estado = E.Id_Estado
                INNER JOIN tbl_inscripciones I (NOLOCK) ON U.Id_Usuario = I.Id_Estudiante
                WHERE UR.Id_Rol = 2  -- Rol Estudiante
                ORDER BY U.Id_Usuario;

                SET @o_Num = 0;
                SET @o_Msg = '¡Estudiantes listados exitosamente!';
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
        /* OBTENER DETALLE ESTUDIANTE (Id_Tipo_Transaccion = 168) */
        ELSE IF (@Id_Tipo_Transaccion = 168)
        BEGIN
            BEGIN TRY
                IF (@Id_Usuario IS NULL)
                BEGIN
                    SET @o_Num = -1;
                    SET @o_Msg = '¡Debe proporcionar el ID del estudiante!';
                END
                ELSE
                BEGIN
                    SELECT 
                        U.Id_Usuario,
                        U.Usuario,
                        U.Id_Persona,
                        P.Primer_Nombre + ' ' + ISNULL(P.Segundo_Nombre + ' ', '') + P.Primer_Apellido + ' ' + ISNULL(P.Segundo_Apellido, '') AS Nombre_Completo,
                        P.Valor_Documento,
                        P.Fecha_Nacimiento,
                        EU.Nombre_Estado AS Estado_Usuario,
                        (SELECT COUNT(*) FROM tbl_inscripciones WHERE Id_Estudiante = U.Id_Usuario AND Id_Estado = 1) AS Total_Inscripciones_Activas,
                        (SELECT COUNT(DISTINCT GI.Id_Grupo) FROM tbl_inscripciones I 
                         INNER JOIN tbl_grupos_inscripciones GI ON I.Id_Inscripcion = GI.Id_Inscripcion 
                         WHERE I.Id_Estudiante = U.Id_Usuario AND GI.Activo = 1) AS Total_Grupos,
                        (SELECT COUNT(*) FROM tbl_inscripciones I 
                         INNER JOIN tbl_evaluaciones_alumnos EA ON I.Id_Inscripcion = EA.Id_Inscripcion 
                         WHERE I.Id_Estudiante = U.Id_Usuario) AS Total_Evaluaciones,
                        (SELECT AVG(EA.Porcentaje_Logrado) FROM tbl_inscripciones I 
                         INNER JOIN tbl_evaluaciones_alumnos EA ON I.Id_Inscripcion = EA.Id_Inscripcion 
                         WHERE I.Id_Estudiante = U.Id_Usuario) AS Promedio_General,
                        (SELECT COUNT(*) FROM tbl_sanciones_academicas 
                         WHERE Id_Estudiante = U.Id_Usuario AND Id_Estado = 1) AS Total_Sanciones_Activas,
                        (SELECT TOP 1 PA.Nombre_Periodo FROM tbl_inscripciones I 
                         INNER JOIN tbl_grupos_inscripciones GI ON I.Id_Inscripcion = GI.Id_Inscripcion
                         INNER JOIN cls_grupos_secciones GS ON GI.Id_Grupo = GS.Id_Grupo
                         INNER JOIN tbl_secciones S ON GS.Id_Seccion = S.Id_Seccion
                         INNER JOIN cls_materias_periodos MP ON S.Id_Materia_Periodo = MP.Id_Materia_Periodo
                         INNER JOIN tbl_periodos_academicos PA ON MP.Id_Periodo_Academico = PA.Id_Periodo
                         WHERE I.Id_Estudiante = U.Id_Usuario AND PA.Es_Periodo_Actual = 1 AND GS.Activo = 1 AND GI.Activo = 1) AS Periodo_Actual,
                        U.Ultima_Sesion,
                        U.Fecha_Creacion AS Fecha_Creacion_Usuario
                    FROM tbl_usuarios U (NOLOCK)
                    INNER JOIN cls_usuarios_roles UR (NOLOCK) ON U.Id_Usuario = UR.Id_Usuario AND UR.Activo = 1
                    INNER JOIN tbl_personas P (NOLOCK) ON U.Id_Persona = P.Id_Persona
                    INNER JOIN cls_estados EU (NOLOCK) ON U.Id_Estado = EU.Id_Estado
                    WHERE UR.Id_Rol = 2 AND U.Id_Usuario = @Id_Usuario;

                    SET @o_Num = 0;
                    SET @o_Msg = '¡Detalle del estudiante obtenido exitosamente!';
                END
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
        /* OBTENER INSCRIPCIONES ESTUDIANTE (Id_Tipo_Transaccion = 169) */
        ELSE IF (@Id_Tipo_Transaccion = 169)
        BEGIN
            BEGIN TRY
                IF (@Id_Usuario IS NULL)
                BEGIN
                    SET @o_Num = -1;
                    SET @o_Msg = '¡Debe proporcionar el ID del estudiante!';
                END
                ELSE
                BEGIN
                    SELECT 
                        I.Id_Inscripcion,
                        I.Codigo_Inscripcion,
                        TI.Nombre_Catalogo AS Tipo_Inscripcion,
                        E.Nombre_Estado AS Estado_Inscripcion,
                        I.Fecha_Creacion AS Fecha_Inscripcion,
                        I.Fecha_Validacion,
                        I.Fecha_Retiro,
                        I.Motivo_Retiro
                    FROM tbl_usuarios U (NOLOCK)
                    INNER JOIN cls_usuarios_roles UR (NOLOCK) ON U.Id_Usuario = UR.Id_Usuario AND UR.Activo = 1
                    INNER JOIN tbl_inscripciones I (NOLOCK) ON U.Id_Usuario = I.Id_Estudiante
                    LEFT JOIN cls_catalogos TI (NOLOCK) ON I.Id_Tipo_Inscripcion = TI.Id_Catalogo
                    LEFT JOIN cls_estados E (NOLOCK) ON I.Id_Estado = E.Id_Estado
                    WHERE UR.Id_Rol = 2 AND U.Id_Usuario = @Id_Usuario


                    SET @o_Num = 0;
                    SET @o_Msg = '¡Inscripciones obtenidas exitosamente!';
                END
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
        /* OBTENER GRUPOS ESTUDIANTE (Id_Tipo_Transaccion = 170) */
        ELSE IF (@Id_Tipo_Transaccion = 170)
        BEGIN
            BEGIN TRY
                IF (@Id_Usuario IS NULL)
                BEGIN
                    SET @o_Num = -1;
                    SET @o_Msg = '¡Debe proporcionar el ID del estudiante!';
                END
                ELSE
                BEGIN
                    SELECT 
                        G.Id_Grupo,
                        G.Codigo_Grupo,
                        G.Nombre_Grupo,
                        PA.Nombre_Periodo,
                        PA.Codigo_Periodo,
                        TG.Nombre_Catalogo AS Tipo_Grupo,
                        J.Nombre_Catalogo AS Jornada,
                        COORD.Usuario AS Coordinador,
                        E.Nombre_Estado AS Estado_Grupo,
                        GI.Id_Rol_Grupo,
                        RG.Nombre_Catalogo AS Rol_En_Grupo,
                        GI.Es_Delegado,
                        GI.Fecha_Asignacion,
                        GI.Fecha_Baja,
                        GI.Motivo_Baja
                    FROM tbl_usuarios U (NOLOCK)
                    INNER JOIN cls_usuarios_roles UR (NOLOCK) ON U.Id_Usuario = UR.Id_Usuario AND UR.Activo = 1
                    INNER JOIN tbl_inscripciones I (NOLOCK) ON U.Id_Usuario = I.Id_Estudiante
                    INNER JOIN tbl_grupos_inscripciones GI (NOLOCK) ON I.Id_Inscripcion = GI.Id_Inscripcion AND GI.Activo = 1
                    INNER JOIN tbl_grupos G (NOLOCK) ON GI.Id_Grupo = G.Id_Grupo
                    INNER JOIN tbl_periodos_academicos PA (NOLOCK) ON G.Id_Periodo = PA.Id_Periodo
                    LEFT JOIN cls_catalogos TG (NOLOCK) ON G.Id_Tipo_Grupo = TG.Id_Catalogo
                    LEFT JOIN cls_catalogos J (NOLOCK) ON G.Id_Jornada = J.Id_Catalogo
                    LEFT JOIN tbl_usuarios COORD (NOLOCK) ON G.Id_Coordinador = COORD.Id_Usuario
                    LEFT JOIN cls_estados E (NOLOCK) ON G.Id_Estado = E.Id_Estado
                    LEFT JOIN cls_catalogos RG (NOLOCK) ON GI.Id_Rol_Grupo = RG.Id_Catalogo
                    WHERE UR.Id_Rol = 2 AND U.Id_Usuario = @Id_Usuario
                    ORDER BY PA.Fecha_Inicio DESC, G.Nombre_Grupo;

                    SET @o_Num = 0;
                    SET @o_Msg = '¡Grupos obtenidos exitosamente!';
                END
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
        /* OBTENER SECCIONES ESTUDIANTE (Id_Tipo_Transaccion = 171) */
        ELSE IF (@Id_Tipo_Transaccion = 171)
        BEGIN
            BEGIN TRY
                IF (@Id_Usuario IS NULL)
                BEGIN
                    SET @o_Num = -1;
                    SET @o_Msg = '¡Debe proporcionar el ID del estudiante!';
                END
                ELSE
                BEGIN
                    SELECT 
                        S.Id_Seccion,
                        S.Codigo_Seccion,
                        M.Nombre_Materia,
                        M.Codigo_Materia,
                        PA.Nombre_Periodo,
                        PA.Codigo_Periodo,
                        DOC.Usuario AS Docente,
                        TS.Nombre_Catalogo AS Tipo_Seccion,
                        A.Nombre_Catalogo AS Aula,
                        S.Modalidad,
                        S.Cupo_Maximo,
                        S.Porcentaje_Asistencia_Minima,
                        E.Nombre_Estado AS Estado_Seccion,
                        S.Fecha_Publicacion,
                        S.Fecha_Cierre
                    FROM tbl_usuarios U (NOLOCK)
                    INNER JOIN cls_usuarios_roles UR (NOLOCK) ON U.Id_Usuario = UR.Id_Usuario AND UR.Activo = 1
                    INNER JOIN tbl_inscripciones I (NOLOCK) ON U.Id_Usuario = I.Id_Estudiante
                    INNER JOIN tbl_grupos_inscripciones GI (NOLOCK) ON I.Id_Inscripcion = GI.Id_Inscripcion
                    INNER JOIN cls_grupos_secciones GS (NOLOCK) ON GI.Id_Grupo = GS.Id_Grupo
                    INNER JOIN tbl_secciones S (NOLOCK) ON GS.Id_Seccion = S.Id_Seccion
                    INNER JOIN cls_materias_periodos MP (NOLOCK) ON S.Id_Materia_Periodo = MP.Id_Materia_Periodo
                    INNER JOIN cls_materias M (NOLOCK) ON MP.Id_Materia = M.Id_Materia
                    INNER JOIN tbl_periodos_academicos PA (NOLOCK) ON MP.Id_Periodo_Academico = PA.Id_Periodo
                    LEFT JOIN tbl_usuarios DOC (NOLOCK) ON S.Id_Docente = DOC.Id_Usuario
                    LEFT JOIN cls_catalogos TS (NOLOCK) ON S.Id_Tipo_Seccion = TS.Id_Catalogo
                    LEFT JOIN cls_catalogos A (NOLOCK) ON S.Id_Aula = A.Id_Catalogo
                    LEFT JOIN cls_estados E (NOLOCK) ON S.Id_Estado = E.Id_Estado
                    WHERE UR.Id_Rol = 2 AND U.Id_Usuario = @Id_Usuario AND GS.Activo = 1 AND GI.Activo = 1
                    ORDER BY PA.Fecha_Inicio DESC, M.Nombre_Materia;

                    SET @o_Num = 0;
                    SET @o_Msg = '¡Secciones obtenidas exitosamente!';
                END
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
        /* OBTENER PERÍODOS ESTUDIANTE (Id_Tipo_Transaccion = 172) */
        ELSE IF (@Id_Tipo_Transaccion = 172)
        BEGIN
            BEGIN TRY
                IF (@Id_Usuario IS NULL)
                BEGIN
                    SET @o_Num = -1;
                    SET @o_Msg = '¡Debe proporcionar el ID del estudiante!';
                END
                ELSE
                BEGIN
                    SELECT DISTINCT
                        PA.Id_Periodo,
                        PA.Codigo_Periodo,
                        PA.Nombre_Periodo,
                        TP.Nombre_Catalogo AS Tipo_Periodo,
                        PA.Fecha_Inicio,
                        PA.Fecha_Fin,
                        PA.Es_Periodo_Actual,
                        E.Nombre_Estado AS Estado_Periodo,
                        COUNT(DISTINCT I.Id_Inscripcion) AS Total_Inscripciones
                    FROM tbl_usuarios U (NOLOCK)
                    INNER JOIN cls_usuarios_roles UR (NOLOCK) ON U.Id_Usuario = UR.Id_Usuario AND UR.Activo = 1
                    INNER JOIN tbl_inscripciones I (NOLOCK) ON U.Id_Usuario = I.Id_Estudiante
                    INNER JOIN tbl_grupos_inscripciones GI (NOLOCK) ON I.Id_Inscripcion = GI.Id_Inscripcion
                    INNER JOIN cls_grupos_secciones GS (NOLOCK) ON GI.Id_Grupo = GS.Id_Grupo
                    INNER JOIN tbl_secciones S (NOLOCK) ON GS.Id_Seccion = S.Id_Seccion
                    INNER JOIN cls_materias_periodos MP (NOLOCK) ON S.Id_Materia_Periodo = MP.Id_Materia_Periodo
                    INNER JOIN tbl_periodos_academicos PA (NOLOCK) ON MP.Id_Periodo_Academico = PA.Id_Periodo
                    LEFT JOIN cls_catalogos TP (NOLOCK) ON PA.Id_Tipo_Periodo = TP.Id_Catalogo
                    LEFT JOIN cls_estados E (NOLOCK) ON PA.Id_Estado = E.Id_Estado
                    WHERE UR.Id_Rol = 2 AND U.Id_Usuario = @Id_Usuario AND GS.Activo = 1 AND GI.Activo = 1
                    GROUP BY PA.Id_Periodo, PA.Codigo_Periodo, PA.Nombre_Periodo, 
                             TP.Nombre_Catalogo, PA.Fecha_Inicio, PA.Fecha_Fin, PA.Es_Periodo_Actual, 
                             E.Nombre_Estado
                    ORDER BY PA.Fecha_Inicio DESC;

                    SET @o_Num = 0;
                    SET @o_Msg = '¡Períodos obtenidos exitosamente!';
                END
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
        /* OBTENER EVALUACIONES ESTUDIANTE (Id_Tipo_Transaccion = 173) */
        ELSE IF (@Id_Tipo_Transaccion = 173)
        BEGIN
            BEGIN TRY
                IF (@Id_Usuario IS NULL)
                BEGIN
                    SET @o_Num = -1;
                    SET @o_Msg = '¡Debe proporcionar el ID del estudiante!';
                END
                ELSE
                BEGIN
                    SELECT 
                        EA.Id_Evaluacion_Alumno,
                        EA.Codigo_Registro,
                        EI.Id_Evaluacion_Instancia,
                        EI.Codigo_Instancia,
                        EM.Nombre_Evaluacion,
                        EM.Codigo_Modelo,
                        TE.Nombre_Catalogo AS Tipo_Evaluacion,
                        M.Nombre_Materia,
                        M.Codigo_Materia,
                        S.Codigo_Seccion,
                        PA.Nombre_Periodo,
                        PA.Codigo_Periodo,
                        EA.Puntaje_Obtenido,
                        EA.Porcentaje_Logrado,
                        EI.Calificacion_Maxima,
                        E.Nombre_Estado AS Estado_Evaluacion,
                        EA.Es_Recalculo,
                        EA.Numero_Recalculo,
                        EA.Fecha_Creacion AS Fecha_Evaluacion,
                        EA.Fecha_Publicacion,
                        EA.Fecha_Validacion,
                        EVAL.Usuario AS Usuario_Evaluador,
                        VAL.Usuario AS Usuario_Validador
                    FROM tbl_usuarios U (NOLOCK)
                    INNER JOIN cls_usuarios_roles UR (NOLOCK) ON U.Id_Usuario = UR.Id_Usuario AND UR.Activo = 1
                    INNER JOIN tbl_inscripciones I (NOLOCK) ON U.Id_Usuario = I.Id_Estudiante
                    INNER JOIN tbl_evaluaciones_alumnos EA (NOLOCK) ON I.Id_Inscripcion = EA.Id_Inscripcion
                    INNER JOIN tbl_evaluaciones_instancias EI (NOLOCK) ON EA.Id_Evaluacion_Instancia = EI.Id_Evaluacion_Instancia
                    INNER JOIN cls_evaluaciones_modelos EM (NOLOCK) ON EI.Id_Evaluacion_Modelo = EM.Id_Evaluacion_Modelo
                    INNER JOIN cls_materias M (NOLOCK) ON EM.Id_Materia = M.Id_Materia
                    INNER JOIN tbl_secciones S (NOLOCK) ON EI.Id_Seccion = S.Id_Seccion
                    INNER JOIN tbl_periodos_academicos PA (NOLOCK) ON EI.Id_Periodo = PA.Id_Periodo
                    LEFT JOIN cls_catalogos TE (NOLOCK) ON EM.Id_Tipo_Evaluacion = TE.Id_Catalogo
                    LEFT JOIN cls_estados E (NOLOCK) ON EA.Id_Estado = E.Id_Estado
                    LEFT JOIN tbl_usuarios EVAL (NOLOCK) ON EA.Id_Usuario_Evaluador = EVAL.Id_Usuario
                    LEFT JOIN tbl_usuarios VAL (NOLOCK) ON EA.Id_Usuario_Validador = VAL.Id_Usuario
                    WHERE UR.Id_Rol = 2 AND U.Id_Usuario = @Id_Usuario
                        AND (@Solo_Actuales IS NULL OR @Solo_Actuales = 0 OR PA.Es_Periodo_Actual = 1)
                    ORDER BY PA.Fecha_Inicio DESC, M.Nombre_Materia, EM.Codigo_Modelo, EA.Fecha_Creacion DESC;

                    SET @o_Num = 0;
                    SET @o_Msg = '¡Evaluaciones obtenidas exitosamente!';
                END
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
        /* OBTENER DESEMPEÑO POR PERÍODO ESTUDIANTE (Id_Tipo_Transaccion = 174) */
        ELSE IF (@Id_Tipo_Transaccion = 174)
        BEGIN
            BEGIN TRY
                IF (@Id_Usuario IS NULL)
                BEGIN
                    SET @o_Num = -1;
                    SET @o_Msg = '¡Debe proporcionar el ID del estudiante!';
                END
                ELSE
                BEGIN
                    ;WITH MateriaEval AS (
                        SELECT 
                            PA.Id_Periodo,
                            PA.Nombre_Periodo,
                            PA.Codigo_Periodo,
                            PA.Fecha_Inicio,
                            M.Id_Materia,
                            SUM(ISNULL(EA.Puntaje_Obtenido, 0)) AS TotalObtenido,
                            SUM(ISNULL(EI.Calificacion_Maxima, 0)) AS TotalMaximo
                        FROM tbl_usuarios U (NOLOCK)
                        INNER JOIN cls_usuarios_roles UR (NOLOCK) ON U.Id_Usuario = UR.Id_Usuario AND UR.Activo = 1
                        INNER JOIN tbl_inscripciones I (NOLOCK) ON U.Id_Usuario = I.Id_Estudiante
                        INNER JOIN tbl_grupos_inscripciones GI (NOLOCK) ON I.Id_Inscripcion = GI.Id_Inscripcion
                        INNER JOIN cls_grupos_secciones GS (NOLOCK) ON GI.Id_Grupo = GS.Id_Grupo
                        INNER JOIN tbl_secciones S (NOLOCK) ON GS.Id_Seccion = S.Id_Seccion
                        INNER JOIN cls_materias_periodos MP (NOLOCK) ON S.Id_Materia_Periodo = MP.Id_Materia_Periodo
                        INNER JOIN tbl_periodos_academicos PA (NOLOCK) ON MP.Id_Periodo_Academico = PA.Id_Periodo
                        INNER JOIN cls_materias M (NOLOCK) ON MP.Id_Materia = M.Id_Materia
                        LEFT JOIN tbl_evaluaciones_alumnos EA (NOLOCK) ON I.Id_Inscripcion = EA.Id_Inscripcion
                        LEFT JOIN tbl_evaluaciones_instancias EI (NOLOCK) ON EA.Id_Evaluacion_Instancia = EI.Id_Evaluacion_Instancia
                        WHERE UR.Id_Rol = 2 
                          AND U.Id_Usuario = @Id_Usuario 
                          AND GS.Activo = 1 
                          AND GI.Activo = 1
                        GROUP BY PA.Id_Periodo, PA.Nombre_Periodo, PA.Codigo_Periodo, PA.Fecha_Inicio, M.Id_Materia
                    ),
                    EvalCount AS (
                        SELECT 
                            PA.Id_Periodo,
                            COUNT(DISTINCT EA.Id_Evaluacion_Alumno) AS Total_Evaluaciones
                        FROM tbl_usuarios U (NOLOCK)
                        INNER JOIN cls_usuarios_roles UR (NOLOCK) ON U.Id_Usuario = UR.Id_Usuario AND UR.Activo = 1
                        INNER JOIN tbl_inscripciones I (NOLOCK) ON U.Id_Usuario = I.Id_Estudiante
                        INNER JOIN tbl_grupos_inscripciones GI (NOLOCK) ON I.Id_Inscripcion = GI.Id_Inscripcion
                        INNER JOIN cls_grupos_secciones GS (NOLOCK) ON GI.Id_Grupo = GS.Id_Grupo
                        INNER JOIN tbl_secciones S (NOLOCK) ON GS.Id_Seccion = S.Id_Seccion
                        INNER JOIN cls_materias_periodos MP (NOLOCK) ON S.Id_Materia_Periodo = MP.Id_Materia_Periodo
                        INNER JOIN tbl_periodos_academicos PA (NOLOCK) ON MP.Id_Periodo_Academico = PA.Id_Periodo
                        LEFT JOIN tbl_evaluaciones_alumnos EA (NOLOCK) ON I.Id_Inscripcion = EA.Id_Inscripcion
                        WHERE UR.Id_Rol = 2 
                          AND U.Id_Usuario = @Id_Usuario 
                          AND GS.Activo = 1 
                          AND GI.Activo = 1
                        GROUP BY PA.Id_Periodo
                    )
                    SELECT 
                        ME.Id_Periodo,
                        ME.Nombre_Periodo,
                        ME.Codigo_Periodo,
                        COUNT(DISTINCT ME.Id_Materia) AS Total_Materias,
                        EC.Total_Evaluaciones,
                        AVG(NULLIF(CASE WHEN ME.TotalMaximo > 0 THEN (ME.TotalObtenido / ME.TotalMaximo) * 100.0 END, 0)) AS Promedio_General,
                        SUM(CASE WHEN ME.TotalMaximo > 0 AND (ME.TotalObtenido / ME.TotalMaximo) * 100.0 >= 60 THEN 1 ELSE 0 END) AS Materias_Aprobadas,
                        SUM(CASE WHEN ME.TotalMaximo > 0 AND (ME.TotalObtenido / ME.TotalMaximo) * 100.0 < 60 THEN 1 
                                 WHEN ME.TotalMaximo = 0 THEN 1 ELSE 0 END) AS Materias_Reprobadas
                    FROM MateriaEval ME
                    LEFT JOIN EvalCount EC ON ME.Id_Periodo = EC.Id_Periodo
                    GROUP BY ME.Id_Periodo, ME.Nombre_Periodo, ME.Codigo_Periodo, ME.Fecha_Inicio, EC.Total_Evaluaciones
                    ORDER BY ME.Fecha_Inicio DESC;

                    SET @o_Num = 0;
                    SET @o_Msg = '¡Desempeño obtenido exitosamente!';
                END
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
        /* OBTENER SANCIONES ESTUDIANTE (Id_Tipo_Transaccion = 175) */
        ELSE IF (@Id_Tipo_Transaccion = 175)
        BEGIN
            BEGIN TRY
                IF (@Id_Usuario IS NULL)
                BEGIN
                    SET @o_Num = -1;
                    SET @o_Msg = '¡Debe proporcionar el ID del estudiante!';
                END
                ELSE
                BEGIN
                    SELECT 
                        SA.Id_Sancion,
                        SA.Codigo_Sancion,
                        TS.Nombre_Catalogo AS Tipo_Sancion,
                        TF.Nombre_Catalogo AS Tipo_Falta,
                        SEV.Nombre_Catalogo AS Severidad,
                        E.Nombre_Estado AS Estado_Sancion,
                        SA.Fecha_Registro,
                        SA.Fecha_Fin,
                        SA.Motivo,
                        SA.Es_Apelable,
                        SA.Fecha_Apelacion,
                        SA.Resultado_Apelacion,
                        SA.Observaciones_Apelacion,
                        RES.Usuario AS Usuario_Resolucion,
                        SA.Fecha_Resolucion
                    FROM tbl_usuarios U (NOLOCK)
                    INNER JOIN cls_usuarios_roles UR (NOLOCK) ON U.Id_Usuario = UR.Id_Usuario AND UR.Activo = 1
                    INNER JOIN tbl_sanciones_academicas SA (NOLOCK) ON U.Id_Usuario = SA.Id_Estudiante
                    LEFT JOIN cls_catalogos TS (NOLOCK) ON SA.Id_Tipo_Sancion = TS.Id_Catalogo
                    LEFT JOIN cls_catalogos TF (NOLOCK) ON SA.Id_Tipo_Falta = TF.Id_Catalogo
                    LEFT JOIN cls_catalogos SEV (NOLOCK) ON SA.Id_Severidad = SEV.Id_Catalogo
                    LEFT JOIN cls_estados E (NOLOCK) ON SA.Id_Estado = E.Id_Estado
                    LEFT JOIN tbl_usuarios RES (NOLOCK) ON SA.Id_Usuario_Resolucion = RES.Id_Usuario
                    WHERE UR.Id_Rol = 2 AND U.Id_Usuario = @Id_Usuario
                        AND (@Solo_Activas IS NULL OR @Solo_Activas = 0 OR SA.Id_Estado = 1)
                    ORDER BY SA.Fecha_Registro DESC;

                    SET @o_Num = 0;
                    SET @o_Msg = '¡Sanciones obtenidas exitosamente!';
                END
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
        /* LISTAR ESTUDIANTES SIN INSCRIPCIONES (Id_Tipo_Transaccion = 177) - Para dropdown de crear inscripción */
        ELSE IF (@Id_Tipo_Transaccion = 177)
        BEGIN
            BEGIN TRY
                SELECT DISTINCT
                    U.Id_Usuario,
                    U.Usuario,
                    U.Id_Persona,
                    P.Primer_Nombre + ' ' + ISNULL(P.Segundo_Nombre + ' ', '') + P.Primer_Apellido + ' ' + ISNULL(P.Segundo_Apellido, '') AS Nombre_Completo,
                    P.Valor_Documento,
                    P.Fecha_Nacimiento,
                    U.Id_Estado AS Id_Estado_Usuario,
                    E.Nombre_Estado AS Estado_Usuario,
                    U.Ultima_Sesion,
                    U.Fecha_Creacion AS Fecha_Creacion_Usuario
                FROM tbl_usuarios U (NOLOCK)
                INNER JOIN cls_usuarios_roles UR (NOLOCK) ON U.Id_Usuario = UR.Id_Usuario AND UR.Activo = 1
                INNER JOIN cls_roles R (NOLOCK) ON UR.Id_Rol = R.Id_Rol AND R.Activo = 1
                INNER JOIN tbl_personas P (NOLOCK) ON U.Id_Persona = P.Id_Persona
                INNER JOIN cls_estados E (NOLOCK) ON U.Id_Estado = E.Id_Estado
                WHERE UR.Id_Rol = 2  -- Rol Estudiante
                AND U.Id_Estado = 1  -- Solo activos
                AND NOT EXISTS (
                    SELECT 1 FROM tbl_inscripciones I (NOLOCK) 
                    WHERE I.Id_Estudiante = U.Id_Usuario
                )
                ORDER BY U.Id_Usuario;

                SET @o_Num = 0;
                SET @o_Msg = '¡Estudiantes sin inscripciones listados exitosamente!';
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
        /* OBTENER SOLICITUDES BECAS ESTUDIANTE (Id_Tipo_Transaccion = 176) */
        ELSE IF (@Id_Tipo_Transaccion = 176)
        BEGIN
            BEGIN TRY
                IF (@Id_Usuario IS NULL)
                BEGIN
                    SET @o_Num = -1;
                    SET @o_Msg = '¡Debe proporcionar el ID del estudiante!';
                END
                ELSE
                BEGIN
                    SELECT 
                        SB.Id_Solicitud_Beca,
                        SB.Codigo_Seguimiento,
                        BP.Nombre_Programa,
                        BP.Codigo_Programa,
                        NULL AS Nombre_Convocatoria,
                        NULL AS Codigo_Convocatoria,
                        NULL AS Nombre_Periodo,
                        SB.Promedio_Vigente,
                        SB.Total_Sanciones_Activas,
                        SB.Cumple_Criterios,
                        E.Nombre_Estado AS Estado_Solicitud,
                        SB.Fecha_Solicitud,
                        SB.Fecha_Ultima_Decision,
                        SB.Fecha_Cierre,
                        SB.Motivo_Ultima_Decision
                    FROM tbl_usuarios U (NOLOCK)
                    INNER JOIN cls_usuarios_roles UR (NOLOCK) ON U.Id_Usuario = UR.Id_Usuario AND UR.Activo = 1
                    INNER JOIN tbl_solicitudes_becas SB (NOLOCK) ON U.Id_Usuario = SB.Id_Estudiante
                    INNER JOIN cls_becas_programas BP (NOLOCK) ON SB.Id_Beca_Programa = BP.Id_Beca_Programa
                    LEFT JOIN cls_estados E (NOLOCK) ON SB.Id_Estado = E.Id_Estado
                    WHERE UR.Id_Rol = 2 AND U.Id_Usuario = @Id_Usuario
                    ORDER BY SB.Fecha_Solicitud DESC;

                    SET @o_Num = 0;
                    SET @o_Msg = '¡Solicitudes de becas obtenidas exitosamente!';
                END
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
        SET @o_Msg = '¡No tiene permisos para realizar esta operación!';
    END
END
GO

