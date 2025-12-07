USE umDb;
GO

/* Agrega columna Observaciones a cls_becas_criterios si no existe */
IF COL_LENGTH('dbo.cls_becas_criterios', 'Observaciones') IS NULL
BEGIN
    ALTER TABLE dbo.cls_becas_criterios
    ADD Observaciones NVARCHAR(500) NULL;
END
GO

