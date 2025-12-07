USE umDb;
GO

/* Eliminar columnas obsoletas en cls_becas_criterios */
IF OBJECT_ID('dbo.CK_cls_becas_criterios_Valores', 'C') IS NOT NULL
    ALTER TABLE dbo.cls_becas_criterios DROP CONSTRAINT CK_cls_becas_criterios_Valores;

IF COL_LENGTH('dbo.cls_becas_criterios', 'Valor_Numerico_Minimo') IS NOT NULL
    ALTER TABLE dbo.cls_becas_criterios DROP COLUMN Valor_Numerico_Minimo;

IF COL_LENGTH('dbo.cls_becas_criterios', 'Valor_Numerico_Maximo') IS NOT NULL
    ALTER TABLE dbo.cls_becas_criterios DROP COLUMN Valor_Numerico_Maximo;

IF COL_LENGTH('dbo.cls_becas_criterios', 'Valor_Texto') IS NOT NULL
    ALTER TABLE dbo.cls_becas_criterios DROP COLUMN Valor_Texto;

IF COL_LENGTH('dbo.cls_becas_criterios', 'Valor_Booleano') IS NOT NULL
    ALTER TABLE dbo.cls_becas_criterios DROP COLUMN Valor_Booleano;

IF COL_LENGTH('dbo.cls_becas_criterios', 'Prioridad') IS NOT NULL
BEGIN
    DECLARE @dfPrior NVARCHAR(128) = (SELECT TOP 1 d.name
                                      FROM sys.default_constraints d
                                      INNER JOIN sys.columns c ON d.parent_object_id = c.object_id AND d.parent_column_id = c.column_id
                                      WHERE d.parent_object_id = OBJECT_ID('dbo.cls_becas_criterios')
                                        AND c.name = 'Prioridad');
    IF @dfPrior IS NOT NULL
        EXEC('ALTER TABLE dbo.cls_becas_criterios DROP CONSTRAINT ' + @dfPrior);

    ALTER TABLE dbo.cls_becas_criterios DROP COLUMN Prioridad;
END

IF COL_LENGTH('dbo.cls_becas_criterios', 'Es_Excluyente') IS NOT NULL
BEGIN
    DECLARE @dfExc NVARCHAR(128) = (SELECT TOP 1 d.name
                                    FROM sys.default_constraints d
                                    INNER JOIN sys.columns c ON d.parent_object_id = c.object_id AND d.parent_column_id = c.column_id
                                    WHERE d.parent_object_id = OBJECT_ID('dbo.cls_becas_criterios')
                                      AND c.name = 'Es_Excluyente');
    IF @dfExc IS NOT NULL
        EXEC('ALTER TABLE dbo.cls_becas_criterios DROP CONSTRAINT ' + @dfExc);

    ALTER TABLE dbo.cls_becas_criterios DROP COLUMN Es_Excluyente;
END

IF COL_LENGTH('dbo.cls_becas_criterios', 'Requiere_Soporte') IS NOT NULL
BEGIN
    DECLARE @dfReq NVARCHAR(128) = (SELECT TOP 1 d.name
                                    FROM sys.default_constraints d
                                    INNER JOIN sys.columns c ON d.parent_object_id = c.object_id AND d.parent_column_id = c.column_id
                                    WHERE d.parent_object_id = OBJECT_ID('dbo.cls_becas_criterios')
                                      AND c.name = 'Requiere_Soporte');
    IF @dfReq IS NOT NULL
        EXEC('ALTER TABLE dbo.cls_becas_criterios DROP CONSTRAINT ' + @dfReq);

    ALTER TABLE dbo.cls_becas_criterios DROP COLUMN Requiere_Soporte;
END

/* Constraint de unicidad por Programa + Tipo de Criterio */
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'UQ_cls_becas_criterios_ProgramaTipo'
      AND object_id = OBJECT_ID('dbo.cls_becas_criterios')
)
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UQ_cls_becas_criterios_ProgramaTipo
    ON dbo.cls_becas_criterios (Id_Programa, Id_Tipo_Criterio)
    WHERE Activo = 1;
END
GO

