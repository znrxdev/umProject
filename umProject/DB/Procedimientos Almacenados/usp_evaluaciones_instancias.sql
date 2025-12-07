USE umDb
GO

/*
tbl_evaluaciones_instancias
*/

CREATE OR ALTER PROC usp_evaluaciones_instancias
(
    @Id_Evaluacion_Instancia INT = NULL,
    @Codigo_Instancia VARCHAR(30) = NULL,
    @Id_Seccion INT = NULL,
    @Id_Evaluacion_Modelo INT = NULL,
    @Id_Periodo INT = NULL,
    @Fecha_Programada DATETIME2(3) = NULL,
    @Fecha_Limite DATETIME2(3) = NULL,
    @Requiere_Revision_Interna BIT = NULL,
    @Numero_Version INT = NULL,
    @Nivel_Aprobacion_Actual TINYINT = NULL,
    @Calificacion_Maxima DECIMAL(6,2) = NULL,
    @Id_Estado INT = NULL,
    @Id_Responsable_Revision INT = NULL,
    @Fecha_Revision DATETIME2(3) = NULL,
    @Id_Responsable_Publicacion INT = NULL,
    @Fecha_Publicacion DATETIME2(3) = NULL,
    @Id_Evaluacion_Padre INT = NULL,
    @Observaciones_Revision NVARCHAR(500) = NULL,
    @Motivo_Rechazo NVARCHAR(500) = NULL,
    @Fecha_Creacion DATETIME2(3) = NULL,
    @Fecha_Modificacion DATETIME2(3) = NULL,
    @Id_Creador INT = NULL,
    @Id_Modificador INT = NULL,
    @Id_Tipo_Transaccion INT,
    @Id_Transaccion INT = NULL,
    @Id_Sesion INT = NULL,
    @o_Num INT = NULL OUTPUT,
    @o_Msg NVARCHAR(MAX) = NULL OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Permiso INT, @iConcepto NVARCHAR(255), @Linea_Error INT, @Numero_Error INT, @Mensaje_Error NVARCHAR(255), @Origen_Error NVARCHAR(50) = ERROR_PROCEDURE();
    SET @o_Num = 0;
    SET @o_Msg = '';
    SET @Fecha_Creacion = GETDATE();
    SET @Fecha_Modificacion = GETDATE();
    SET @Id_Creador = @Id_Sesion;
    SET @Id_Modificador = @Id_Sesion;
    SET @Permiso = dbo.fn_Validar_Permisos(@Id_Sesion, @Id_Tipo_Transaccion);

    IF(@Permiso = 1)
        BEGIN
            /* FILTRAR POR ID EVALUACION INSTANCIA */
            IF(@Id_Tipo_Transaccion = 126)
                BEGIN
                    IF ISNULL(@Id_Evaluacion_Instancia, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de evaluación instancia!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_evaluaciones_instancias(NOLOCK) WHERE Id_Evaluacion_Instancia = @Id_Evaluacion_Instancia)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La evaluación instancia no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    ei.Id_Evaluacion_Instancia,
                                    ei.Codigo_Instancia,
                                    ei.Id_Seccion,
                                    ei.Id_Evaluacion_Modelo,
                                    ei.Id_Periodo,
                                    ei.Fecha_Programada,
                                    ei.Fecha_Limite,
                                    ei.Requiere_Revision_Interna,
                                    ei.Numero_Version,
                                    ei.Nivel_Aprobacion_Actual,
                                    ei.Calificacion_Maxima,
                                    ei.Id_Estado,
                                    ei.Id_Responsable_Revision,
                                    ei.Fecha_Revision,
                                    ei.Id_Responsable_Publicacion,
                                    ei.Fecha_Publicacion,
                                    ei.Id_Evaluacion_Padre,
                                    ei.Hash_Instancia,
                                    ei.Observaciones_Revision,
                                    ei.Motivo_Rechazo,
                                    ei.Fecha_Creacion,
                                    ei.Fecha_Modificacion,
                                    ei.Id_Creador,
                                    ei.Id_Modificador,
                                    ei.Id_Transaccion,
                                    -- Información adicional de JOINs
                                    s.Codigo_Seccion,
                                    m.Nombre_Materia,
                                    em.Nombre_Evaluacion AS Nombre_Modelo_Evaluacion,
                                    em.Codigo_Modelo,
                                    pa.Nombre_Periodo,
                                    pa.Codigo_Periodo,
                                    e.Nombre_Estado,
                                    e.Nombre_Estado AS Nombre_Estado
                                FROM tbl_evaluaciones_instancias(NOLOCK) ei
                                LEFT JOIN tbl_secciones(NOLOCK) s ON ei.Id_Seccion = s.Id_Seccion
                                LEFT JOIN cls_materias_periodos(NOLOCK) mp ON s.Id_Materia_Periodo = mp.Id_Materia_Periodo
                                LEFT JOIN cls_materias(NOLOCK) m ON mp.Id_Materia = m.Id_Materia
                                LEFT JOIN cls_evaluaciones_modelos(NOLOCK) em ON ei.Id_Evaluacion_Modelo = em.Id_Evaluacion_Modelo
                                LEFT JOIN tbl_periodos_academicos(NOLOCK) pa ON ei.Id_Periodo = pa.Id_Periodo
                                LEFT JOIN cls_estados(NOLOCK) e ON ei.Id_Estado = e.Id_Estado
                                WHERE ei.Id_Evaluacion_Instancia = @Id_Evaluacion_Instancia;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Evaluación instancia encontrada!';
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
            /* FILTRAR POR ID SECCION */
            ELSE IF(@Id_Tipo_Transaccion = 127)
                BEGIN
                    IF ISNULL(@Id_Seccion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado la sección para listar sus evaluaciones instancias!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Evaluacion_Instancia, Codigo_Instancia, Id_Seccion, Id_Evaluacion_Modelo,
                                    Id_Periodo, Fecha_Programada, Fecha_Limite, Requiere_Revision_Interna,
                                    Numero_Version, Nivel_Aprobacion_Actual, Id_Estado,
                                    Id_Responsable_Revision, Fecha_Revision, Id_Responsable_Publicacion,
                                    Fecha_Publicacion, Id_Evaluacion_Padre, Hash_Instancia,
                                    Observaciones_Revision, Motivo_Rechazo,
                                    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,
                                    Id_Transaccion, RowVersion
                                FROM tbl_evaluaciones_instancias(NOLOCK)
                                WHERE Id_Seccion = @Id_Seccion
                                ORDER BY Fecha_Programada DESC;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Evaluaciones instancias filtradas por sección!';
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
            /* AGREGAR EVALUACION INSTANCIA */
            ELSE IF(@Id_Tipo_Transaccion = 124)
                BEGIN
                    IF ISNULL(@Id_Seccion, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar una sección!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_secciones(NOLOCK) WHERE Id_Seccion = @Id_Seccion)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La sección no existe!';
                        END
                    ELSE IF ISNULL(@Id_Evaluacion_Modelo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un modelo de evaluación!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_evaluaciones_modelos(NOLOCK) WHERE Id_Evaluacion_Modelo = @Id_Evaluacion_Modelo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El modelo de evaluación no existe!';
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
                    ELSE IF ISNULL(@Id_Estado, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un estado!';
                        END
                    ELSE IF ISNULL(@Calificacion_Maxima, 0) <= 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La calificación máxima debe ser mayor a 0!';
                        END
                    ELSE IF (@Id_Estado = 1 AND NOT EXISTS(SELECT 1 FROM tbl_periodos_academicos(NOLOCK) WHERE Id_Periodo = @Id_Periodo AND Id_Estado = 1))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No se puede activar la instancia porque el período académico relacionado no está ACTIVO!';
                        END
                    ELSE IF (@Fecha_Limite IS NOT NULL AND @Fecha_Programada IS NOT NULL AND @Fecha_Limite < @Fecha_Programada)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha límite no puede ser anterior a la fecha programada!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM tbl_evaluaciones_instancias(NOLOCK) WHERE Codigo_Instancia = @Codigo_Instancia AND @Codigo_Instancia IS NOT NULL AND @Codigo_Instancia != '')
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe una evaluación instancia con ese código!';
                        END
                    ELSE
                        BEGIN
                            -- Asignar valor por defecto si no se envía
                            SET @Calificacion_Maxima = ISNULL(@Calificacion_Maxima, 100);
                        END
                    -- Validar que la fecha programada esté dentro del rango del período académico
                    IF @o_Num = 0 AND @Fecha_Programada IS NOT NULL
                        BEGIN
                            DECLARE @FechaInicioPeriodo DATE;
                            DECLARE @FechaFinPeriodo DATE;
                            
                            SELECT @FechaInicioPeriodo = Fecha_Inicio, @FechaFinPeriodo = Fecha_Fin
                            FROM tbl_periodos_academicos(NOLOCK)
                            WHERE Id_Periodo = @Id_Periodo;
                            
                            IF @FechaInicioPeriodo IS NOT NULL AND CAST(@Fecha_Programada AS DATE) < @FechaInicioPeriodo
                                BEGIN
                                    SET @o_Num = -1;
                                    SET @o_Msg = '¡La fecha programada debe estar dentro del rango del período académico (no puede ser anterior a la fecha de inicio del período)!';
                                END
                            ELSE IF @FechaFinPeriodo IS NOT NULL AND CAST(@Fecha_Programada AS DATE) > @FechaFinPeriodo
                                BEGIN
                                    SET @o_Num = -1;
                                    SET @o_Msg = '¡La fecha programada debe estar dentro del rango del período académico (no puede ser posterior a la fecha de fin del período)!';
                                END
                        END
                    -- Validar que no haya otra instancia de evaluación en el rango de fechas (solo si ambas fechas están presentes)
                    IF @o_Num = 0 AND @Fecha_Programada IS NOT NULL AND @Fecha_Limite IS NOT NULL
                        BEGIN
                            IF EXISTS(
                                SELECT 1 
                                FROM tbl_evaluaciones_instancias(NOLOCK)
                                WHERE Id_Seccion = @Id_Seccion
                                AND Id_Evaluacion_Instancia != ISNULL(@Id_Evaluacion_Instancia, 0)
                                AND Fecha_Programada IS NOT NULL
                                AND (
                                    -- Nueva instancia empieza dentro del rango de una existente
                                    (@Fecha_Programada >= Fecha_Programada AND @Fecha_Programada <= ISNULL(Fecha_Limite, Fecha_Programada))
                                    -- Nueva instancia termina dentro del rango de una existente
                                    OR (@Fecha_Limite >= Fecha_Programada AND @Fecha_Limite <= ISNULL(Fecha_Limite, Fecha_Programada))
                                    -- Nueva instancia contiene completamente una existente
                                    OR (Fecha_Programada >= @Fecha_Programada AND ISNULL(Fecha_Limite, Fecha_Programada) <= @Fecha_Limite)
                                    -- Nueva instancia está completamente contenida en una existente
                                    OR (@Fecha_Programada >= Fecha_Programada AND @Fecha_Limite <= ISNULL(Fecha_Limite, Fecha_Programada))
                                )
                            )
                                BEGIN
                                    SET @o_Num = -1;
                                    SET @o_Msg = '¡Ya existe otra instancia de evaluación en el rango de fechas seleccionado para esta sección!';
                                END
                        END
                    IF @o_Num = 0
                        BEGIN
                            -- Generar código de instancia único si no se proporciona
                            IF ISNULL(@Codigo_Instancia, '') = ''
                                BEGIN
                                    DECLARE @Prefijo VARCHAR(10) = 'EVI-';
                                    DECLARE @Anio VARCHAR(4) = CAST(YEAR(GETDATE()) AS VARCHAR);
                                    DECLARE @Contador INT;
                                    SELECT @Contador = ISNULL(MAX(CAST(SUBSTRING(Codigo_Instancia, LEN(@Prefijo + @Anio + '-') + 1, LEN(Codigo_Instancia)) AS INT)), 0) + 1
                                    FROM tbl_evaluaciones_instancias(NOLOCK)
                                    WHERE Codigo_Instancia LIKE @Prefijo + @Anio + '-%';
                                    SET @Codigo_Instancia = @Prefijo + @Anio + '-' + RIGHT('000000' + CAST(@Contador AS VARCHAR), 6);
                                END

                            SET @iConcepto = CONCAT('AGREGANDO EVALUACIÓN INSTANCIA: ', @Codigo_Instancia);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_AgregarEvaluacionInstancia
                            BEGIN TRY
                                INSERT INTO tbl_evaluaciones_instancias(
                                    Codigo_Instancia, Id_Seccion, Id_Evaluacion_Modelo, Id_Periodo,
                                    Fecha_Programada, Fecha_Limite, Requiere_Revision_Interna,
                                    Numero_Version, Nivel_Aprobacion_Actual, Calificacion_Maxima, Id_Estado,
                                    Id_Estado_Publicacion,
                                    Id_Responsable_Revision, Fecha_Revision, Id_Responsable_Publicacion,
                                    Fecha_Publicacion, Id_Evaluacion_Padre, Observaciones_Revision,
                                    Motivo_Rechazo, Fecha_Creacion, Fecha_Modificacion,
                                    Id_Creador, Id_Modificador, Id_Transaccion
                                ) VALUES (
                                    @Codigo_Instancia, @Id_Seccion, @Id_Evaluacion_Modelo, @Id_Periodo,
                                    @Fecha_Programada, @Fecha_Limite, ISNULL(@Requiere_Revision_Interna, 0),
                                    ISNULL(@Numero_Version, 1), ISNULL(@Nivel_Aprobacion_Actual, 1), @Calificacion_Maxima, @Id_Estado,
                                    @Id_Estado, -- fallback para bases que aún tienen la columna
                                    @Id_Responsable_Revision, @Fecha_Revision, @Id_Responsable_Publicacion,
                                    @Fecha_Publicacion, @Id_Evaluacion_Padre, @Observaciones_Revision,
                                    @Motivo_Rechazo, @Fecha_Creacion, @Fecha_Modificacion,
                                    @Id_Creador, @Id_Modificador, @Id_Transaccion
                                );

                                IF @@ROWCOUNT = 0
                                    BEGIN
                                        ROLLBACK TRAN trx_AgregarEvaluacionInstancia;
                                        SET @o_Num = -1;
                                        SET @o_Msg = '¡No se insertó la instancia (0 filas afectadas)!';
                                        EXEC sp_transacciones
                                        @Modo = 'RBK',
                                        @Id_Transaccion = @Id_Transaccion;
                                    END
                                ELSE
                                    BEGIN
                                        COMMIT TRAN trx_AgregarEvaluacionInstancia;

                                        SET @o_Num = SCOPE_IDENTITY();
                                        SET @o_Msg = '¡Evaluación instancia agregada exitosamente!';

                                        EXEC sp_transacciones
                                        @Modo = 'UPD',
                                        @Id_Transaccion = @Id_Transaccion;
                                    END
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_AgregarEvaluacionInstancia;

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
            /* LISTAR TODAS LAS INSTANCIAS DE EVALUACIÓN */
            ELSE IF(@Id_Tipo_Transaccion = 146)
                BEGIN
                    BEGIN TRY
                        SELECT 
                            ei.Id_Evaluacion_Instancia,
                            ei.Codigo_Instancia,
                            ei.Id_Seccion,
                            ei.Id_Evaluacion_Modelo,
                            ei.Id_Periodo,
                            ei.Fecha_Programada,
                            ei.Fecha_Limite,
                            ei.Requiere_Revision_Interna,
                            ei.Numero_Version,
                            ei.Nivel_Aprobacion_Actual,
                            ei.Calificacion_Maxima,
                            ei.Id_Estado,
                            ei.Id_Responsable_Revision,
                            ei.Fecha_Revision,
                            ei.Id_Responsable_Publicacion,
                            ei.Fecha_Publicacion,
                            ei.Id_Evaluacion_Padre,
                            ei.Observaciones_Revision,
                            ei.Motivo_Rechazo,
                            ei.Fecha_Creacion,
                            ei.Fecha_Modificacion,
                            ei.Id_Creador,
                            ei.Id_Modificador,
                            ei.Id_Transaccion,
                            -- Información adicional de JOINs
                            s.Codigo_Seccion,
                            m.Nombre_Materia,
                            em.Nombre_Evaluacion AS Nombre_Modelo_Evaluacion,
                            em.Codigo_Modelo,
                            pa.Nombre_Periodo,
                            pa.Codigo_Periodo,
                            e.Nombre_Estado,
                            e.Nombre_Estado AS Nombre_Estado
                        FROM tbl_evaluaciones_instancias(NOLOCK) ei
                        LEFT JOIN tbl_secciones(NOLOCK) s ON ei.Id_Seccion = s.Id_Seccion
                        LEFT JOIN cls_materias_periodos(NOLOCK) mp ON s.Id_Materia_Periodo = mp.Id_Materia_Periodo
                        LEFT JOIN cls_materias(NOLOCK) m ON mp.Id_Materia = m.Id_Materia
                        LEFT JOIN cls_evaluaciones_modelos(NOLOCK) em ON ei.Id_Evaluacion_Modelo = em.Id_Evaluacion_Modelo
                        LEFT JOIN tbl_periodos_academicos(NOLOCK) pa ON ei.Id_Periodo = pa.Id_Periodo
                        LEFT JOIN cls_estados(NOLOCK) e ON ei.Id_Estado = e.Id_Estado
                        ORDER BY ei.Fecha_Programada DESC, m.Nombre_Materia ASC;

                        SET @o_Num = 0;
                        SET @o_Msg = '¡Instancias de evaluación listadas exitosamente!';
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
            /* ACTUALIZAR EVALUACION INSTANCIA */
            ELSE IF(@Id_Tipo_Transaccion = 125)
                BEGIN
                    IF ISNULL(@Id_Evaluacion_Instancia, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de evaluación instancia!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_evaluaciones_instancias(NOLOCK) WHERE Id_Evaluacion_Instancia = @Id_Evaluacion_Instancia)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La evaluación instancia no existe!';
                        END
                    ELSE IF (@Fecha_Limite IS NOT NULL AND @Fecha_Programada IS NOT NULL AND @Fecha_Limite < @Fecha_Programada)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha límite no puede ser anterior a la fecha programada!';
                        END
                    ELSE IF (@Codigo_Instancia IS NOT NULL AND EXISTS(SELECT 1 FROM tbl_evaluaciones_instancias(NOLOCK) WHERE Codigo_Instancia = @Codigo_Instancia AND Id_Evaluacion_Instancia <> @Id_Evaluacion_Instancia))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe otra evaluación instancia con ese código!';
                        END
                    ELSE IF (@Calificacion_Maxima IS NOT NULL AND @Calificacion_Maxima <= 0)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La calificación máxima debe ser mayor a 0!';
                        END
                    ELSE
                        BEGIN
                            DECLARE @EstadoFinalInst INT = COALESCE(@Id_Estado, (SELECT Id_Estado FROM tbl_evaluaciones_instancias WHERE Id_Evaluacion_Instancia = @Id_Evaluacion_Instancia));
                            DECLARE @PeriodoFinalInst INT = COALESCE(@Id_Periodo, (SELECT Id_Periodo FROM tbl_evaluaciones_instancias WHERE Id_Evaluacion_Instancia = @Id_Evaluacion_Instancia));

                            IF (@EstadoFinalInst = 1 AND NOT EXISTS(SELECT 1 FROM tbl_periodos_academicos(NOLOCK) WHERE Id_Periodo = @PeriodoFinalInst AND Id_Estado = 1))
                                BEGIN
                                    SET @o_Num = -1;
                                    SET @o_Msg = '¡No se puede activar la instancia porque el período académico relacionado no está ACTIVO!';
                                END
                        END

                    IF @o_Num = 0
                        BEGIN
                            SET @iConcepto = CONCAT('ACTUALIZANDO EVALUACIÓN INSTANCIA ID: ', @Id_Evaluacion_Instancia);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_ActualizarEvalIns
                            BEGIN TRY
                                UPDATE tbl_evaluaciones_instancias
                                SET Codigo_Instancia = COALESCE(@Codigo_Instancia, Codigo_Instancia),
                                    Id_Seccion = COALESCE(@Id_Seccion, Id_Seccion),
                                    Id_Evaluacion_Modelo = COALESCE(@Id_Evaluacion_Modelo, Id_Evaluacion_Modelo),
                                    Id_Periodo = COALESCE(@Id_Periodo, Id_Periodo),
                                    Fecha_Programada = COALESCE(@Fecha_Programada, Fecha_Programada),
                                    Fecha_Limite = COALESCE(@Fecha_Limite, Fecha_Limite),
                                    Requiere_Revision_Interna = COALESCE(@Requiere_Revision_Interna, Requiere_Revision_Interna),
                                    Numero_Version = COALESCE(@Numero_Version, Numero_Version),
                                    Nivel_Aprobacion_Actual = COALESCE(@Nivel_Aprobacion_Actual, Nivel_Aprobacion_Actual),
                                    Calificacion_Maxima = COALESCE(@Calificacion_Maxima, Calificacion_Maxima),
                                    Id_Estado = COALESCE(@Id_Estado, Id_Estado),
                                    Id_Responsable_Revision = COALESCE(@Id_Responsable_Revision, Id_Responsable_Revision),
                                    Fecha_Revision = COALESCE(@Fecha_Revision, Fecha_Revision),
                                    Id_Responsable_Publicacion = COALESCE(@Id_Responsable_Publicacion, Id_Responsable_Publicacion),
                                    Fecha_Publicacion = COALESCE(@Fecha_Publicacion, Fecha_Publicacion),
                                    Id_Evaluacion_Padre = COALESCE(@Id_Evaluacion_Padre, Id_Evaluacion_Padre),
                                    Observaciones_Revision = COALESCE(@Observaciones_Revision, Observaciones_Revision),
                                    Motivo_Rechazo = COALESCE(@Motivo_Rechazo, Motivo_Rechazo),
                                    Fecha_Modificacion = @Fecha_Modificacion,
                                    Id_Modificador = @Id_Modificador,
                                    Id_Transaccion = @Id_Transaccion
                                WHERE Id_Evaluacion_Instancia = @Id_Evaluacion_Instancia;

                                COMMIT TRAN trx_ActualizarEvalIns;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Evaluación instancia actualizada exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_ActualizarEvalIns;

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

