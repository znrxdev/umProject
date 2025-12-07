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
                                    G.Id_Grupo, 
                                    G.Codigo_Grupo, 
                                    G.Nombre_Grupo, 
                                    G.Id_Periodo,
                                    G.Id_Tipo_Grupo, 
                                    G.Id_Coordinador, 
                                    G.Id_Jornada, 
                                    G.Id_Estado,
                                    G.Fecha_Cierre, 
                                    G.Observaciones, 
                                    G.Codigo_Seguimiento, 
                                    G.Activo,
                                    G.Fecha_Creacion, 
                                    G.Fecha_Modificacion, 
                                    G.Id_Creador, 
                                    G.Id_Modificador,
                                    G.Id_Transaccion, 
                                    G.RowVersion,
                                    -- Información relacionada para mostrar en la UI
                                    PA.Nombre_Periodo,
                                    PA.Codigo_Periodo,
                                    TG.Nombre_Catalogo AS Nombre_Tipo_Grupo,
                                    COORD.Usuario AS Coordinador_Usuario,
                                    P.Primer_Nombre + ' ' + P.Primer_Apellido AS Coordinador_Nombre,
                                    J.Nombre_Catalogo AS Nombre_Jornada,
                                    EST.Nombre_Estado AS Nombre_Estado
                                FROM tbl_grupos G (NOLOCK)
                                INNER JOIN tbl_periodos_academicos PA (NOLOCK) ON G.Id_Periodo = PA.Id_Periodo
                                INNER JOIN cls_catalogos TG (NOLOCK) ON G.Id_Tipo_Grupo = TG.Id_Catalogo
                                LEFT JOIN tbl_usuarios COORD (NOLOCK) ON G.Id_Coordinador = COORD.Id_Usuario
                                LEFT JOIN tbl_personas P (NOLOCK) ON COORD.Id_Persona = P.Id_Persona
                                LEFT JOIN cls_catalogos J (NOLOCK) ON G.Id_Jornada = J.Id_Catalogo
                                LEFT JOIN cls_estados EST (NOLOCK) ON G.Id_Estado = EST.Id_Estado
                                WHERE G.Id_Grupo = @Id_Grupo;

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
            /* LISTAR TODOS LOS GRUPOS / FILTRAR POR ID PERIODO */
            ELSE IF(@Id_Tipo_Transaccion = 104)
                BEGIN
                    BEGIN TRY
                        -- Si @Id_Periodo es NULL o 0, listar todos los grupos activos
                        -- Si tiene un valor, filtrar por ese Id_Periodo específico
                        SELECT 
                            G.Id_Grupo, 
                            G.Codigo_Grupo, 
                            G.Nombre_Grupo, 
                            G.Id_Periodo,
                            G.Id_Tipo_Grupo, 
                            G.Id_Coordinador, 
                            G.Id_Jornada, 
                            G.Id_Estado,
                            G.Fecha_Cierre, 
                            G.Observaciones, 
                            G.Codigo_Seguimiento, 
                            G.Activo,
                            G.Fecha_Creacion, 
                            G.Fecha_Modificacion, 
                            G.Id_Creador, 
                            G.Id_Modificador,
                            G.Id_Transaccion, 
                            G.RowVersion,
                            -- Información relacionada para mostrar en la UI
                            PA.Nombre_Periodo,
                            PA.Codigo_Periodo,
                            TG.Nombre_Catalogo AS Nombre_Tipo_Grupo,
                            COORD.Usuario AS Coordinador_Usuario,
                            P.Primer_Nombre + ' ' + P.Primer_Apellido AS Coordinador_Nombre,
                            J.Nombre_Catalogo AS Nombre_Jornada,
                            EST.Nombre_Estado AS Nombre_Estado
                        FROM tbl_grupos G (NOLOCK)
                        INNER JOIN tbl_periodos_academicos PA (NOLOCK) ON G.Id_Periodo = PA.Id_Periodo
                        INNER JOIN cls_catalogos TG (NOLOCK) ON G.Id_Tipo_Grupo = TG.Id_Catalogo
                        LEFT JOIN tbl_usuarios COORD (NOLOCK) ON G.Id_Coordinador = COORD.Id_Usuario
                        LEFT JOIN tbl_personas P (NOLOCK) ON COORD.Id_Persona = P.Id_Persona
                        LEFT JOIN cls_catalogos J (NOLOCK) ON G.Id_Jornada = J.Id_Catalogo
                        LEFT JOIN cls_estados EST (NOLOCK) ON G.Id_Estado = EST.Id_Estado
                        WHERE G.Activo = 1
                        AND (ISNULL(@Id_Periodo, 0) = 0 OR G.Id_Periodo = @Id_Periodo)
                        ORDER BY PA.Fecha_Inicio DESC, G.Codigo_Grupo;

                        SET @o_Num = 0;
                        SET @o_Msg = CASE 
                            WHEN ISNULL(@Id_Periodo, 0) = 0 THEN '¡Grupos listados exitosamente!'
                            ELSE '¡Grupos filtrados por período!'
                        END;
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
                    ELSE IF ISNULL(@Nombre_Grupo, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El campo Nombre Grupo no debe ir vacío!';
                        END
                    ELSE
                        BEGIN
                            -- Forzar estado EN REVISION (4) al agregar
                            SET @Id_Estado = 4;
                            
                            -- Forzar Id_Jornada como NULL
                            SET @Id_Jornada = NULL;
                            
                            -- Forzar Activo como true
                            SET @Activo = 1;
                            
                            -- Autocalcular Fecha_Cierre si viene NULL usando la fecha de inicio del período
                            IF @Fecha_Cierre IS NULL
                                BEGIN
                                    SELECT @Fecha_Cierre = CAST(Fecha_Inicio AS DATETIME)
                                    FROM tbl_periodos_academicos(NOLOCK)
                                    WHERE Id_Periodo = @Id_Periodo;
                                END
                            
                            -- Variables para generar códigos
                            DECLARE @AnioActual INT = YEAR(GETDATE());
                            DECLARE @CodigoPeriodo VARCHAR(20);
                            DECLARE @NumeroRomano VARCHAR(5);
                            DECLARE @CodigoGrupoGenerado VARCHAR(20);
                            DECLARE @CodigoSeguimientoGenerado VARCHAR(30);
                            DECLARE @LetraSiguiente CHAR(1) = 'A';
                            DECLARE @UltimoCodigo VARCHAR(20);
                            
                            -- Obtener código del período
                            SELECT @CodigoPeriodo = Codigo_Periodo 
                            FROM tbl_periodos_academicos(NOLOCK) 
                            WHERE Id_Periodo = @Id_Periodo;
                            
                            -- Extraer número romano del código de período (formato: YYYY-I, YYYY-II, YYYY-III)
                            IF @CodigoPeriodo LIKE '%-I'
                                SET @NumeroRomano = 'I'
                            ELSE IF @CodigoPeriodo LIKE '%-II'
                                SET @NumeroRomano = 'II'
                            ELSE IF @CodigoPeriodo LIKE '%-III'
                                SET @NumeroRomano = 'III'
                            ELSE
                                SET @NumeroRomano = 'I'; -- Por defecto
                            
                            -- Generar código de grupo: GRP-YYYY-R-N
                            -- Buscar el último código de grupo para este período y año
                            SELECT TOP 1 @UltimoCodigo = Codigo_Grupo
                            FROM tbl_grupos(NOLOCK)
                            WHERE Codigo_Grupo LIKE CONCAT('GRP-', @AnioActual, '-', @NumeroRomano, '-%')
                            ORDER BY Codigo_Grupo DESC;
                            
                            -- Si existe un código previo, extraer la letra y generar la siguiente
                            IF @UltimoCodigo IS NOT NULL
                                BEGIN
                                    DECLARE @UltimaLetra CHAR(1) = RIGHT(@UltimoCodigo, 1);
                                    IF @UltimaLetra >= 'A' AND @UltimaLetra < 'Z'
                                        SET @LetraSiguiente = CHAR(ASCII(@UltimaLetra) + 1);
                                    ELSE IF @UltimaLetra = 'Z'
                                        BEGIN
                                            SET @o_Num = -1;
                                            SET @o_Msg = '¡Se ha alcanzado el límite de grupos para este período (Z)!';
                                            RETURN;
                                        END
                                END
                            
                            SET @CodigoGrupoGenerado = CONCAT('GRP-', @AnioActual, '-', @NumeroRomano, '-', @LetraSiguiente);
                            
                            -- Generar código de seguimiento usando función o patrón
                            -- Formato: SEG-GRP-YYYY-R-N
                            SET @CodigoSeguimientoGenerado = CONCAT('SEG-', @CodigoGrupoGenerado);
                            
                            -- Verificar que no exista el código generado
                            IF EXISTS(SELECT 1 FROM tbl_grupos(NOLOCK) WHERE Codigo_Grupo = @CodigoGrupoGenerado AND Id_Periodo = @Id_Periodo)
                                BEGIN
                                    SET @o_Num = -1;
                                    SET @o_Msg = '¡Ya existe un grupo con ese código para ese período!';
                                END
                            ELSE IF EXISTS(SELECT 1 FROM tbl_grupos(NOLOCK) WHERE Codigo_Seguimiento = @CodigoSeguimientoGenerado)
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
                                            @CodigoGrupoGenerado, @Nombre_Grupo, @Id_Periodo, @Id_Tipo_Grupo,
                                            @Id_Coordinador, @Id_Jornada, @Id_Estado, @Fecha_Cierre,
                                            @Observaciones, @CodigoSeguimientoGenerado, ISNULL(@Activo, 1),
                                            @Fecha_Creacion, @Fecha_Modificacion, @Id_Creador, @Id_Modificador,
                                            @Id_Transaccion
                                        );

                                        COMMIT TRAN trx_AgregarGrupo;

                                        SET @o_Num = SCOPE_IDENTITY();
                                        SET @o_Msg = CONCAT('¡Grupo agregado exitosamente! Código: ', @CodigoGrupoGenerado);

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
                END
            /* ACTUALIZAR GRUPO */
            ELSE IF(@Id_Tipo_Transaccion = 102)
                BEGIN
                    SET @o_Num = 0;

                    IF ISNULL(@Id_Grupo, 0) = 0
                    BEGIN
                        SET @o_Num = -1;
                        SET @o_Msg = '¡Debe seleccionar un ID de grupo!';
                        RETURN;
                    END

                    IF NOT EXISTS(SELECT 1 FROM tbl_grupos(NOLOCK) WHERE Id_Grupo = @Id_Grupo)
                    BEGIN
                        SET @o_Num = -1;
                        SET @o_Msg = '¡El grupo no existe!';
                        RETURN;
                    END

                    IF ((@Codigo_Grupo IS NOT NULL OR @Id_Periodo IS NOT NULL) AND
                         EXISTS(SELECT 1 FROM tbl_grupos(NOLOCK) 
                                WHERE Codigo_Grupo = COALESCE(@Codigo_Grupo, (SELECT Codigo_Grupo FROM tbl_grupos WHERE Id_Grupo = @Id_Grupo))
                                  AND Id_Periodo = COALESCE(@Id_Periodo, (SELECT Id_Periodo FROM tbl_grupos WHERE Id_Grupo = @Id_Grupo))
                                  AND Id_Grupo <> @Id_Grupo))
                    BEGIN
                        SET @o_Num = -1;
                        SET @o_Msg = '¡Ya existe otro grupo con ese código para ese período!';
                        RETURN;
                    END

                    IF (@Codigo_Seguimiento IS NOT NULL AND EXISTS(SELECT 1 FROM tbl_grupos(NOLOCK) WHERE Codigo_Seguimiento = @Codigo_Seguimiento AND Id_Grupo <> @Id_Grupo))
                    BEGIN
                        SET @o_Num = -1;
                        SET @o_Msg = '¡Ya existe otro grupo con ese código de seguimiento!';
                        RETURN;
                    END

                    DECLARE @EstadoFinalGrupo INT = COALESCE(@Id_Estado, (SELECT Id_Estado FROM tbl_grupos WHERE Id_Grupo = @Id_Grupo));
                    DECLARE @ActivoFinalGrupo BIT = COALESCE(@Activo, (SELECT Activo FROM tbl_grupos WHERE Id_Grupo = @Id_Grupo));

                    IF (@EstadoFinalGrupo = 2 OR @ActivoFinalGrupo = 0)
                        AND EXISTS(
                            SELECT 1
                            FROM cls_grupos_secciones gs(NOLOCK)
                            INNER JOIN tbl_secciones s(NOLOCK) ON gs.Id_Seccion = s.Id_Seccion
                            INNER JOIN cls_materias_periodos mp(NOLOCK) ON s.Id_Materia_Periodo = mp.Id_Materia_Periodo
                            INNER JOIN tbl_periodos_academicos p(NOLOCK) ON mp.Id_Periodo_Academico = p.Id_Periodo
                            WHERE gs.Id_Grupo = @Id_Grupo
                              AND gs.Activo = 1
                              AND p.Id_Estado = 1 -- período ACTIVO
                        )
                    BEGIN
                        SET @o_Num = -1;
                        SET @o_Msg = '¡No se puede inactivar el grupo porque está vinculado a un período académico ACTIVO!';
                        RETURN;
                    END

                    IF @o_Num = 0
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

