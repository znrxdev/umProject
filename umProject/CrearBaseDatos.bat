a@echo off
chcp 65001 >nul

REM Cambiar al directorio donde está ubicado este script
cd /d "%~dp0"

echo ============================================================
echo    SCRIPT DE CREACION DE BASE DE DATOS umDb
echo ============================================================
echo.
echo Directorio de trabajo: %CD%
echo.

REM Configuración de conexión SQL Server
set SERVER_NAME=ZNR
set DATABASE_NAME=umDb
set SQL_CMD=sqlcmd -S %SERVER_NAME% -E -C -b

echo [1/6] Eliminando base de datos existente (si existe)...
%SQL_CMD% -Q "IF EXISTS (SELECT name FROM sys.databases WHERE name = '%DATABASE_NAME%') BEGIN ALTER DATABASE [%DATABASE_NAME%] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [%DATABASE_NAME%]; END"
if %ERRORLEVEL% NEQ 0 (
    echo ADVERTENCIA: No se pudo eliminar la base de datos (puede que no exista)
)
echo.

echo [2/6] Creando base de datos...
%SQL_CMD% -Q "CREATE DATABASE [%DATABASE_NAME%]"
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: No se pudo crear la base de datos
    pause
    exit /b 1
)
echo Base de datos creada exitosamente.
echo.

echo [3/6] Creando estructura de tablas e indices...
%SQL_CMD% -d %DATABASE_NAME% -i "Estructura\umDb.sql"
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: No se pudo ejecutar umDb.sql
    pause
    exit /b 1
)
echo Estructura creada exitosamente.
echo.

echo [4/6] Insertando datos iniciales...
echo   - Ejecutando umDbData.sql...
%SQL_CMD% -d %DATABASE_NAME% -i "Estructura\umDbData.sql"
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: No se pudo ejecutar umDbData.sql
    pause
    exit /b 1
)
echo   - Ejecutando umDbData_Roles_Menus_Complemento.sql...
%SQL_CMD% -d %DATABASE_NAME% -i "Estructura\umDbData_Roles_Menus_Complemento.sql"
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: No se pudo ejecutar umDbData_Roles_Menus_Complemento.sql
    pause
    exit /b 1
)
echo Datos insertados exitosamente.
echo.

echo [5/6] Creando funciones...
echo   - Ejecutando fn_Validar_Permisos.sql...
%SQL_CMD% -d %DATABASE_NAME% -i "Funciones\fn_Validar_Permisos.sql"
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: No se pudo ejecutar fn_Validar_Permisos.sql
    pause
    exit /b 1
)
echo Funciones creadas exitosamente.
echo.

echo [6/6] Creando procedimientos almacenados...
echo   - Ejecutando sp_logs_errores_sql.sql (PRIMERO)...
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\sp_logs_errores_sql.sql"
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: No se pudo ejecutar sp_logs_errores_sql.sql
    pause
    exit /b 1
)
echo   - Ejecutando sp_transacciones.sql (SEGUNDO)...
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\sp_transacciones.sql"
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: No se pudo ejecutar sp_transacciones.sql
    pause
    exit /b 1
)
echo   - Ejecutando el resto de procedimientos almacenados...
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_becas_convocatorias.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_becas_criterios.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_becas_programas.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_catalogos.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_contactos.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_estados.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_evaluaciones_alumnos.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_evaluaciones_instancias.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_evaluaciones_modelos.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_grupos_inscripciones.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_grupos_secciones.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_grupos.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_inscripciones.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_materias_periodos.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_materias.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_menus_roles.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_menus.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_periodos_academicos.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_personas.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_roles.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_sanciones_academicas.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_secciones.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_solicitudes_becas.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_tipos_catalogos.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_tipos_transacciones.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_transacciones_estados.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_transacciones_roles.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_usuarios_roles.sql" >nul 2>&1
%SQL_CMD% -d %DATABASE_NAME% -i "Procedimientos Almacenados\usp_usuarios.sql" >nul 2>&1
echo Procedimientos almacenados creados exitosamente.
echo.

echo ============================================================
echo    BASE DE DATOS CREADA EXITOSAMENTE
echo ============================================================
echo.
echo Base de datos: %DATABASE_NAME%
echo Servidor: %SERVER_NAME%
echo.
pause
