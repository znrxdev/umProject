USE umDb
GO

/*
===========================================================
ÍNDICES PARA OPTIMIZAR fn_Validar_Permisos
===========================================================
Esta función es crítica ya que se ejecuta en cada procedimiento almacenado
para validar permisos. Los índices optimizan los JOINs y filtros utilizados.
===========================================================
*/

-- Índice para cls_transacciones_roles
-- Optimiza el JOIN con cls_usuarios_roles por Id_Rol y el filtro por Activo e Id_Tipo_Transaccion
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_cls_transacciones_roles_Id_Rol_Activo_Id_Tipo_Transaccion')
BEGIN
    CREATE NONCLUSTERED INDEX IX_cls_transacciones_roles_Id_Rol_Activo_Id_Tipo_Transaccion
    ON dbo.cls_transacciones_roles(Id_Rol, Activo, Id_Tipo_Transaccion)
    INCLUDE (Id_Transaccion_Rol);
    PRINT 'Índice IX_cls_transacciones_roles_Id_Rol_Activo_Id_Tipo_Transaccion creado exitosamente.';
END
ELSE
BEGIN
    PRINT 'El índice IX_cls_transacciones_roles_Id_Rol_Activo_Id_Tipo_Transaccion ya existe.';
END
GO

-- Índice para cls_usuarios_roles
-- Optimiza el JOIN con tbl_usuarios por Id_Usuario y con cls_transacciones_roles por Id_Rol, y el filtro por Activo
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_cls_usuarios_roles_Id_Usuario_Id_Rol_Activo')
BEGIN
    CREATE NONCLUSTERED INDEX IX_cls_usuarios_roles_Id_Usuario_Id_Rol_Activo
    ON dbo.cls_usuarios_roles(Id_Usuario, Id_Rol, Activo)
    INCLUDE (Id_Usuario_Rol);
    PRINT 'Índice IX_cls_usuarios_roles_Id_Usuario_Id_Rol_Activo creado exitosamente.';
END
ELSE
BEGIN
    PRINT 'El índice IX_cls_usuarios_roles_Id_Usuario_Id_Rol_Activo ya existe.';
END
GO

-- Índice para tbl_usuarios
-- Optimiza el filtro WHERE por Id_Usuario y el filtro por Id_Estado = 1
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_tbl_usuarios_Id_Usuario_Id_Estado')
BEGIN
    CREATE NONCLUSTERED INDEX IX_tbl_usuarios_Id_Usuario_Id_Estado
    ON dbo.tbl_usuarios(Id_Usuario, Id_Estado)
    INCLUDE (Id_Persona);
    PRINT 'Índice IX_tbl_usuarios_Id_Usuario_Id_Estado creado exitosamente.';
END
ELSE
BEGIN
    PRINT 'El índice IX_tbl_usuarios_Id_Usuario_Id_Estado ya existe.';
END
GO

-- Índice para cls_tipos_transacciones
-- Optimiza el filtro WHERE por Id_Tipo_Transaccion y el filtro por Activo = 1
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_cls_tipos_transacciones_Id_Tipo_Transaccion_Activo')
BEGIN
    CREATE NONCLUSTERED INDEX IX_cls_tipos_transacciones_Id_Tipo_Transaccion_Activo
    ON dbo.cls_tipos_transacciones(Id_Tipo_Transaccion, Activo)
    INCLUDE (Nombre_Tipo_Transaccion);
    PRINT 'Índice IX_cls_tipos_transacciones_Id_Tipo_Transaccion_Activo creado exitosamente.';
END
ELSE
BEGIN
    PRINT 'El índice IX_cls_tipos_transacciones_Id_Tipo_Transaccion_Activo ya existe.';
END
GO

-- Índice para cls_roles
-- Optimiza el JOIN con cls_usuarios_roles por Id_Rol y el filtro por Activo = 1
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_cls_roles_Id_Rol_Activo')
BEGIN
    CREATE NONCLUSTERED INDEX IX_cls_roles_Id_Rol_Activo
    ON dbo.cls_roles(Id_Rol, Activo)
    INCLUDE (Nombre_Rol);
    PRINT 'Índice IX_cls_roles_Id_Rol_Activo creado exitosamente.';
END
ELSE
BEGIN
    PRINT 'El índice IX_cls_roles_Id_Rol_Activo ya existe.';
END
GO

PRINT '===========================================================';
PRINT 'Proceso de creación de índices para fn_Validar_Permisos completado.';
PRINT '===========================================================';

