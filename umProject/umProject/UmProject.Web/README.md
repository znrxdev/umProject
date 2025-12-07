# Sistema de Control de Evaluaciones Académicas - ASP.NET MVC Core

Este proyecto es la adaptación web del sistema de gestión académica desarrollado originalmente en Windows Forms a ASP.NET MVC Core.

## Estructura del Proyecto

El proyecto sigue una arquitectura en capas bien definida:

### Capas del Proyecto

1. **UmProject.Entities** - Capa de Entidades
   - Contiene los modelos de datos (Usuario, Menu, Persona, etc.)
   - Representa las entidades del dominio

2. **UmProject.Data** - Capa de Acceso a Datos
   - `IConexionService` / `ConexionService`: Manejo de conexiones a SQL Server
   - `IUsuarioRepository` / `UsuarioRepository`: Implementación de acceso a datos usando ADO.NET
   - `Utilidades`: Funciones de utilidad (hash de contraseñas con BCrypt)

3. **UmProject.Business** - Capa de Lógica de Negocio
   - `IUsuarioService` / `UsuarioService`: Servicios de negocio que encapsulan la lógica
   - Actúa como intermediario entre la capa de presentación y la capa de datos

4. **UmProject.Web** - Capa de Presentación (MVC)
   - Controladores: AccountController, HomeController, UsuariosController
   - Vistas: Razor views para la interfaz de usuario
   - Filtros: RequireSessionAttribute para proteger acciones que requieren sesión

## Configuración

### Base de Datos

La cadena de conexión se configura en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=ZNR;Initial Catalog=umDb;Integrated Security=true;TrustServerCertificate=True"
  }
}
```

**Nota:** Ajuste la cadena de conexión según su entorno.

### Dependencias Principales

- **Microsoft.Data.SqlClient**: Para acceso a SQL Server
- **BCrypt.Net-Next**: Para hash de contraseñas
- **Microsoft.Extensions.Configuration**: Para configuración

## Características Implementadas

### Autenticación y Sesión
- Sistema de login con validación de credenciales
- Manejo de sesión usando `HttpContext.Session`
- Menú dinámico basado en roles del usuario
- Filtro `RequireSessionAttribute` para proteger acciones

### Gestión de Usuarios
- Listado de usuarios
- Crear nuevo usuario
- Editar usuario existente
- Ver detalles de usuario
- Filtrado por diferentes criterios

### Navegación Dinámica
- Menú generado automáticamente según los permisos del usuario
- Los menús se obtienen de la base de datos según el rol asignado

## Uso de Stored Procedures

El sistema utiliza los mismos stored procedures del proyecto original:
- `usp_usuarios`: Maneja todas las operaciones de usuarios según `@Id_Tipo_Transaccion`
- Los tipos de transacción están definidos en la base de datos

## Flujo de Autenticación

1. Usuario accede a `/Account/Login`
2. Ingresa credenciales (usuario y contraseña)
3. El sistema valida contra la base de datos usando `usp_usuarios` con `@Id_Tipo_Transaccion = 19`
4. Si es válido, se crea la sesión con:
   - `IdSesion`: ID del usuario
   - `IdPersonaSesion`: ID de la persona asociada
   - `UsuarioSesion`: Nombre de usuario
   - `Menus`: Lista serializada de menús disponibles
5. Redirección al dashboard (`/Home/Index`)

## Próximos Pasos

Para completar la migración del sistema Windows Forms a web, se recomienda:

1. **Implementar más módulos:**
   - Evaluaciones Académicas
   - Materias
   - Becas y Solicitudes
   - Sanciones Académicas
   - Reportes
   - Auditoría

2. **Mejorar seguridad:**
   - Implementar ASP.NET Core Identity
   - Agregar autorización basada en políticas
   - Implementar tokens JWT para APIs

3. **Mejorar UI/UX:**
   - Agregar validaciones del lado del cliente
   - Implementar paginación en listados
   - Agregar búsqueda y filtros avanzados

4. **Testing:**
   - Unit tests para servicios
   - Integration tests para controladores
   - Tests de base de datos

## Ejecutar el Proyecto

```bash
cd UmProject.Web
dotnet restore
dotnet run
```

El proyecto estará disponible en `https://localhost:5001` o `http://localhost:5000` (según configuración).

## Notas Importantes

- El sistema mantiene la misma estructura de base de datos y stored procedures del proyecto original
- La lógica de negocio se mantiene igual, solo se adapta a los patrones de ASP.NET Core
- Los permisos se validan usando la función `fn_Validar_Permisos` de la base de datos
- Las transacciones se registran usando `sp_transacciones` para auditoría

