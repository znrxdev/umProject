USE umDb;
GO

/*
Agrega la columna Calificacion_Maxima a tbl_evaluaciones_instancias si no existe.
Debe ejecutarse antes de recompilar los SP que la usan.
*/

IF COL_LENGTH('dbo.tbl_evaluaciones_instancias', 'Calificacion_Maxima') IS NULL
BEGIN
    PRINT 'Agregando columna Calificacion_Maxima a tbl_evaluaciones_instancias...';

    ALTER TABLE dbo.tbl_evaluaciones_instancias
    ADD Calificacion_Maxima DECIMAL(6,2) NULL CONSTRAINT DF_tbl_evaluaciones_instancias_Calificacion_Maxima DEFAULT(100);

    -- Asegurar valor para filas existentes
    UPDATE dbo.tbl_evaluaciones_instancias
    SET Calificacion_Maxima = 100
    WHERE Calificacion_Maxima IS NULL;
END
ELSE
BEGIN
    PRINT 'La columna Calificacion_Maxima ya existe en tbl_evaluaciones_instancias. No se realizaron cambios.';
END

GO

