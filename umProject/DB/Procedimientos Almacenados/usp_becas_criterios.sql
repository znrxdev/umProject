USE umDb
GO

/*
cls_becas_criterios
*/

CREATE OR ALTER PROC usp_becas_criterios
(
    @Id_Beca_Criterio INT = NULL,
    @Id_Programa INT = NULL,
    @Codigo VARCHAR(50) = NULL,
    @Nombre_Criterio NVARCHAR(150) = NULL,
    @Clave_Criterio NVARCHAR(100) = NULL,
    @Valor_Criterio NVARCHAR(255) = NULL,
    @Tipo_Dato_Valor NVARCHAR(50) = NULL,
    @Id_Tipo_Criterio INT = NULL,
    @Operador_Comparacion NVARCHAR(10) = NULL,
    @Observaciones NVARCHAR(500) = NULL,
    @Fuente_Validacion NVARCHAR(150) = NULL,
    @Expresion_Validacion NVARCHAR(1000) = NULL,
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
    DECLARE @IdProgramaFinal INT, @TipoCriterioFinal INT, @TipoDatoFinal NVARCHAR(50), @ValorCriterioFinal NVARCHAR(255), @OperadorFinal NVARCHAR(10);
    SET @Fecha_Creacion = GETDATE();
    SET @Fecha_Modificacion = GETDATE();
    SET @Id_Creador = @Id_Sesion;
    SET @Id_Modificador = @Id_Sesion;
    SET @o_Num = 0;
    SET @o_Msg = '';
    SET @Permiso = dbo.fn_Validar_Permisos(@Id_Sesion, @Id_Tipo_Transaccion);

    IF(@Permiso = 1)
        BEGIN
            /* FILTRAR POR ID PROGRAMA */
            IF(@Id_Tipo_Transaccion = 66)
                BEGIN
                    IF ISNULL(@Id_Programa, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡No ha seleccionado el programa para listar sus criterios!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_becas_programas(NOLOCK) WHERE Id_Beca_Programa = @Id_Programa)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El programa de beca no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Beca_Criterio,
                                    Id_Programa,
                                    Codigo,
                                    Nombre_Criterio,
                                    Clave_Criterio,
                                    Valor_Criterio,
                                    Tipo_Dato_Valor,
                                    Id_Tipo_Criterio,
                                    Operador_Comparacion,
                                    Observaciones,
                                    Fuente_Validacion,
                                    Expresion_Validacion,
                                    Activo,
                                    Fecha_Creacion,
                                    Fecha_Modificacion,
                                    Id_Creador,
                                    Id_Modificador,
                                    RowVersion
                                FROM cls_becas_criterios(NOLOCK)
                                WHERE Id_Programa = @Id_Programa
                                AND Activo = 1
                                ORDER BY Nombre_Criterio;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Criterios filtrados por programa!';
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
            /* FILTRAR POR ID CRITERIO */
            ELSE IF(@Id_Tipo_Transaccion = 65)
                BEGIN
                    IF ISNULL(@Id_Beca_Criterio, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de criterio!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_becas_criterios(NOLOCK) WHERE Id_Beca_Criterio = @Id_Beca_Criterio)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El criterio no existe!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Beca_Criterio,
                                    Id_Programa,
                                    Codigo,
                                    Nombre_Criterio,
                                    Clave_Criterio,
                                    Valor_Criterio,
                                    Tipo_Dato_Valor,
                                    Id_Tipo_Criterio,
                                    Operador_Comparacion,
                                    Peso_Criterio,
                                    Observaciones,
                                    Fuente_Validacion,
                                    Expresion_Validacion,
                                    Activo,
                                    Fecha_Creacion,
                                    Fecha_Modificacion,
                                    Id_Creador,
                                    Id_Modificador,
                                    RowVersion
                                FROM cls_becas_criterios(NOLOCK)
                                WHERE Id_Beca_Criterio = @Id_Beca_Criterio;

                                SET @o_Num = 0;
                                SET @o_Msg = '¡Criterio encontrado!';
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
            /* AGREGAR CRITERIO */
            ELSE IF(@Id_Tipo_Transaccion = 64)
                BEGIN
                    IF ISNULL(@Id_Programa, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un programa de beca!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_becas_programas(NOLOCK) WHERE Id_Beca_Programa = @Id_Programa)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El programa de beca no existe!';
                        END
                    ELSE IF ISNULL(@Nombre_Criterio, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El campo Nombre Criterio no debe ir vacío!';
                        END
                    ELSE IF ISNULL(@Clave_Criterio, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El campo Clave Criterio no debe ir vacío!';
                        END
                    ELSE IF ISNULL(@Tipo_Dato_Valor, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe especificar el tipo de dato del valor!';
                        END
                    ELSE IF ISNULL(@Id_Tipo_Criterio, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un tipo de criterio!';
                        END
                    ELSE IF ISNULL(@Operador_Comparacion, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe especificar el operador de comparación!';
                        END
                    IF EXISTS(SELECT 1 FROM cls_becas_criterios(NOLOCK) WHERE Id_Programa = @Id_Programa AND Codigo = @Codigo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe un criterio con ese código para este programa!';
                        END

                    IF @o_Num = 0 AND EXISTS(SELECT 1 FROM cls_becas_criterios(NOLOCK) WHERE Id_Programa = @Id_Programa AND Id_Tipo_Criterio = @Id_Tipo_Criterio AND Activo = 1)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe un criterio para este programa con el mismo tipo de criterio!';
                        END

                    IF @o_Num = 0 AND (ISNULL(@Valor_Criterio, '') = '')
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe especificar un valor para el criterio!';
                        END

                    IF @o_Num = 0 AND (@Activo) IS NULL
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe colocar un estado válido (activo/inactivo)!';
                        END

                    IF @o_Num = 0
                        BEGIN
                            -- Autogenerar código si viene vacío o NULL
                            IF ISNULL(@Codigo, '') = ''
                                BEGIN
                                    DECLARE @NextCod INT = ISNULL((
                                        SELECT MAX(CAST(RIGHT(Codigo, 6) AS INT))
                                        FROM cls_becas_criterios WITH (NOLOCK)
                                        WHERE Codigo LIKE 'BCR-%'
                                    ), 0) + 1;

                                    SET @Codigo = 'BCR-' + RIGHT('000000' + CAST(@NextCod AS VARCHAR(6)), 6);
                                END

                            -- Validaciones específicas por tipo de criterio
                            IF (@Id_Tipo_Criterio IN (44, 46, 83)) -- PROMEDIO, SANCIONES, MATERIAS APROBADAS
                                BEGIN
                                    SET @Tipo_Dato_Valor = 'NUMERICO';

                                    IF @Operador_Comparacion NOT IN ('=', '>=')
                                        BEGIN
                                            SET @o_Num = -1;
                                            SET @o_Msg = '¡Para este tipo de criterio solo se permiten operadores "=" o ">="!';
                                        END

                                    IF @o_Num = 0 AND TRY_CONVERT(INT, @Valor_Criterio) IS NULL
                                        BEGIN
                                            SET @o_Num = -1;
                                            SET @o_Msg = '¡El valor debe ser un número entero para este tipo de criterio!';
                                        END
                                END

                            IF @o_Num = 0
                            SET @iConcepto = CONCAT('AGREGANDO CRITERIO DE BECA: ', @Nombre_Criterio);
                            EXEC sp_transacciones
                            @Modo = 'INS',
                            @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                            @Id_Autor = @Id_Sesion,
                            @Concepto = @iConcepto,
                            @o_Num = @Id_Transaccion OUTPUT;

                            BEGIN TRAN trx_AgregarBecaCriterio
                            BEGIN TRY
                                INSERT INTO cls_becas_criterios(
                                    Id_Programa,
                                    Codigo,
                                    Nombre_Criterio,
                                    Clave_Criterio,
                                    Valor_Criterio,
                                    Tipo_Dato_Valor,
                                    Id_Tipo_Criterio,
                                    Operador_Comparacion,
                                    Observaciones,
                                    Fuente_Validacion,
                                    Expresion_Validacion,
                                    Activo,
                                    Fecha_Creacion,
                                    Fecha_Modificacion,
                                    Id_Creador,
                                    Id_Modificador
                                ) VALUES (
                                    @Id_Programa,
                                    @Codigo,
                                    @Nombre_Criterio,
                                    @Clave_Criterio,
                                    @Valor_Criterio,
                                    @Tipo_Dato_Valor,
                                    @Id_Tipo_Criterio,
                                    @Operador_Comparacion,
                                    @Observaciones,
                                    ISNULL(@Fuente_Validacion, ''),
                                    @Expresion_Validacion,
                                    @Activo,
                                    @Fecha_Creacion,
                                    @Fecha_Modificacion,
                                    @Id_Creador,
                                    @Id_Modificador
                                );

                                COMMIT TRAN trx_AgregarBecaCriterio;

                                SET @o_Num = SCOPE_IDENTITY();
                                SET @o_Msg = '¡Criterio de beca agregado exitosamente!';

                                EXEC sp_transacciones
                                @Modo = 'UPD',
                                @Id_Transaccion = @Id_Transaccion;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRAN trx_AgregarBecaCriterio;

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
            /* ACTUALIZAR CRITERIO */
            ELSE IF(@Id_Tipo_Transaccion = 67)
                BEGIN
                    IF ISNULL(@Id_Beca_Criterio, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe seleccionar un ID de criterio!';
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM cls_becas_criterios(NOLOCK) WHERE Id_Beca_Criterio = @Id_Beca_Criterio)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El criterio no existe!';
                        END
                    ELSE
                        BEGIN
                            SELECT @IdProgramaFinal = Id_Programa,
                                   @TipoCriterioFinal = Id_Tipo_Criterio,
                                   @TipoDatoFinal = Tipo_Dato_Valor,
                                   @ValorCriterioFinal = Valor_Criterio,
                                   @OperadorFinal = Operador_Comparacion
                            FROM cls_becas_criterios WITH (NOLOCK)
                            WHERE Id_Beca_Criterio = @Id_Beca_Criterio;

                            SET @IdProgramaFinal = COALESCE(@Id_Programa, @IdProgramaFinal);
                            SET @TipoCriterioFinal = COALESCE(@Id_Tipo_Criterio, @TipoCriterioFinal);
                            SET @TipoDatoFinal = COALESCE(@Tipo_Dato_Valor, @TipoDatoFinal);
                            SET @ValorCriterioFinal = COALESCE(@Valor_Criterio, @ValorCriterioFinal);
                            SET @OperadorFinal = COALESCE(@Operador_Comparacion, @OperadorFinal);

                            IF (@Codigo IS NOT NULL AND EXISTS(SELECT 1 FROM cls_becas_criterios(NOLOCK) WHERE Id_Programa = @IdProgramaFinal AND Codigo = @Codigo AND Id_Beca_Criterio <> @Id_Beca_Criterio))
                                BEGIN
                                    SET @o_Num = -1;
                                    SET @o_Msg = '¡Ya existe otro criterio con ese código para este programa!';
                                END



                            IF @o_Num = 0 AND (ISNULL(@ValorCriterioFinal, '') = '')
                                BEGIN
                                    SET @o_Num = -1;
                                    SET @o_Msg = '¡Debe especificar un valor para el criterio!';
                                END

                            IF @o_Num = 0 AND EXISTS(SELECT 1 FROM cls_becas_criterios(NOLOCK) WHERE Id_Programa = @IdProgramaFinal AND Id_Tipo_Criterio = @TipoCriterioFinal AND Activo = 1 AND Id_Beca_Criterio <> @Id_Beca_Criterio)
                                BEGIN
                                    SET @o_Num = -1;
                                    SET @o_Msg = '¡Ya existe un criterio activo para este programa con el mismo tipo de criterio!';
                                END

                            IF @TipoCriterioFinal IN (44, 46, 83)
                                BEGIN
                                    SET @TipoDatoFinal = 'NUMERICO';

                                    IF @OperadorFinal NOT IN ('=', '>=')
                                        BEGIN
                                            SET @o_Num = -1;
                                            SET @o_Msg = '¡Para este tipo de criterio solo se permiten operadores "=" o ">="!';
                                        END

                                    IF @o_Num = 0 AND TRY_CONVERT(INT, @ValorCriterioFinal) IS NULL
                                        BEGIN
                                            SET @o_Num = -1;
                                            SET @o_Msg = '¡El valor debe ser un número entero para este tipo de criterio!';
                                        END
                                END

                            IF @o_Num = 0
                                BEGIN
                                    SET @iConcepto = CONCAT('ACTUALIZANDO CRITERIO DE BECA ID: ', @Id_Beca_Criterio);
                                    EXEC sp_transacciones
                                    @Modo = 'INS',
                                    @Id_Tipo_Transaccion = @Id_Tipo_Transaccion,
                                    @Id_Autor = @Id_Sesion,
                                    @Concepto = @iConcepto,
                                    @o_Num = @Id_Transaccion OUTPUT;

                                    BEGIN TRAN trx_ActualizarBecaCriterio
                                    BEGIN TRY
                                        UPDATE cls_becas_criterios
                                        SET Id_Programa = @IdProgramaFinal,
                                            Codigo = COALESCE(@Codigo, Codigo),
                                            Nombre_Criterio = COALESCE(@Nombre_Criterio, Nombre_Criterio),
                                            Clave_Criterio = COALESCE(@Clave_Criterio, Clave_Criterio),
                                            Valor_Criterio = @ValorCriterioFinal,
                                            Tipo_Dato_Valor = @TipoDatoFinal,
                                            Id_Tipo_Criterio = @TipoCriterioFinal,
                                            Operador_Comparacion = @OperadorFinal,
                                            Observaciones = COALESCE(@Observaciones, Observaciones),
                                            Fuente_Validacion = COALESCE(@Fuente_Validacion, Fuente_Validacion),
                                            Expresion_Validacion = COALESCE(@Expresion_Validacion, Expresion_Validacion),
                                            Activo = COALESCE(@Activo, Activo),
                                            Fecha_Modificacion = @Fecha_Modificacion,
                                            Id_Modificador = @Id_Modificador
                                        WHERE Id_Beca_Criterio = @Id_Beca_Criterio;

                                        COMMIT TRAN trx_ActualizarBecaCriterio;

                                        SET @o_Num = 0;
                                        SET @o_Msg = '¡Criterio de beca actualizado exitosamente!';

                                        EXEC sp_transacciones
                                        @Modo = 'UPD',
                                        @Id_Transaccion = @Id_Transaccion;
                                    END TRY
                                    BEGIN CATCH
                                        ROLLBACK TRAN trx_ActualizarBecaCriterio;

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
        END
    ELSE
        BEGIN
            SET @o_Num = -1;
            SET @o_Msg = '¡Su usuario no tiene permiso para realizar este tipo de transacción!';
        END
END

