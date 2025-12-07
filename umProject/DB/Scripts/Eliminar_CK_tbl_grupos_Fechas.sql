USE umDb
GO

-- Eliminar la restricción CHECK CK_tbl_grupos_Fechas
-- Esta restricción verificaba que Fecha_Cierre >= Fecha_Creacion
-- Se elimina porque Fecha_Cierre se establece como la fecha de inicio del período académico,
-- que puede ser anterior a la fecha de creación del grupo

IF EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_tbl_grupos_Fechas')
BEGIN
    ALTER TABLE [dbo].[tbl_grupos]
    DROP CONSTRAINT [CK_tbl_grupos_Fechas];
    
    PRINT 'Restricción CK_tbl_grupos_Fechas eliminada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La restricción CK_tbl_grupos_Fechas no existe.';
END
GO

