USE umDb
GO

CREATE OR ALTER	FUNCTION fn_Validar_Permisos
(
@Id_Usuario INT,
@Id_Tipo_Transaccion INT
)
RETURNS INT
BEGIN
	DECLARE @Permiso INT

	SELECT @Permiso = 1
	FROM cls_transacciones_roles (NOLOCK) TP
	INNER JOIN cls_usuarios_roles (NOLOCK) UP ON TP.Id_Rol = UP.Id_Rol AND TP.Activo = 1 AND UP.Activo = 1
	INNER JOIN tbl_usuarios (NOLOCK) US ON UP.Id_Usuario = US.Id_Usuario AND US.Id_Estado = 1
	INNER JOIN cls_tipos_transacciones (NOLOCK) TT ON TP.Id_Tipo_Transaccion = TT.Id_Tipo_Transaccion AND TT.Activo = 1
	INNER JOIN cls_roles (NOLOCK) PE ON UP.Id_Rol = PE.Id_Rol AND PE.Activo = 1
	WHERE US.Id_Usuario = @Id_Usuario
	AND TT.Id_Tipo_Transaccion = @Id_Tipo_Transaccion

		IF(@Permiso IS NULL)
			SET @Permiso = 0

		RETURN @Permiso
 END