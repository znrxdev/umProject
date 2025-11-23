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
    @Valor_Numerico_Minimo DECIMAL(10,2) = NULL,
    @Valor_Numerico_Maximo DECIMAL(10,2) = NULL,
    @Valor_Texto NVARCHAR(255) = NULL,
    @Valor_Booleano BIT = NULL,
    @Peso_Criterio DECIMAL(5,2) = NULL,
    @Prioridad INT = NULL,
    @Es_Excluyente BIT = NULL,
    @Fuente_Validacion NVARCHAR(150) = NULL,
    @Expresion_Validacion NVARCHAR(1000) = NULL,
    @Requiere_Soporte BIT = NULL,
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
                                    Valor_Numerico_Minimo,
                                    Valor_Numerico_Maximo,
                                    Valor_Texto,
                                    Valor_Booleano,
                                    Peso_Criterio,
                                    Prioridad,
                                    Es_Excluyente,
                                    Fuente_Validacion,
                                    Expresion_Validacion,
                                    Requiere_Soporte,
                                    Activo,
                                    Fecha_Creacion,
                                    Fecha_Modificacion,
                                    Id_Creador,
                                    Id_Modificador,
                                    RowVersion
                                FROM cls_becas_criterios(NOLOCK)
                                WHERE Id_Programa = @Id_Programa
                                AND Activo = 1
                                ORDER BY Prioridad, Nombre_Criterio;

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
                                    Valor_Numerico_Minimo,
                                    Valor_Numerico_Maximo,
                                    Valor_Texto,
                                    Valor_Booleano,
                                    Peso_Criterio,
                                    Prioridad,
                                    Es_Excluyente,
                                    Fuente_Validacion,
                                    Expresion_Validacion,
                                    Requiere_Soporte,
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
                    ELSE IF ISNULL(@Codigo, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El campo Código no debe ir vacío!';
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
                    ELSE IF ISNULL(@Fuente_Validacion, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe especificar la fuente de validación!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM cls_becas_criterios(NOLOCK) WHERE Id_Programa = @Id_Programa AND Codigo = @Codigo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe un criterio con ese código para este programa!';
                        END
                    ELSE IF (@Peso_Criterio IS NOT NULL AND (@Peso_Criterio < 0 OR @Peso_Criterio > 100))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El peso del criterio debe estar entre 0 y 100!';
                        END
                    ELSE IF (@Valor_Numerico_Minimo IS NULL AND @Valor_Numerico_Maximo IS NULL AND @Valor_Texto IS NULL AND @Valor_Booleano IS NULL AND ISNULL(@Valor_Criterio, '') = '')
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe especificar al menos un valor (numérico mínimo/máximo, texto, booleano o valor criterio)!';
                        END
                    ELSE IF (@Activo) IS NULL
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe colocar un estado válido (activo/inactivo)!';
                        END
                    ELSE
                        BEGIN
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
                                    Valor_Numerico_Minimo,
                                    Valor_Numerico_Maximo,
                                    Valor_Texto,
                                    Valor_Booleano,
                                    Peso_Criterio,
                                    Prioridad,
                                    Es_Excluyente,
                                    Fuente_Validacion,
                                    Expresion_Validacion,
                                    Requiere_Soporte,
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
                                    @Valor_Numerico_Minimo,
                                    @Valor_Numerico_Maximo,
                                    @Valor_Texto,
                                    @Valor_Booleano,
                                    ISNULL(@Peso_Criterio, 0),
                                    ISNULL(@Prioridad, 1),
                                    ISNULL(@Es_Excluyente, 0),
                                    @Fuente_Validacion,
                                    @Expresion_Validacion,
                                    ISNULL(@Requiere_Soporte, 0),
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
                    ELSE IF (@Codigo IS NOT NULL AND @Id_Programa IS NOT NULL AND EXISTS(SELECT 1 FROM cls_becas_criterios(NOLOCK) WHERE Id_Programa = @Id_Programa AND Codigo = @Codigo AND Id_Beca_Criterio <> @Id_Beca_Criterio))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe otro criterio con ese código para este programa!';
                        END
                    ELSE IF (@Peso_Criterio IS NOT NULL AND (@Peso_Criterio < 0 OR @Peso_Criterio > 100))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El peso del criterio debe estar entre 0 y 100!';
                        END
                    ELSE
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
                                SET Id_Programa = COALESCE(@Id_Programa, Id_Programa),
                                    Codigo = COALESCE(@Codigo, Codigo),
                                    Nombre_Criterio = COALESCE(@Nombre_Criterio, Nombre_Criterio),
                                    Clave_Criterio = COALESCE(@Clave_Criterio, Clave_Criterio),
                                    Valor_Criterio = COALESCE(@Valor_Criterio, Valor_Criterio),
                                    Tipo_Dato_Valor = COALESCE(@Tipo_Dato_Valor, Tipo_Dato_Valor),
                                    Id_Tipo_Criterio = COALESCE(@Id_Tipo_Criterio, Id_Tipo_Criterio),
                                    Operador_Comparacion = COALESCE(@Operador_Comparacion, Operador_Comparacion),
                                    Valor_Numerico_Minimo = COALESCE(@Valor_Numerico_Minimo, Valor_Numerico_Minimo),
                                    Valor_Numerico_Maximo = COALESCE(@Valor_Numerico_Maximo, Valor_Numerico_Maximo),
                                    Valor_Texto = COALESCE(@Valor_Texto, Valor_Texto),
                                    Valor_Booleano = COALESCE(@Valor_Booleano, Valor_Booleano),
                                    Peso_Criterio = COALESCE(@Peso_Criterio, Peso_Criterio),
                                    Prioridad = COALESCE(@Prioridad, Prioridad),
                                    Es_Excluyente = COALESCE(@Es_Excluyente, Es_Excluyente),
                                    Fuente_Validacion = COALESCE(@Fuente_Validacion, Fuente_Validacion),
                                    Expresion_Validacion = COALESCE(@Expresion_Validacion, Expresion_Validacion),
                                    Requiere_Soporte = COALESCE(@Requiere_Soporte, Requiere_Soporte),
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
    ELSE
        BEGIN
            SET @o_Num = -1;
            SET @o_Msg = '¡Su usuario no tiene permiso para realizar este tipo de transacción!';
        END
END

