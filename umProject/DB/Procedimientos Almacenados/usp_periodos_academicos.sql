USE umDb
GO

/*
tbl_periodos_academicos
*/

CREATE OR ALTER PROC usp_periodos_academicos
(
    @Id_Periodo INT = NULL,
    @Codigo_Periodo VARCHAR(20) = NULL,
    @Nombre_Periodo NVARCHAR(100) = NULL,
    @Id_Tipo_Periodo INT = NULL,
    @Fecha_Inicio DATE = NULL,
    @Fecha_Fin DATE = NULL,
    @Fecha_Cierre_Calificaciones DATE = NULL,
    @Es_Periodo_Actual BIT = NULL,
    @Codigo_Integracion VARCHAR(30) = NULL,
    @Observaciones NVARCHAR(255) = NULL,
    @Id_Estado INT = NULL,
    @Id_Estado_Publicacion INT = NULL, -- obsoleto, se mantiene para compatibilidad pero no se usa
    @Fecha_Creacion DATETIME = NULL,
    @Fecha_Modificacion DATETIME = NULL,
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
    DECLARE @Permiso INT, @iConcepto NVARCHAR(255), @Linea_Error INT, @Numero_Error INT, @Mensaje_Error NVARCHAR(255), @Origen_Error NVARCHAR(50) = ERROR_PROCEDURE();
    SET @Fecha_Creacion = GETDATE();
    SET @Fecha_Modificacion = GETDATE();
    SET @Id_Creador = @Id_Sesion;
    SET @Id_Modificador = @Id_Sesion;
    SET @Permiso = dbo.fn_Validar_Permisos(@Id_Sesion, @Id_Tipo_Transaccion);
    SET @o_Num = 0; -- Inicializar parámetro de salida

    IF(@Permiso = 1)
        BEGIN
            /* FILTRAR POR ID PERIODO */
            IF(@Id_Tipo_Transaccion = 80)
                BEGIN
                    IF ISNULL(@Id_Periodo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de período!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_periodos_academicos(NOLOCK) WHERE Id_Periodo = @Id_Periodo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El período no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    p.Id_Periodo,
                                    p.Codigo_Periodo,
                                    p.Nombre_Periodo,
                                    p.Id_Tipo_Periodo,
                                    p.Fecha_Inicio,
                                    p.Fecha_Fin,
                                    p.Fecha_Cierre_Calificaciones,
                                    p.Es_Periodo_Actual,
                                    p.Codigo_Integracion,
                                    p.Observaciones,
                                     p.Id_Estado,
                                     CAST(NULL AS INT) AS Id_Estado_Publicacion, -- compatibilidad
                                    p.Hash_Configuracion,
                                    p.Codigo_Control,
                                    p.Fecha_Creacion,
                                    p.Fecha_Modificacion,
                                    p.Id_Creador,
                                    p.Id_Modificador,
                                    p.Id_Transaccion,
                                    p.RowVersion,
                                    e.Nombre_Estado AS Nombre_Estado
                                FROM tbl_periodos_academicos p(NOLOCK)
                                INNER JOIN cls_estados e(NOLOCK) ON p.Id_Estado = e.Id_Estado
                                WHERE p.Id_Periodo = @Id_Periodo;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Período encontrado!';
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
            /* FILTRAR POR CODIGO PERIODO O LISTAR TODOS */
            ELSE IF(@Id_Tipo_Transaccion = 81)
                BEGIN
                    BEGIN TRY
                        SELECT 
                            p.Id_Periodo,
                            p.Codigo_Periodo,
                            p.Nombre_Periodo,
                            p.Id_Tipo_Periodo,
                            p.Fecha_Inicio,
                            p.Fecha_Fin,
                            p.Fecha_Cierre_Calificaciones,
                            p.Es_Periodo_Actual,
                            p.Codigo_Integracion,
                            p.Observaciones,
                             p.Id_Estado,
                             CAST(NULL AS INT) AS Id_Estado_Publicacion, -- compatibilidad
                            p.Hash_Configuracion,
                            p.Codigo_Control,
                            p.Fecha_Creacion,
                            p.Fecha_Modificacion,
                            p.Id_Creador,
                            p.Id_Modificador,
                            p.Id_Transaccion,
                            p.RowVersion,
                            e.Nombre_Estado AS Nombre_Estado
                        FROM tbl_periodos_academicos p(NOLOCK)
                        INNER JOIN cls_estados e(NOLOCK) ON p.Id_Estado = e.Id_Estado
                        WHERE (@Codigo_Periodo IS NULL OR @Codigo_Periodo = '' OR UPPER(p.Codigo_Periodo) LIKE '%' + UPPER(@Codigo_Periodo) + '%')
                        -- Mostrar todos los períodos independientemente del estado
                        ORDER BY p.Fecha_Creacion DESC, p.Id_Periodo DESC;

                        SET @o_Num = 0;
                        SET @o_Msg = '¡Períodos encontrados!';
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
            /* AGREGAR PERIODO */
            ELSE IF(@Id_Tipo_Transaccion = 78)
                BEGIN
                    DECLARE @AnioValido INT;
                    DECLARE @NumeroRomano VARCHAR(5);
                    DECLARE @CodigoValido BIT = 0;
                    
                    IF ISNULL(@Codigo_Periodo, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El campo Código Período no debe ir vacío!';
                        END
                    -- Validar formato del código: AÑO-I, AÑO-II, AÑO-III
                    ELSE IF LEN(@Codigo_Periodo) < 6 OR CHARINDEX('-', @Codigo_Periodo) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El código del período debe tener el formato: AÑO-I, AÑO-II o AÑO-III (ejemplo: 2024-I)!';
                        END
                    ELSE
                        BEGIN
                            -- Extraer año y número romano
                            SET @AnioValido = TRY_CAST(LEFT(@Codigo_Periodo, CHARINDEX('-', @Codigo_Periodo) - 1) AS INT);
                            SET @NumeroRomano = UPPER(LTRIM(RTRIM(SUBSTRING(@Codigo_Periodo, CHARINDEX('-', @Codigo_Periodo) + 1, LEN(@Codigo_Periodo)))));
                            
                            -- Validar año válido (entre 2000 y 2100)
                            IF @AnioValido IS NULL OR @AnioValido < 2000 OR @AnioValido > 2100
                                BEGIN
                                    SET @o_Num = -1;
                                    SET @o_Msg = '¡El año en el código del período debe ser un número válido entre 2000 y 2100!';
                                END
                            -- Validar número romano (I, II, III)
                            ELSE IF @NumeroRomano NOT IN ('I', 'II', 'III')
                                BEGIN
                                    SET @o_Num = -1;
                                    SET @o_Msg = '¡El código del período debe terminar en I, II o III (ejemplo: 2024-I, 2024-II, 2024-III)!';
                                END
                            ELSE
                                BEGIN
                                    SET @CodigoValido = 1;
                                END
                        END
                    
                    IF @CodigoValido = 1 AND EXISTS(SELECT 1 FROM tbl_periodos_academicos(NOLOCK) WHERE Codigo_Periodo = @Codigo_Periodo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe un período con ese código!';
                        END
                    ELSE IF @CodigoValido = 1 AND ISNULL(@Nombre_Periodo, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El campo Nombre Período no debe ir vacío!';
                        END
                    ELSE IF @CodigoValido = 1 AND ISNULL(@Fecha_Inicio, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe especificar la fecha de inicio!';
                        END
                    ELSE IF @CodigoValido = 1 AND ISNULL(@Fecha_Fin, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe especificar la fecha de fin!';
                        END
                    -- Validar que la fecha de fin sea mayor que la fecha de inicio (validación básica)
                    ELSE IF @CodigoValido = 1 AND @Fecha_Fin <= @Fecha_Inicio
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de fin debe ser mayor que la fecha de inicio!';
                        END
                    -- Validar que la fecha de cierre de calificaciones sea mayor o igual a la fecha de fin
                    ELSE IF @CodigoValido = 1 AND (@Fecha_Cierre_Calificaciones IS NOT NULL AND @Fecha_Cierre_Calificaciones < @Fecha_Fin)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de cierre de calificaciones no puede ser menor a la fecha de fin!';
                        END
                    ELSE IF @CodigoValido = 1 AND ISNULL(@Id_Estado, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un estado al período!';
                        END
                    -- Validar que solo se permita estado "EN REVISION" (4) al agregar
                    ELSE IF @CodigoValido = 1 AND @Id_Estado <> 4
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Al crear un período académico solo se permite el estado "EN REVISION"!';
                        END
                    -- Validar Es_Periodo_Actual según el estado
                    -- Si estado es PLANIFICADA (7) o PENDIENTE (3), Es_Periodo_Actual debe ser 0
                    -- Si estado es ACTIVO (1), Es_Periodo_Actual debe ser 1
                    ELSE IF @CodigoValido = 1 AND (@Id_Estado = 7 OR @Id_Estado = 3) AND ISNULL(@Es_Periodo_Actual, 0) = 1
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Si el estado es PLANIFICADA o PENDIENTE, el campo "Es Período Actual" debe estar desactivado!';
                        END
                    ELSE IF @CodigoValido = 1 AND @Id_Estado = 1 AND ISNULL(@Es_Periodo_Actual, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Si el estado es ACTIVO, el campo "Es Período Actual" debe estar activado!';
                        END
                    ELSE
                        BEGIN
                            SET @iConcepto = CONCAT('AGREGANDO PERÍODO ACADÉMICO: ', @Nombre_Periodo);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;
                            PRINT 'DEBUG PA79: transaccion creada Id_Transaccion=' + ISNULL(CAST(@Id_Transaccion AS VARCHAR(20)),'NULL');

                            -- Generar código de integración si no se proporciona
                            IF ISNULL(@Codigo_Integracion, '') = ''
                                BEGIN
                                    SET @Codigo_Integracion = 'PER-' + @Codigo_Periodo;
                                END

                            BEGIN TRAN trx_AgregarPeriodo
                            BEGIN TRY
                                INSERT INTO tbl_periodos_academicos(
                                    Codigo_Periodo,
                                    Nombre_Periodo,
                                    Id_Tipo_Periodo,
                                    Fecha_Inicio,
                                    Fecha_Fin,
                                    Fecha_Cierre_Calificaciones,
                                    Es_Periodo_Actual,
                                    Codigo_Integracion,
                                    Observaciones,
                                    Id_Estado,
                                    -- Id_Estado_Publicacion, -- columna obsoleta
                                    Fecha_Creacion,
                                    Fecha_Modificacion,
                                    Id_Creador,
                                    Id_Modificador,
                                    Id_Transaccion
                                ) VALUES (
                                    @Codigo_Periodo,
                                    @Nombre_Periodo,
                                    @Id_Tipo_Periodo,
                                    @Fecha_Inicio,
                                    @Fecha_Fin,
                                    @Fecha_Cierre_Calificaciones,
                                    ISNULL(@Es_Periodo_Actual, 0),
                                    @Codigo_Integracion,
                                    @Observaciones,
                                    @Id_Estado,
                                    -- @Id_Estado_Publicacion,
                                    @Fecha_Creacion,
                                    @Fecha_Modificacion,
                                    @Id_Creador,
                                    @Id_Modificador,
                                    @Id_Transaccion
                                );

                                COMMIT TRAN trx_AgregarPeriodo;

                                SET @o_Num = SCOPE_IDENTITY();
                                SET @o_Msg = '¡Período académico agregado exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_AgregarPeriodo;

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
            /* ACTUALIZAR PERIODO */
            ELSE IF(@Id_Tipo_Transaccion = 79)
                BEGIN
                    IF ISNULL(@Id_Periodo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de período!';
                            RETURN;
                        END
                    IF NOT EXISTS(SELECT 1 FROM tbl_periodos_academicos(NOLOCK) WHERE Id_Periodo = @Id_Periodo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El período no existe!';
                            RETURN;
                        END
                    IF (@Fecha_Inicio IS NOT NULL AND @Fecha_Fin IS NOT NULL AND @Fecha_Fin <= @Fecha_Inicio)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de fin debe ser mayor que la fecha de inicio!';
                            RETURN;
                        END
                    IF (@Fecha_Cierre_Calificaciones IS NOT NULL AND @Fecha_Inicio IS NOT NULL AND @Fecha_Cierre_Calificaciones < @Fecha_Inicio)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de cierre de calificaciones no puede ser anterior a la fecha de inicio!';
                            RETURN;
                        END
                    IF @Codigo_Periodo IS NOT NULL
                        BEGIN
                            -- Validar formato y contenido del código
                            IF (LEN(@Codigo_Periodo) < 6 OR CHARINDEX('-', @Codigo_Periodo) = 0)
                                BEGIN
                                    SET @o_Num = -1;
                                    SET @o_Msg = '¡El código del período debe tener el formato: AÑO-I, AÑO-II o AÑO-III (ejemplo: 2024-I)!';
                                    RETURN;
                                END
                            
                            DECLARE @AnioValidoUpd INT;
                            DECLARE @NumeroRomanoUpd VARCHAR(5);
                            
                            SET @AnioValidoUpd = TRY_CAST(LEFT(@Codigo_Periodo, CHARINDEX('-', @Codigo_Periodo) - 1) AS INT);
                            SET @NumeroRomanoUpd = UPPER(LTRIM(RTRIM(SUBSTRING(@Codigo_Periodo, CHARINDEX('-', @Codigo_Periodo) + 1, LEN(@Codigo_Periodo)))));
                            
                            IF @AnioValidoUpd IS NULL OR @AnioValidoUpd < 2000 OR @AnioValidoUpd > 2100
                                BEGIN
                                    SET @o_Num = -1;
                                    SET @o_Msg = '¡El año en el código del período debe ser un número válido entre 2000 y 2100!';
                                    RETURN;
                                END
                            IF @NumeroRomanoUpd NOT IN ('I', 'II', 'III')
                                BEGIN
                                    SET @o_Num = -1;
                                    SET @o_Msg = '¡El código del período debe terminar en I, II o III (ejemplo: 2024-I, 2024-II, 2024-III)!';
                                    RETURN;
                                END
                            IF EXISTS(SELECT 1 FROM tbl_periodos_academicos(NOLOCK) WHERE Codigo_Periodo = @Codigo_Periodo AND Id_Periodo <> @Id_Periodo)
                                BEGIN
                                    SET @o_Num = -1;
                                    SET @o_Msg = '¡Ya existe otro período con ese código!';
                                    RETURN;
                                END
                        END
                    -- Validar que solo se permitan estados PENDIENTE (3), ACTIVO (1) e INACTIVO (2) al actualizar
                    IF @Id_Estado IS NOT NULL AND @Id_Estado NOT IN (1, 2, 3)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Al actualizar un período académico solo se permiten los estados: PENDIENTE, ACTIVO o INACTIVO (FINALIZADO)!';
                            RETURN;
                        END
                    -- Validar Es_Periodo_Actual según el estado
                    IF @Id_Estado = 1 AND ISNULL(@Es_Periodo_Actual, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Si el estado es ACTIVO, el campo "Es Período Actual" debe estar activado!';
                            RETURN;
                        END
                    -- Validar que solo un período tenga Es_Periodo_Actual = 1
                    IF ISNULL(@Es_Periodo_Actual, 0) = 1
                        BEGIN
                            IF EXISTS(SELECT 1 FROM tbl_periodos_academicos(NOLOCK) WHERE Es_Periodo_Actual = 1 AND Id_Periodo <> @Id_Periodo)
                                BEGIN
                                    SET @o_Num = -1;
                                    SET @o_Msg = '¡Solo puede haber un período académico con "Es Período Actual" activado! Debe desactivar el período actual antes de activar este.';
                                    RETURN;
                                END
                        END

                    SET @iConcepto = CONCAT('ACTUALIZANDO PERÍODO ACADÉMICO ID: ', @Id_Periodo);
                    EXEC sp_transacciones
                    @Modo = 'INS',
                    @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                    @Id_Autor = @Id_Sesion,
                    @Concepto = @iConcepto,
                    @o_Num = @Id_Transaccion OUTPUT;

                    BEGIN TRAN trx_ActualizarPeriodo
                    BEGIN TRY
                        UPDATE tbl_periodos_academicos
                        SET Codigo_Periodo = COALESCE(@Codigo_Periodo, Codigo_Periodo),
                            Nombre_Periodo = COALESCE(@Nombre_Periodo, Nombre_Periodo),
                            Id_Tipo_Periodo = COALESCE(@Id_Tipo_Periodo, Id_Tipo_Periodo),
                            Fecha_Inicio = COALESCE(@Fecha_Inicio, Fecha_Inicio),
                            Fecha_Fin = COALESCE(@Fecha_Fin, Fecha_Fin),
                            Fecha_Cierre_Calificaciones = COALESCE(@Fecha_Cierre_Calificaciones, Fecha_Cierre_Calificaciones),
                            Es_Periodo_Actual = COALESCE(@Es_Periodo_Actual, Es_Periodo_Actual),
                            Codigo_Integracion = COALESCE(@Codigo_Integracion, Codigo_Integracion),
                            Observaciones = COALESCE(@Observaciones, Observaciones),
                            Id_Estado = COALESCE(@Id_Estado, Id_Estado),
                            -- Id_Estado_Publicacion = COALESCE(@Id_Estado_Publicacion, Id_Estado_Publicacion),
                            Fecha_Modificacion = @Fecha_Modificacion,
                            Id_Modificador = @Id_Modificador,
                            Id_Transaccion = @Id_Transaccion
                        WHERE Id_Periodo = @Id_Periodo;

                        COMMIT TRAN trx_ActualizarPeriodo;

                        SET @o_Num = 0;
                        SET @o_Msg = '¡Período académico actualizado exitosamente!';

                        EXEC sp_transacciones
                        @Modo = 'UPD',
                        @Id_Transaccion = @Id_Transaccion;
                    END TRY
                    BEGIN CATCH
                        ROLLBACK TRAN trx_ActualizarPeriodo;

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
                        RETURN;
                    END CATCH
                END
            /* VALIDAR ACTIVACION PERIODO */
            ELSE IF(@Id_Tipo_Transaccion = 185)
                BEGIN
                    IF ISNULL(@Id_Periodo, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de período!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_periodos_academicos(NOLOCK) WHERE Id_Periodo = @Id_Periodo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El período no existe!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_periodos_academicos(NOLOCK) WHERE Id_Periodo = @Id_Periodo AND Id_Estado = 4)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Solo se puede validar la activación de un período que esté en estado EN REVISION!';
                        END
                    ELSE
                        BEGIN
                            DECLARE @ErroresValidacion NVARCHAR(MAX) = '';
                            DECLARE @MateriasPeriodo TABLE(Id_Materia_Periodo INT);
                            
                            -- 1. Obtener todas las materias-período del período
                            INSERT INTO @MateriasPeriodo(Id_Materia_Periodo)
                            SELECT Id_Materia_Periodo
                            FROM cls_materias_periodos(NOLOCK)
                            WHERE Id_Periodo_Academico = @Id_Periodo AND Activo = 1;
                            
                            IF NOT EXISTS(SELECT 1 FROM @MateriasPeriodo)
                                BEGIN
                                    SET @ErroresValidacion = @ErroresValidacion + 'El período académico no tiene materias asignadas. Debe agregar al menos una materia antes de activar el período. ';
                                END
                            ELSE
                                BEGIN
                                    -- 2. Para cada materia-período, validar sus secciones
                                    DECLARE @IdMateriaPeriodo INT;
                                    DECLARE @IdMateria INT;
                                    DECLARE materias_cursor CURSOR FOR 
                                        SELECT mp.Id_Materia_Periodo, mp.Id_Materia 
                                        FROM @MateriasPeriodo mp_temp
                                        INNER JOIN cls_materias_periodos(NOLOCK) mp ON mp_temp.Id_Materia_Periodo = mp.Id_Materia_Periodo;
                                    OPEN materias_cursor;
                                    FETCH NEXT FROM materias_cursor INTO @IdMateriaPeriodo, @IdMateria;
                                    
                                    WHILE @@FETCH_STATUS = 0
                                    BEGIN
                                        DECLARE @Secciones TABLE(Id_Seccion INT, Id_Estado INT, Codigo_Seccion VARCHAR(20), Nombre_Estado NVARCHAR(50));
                                        
                                        -- Obtener secciones de la materia-período con nombre del estado
                                        INSERT INTO @Secciones(Id_Seccion, Id_Estado, Codigo_Seccion, Nombre_Estado)
                                        SELECT s.Id_Seccion, s.Id_Estado, s.Codigo_Seccion, ISNULL(e.Nombre_Estado, 'DESCONOCIDO')
                                        FROM tbl_secciones(NOLOCK) s
                                        LEFT JOIN cls_estados(NOLOCK) e ON s.Id_Estado = e.Id_Estado
                                        WHERE s.Id_Materia_Periodo = @IdMateriaPeriodo AND s.Activo = 1;
                                        
                                        -- Validar que la materia-período tenga al menos una sección
                                        IF NOT EXISTS(SELECT 1 FROM @Secciones)
                                            BEGIN
                                                DECLARE @CodigoMateria VARCHAR(10);
                                                SELECT @CodigoMateria = Codigo_Materia FROM cls_materias(NOLOCK) WHERE Id_Materia = @IdMateria;
                                                SET @ErroresValidacion = @ErroresValidacion + 'La materia ' + ISNULL(@CodigoMateria, 'N/A') + ' no tiene secciones asignadas. Debe crear al menos una sección antes de activar el período. ';
                                            END
                                        ELSE
                                            BEGIN
                                                DECLARE @IdSeccion INT;
                                                DECLARE @IdEstadoSeccion INT;
                                                DECLARE @CodigoSeccion VARCHAR(20);
                                                DECLARE @NombreEstadoSeccion NVARCHAR(50);
                                                DECLARE secciones_cursor CURSOR FOR SELECT Id_Seccion, Id_Estado, Codigo_Seccion, Nombre_Estado FROM @Secciones;
                                                OPEN secciones_cursor;
                                                FETCH NEXT FROM secciones_cursor INTO @IdSeccion, @IdEstadoSeccion, @CodigoSeccion, @NombreEstadoSeccion;
                                                
                                                WHILE @@FETCH_STATUS = 0
                                                BEGIN
                                                    -- Omitir secciones INACTIVAS (Id_Estado = 2) para la validación
                                                    IF @IdEstadoSeccion = 2
                                                    BEGIN
                                                        FETCH NEXT FROM secciones_cursor INTO @IdSeccion, @IdEstadoSeccion, @CodigoSeccion, @NombreEstadoSeccion;
                                                        CONTINUE;
                                                    END
                                                    -- 2.1. La sección debe estar ACTIVA (Id_Estado = 1)
                                                    IF @IdEstadoSeccion != 1
                                                        BEGIN
                                                            SET @ErroresValidacion = @ErroresValidacion + 'La sección ' + @CodigoSeccion + ' no está ACTIVA (Estado actual: ' + @NombreEstadoSeccion + '). Todas las secciones deben estar ACTIVAS antes de activar el período. ';
                                                        END
                                                    ELSE
                                                        BEGIN
                                                            -- 2.2. La sección debe tener al menos un grupo asignado
                                                            IF NOT EXISTS(SELECT 1 FROM cls_grupos_secciones(NOLOCK) WHERE Id_Seccion = @IdSeccion AND Activo = 1)
                                                                BEGIN
                                                                    SET @ErroresValidacion = @ErroresValidacion + 'La sección ' + @CodigoSeccion + ' no tiene grupos asignados. Debe asignar al menos un grupo antes de activar el período. ';
                                                                END
                                                            ELSE
                                                                BEGIN
                                                                    -- 2.3. Validar grupos asignados: deben estar ACTIVOS y tener inscripciones activas
                                                                    DECLARE @IdGrupo INT;
                                                                    DECLARE @CodigoGrupo VARCHAR(20);
                                                                    DECLARE grupos_cursor CURSOR FOR 
                                                                        SELECT DISTINCT gs.Id_Grupo, g.Codigo_Grupo
                                                                        FROM cls_grupos_secciones gs(NOLOCK)
                                                                        INNER JOIN tbl_grupos(NOLOCK) g ON gs.Id_Grupo = g.Id_Grupo
                                                                        WHERE gs.Id_Seccion = @IdSeccion AND gs.Activo = 1;
                                                                    OPEN grupos_cursor;
                                                                    FETCH NEXT FROM grupos_cursor INTO @IdGrupo, @CodigoGrupo;
                                                                    
                                                                    WHILE @@FETCH_STATUS = 0
                                                                    BEGIN
                                                                        -- 2.3.1. El grupo debe estar ACTIVO (Id_Estado = 1 y Activo = 1)
                                                                        IF NOT EXISTS(
                                                                            SELECT 1 
                                                                            FROM tbl_grupos(NOLOCK) 
                                                                            WHERE Id_Grupo = @IdGrupo 
                                                                            AND Id_Estado = 1 
                                                                            AND Activo = 1
                                                                        )
                                                                            BEGIN
                                                                                SET @ErroresValidacion = @ErroresValidacion + 'El grupo ' + @CodigoGrupo + ' asignado a la sección ' + @CodigoSeccion + ' no está ACTIVO. Todos los grupos deben estar ACTIVOS antes de activar el período. ';
                                                                            END
                                                                        ELSE
                                                                            BEGIN
                                                                                -- 2.3.2. El grupo debe tener al menos una inscripción activa
                                                                                IF NOT EXISTS(
                                                                                    SELECT 1 
                                                                                    FROM tbl_grupos_inscripciones gi(NOLOCK)
                                                                                    INNER JOIN tbl_inscripciones i(NOLOCK) ON gi.Id_Inscripcion = i.Id_Inscripcion
                                                                                    WHERE gi.Id_Grupo = @IdGrupo 
                                                                                    AND gi.Activo = 1 
                                                                                    AND i.Id_Estado = 1 -- ACTIVA
                                                                                )
                                                                                    BEGIN
                                                                                        SET @ErroresValidacion = @ErroresValidacion + 'El grupo ' + @CodigoGrupo + ' asignado a la sección ' + @CodigoSeccion + ' no tiene inscripciones ACTIVAS. Todos los grupos deben tener al menos una inscripción ACTIVA antes de activar el período. ';
                                                                                    END
                                                                                ELSE
                                                                                    BEGIN
                                                                                        -- 2.3.3. Validar que las inscripciones no estén en REVISION (4) o PENDIENTE (3)
                                                                                        IF EXISTS(
                                                                                            SELECT 1 
                                                                                            FROM tbl_grupos_inscripciones gi(NOLOCK)
                                                                                            INNER JOIN tbl_inscripciones i(NOLOCK) ON gi.Id_Inscripcion = i.Id_Inscripcion
                                                                                            WHERE gi.Id_Grupo = @IdGrupo 
                                                                                            AND gi.Activo = 1 
                                                                                            AND i.Id_Estado IN (3, 4) -- PENDIENTE o EN REVISION
                                                                                        )
                                                                                            BEGIN
                                                                                                SET @ErroresValidacion = @ErroresValidacion + 'El grupo ' + @CodigoGrupo + ' asignado a la sección ' + @CodigoSeccion + ' tiene inscripciones en estado PENDIENTE o EN REVISION. Todas las inscripciones deben estar ACTIVAS o INACTIVAS antes de activar el período. ';
                                                                                            END
                                                                                    END
                                                                            END
                                                                        
                                                                        FETCH NEXT FROM grupos_cursor INTO @IdGrupo, @CodigoGrupo;
                                                                    END
                                                                    CLOSE grupos_cursor;
                                                                    DEALLOCATE grupos_cursor;
                                                                    
                                                                    -- 2.4. La sección debe tener al menos una instancia de evaluación asignada
                                                                    IF NOT EXISTS(
                                                                        SELECT 1 
                                                                        FROM tbl_evaluaciones_instancias ei(NOLOCK)
                                                                        WHERE ei.Id_Seccion = @IdSeccion 
                                                                    )
                                                                        BEGIN
                                                                            SET @ErroresValidacion = @ErroresValidacion + 'La sección ' + @CodigoSeccion + ' no tiene instancias de evaluación asignadas. Debe asignar al menos una instancia de evaluación antes de activar el período. ';
                                                                        END
                                                                    ELSE
                                                                        BEGIN
                                                                            -- 2.5. Las instancias de evaluación deben estar en PENDIENTE (3), no en REVISION (4)
                                                                            IF EXISTS(
                                                                                SELECT 1 
                                                                                FROM tbl_evaluaciones_instancias ei(NOLOCK)
                                                                                WHERE ei.Id_Seccion = @IdSeccion 
                                                                                AND ei.Id_Estado = 4 -- EN REVISION
                                                                            )
                                                                                BEGIN
                                                                                    SET @ErroresValidacion = @ErroresValidacion + 'La sección ' + @CodigoSeccion + ' tiene instancias de evaluación en estado EN REVISION. Todas las instancias de evaluación deben estar en estado PENDIENTE antes de activar el período. ';
                                                                                END
                                                                            ELSE IF EXISTS(
                                                                                SELECT 1 
                                                                                FROM tbl_evaluaciones_instancias ei(NOLOCK)
                                                                                WHERE ei.Id_Seccion = @IdSeccion 
                                                                                AND ei.Id_Estado != 3 -- No es PENDIENTE
                                                                            )
                                                                                BEGIN
                                                                                    SET @ErroresValidacion = @ErroresValidacion + 'La sección ' + @CodigoSeccion + ' tiene instancias de evaluación que no están en estado PENDIENTE. Todas las instancias de evaluación deben estar en estado PENDIENTE antes de activar el período. ';
                                                                                END
                                                                            ELSE
                                                                                BEGIN
                                                                                    -- 2.6. Validar que la suma de calificaciones máximas sea 100
                                                                                    DECLARE @SumaCalificacionMax DECIMAL(10,2);
                                                                                    SELECT @SumaCalificacionMax = SUM(ISNULL(ei.Calificacion_Maxima, 0))
                                                                                    FROM tbl_evaluaciones_instancias ei(NOLOCK)
                                                                                    WHERE ei.Id_Seccion = @IdSeccion
                                                                                    AND ei.Id_Estado = 3; -- PENDIENTE

                                                                                    IF ISNULL(@SumaCalificacionMax, 0) <> 100
                                                                                        BEGIN
                                                                                            SET @ErroresValidacion = @ErroresValidacion + 'La sección ' + @CodigoSeccion + ' tiene un total de calificación máxima de ' + CAST(ISNULL(@SumaCalificacionMax, 0) AS VARCHAR(20)) + ' puntos. Debe sumar exactamente 100 puntos antes de activar el período. ';
                                                                                        END
                                                                                END
                                                                        END
                                                                END
                                                        END
                                                    
                                                    FETCH NEXT FROM secciones_cursor INTO @IdSeccion, @IdEstadoSeccion, @CodigoSeccion, @NombreEstadoSeccion;
                                                END
                                                CLOSE secciones_cursor;
                                                DEALLOCATE secciones_cursor;
                                            END
                                        
                                        FETCH NEXT FROM materias_cursor INTO @IdMateriaPeriodo, @IdMateria;
                                    END
                                    CLOSE materias_cursor;
                                    DEALLOCATE materias_cursor;
                                END
                            
                            IF LEN(@ErroresValidacion) > 0
                                BEGIN
                                    SET @o_Num = -1;
                                    SET @o_Msg = @ErroresValidacion;
                                END
                            ELSE
                                BEGIN
                                    SET @o_Num = 0;
                                    SET @o_Msg = '¡Validación exitosa. El período puede ser activado.';
                                END
                        END
                END
        END
    ELSE
        BEGIN
            SET @o_Num = -1;
            SET @o_Msg = '¡Su usuario no tiene permiso para realizar este tipo de transacción!';
        END
END

