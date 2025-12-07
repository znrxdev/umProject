USE umDb
GO
/*
usp_docentes
Stored Procedure para gestionar operaciones del módulo de Docentes
*/

CREATE OR ALTER PROCEDURE usp_docentes
(
    @Id_Tipo_Transaccion INT,
    @Id_Sesion INT = NULL,
    @Id_Usuario INT = NULL,
    @Id_Evaluacion_Alumno INT = NULL,
    @Id_Seccion INT = NULL,
    @Id_Periodo INT = NULL,
    @o_Msg NVARCHAR(255) = NULL OUTPUT,
    @o_Num INT = NULL OUTPUT
)
AS
BEGIN
    DECLARE @Permiso INT, @Linea_Error INT, @Numero_Error INT, @Mensaje_Error NVARCHAR(255), @Origen_Error NVARCHAR(50) = ERROR_PROCEDURE();
    
    SET @Permiso = dbo.fn_Validar_Permisos(@Id_Sesion, @Id_Tipo_Transaccion);

    IF (@Permiso = 1)
    BEGIN
        /* LISTAR DOCENTES (Id_Tipo_Transaccion = 177) */
        IF (@Id_Tipo_Transaccion = 177)
        BEGIN
            BEGIN TRY
                SELECT 
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
                WHERE UR.Id_Rol = 3  -- Rol Docente
                ORDER BY U.Id_Usuario;

                SET @o_Num = 0;
                SET @o_Msg = '¡Docentes listados exitosamente!';
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
        /* OBTENER DETALLE DOCENTE (Id_Tipo_Transaccion = 178) */
        ELSE IF (@Id_Tipo_Transaccion = 178)
        BEGIN
            BEGIN TRY
                IF (@Id_Usuario IS NULL)
                BEGIN
                    SET @o_Num = -1;
                    SET @o_Msg = '¡Debe proporcionar el ID del docente!';
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
                        (SELECT COUNT(*) FROM tbl_secciones WHERE Id_Docente = U.Id_Usuario AND Activo = 1) AS Total_Secciones_Activas,
                        (SELECT COUNT(*) FROM tbl_secciones S 
                         INNER JOIN cls_grupos_secciones GS ON S.Id_Seccion = GS.Id_Seccion
                         INNER JOIN tbl_grupos_inscripciones GI ON GS.Id_Grupo = GI.Id_Grupo
                         INNER JOIN tbl_inscripciones I ON GI.Id_Inscripcion = I.Id_Inscripcion
                         INNER JOIN tbl_evaluaciones_alumnos EA ON I.Id_Inscripcion = EA.Id_Inscripcion 
                         WHERE S.Id_Docente = U.Id_Usuario AND GS.Activo = 1 AND GI.Activo = 1) AS Total_Evaluaciones_Realizadas,
                        (SELECT COUNT(DISTINCT I.Id_Estudiante) FROM tbl_secciones S 
                         INNER JOIN cls_grupos_secciones GS ON S.Id_Seccion = GS.Id_Seccion
                         INNER JOIN tbl_grupos_inscripciones GI ON GS.Id_Grupo = GI.Id_Grupo
                         INNER JOIN tbl_inscripciones I ON GI.Id_Inscripcion = I.Id_Inscripcion
                         WHERE S.Id_Docente = U.Id_Usuario AND GS.Activo = 1 AND GI.Activo = 1) AS Total_Estudiantes_Activos,
                        (SELECT TOP 1 PA.Nombre_Periodo FROM tbl_secciones S
                         INNER JOIN cls_materias_periodos MP ON S.Id_Materia_Periodo = MP.Id_Materia_Periodo
                         INNER JOIN tbl_periodos_academicos PA ON MP.Id_Periodo_Academico = PA.Id_Periodo
                         WHERE S.Id_Docente = U.Id_Usuario AND PA.Es_Periodo_Actual = 1) AS Periodo_Actual,
                        U.Ultima_Sesion,
                        U.Fecha_Creacion AS Fecha_Creacion_Usuario
                    FROM tbl_usuarios U (NOLOCK)
                    INNER JOIN cls_usuarios_roles UR (NOLOCK) ON U.Id_Usuario = UR.Id_Usuario AND UR.Activo = 1
                    INNER JOIN tbl_personas P (NOLOCK) ON U.Id_Persona = P.Id_Persona
                    INNER JOIN cls_estados EU (NOLOCK) ON U.Id_Estado = EU.Id_Estado
                    WHERE UR.Id_Rol = 3 AND U.Id_Usuario = @Id_Usuario;

                    SET @o_Num = 0;
                    SET @o_Msg = '¡Detalle obtenido exitosamente!';
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
        /* OBTENER EVALUACIONES REALIZADAS DOCENTE (Id_Tipo_Transaccion = 179) */
        ELSE IF (@Id_Tipo_Transaccion = 179)
        BEGIN
            BEGIN TRY
                IF (@Id_Usuario IS NULL)
                BEGIN
                    SET @o_Num = -1;
                    SET @o_Msg = '¡Debe proporcionar el ID del docente!';
                END
                ELSE
                BEGIN
                    SELECT 
                        EA.Id_Evaluacion_Alumno,
                        EA.Codigo_Registro,
                        PE.Primer_Nombre + ' ' + ISNULL(PE.Segundo_Nombre + ' ', '') + PE.Primer_Apellido + ' ' + ISNULL(PE.Segundo_Apellido, '') AS Nombre_Estudiante,
                        UE.Usuario AS Usuario_Estudiante,
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
                        EST.Nombre_Estado AS Estado_Evaluacion,
                        ESTP.Nombre_Estado AS Estado_Publicacion,
                        EA.Fecha_Creacion AS Fecha_Evaluacion,
                        EA.Fecha_Publicacion
                    FROM tbl_secciones S (NOLOCK)
                    INNER JOIN cls_grupos_secciones GS (NOLOCK) ON S.Id_Seccion = GS.Id_Seccion
                    INNER JOIN tbl_grupos_inscripciones GI (NOLOCK) ON GS.Id_Grupo = GI.Id_Grupo
                    INNER JOIN tbl_inscripciones I (NOLOCK) ON GI.Id_Inscripcion = I.Id_Inscripcion
                    INNER JOIN tbl_evaluaciones_alumnos EA (NOLOCK) ON I.Id_Inscripcion = EA.Id_Inscripcion
                    INNER JOIN tbl_evaluaciones_instancias EI (NOLOCK) ON EA.Id_Evaluacion_Instancia = EI.Id_Evaluacion_Instancia
                    INNER JOIN cls_evaluaciones_modelos EM (NOLOCK) ON EI.Id_Evaluacion_Modelo = EM.Id_Evaluacion_Modelo
                    INNER JOIN cls_materias_periodos MP (NOLOCK) ON S.Id_Materia_Periodo = MP.Id_Materia_Periodo
                    INNER JOIN cls_materias M (NOLOCK) ON MP.Id_Materia = M.Id_Materia
                    INNER JOIN tbl_periodos_academicos PA (NOLOCK) ON MP.Id_Periodo_Academico = PA.Id_Periodo
                    INNER JOIN tbl_usuarios UE (NOLOCK) ON I.Id_Estudiante = UE.Id_Usuario
                    INNER JOIN tbl_personas PE (NOLOCK) ON UE.Id_Persona = PE.Id_Persona
                    LEFT JOIN cls_catalogos TE (NOLOCK) ON EM.Id_Tipo_Evaluacion = TE.Id_Catalogo
                    LEFT JOIN cls_estados EST (NOLOCK) ON EA.Id_Estado = EST.Id_Estado
                    LEFT JOIN cls_estados ESTP (NOLOCK) ON EA.Id_Estado_Publicacion = ESTP.Id_Estado
                    WHERE S.Id_Docente = @Id_Usuario AND GS.Activo = 1 AND GI.Activo = 1
                        AND (@Id_Periodo IS NULL OR PA.Id_Periodo = @Id_Periodo)
                    ORDER BY EA.Fecha_Creacion DESC;

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
        /* OBTENER DETALLE EVALUACION (Id_Tipo_Transaccion = 180) */
        ELSE IF (@Id_Tipo_Transaccion = 180)
        BEGIN
            BEGIN TRY
                IF (@Id_Evaluacion_Alumno IS NULL)
                BEGIN
                    SET @o_Num = -1;
                    SET @o_Msg = '¡Debe proporcionar el ID de la evaluación!';
                END
                ELSE
                BEGIN
                    SELECT 
                        -- Datos del Alumno
                        EA.Id_Evaluacion_Alumno,
                        EA.Codigo_Registro,
                        PE.Primer_Nombre + ' ' + ISNULL(PE.Segundo_Nombre + ' ', '') + PE.Primer_Apellido + ' ' + ISNULL(PE.Segundo_Apellido, '') AS Nombre_Estudiante,
                        UE.Usuario AS Usuario_Estudiante,
                        PE.Valor_Documento AS Valor_Documento_Estudiante,
                        
                        -- Datos de la Instancia de Evaluación
                        EI.Id_Evaluacion_Instancia,
                        EI.Codigo_Instancia,
                        EI.Fecha_Programada,
                        EI.Fecha_Limite,
                        
                        -- Datos del Modelo de Evaluación
                        EM.Id_Evaluacion_Modelo,
                        EM.Codigo_Modelo,
                        EM.Nombre_Evaluacion,
                        EM.Concepto,
                        TE.Nombre_Catalogo AS Tipo_Evaluacion,
                        EI.Calificacion_Maxima,
                        
                        -- Datos de la Materia y Sección
                        M.Nombre_Materia,
                        M.Codigo_Materia,
                        S.Codigo_Seccion,
                        PA.Nombre_Periodo,
                        PA.Codigo_Periodo,
                        
                        -- Resultado del Alumno
                        EA.Puntaje_Obtenido,
                        EA.Porcentaje_Logrado,
                        EA.Puntaje_Normalizado,
                        EA.Es_Recalculo,
                        EA.Numero_Recalculo,
                        EA.Motivo_Ajuste,
                        EA.Observaciones,
                        
                        -- Usuarios involucrados
                        EA.Id_Usuario_Evaluador,
                        UEVAL.Usuario AS Usuario_Evaluador,
                        PEVAL.Primer_Nombre + ' ' + ISNULL(PEVAL.Segundo_Nombre + ' ', '') + PEVAL.Primer_Apellido + ' ' + ISNULL(PEVAL.Segundo_Apellido, '') AS Nombre_Evaluador,
                        EA.Id_Usuario_Validador,
                        UVAL.Usuario AS Usuario_Validador,
                        PVAL.Primer_Nombre + ' ' + ISNULL(PVAL.Segundo_Nombre + ' ', '') + PVAL.Primer_Apellido + ' ' + ISNULL(PVAL.Segundo_Apellido, '') AS Nombre_Validador,
                        EA.Fecha_Validacion,
                        
                        -- Estados
                        EST.Nombre_Estado AS Estado_Evaluacion,
                        ESTP.Nombre_Estado AS Estado_Publicacion,
                        
                        -- Firma
                        EA.Firmado_Por_Estudiante,
                        EA.Firma_Digital,
                        EA.Fecha_Notificacion,
                        EA.Fecha_Publicacion AS Fecha_Publicacion_Resultado,
                        
                        -- Fechas de auditoría
                        EA.Fecha_Creacion,
                        EA.Fecha_Modificacion
                        
                    FROM tbl_evaluaciones_alumnos EA (NOLOCK)
                    INNER JOIN tbl_evaluaciones_instancias EI (NOLOCK) ON EA.Id_Evaluacion_Instancia = EI.Id_Evaluacion_Instancia
                    INNER JOIN cls_evaluaciones_modelos EM (NOLOCK) ON EI.Id_Evaluacion_Modelo = EM.Id_Evaluacion_Modelo
                    INNER JOIN tbl_inscripciones I (NOLOCK) ON EA.Id_Inscripcion = I.Id_Inscripcion
                    INNER JOIN tbl_secciones S (NOLOCK) ON EI.Id_Seccion = S.Id_Seccion
                    INNER JOIN cls_materias_periodos MP (NOLOCK) ON S.Id_Materia_Periodo = MP.Id_Materia_Periodo
                    INNER JOIN cls_materias M (NOLOCK) ON MP.Id_Materia = M.Id_Materia
                    INNER JOIN tbl_periodos_academicos PA (NOLOCK) ON MP.Id_Periodo_Academico = PA.Id_Periodo
                    INNER JOIN tbl_usuarios UE (NOLOCK) ON I.Id_Estudiante = UE.Id_Usuario
                    INNER JOIN tbl_personas PE (NOLOCK) ON UE.Id_Persona = PE.Id_Persona
                    LEFT JOIN cls_catalogos TE (NOLOCK) ON EM.Id_Tipo_Evaluacion = TE.Id_Catalogo
                    LEFT JOIN cls_estados EST (NOLOCK) ON EA.Id_Estado = EST.Id_Estado
                    LEFT JOIN cls_estados ESTP (NOLOCK) ON EA.Id_Estado_Publicacion = ESTP.Id_Estado
                    LEFT JOIN tbl_usuarios UEVAL (NOLOCK) ON EA.Id_Usuario_Evaluador = UEVAL.Id_Usuario
                    LEFT JOIN tbl_personas PEVAL (NOLOCK) ON UEVAL.Id_Persona = PEVAL.Id_Persona
                    LEFT JOIN tbl_usuarios UVAL (NOLOCK) ON EA.Id_Usuario_Validador = UVAL.Id_Usuario
                    LEFT JOIN tbl_personas PVAL (NOLOCK) ON UVAL.Id_Persona = PVAL.Id_Persona
                    WHERE EA.Id_Evaluacion_Alumno = @Id_Evaluacion_Alumno;

                    SET @o_Num = 0;
                    SET @o_Msg = '¡Detalle de evaluación obtenido exitosamente!';
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
        /* OBTENER SECCIONES ASIGNADAS DOCENTE (Id_Tipo_Transaccion = 181) */
        ELSE IF (@Id_Tipo_Transaccion = 181)
        BEGIN
            BEGIN TRY
                IF (@Id_Usuario IS NULL)
                BEGIN
                    SET @o_Num = -1;
                    SET @o_Msg = '¡Debe proporcionar el ID del docente!';
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
                        TS.Nombre_Catalogo AS Tipo_Seccion,
                        AU.Nombre_Catalogo AS Aula,
                        S.Horario_Descripcion,
                        S.Modalidad,
                        S.Cupo_Maximo,
                        -- Total estudiantes: ahora se obtiene a través de grupos-secciones-inscripciones
                        (SELECT COUNT(DISTINCT GI.Id_Inscripcion) 
                         FROM cls_grupos_secciones GS (NOLOCK)
                         INNER JOIN tbl_grupos_inscripciones GI (NOLOCK) ON GS.Id_Grupo = GI.Id_Grupo
                         WHERE GS.Id_Seccion = S.Id_Seccion AND GS.Activo = 1 AND GI.Activo = 1) AS Total_Estudiantes,
                        S.Requiere_Asistencia,
                        S.Porcentaje_Asistencia_Minima,
                        EST.Nombre_Estado AS Estado_Seccion,
                        ESTP.Nombre_Estado AS Estado_Publicacion,
                        S.Fecha_Publicacion,
                        S.Fecha_Cierre
                    FROM tbl_secciones S (NOLOCK)
                    INNER JOIN cls_materias_periodos MP (NOLOCK) ON S.Id_Materia_Periodo = MP.Id_Materia_Periodo
                    INNER JOIN cls_materias M (NOLOCK) ON MP.Id_Materia = M.Id_Materia
                    INNER JOIN tbl_periodos_academicos PA (NOLOCK) ON MP.Id_Periodo_Academico = PA.Id_Periodo
                    LEFT JOIN cls_catalogos TS (NOLOCK) ON S.Id_Tipo_Seccion = TS.Id_Catalogo
                    LEFT JOIN cls_catalogos AU (NOLOCK) ON S.Id_Aula = AU.Id_Catalogo
                    LEFT JOIN cls_estados EST (NOLOCK) ON S.Id_Estado = EST.Id_Estado
                    LEFT JOIN cls_estados ESTP (NOLOCK) ON S.Id_Estado_Publicacion = ESTP.Id_Estado
                    WHERE S.Id_Docente = @Id_Usuario
                    ORDER BY PA.Fecha_Inicio DESC, S.Codigo_Seccion;

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
        /* OBTENER ESTUDIANTES DE SECCION (Id_Tipo_Transaccion = 182) */
        ELSE IF (@Id_Tipo_Transaccion = 182)
        BEGIN
            BEGIN TRY
                IF (@Id_Seccion IS NULL)
                BEGIN
                    SET @o_Num = -1;
                    SET @o_Msg = '¡Debe proporcionar el ID de la sección!';
                END
                ELSE
                BEGIN
                    SELECT 
                        I.Id_Inscripcion,
                        I.Codigo_Inscripcion,
                        I.Id_Estudiante,
                        P.Primer_Nombre + ' ' + ISNULL(P.Segundo_Nombre + ' ', '') + P.Primer_Apellido + ' ' + ISNULL(P.Segundo_Apellido, '') AS Nombre_Estudiante,
                        U.Usuario AS Usuario_Estudiante,
                        P.Valor_Documento,
                        TI.Nombre_Catalogo AS Tipo_Inscripcion,
                        EST.Nombre_Estado AS Estado_Inscripcion,
                        I.Fecha_Creacion AS Fecha_Inscripcion,
                        I.Fecha_Validacion
                    FROM tbl_inscripciones I (NOLOCK)
                    INNER JOIN tbl_grupos_inscripciones GI (NOLOCK) ON I.Id_Inscripcion = GI.Id_Inscripcion
                    INNER JOIN cls_grupos_secciones GS (NOLOCK) ON GI.Id_Grupo = GS.Id_Grupo
                    INNER JOIN tbl_usuarios U (NOLOCK) ON I.Id_Estudiante = U.Id_Usuario
                    INNER JOIN tbl_personas P (NOLOCK) ON U.Id_Persona = P.Id_Persona
                    LEFT JOIN cls_catalogos TI (NOLOCK) ON I.Id_Tipo_Inscripcion = TI.Id_Catalogo
                    LEFT JOIN cls_estados EST (NOLOCK) ON I.Id_Estado = EST.Id_Estado
                    WHERE GS.Id_Seccion = @Id_Seccion AND GS.Activo = 1 AND GI.Activo = 1
                    ORDER BY P.Primer_Apellido, P.Primer_Nombre;

                    SET @o_Num = 0;
                    SET @o_Msg = '¡Estudiantes obtenidos exitosamente!';
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
    END
    ELSE
    BEGIN
        SET @o_Num = -1;
        SET @o_Msg = '¡No tiene permisos para realizar esta operación!';
    END
END
GO

