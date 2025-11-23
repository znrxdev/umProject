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
    @Permite_Inscripciones BIT = NULL,
    @Codigo_Integracion VARCHAR(30) = NULL,
    @Observaciones NVARCHAR(255) = NULL,
    @Id_Estado INT = NULL,
    @Id_Estado_Publicacion INT = NULL,
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
                                    Id_Periodo,
                                    Codigo_Periodo,
                                    Nombre_Periodo,
                                    Id_Tipo_Periodo,
                                    Fecha_Inicio,
                                    Fecha_Fin,
                                    Fecha_Cierre_Calificaciones,
                                    Es_Periodo_Actual,
                                    Permite_Inscripciones,
                                    Codigo_Integracion,
                                    Observaciones,
                                    Id_Estado,
                                    Id_Estado_Publicacion,
                                    Hash_Configuracion,
                                    Codigo_Control,
                                    Fecha_Creacion,
                                    Fecha_Modificacion,
                                    Id_Creador,
                                    Id_Modificador,
                                    Id_Transaccion,
                                    RowVersion
                                FROM tbl_periodos_academicos(NOLOCK)
                                WHERE Id_Periodo = @Id_Periodo;

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
            /* FILTRAR POR CODIGO PERIODO */
            ELSE IF(@Id_Tipo_Transaccion = 81)
                BEGIN
                    IF ISNULL(@Codigo_Periodo, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe ingresar un código de período para buscar!';
                        END
                    ELSE
                        BEGIN
                            BEGIN TRY
                                SELECT 
                                    Id_Periodo,
                                    Codigo_Periodo,
                                    Nombre_Periodo,
                                    Id_Tipo_Periodo,
                                    Fecha_Inicio,
                                    Fecha_Fin,
                                    Fecha_Cierre_Calificaciones,
                                    Es_Periodo_Actual,
                                    Permite_Inscripciones,
                                    Codigo_Integracion,
                                    Observaciones,
                                    Id_Estado,
                                    Id_Estado_Publicacion,
                                    Hash_Configuracion,
                                    Codigo_Control,
                                    Fecha_Creacion,
                                    Fecha_Modificacion,
                                    Id_Creador,
                                    Id_Modificador,
                                    Id_Transaccion,
                                    RowVersion
                                FROM tbl_periodos_academicos(NOLOCK)
                                WHERE Codigo_Periodo = @Codigo_Periodo;

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
            /* AGREGAR PERIODO */
            ELSE IF(@Id_Tipo_Transaccion = 78)
                BEGIN
                    IF ISNULL(@Codigo_Periodo, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El campo Código Período no debe ir vacío!';
                        END
                    ELSE IF ISNULL(@Nombre_Periodo, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El campo Nombre Período no debe ir vacío!';
                        END
                    ELSE IF ISNULL(@Fecha_Inicio, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe especificar la fecha de inicio!';
                        END
                    ELSE IF ISNULL(@Fecha_Fin, '') = ''
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe especificar la fecha de fin!';
                        END
                    ELSE IF @Fecha_Fin <= @Fecha_Inicio
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de fin debe ser mayor que la fecha de inicio!';
                        END
                    ELSE IF (@Fecha_Cierre_Calificaciones IS NOT NULL AND @Fecha_Cierre_Calificaciones < @Fecha_Inicio)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de cierre de calificaciones no puede ser anterior a la fecha de inicio!';
                        END
                    ELSE IF ISNULL(@Id_Estado, 0) = 0
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Debe asignar un estado al período!';
                        END
                    ELSE IF EXISTS(SELECT 1 FROM tbl_periodos_academicos(NOLOCK) WHERE Codigo_Periodo = @Codigo_Periodo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe un período con ese código!';
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
                                    Permite_Inscripciones,
                                    Codigo_Integracion,
                                    Observaciones,
                                    Id_Estado,
                                    Id_Estado_Publicacion,
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
                                    ISNULL(@Permite_Inscripciones, 0),
                                    @Codigo_Integracion,
                                    @Observaciones,
                                    @Id_Estado,
                                    @Id_Estado_Publicacion,
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
                        END
                    ELSE IF NOT EXISTS(SELECT 1 FROM tbl_periodos_academicos(NOLOCK) WHERE Id_Periodo = @Id_Periodo)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡El período no existe!';
                        END
                    ELSE IF (@Fecha_Inicio IS NOT NULL AND @Fecha_Fin IS NOT NULL AND @Fecha_Fin <= @Fecha_Inicio)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de fin debe ser mayor que la fecha de inicio!';
                        END
                    ELSE IF (@Fecha_Cierre_Calificaciones IS NOT NULL AND @Fecha_Inicio IS NOT NULL AND @Fecha_Cierre_Calificaciones < @Fecha_Inicio)
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡La fecha de cierre de calificaciones no puede ser anterior a la fecha de inicio!';
                        END
                    ELSE IF (@Codigo_Periodo IS NOT NULL AND EXISTS(SELECT 1 FROM tbl_periodos_academicos(NOLOCK) WHERE Codigo_Periodo = @Codigo_Periodo AND Id_Periodo <> @Id_Periodo))
                        BEGIN
                            SET @o_Num = -1;
                            SET @o_Msg = '¡Ya existe otro período con ese código!';
                        END
                    ELSE
                        BEGIN
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
                                    Permite_Inscripciones = COALESCE(@Permite_Inscripciones, Permite_Inscripciones),
                                    Codigo_Integracion = COALESCE(@Codigo_Integracion, Codigo_Integracion),
                                    Observaciones = COALESCE(@Observaciones, Observaciones),
                                    Id_Estado = COALESCE(@Id_Estado, Id_Estado),
                                    Id_Estado_Publicacion = COALESCE(@Id_Estado_Publicacion, Id_Estado_Publicacion),
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

