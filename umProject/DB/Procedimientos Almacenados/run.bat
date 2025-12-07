@echo off
SETLOCAL ENABLEDELAYEDEXPANSION

set SERVER=ZNR
set DATABASE=umDb
set SCRIPTS_PATH=C:\Users\justi\Desktop\umProject\DB\Procedimientos Almacenados

echo.
echo ============================================
echo   INICIANDO EJECUCION DE TODOS LOS SP (umDb)
echo ============================================
echo.

call :RunSQL "%SCRIPTS_PATH%\sp_logs_errores_sql.sql"
call :RunSQL "%SCRIPTS_PATH%\sp_transacciones.sql"

echo.
echo Ejecutando el resto de scripts...
echo.

for %%F in ("%SCRIPTS_PATH%\*.sql") do (
    if /I NOT "%%~nxF"=="sp_logs_errores_sql.sql" (
        if /I NOT "%%~nxF"=="sp_transacciones.sql" (
            call :RunSQL "%%F"
        )
    )
)

echo.
echo ============================================
echo   EJECUCIÓN COMPLETADA EXITOSAMENTE
echo ============================================
echo.

ENDLOCAL
pause
goto :EOF

:RunSQL
echo --------------------------------------------------
echo Ejecutando: %1
echo --------------------------------------------------
sqlcmd -S %SERVER% -d %DATABASE% -E -C -b -i "%~1"
IF ERRORLEVEL 1 (
    echo ERROR al ejecutar %1
    pause
    exit /b 1
)
goto :EOF
