USE umDb;
GO

/* Migración: remover columnas obsoletas de tbl_solicitudes_becas */
IF COL_LENGTH('dbo.tbl_solicitudes_becas', 'Id_Convocatoria') IS NOT NULL
BEGIN
    -- Borrar dependencias de índices y FKs sobre Id_Convocatoria
    IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_tbl_solicitudes_becas_ProgramaConvocatoriaEstado' AND object_id = OBJECT_ID('dbo.tbl_solicitudes_becas'))
        DROP INDEX IX_tbl_solicitudes_becas_ProgramaConvocatoriaEstado ON dbo.tbl_solicitudes_becas;

    DECLARE @fkConv NVARCHAR(128) = (SELECT TOP 1 fk.name
        FROM sys.foreign_keys fk
        INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
        INNER JOIN sys.columns c ON fkc.parent_object_id = c.object_id AND fkc.parent_column_id = c.column_id
        WHERE fk.parent_object_id = OBJECT_ID('dbo.tbl_solicitudes_becas') AND c.name = 'Id_Convocatoria');
    IF @fkConv IS NOT NULL
    BEGIN
        DECLARE @sql NVARCHAR(400) = N'ALTER TABLE dbo.tbl_solicitudes_becas DROP CONSTRAINT ' + QUOTENAME(@fkConv) + N';';
        EXEC(@sql);
    END

    ALTER TABLE dbo.tbl_solicitudes_becas DROP COLUMN Id_Convocatoria;
END;

IF COL_LENGTH('dbo.tbl_solicitudes_becas', 'Creditos_Aprobados') IS NOT NULL
    ALTER TABLE dbo.tbl_solicitudes_becas DROP COLUMN Creditos_Aprobados;

IF COL_LENGTH('dbo.tbl_solicitudes_becas', 'Nivel_Aprobacion_Actual') IS NOT NULL
BEGIN
    DECLARE @dfNivelActual NVARCHAR(128) = (SELECT TOP 1 d.name
        FROM sys.default_constraints d
        INNER JOIN sys.columns c ON d.parent_object_id = c.object_id AND d.parent_column_id = c.column_id
        WHERE d.parent_object_id = OBJECT_ID('dbo.tbl_solicitudes_becas') AND c.name = 'Nivel_Aprobacion_Actual');
    IF @dfNivelActual IS NOT NULL
    BEGIN
        DECLARE @sqlDropDf NVARCHAR(400) = N'ALTER TABLE dbo.tbl_solicitudes_becas DROP CONSTRAINT ' + QUOTENAME(@dfNivelActual) + N';';
        EXEC(@sqlDropDf);
    END;

    IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_tbl_solicitudes_becas_Nivel' AND parent_object_id = OBJECT_ID('dbo.tbl_solicitudes_becas'))
        ALTER TABLE dbo.tbl_solicitudes_becas DROP CONSTRAINT CK_tbl_solicitudes_becas_Nivel;

    ALTER TABLE dbo.tbl_solicitudes_becas DROP COLUMN Nivel_Aprobacion_Actual;
END;

IF COL_LENGTH('dbo.tbl_solicitudes_becas', 'Nivel_Aprobacion_Maximo') IS NOT NULL
BEGIN
    DECLARE @dfNivelMax NVARCHAR(128);
    WHILE EXISTS (
        SELECT 1
        FROM sys.default_constraints d
        INNER JOIN sys.columns c ON d.parent_object_id = c.object_id AND d.parent_column_id = c.column_id
        WHERE d.parent_object_id = OBJECT_ID('dbo.tbl_solicitudes_becas') AND c.name = 'Nivel_Aprobacion_Maximo'
    )
    BEGIN
        SELECT TOP 1 @dfNivelMax = d.name
        FROM sys.default_constraints d
        INNER JOIN sys.columns c ON d.parent_object_id = c.object_id AND d.parent_column_id = c.column_id
        WHERE d.parent_object_id = OBJECT_ID('dbo.tbl_solicitudes_becas') AND c.name = 'Nivel_Aprobacion_Maximo';

        IF @dfNivelMax IS NOT NULL
        BEGIN
            DECLARE @sqlDropDfMax NVARCHAR(400) = N'ALTER TABLE dbo.tbl_solicitudes_becas DROP CONSTRAINT ' + QUOTENAME(@dfNivelMax) + N';';
            EXEC(@sqlDropDfMax);
        END;
    END;

    -- La check CK_tbl_solicitudes_becas_Nivel ya se elimina antes al dropear Nivel_Aprobacion_Actual
    ALTER TABLE dbo.tbl_solicitudes_becas DROP COLUMN Nivel_Aprobacion_Maximo;
END;

IF COL_LENGTH('dbo.tbl_solicitudes_becas', 'Id_Estado_Flujo') IS NOT NULL
BEGIN
    -- Eliminar FKs que referencien Id_Estado_Flujo en la tabla
    DECLARE @fkFlujo NVARCHAR(128);
    WHILE EXISTS (
        SELECT 1
        FROM sys.foreign_keys fk
        INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
        INNER JOIN sys.columns c ON fkc.parent_object_id = c.object_id AND fkc.parent_column_id = c.column_id
        WHERE fk.parent_object_id = OBJECT_ID('dbo.tbl_solicitudes_becas') AND c.name = 'Id_Estado_Flujo'
    )
    BEGIN
        SELECT TOP 1 @fkFlujo = fk.name
        FROM sys.foreign_keys fk
        INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
        INNER JOIN sys.columns c ON fkc.parent_object_id = c.object_id AND fkc.parent_column_id = c.column_id
        WHERE fk.parent_object_id = OBJECT_ID('dbo.tbl_solicitudes_becas') AND c.name = 'Id_Estado_Flujo';

        DECLARE @sqlDropFkFlujo NVARCHAR(400) = N'ALTER TABLE dbo.tbl_solicitudes_becas DROP CONSTRAINT ' + QUOTENAME(@fkFlujo) + N';';
        EXEC(@sqlDropFkFlujo);
    END;

    ALTER TABLE dbo.tbl_solicitudes_becas DROP COLUMN Id_Estado_Flujo;
END;

IF COL_LENGTH('dbo.tbl_solicitudes_becas', 'Id_Usuario_Responsable') IS NOT NULL
    ALTER TABLE dbo.tbl_solicitudes_becas DROP COLUMN Id_Usuario_Responsable;

IF COL_LENGTH('dbo.tbl_solicitudes_becas', 'Id_Usuario_Supervisor') IS NOT NULL
    ALTER TABLE dbo.tbl_solicitudes_becas DROP COLUMN Id_Usuario_Supervisor;
GO

