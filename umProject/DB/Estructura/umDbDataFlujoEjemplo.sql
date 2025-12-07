USE umDb;
GO

PRINT '============================================';
PRINT 'INICIANDO CARGA DE DATOS COMPLETA (2024-I a 2025-II)';
PRINT '============================================';
GO

BEGIN TRY
    BEGIN TRAN;

    ------------------------------------------------------------
    -- VARIABLES GLOBALES / IDS DE CATALOGOS Y ESTADOS
    ------------------------------------------------------------
    PRINT 'Cargando IDs base de cat�logos y estados...';

    DECLARE 
        @IdEstado_Activo        INT,
        @IdEstado_Inactivo      INT,
        @IdEstado_Pendiente     INT,
        @IdEstado_EnRevision    INT,
        @IdEstado_Aprobada      INT,
        @IdEstado_Rechazada     INT,
        @IdEstado_Planificada   INT;

    SELECT @IdEstado_Activo      = Id_Estado FROM cls_estados WHERE Nombre_Estado = 'ACTIVO';
    SELECT @IdEstado_Inactivo    = Id_Estado FROM cls_estados WHERE Nombre_Estado = 'INACTIVO';
    SELECT @IdEstado_Pendiente   = Id_Estado FROM cls_estados WHERE Nombre_Estado = 'PENDIENTE';
    SELECT @IdEstado_EnRevision  = Id_Estado FROM cls_estados WHERE Nombre_Estado = 'EN REVISION';
    SELECT @IdEstado_Aprobada    = Id_Estado FROM cls_estados WHERE Nombre_Estado = 'APROBADA';
    SELECT @IdEstado_Rechazada   = Id_Estado FROM cls_estados WHERE Nombre_Estado = 'RECHAZADA';
    SELECT @IdEstado_Planificada = Id_Estado FROM cls_estados WHERE Nombre_Estado = 'PLANIFICADA';

    DECLARE
        @IdRol_Admin             INT,
        @IdRol_Estudiante        INT,
        @IdRol_Docente           INT,
        @IdRol_CoordAcademico    INT,
        @IdRol_CoordBecas        INT,
        @IdRol_Secretaria        INT;

    SELECT @IdRol_Admin          = Id_Rol FROM cls_roles WHERE Nombre_Rol = 'ADMINISTRADOR';
    SELECT @IdRol_Estudiante     = Id_Rol FROM cls_roles WHERE Nombre_Rol = 'ESTUDIANTE';
    SELECT @IdRol_Docente        = Id_Rol FROM cls_roles WHERE Nombre_Rol = 'DOCENTE';
    SELECT @IdRol_CoordAcademico = Id_Rol FROM cls_roles WHERE Nombre_Rol = 'COORDINADOR ACAD�MICO';
    SELECT @IdRol_CoordBecas     = Id_Rol FROM cls_roles WHERE Nombre_Rol = 'COORDINADOR DE BECAS';
    SELECT @IdRol_Secretaria     = Id_Rol FROM cls_roles WHERE Nombre_Rol = 'SECRETAR�A ACAD�MICA';

    DECLARE
        @IdTipoPeriodo_Cuatrimestre INT,
        @IdJornada_Matutina         INT,
        @IdJornada_Vespertina       INT,
        @IdJornada_Nocturna         INT,
        @IdJornada_Sabatina         INT,
        @IdTipoSeccion_Teorica      INT,
        @IdTipoSeccion_Practica     INT,
        @IdTipoSeccion_Lab          INT,
        @IdTipoSeccion_Taller       INT,
        @IdAula_A101                INT,
        @IdAula_A102                INT,
        @IdAula_Lab201              INT,
        @IdAula_Lab202              INT,
        @IdAula_Virtual             INT,
        @IdTipoGrupo_Academico      INT,
        @IdTipoInscripcion_Regular  INT,
        @IdTipoVinculo_Principal    INT,
        @IdMetodoCalc_Ponderado     INT,
        @IdTipoEval_Escrita         INT,
        @IdTipoEval_SelMult         INT,
        @IdTipoEval_Defensa         INT,
        @IdMoneda_Cordoba           INT,
        @IdMoneda_Dolar             INT,
        @IdTipoProg_Beca            INT,
        @IdTipoCriterio_Promedio    INT,
        @IdTipoCriterio_Creditos    INT,
        @IdTipoCriterio_Sanciones   INT,
        @IdTipoFalta_Academica      INT,
        @IdTipoFalta_Disciplinaria  INT,
        @IdTipoSancion_Plagio       INT,
        @IdTipoSancion_Copia        INT,
        @IdTipoSancion_Inasist      INT;

    -- TIPO PERIODO = CUATRIMESTRE
    SELECT @IdTipoPeriodo_Cuatrimestre = Id_Catalogo 
      FROM cls_catalogos 
     WHERE Id_Tipo_Catalogo = 10 AND Nombre_Catalogo = 'CUATRIMESTRE';

    -- JORNADAS
    SELECT @IdJornada_Matutina   = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 14 AND Nombre_Catalogo = 'MATUTINA';
    SELECT @IdJornada_Vespertina = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 14 AND Nombre_Catalogo = 'VESPERTINA';
    SELECT @IdJornada_Nocturna   = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 14 AND Nombre_Catalogo = 'NOCTURNA';
    SELECT @IdJornada_Sabatina   = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 14 AND Nombre_Catalogo = 'SABATINA';

    -- TIPOS DE SECCION
    SELECT @IdTipoSeccion_Teorica  = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 15 AND Nombre_Catalogo = 'TEORICA';
    SELECT @IdTipoSeccion_Practica = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 15 AND Nombre_Catalogo = 'PRACTICA';
    SELECT @IdTipoSeccion_Lab      = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 15 AND Nombre_Catalogo = 'LABORATORIO';
    SELECT @IdTipoSeccion_Taller   = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 15 AND Nombre_Catalogo = 'TALLER';

    -- AULAS
    SELECT @IdAula_A101    = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 16 AND Nombre_Catalogo = 'A-101';
    SELECT @IdAula_A102    = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 16 AND Nombre_Catalogo = 'A-102';
    SELECT @IdAula_Lab201  = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 16 AND Nombre_Catalogo = 'LAB-201';
    SELECT @IdAula_Lab202  = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 16 AND Nombre_Catalogo = 'LAB-202';
    SELECT @IdAula_Virtual = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 16 AND Nombre_Catalogo = 'VIRTUAL';

    -- TIPO GRUPO
    SELECT @IdTipoGrupo_Academico = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 17 AND Nombre_Catalogo = 'ACADEMICO';

    -- TIPO INSCRIPCION
    SELECT @IdTipoInscripcion_Regular = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 18 AND Nombre_Catalogo = 'REGULAR';

    -- TIPO VINCULO
    SELECT @IdTipoVinculo_Principal = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 20 AND Nombre_Catalogo = 'PRINCIPAL';

    -- METODO CALCULO
    SELECT @IdMetodoCalc_Ponderado = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 21 AND Nombre_Catalogo = 'PROMEDIO PONDERADO';

    -- TIPOS EVALUACION
    SELECT @IdTipoEval_Escrita = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 8 AND Nombre_Catalogo = 'ESCRITA';
    SELECT @IdTipoEval_SelMult = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 8 AND Nombre_Catalogo = 'SELECCION MULTIPLE';
    SELECT @IdTipoEval_Defensa = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 8 AND Nombre_Catalogo = 'DEFENSA';

    -- MONEDA
    SELECT @IdMoneda_Cordoba = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 12 AND Nombre_Catalogo = 'CORDOBA NIO';
    SELECT @IdMoneda_Dolar   = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 12 AND Nombre_Catalogo = 'DOLAR USD';

    -- TIPO PROGRAMA
    SELECT @IdTipoProg_Beca = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 5 AND Nombre_Catalogo = 'BECA';

    -- TIPOS CRITERIO
    SELECT @IdTipoCriterio_Promedio  = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 13 AND Nombre_Catalogo = 'PROMEDIO';
    SELECT @IdTipoCriterio_Creditos  = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 13 AND Nombre_Catalogo = 'CREDITOS';
    SELECT @IdTipoCriterio_Sanciones = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 13 AND Nombre_Catalogo = 'SANCIONES';

    -- TIPOS SANCION / FALTA
    SELECT @IdTipoSancion_Plagio = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 6 AND Nombre_Catalogo = 'PLAGIO';
    SELECT @IdTipoSancion_Copia  = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 6 AND Nombre_Catalogo = 'COPIA EN EXAMEN';
    SELECT @IdTipoSancion_Inasist= Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 6 AND Nombre_Catalogo = 'INASISTENCIAS';

    SELECT @IdTipoFalta_Academica     = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 24 AND Nombre_Catalogo = 'ACADEMICA';
    SELECT @IdTipoFalta_Disciplinaria = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 24 AND Nombre_Catalogo = 'DISCIPLINARIA';

    DECLARE @IdTipoDocumento_Cedula INT,
            @IdGenero_Masculino   INT,
            @IdGenero_Femenino    INT,
            @IdNacionalidad_Nica  INT,
            @IdEstadoCivil_Soltero INT;

    SELECT @IdTipoDocumento_Cedula = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 1 AND Nombre_Catalogo = 'CEDULA NICARAGUENSE';
    SELECT @IdGenero_Masculino     = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 2 AND Nombre_Catalogo = 'MASCULINO';
    SELECT @IdGenero_Femenino      = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 2 AND Nombre_Catalogo = 'FEMENINO';
    SELECT @IdNacionalidad_Nica    = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 3 AND Nombre_Catalogo = 'NICARAGUENSE';
    SELECT @IdEstadoCivil_Soltero  = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 4 AND Nombre_Catalogo = 'SOLTERO/A';

    DECLARE @IdUsuario_AdminPrincipal INT;
    SELECT  @IdUsuario_AdminPrincipal = Id_Usuario 
    FROM    tbl_usuarios 
    WHERE   Usuario = 'znr';

    ------------------------------------------------------------
    -- FASE 1: USUARIOS Y ROLES
    ------------------------------------------------------------
    PRINT '';
    PRINT '=== FASE 1: CREANDO PERSONAS, USUARIOS Y ROLES ===';

    -- TABLAS TEMPORALES PARA MAPEAR
    DECLARE @Personas TABLE(
        Alias       VARCHAR(50),
        Id_Persona  INT
    );

    DECLARE @Usuarios TABLE(
        Alias       VARCHAR(50),
        Id_Usuario  INT
    );

    --------------------------------------------------------
    -- 1.1 PERSONAS (admins, coordinadores, secretarias, docentes, estudiantes)
    --------------------------------------------------------

    PRINT 'Insertando personas base (personal administrativo y docentes)...';

    -- Admin extra
    INSERT INTO tbl_personas(
        Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido,
        Fecha_Nacimiento, Id_Tipo_Documento, Valor_Documento,
        Id_Genero_Persona, Id_Nacionalidad, Id_Estado_Civil,
        Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Id_Estado
    )
    VALUES
    ('CARLOS', 'ALBERTO', 'RAMIREZ', 'PEREZ', '1985-02-10', @IdTipoDocumento_Cedula, '001-ADMIN-02',
     @IdGenero_Masculino, @IdNacionalidad_Nica, @IdEstadoCivil_Soltero,
     GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, @IdEstado_Activo);

    DECLARE @IdPersona_Admin2 INT = SCOPE_IDENTITY();
    INSERT INTO @Personas(Alias, Id_Persona) VALUES ('ADMIN2', @IdPersona_Admin2);

    -- Coordinadores acad�micos (2)
    INSERT INTO tbl_personas(Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido,
        Fecha_Nacimiento, Id_Tipo_Documento, Valor_Documento,
        Id_Genero_Persona, Id_Nacionalidad, Id_Estado_Civil,
        Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Id_Estado)
    VALUES
    ('MARIA', 'JOSE', 'GARCIA', 'LOPEZ', '1990-05-20', @IdTipoDocumento_Cedula, '001-COORD-01',
     @IdGenero_Femenino, @IdNacionalidad_Nica, @IdEstadoCivil_Soltero,
     GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, @IdEstado_Activo),
    ('PEDRO', 'ANTONIO', 'SANCHEZ', 'ROJAS', '1988-09-15', @IdTipoDocumento_Cedula, '001-COORD-02',
     @IdGenero_Masculino, @IdNacionalidad_Nica, @IdEstadoCivil_Soltero,
     GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, @IdEstado_Activo);

    INSERT INTO @Personas(Alias, Id_Persona)
    SELECT 'COORD1', MIN(Id_Persona) FROM tbl_personas WHERE Valor_Documento = '001-COORD-01';
    INSERT INTO @Personas(Alias, Id_Persona)
    SELECT 'COORD2', MIN(Id_Persona) FROM tbl_personas WHERE Valor_Documento = '001-COORD-02';

    -- Coordinador de Becas (1)
    INSERT INTO tbl_personas(Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido,
        Fecha_Nacimiento, Id_Tipo_Documento, Valor_Documento,
        Id_Genero_Persona, Id_Nacionalidad, Id_Estado_Civil,
        Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Id_Estado)
    VALUES
    ('LUISA', 'FERNANDA', 'MARTINEZ', 'CRUZ', '1992-03-11', @IdTipoDocumento_Cedula, '001-BECA-01',
     @IdGenero_Femenino, @IdNacionalidad_Nica, @IdEstadoCivil_Soltero,
     GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, @IdEstado_Activo);

    INSERT INTO @Personas(Alias, Id_Persona)
    SELECT 'COORDBECAS1', MIN(Id_Persona) FROM tbl_personas WHERE Valor_Documento = '001-BECA-01';

    -- Secretaria acad�mica (2)
    INSERT INTO tbl_personas(Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido,
        Fecha_Nacimiento, Id_Tipo_Documento, Valor_Documento,
        Id_Genero_Persona, Id_Nacionalidad, Id_Estado_Civil,
        Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Id_Estado)
    VALUES
    ('ANA', 'SOFIA', 'ORTIZ', 'MENDOZA', '1995-01-25', @IdTipoDocumento_Cedula, '001-SEC-01',
     @IdGenero_Femenino, @IdNacionalidad_Nica, @IdEstadoCivil_Soltero,
     GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, @IdEstado_Activo),
    ('JAVIER', 'ANDRES', 'MORALES', 'TELLERIA', '1993-07-19', @IdTipoDocumento_Cedula, '001-SEC-02',
     @IdGenero_Masculino, @IdNacionalidad_Nica, @IdEstadoCivil_Soltero,
     GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, @IdEstado_Activo);

    INSERT INTO @Personas(Alias, Id_Persona)
    SELECT 'SEC1', MIN(Id_Persona) FROM tbl_personas WHERE Valor_Documento = '001-SEC-01';
    INSERT INTO @Personas(Alias, Id_Persona)
    SELECT 'SEC2', MIN(Id_Persona) FROM tbl_personas WHERE Valor_Documento = '001-SEC-02';

    -- Docentes (8)
    PRINT 'Insertando docentes...';

    DECLARE @i INT = 1;
    WHILE @i <= 8
    BEGIN
        INSERT INTO tbl_personas(Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido,
            Fecha_Nacimiento, Id_Tipo_Documento, Valor_Documento,
            Id_Genero_Persona, Id_Nacionalidad, Id_Estado_Civil,
            Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Id_Estado)
        VALUES(
            CONCAT('DOCENTE', @i), NULL, 'APELLIDO', CONCAT('D', @i),
            DATEFROMPARTS(1980 + (@i % 10), ((@i % 12) + 1), ((@i % 27) + 1)),
            @IdTipoDocumento_Cedula, CONCAT('001-DOC-', RIGHT('00' + CAST(@i AS VARCHAR(2)), 2)),
            CASE WHEN @i % 2 = 0 THEN @IdGenero_Femenino ELSE @IdGenero_Masculino END,
            @IdNacionalidad_Nica, @IdEstadoCivil_Soltero,
            GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, @IdEstado_Activo
        );

        INSERT INTO @Personas(Alias, Id_Persona)
        SELECT CONCAT('DOC', @i), SCOPE_IDENTITY();

        SET @i += 1;
    END

    -- Estudiantes (30)
    PRINT 'Insertando estudiantes...';

    SET @i = 1;
    WHILE @i <= 30
    BEGIN
        INSERT INTO tbl_personas(Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido,
            Fecha_Nacimiento, Id_Tipo_Documento, Valor_Documento,
            Id_Genero_Persona, Id_Nacionalidad, Id_Estado_Civil,
            Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Id_Estado)
        VALUES(
            CONCAT('EST', RIGHT('000' + CAST(@i AS VARCHAR(3)), 3)), NULL, 'ALUMNO', CONCAT('X', @i),
            DATEFROMPARTS(2003 + (@i % 3), ((@i % 12) + 1), ((@i % 27) + 1)),
            @IdTipoDocumento_Cedula, CONCAT('001-EST-', RIGHT('000' + CAST(@i AS VARCHAR(3)), 3)),
            CASE WHEN @i % 2 = 0 THEN @IdGenero_Femenino ELSE @IdGenero_Masculino END,
            @IdNacionalidad_Nica, @IdEstadoCivil_Soltero,
            GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, @IdEstado_Activo
        );

        INSERT INTO @Personas(Alias, Id_Persona)
        SELECT CONCAT('EST', @i), SCOPE_IDENTITY();

        SET @i += 1;
    END

    --------------------------------------------------------
    -- 1.2 USUARIOS
    --------------------------------------------------------
    PRINT 'Creando usuarios para cada persona...';

    -- Helper: misma contrase�a hash de prueba (ejemplo, misma que ya usas o cualquier Bcrypt)
    DECLARE @HashPwd VARCHAR(100) = '$2a$11$lrqmyt.z2dgs1mnBp8InyuT.XJvS49jlTN.iPbGi.VYVlYYfWl22W';

    -- Admin2
    DECLARE @IdUsuario INT, @IdPersona INT;

    SELECT @IdPersona = Id_Persona FROM @Personas WHERE Alias = 'ADMIN2';

    INSERT INTO tbl_usuarios(
        Id_Persona, Usuario, Contrasena,
        Fecha_Creacion, Fecha_Modificacion, Ultima_Sesion, Ultimo_Cambio_Contrasena,
        Id_Creador, Id_Modificador, Id_Transaccion, Id_Estado
    )
    VALUES(
        @IdPersona, 'admin2', @HashPwd,
        GETDATE(), GETDATE(), NULL, NULL,
        @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, @IdEstado_Activo
    );

    SET @IdUsuario = SCOPE_IDENTITY();
    INSERT INTO @Usuarios(Alias, Id_Usuario) VALUES('ADMIN2', @IdUsuario);

    -- Coord Acad�micos
    DECLARE @tmpAlias VARCHAR(50);

    DECLARE curCoord CURSOR FOR
        SELECT Alias FROM @Personas WHERE Alias IN ('COORD1','COORD2');

    OPEN curCoord;
    FETCH NEXT FROM curCoord INTO @tmpAlias;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SELECT @IdPersona = Id_Persona FROM @Personas WHERE Alias = @tmpAlias;

        INSERT INTO tbl_usuarios(
            Id_Persona, Usuario, Contrasena,
            Fecha_Creacion, Fecha_Modificacion, Ultima_Sesion, Ultimo_Cambio_Contrasena,
            Id_Creador, Id_Modificador, Id_Transaccion, Id_Estado
        )
        VALUES(
            @IdPersona, LOWER(@tmpAlias), @HashPwd,
            GETDATE(), GETDATE(), NULL, NULL,
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, @IdEstado_Activo
        );

        SET @IdUsuario = SCOPE_IDENTITY();
        INSERT INTO @Usuarios(Alias, Id_Usuario) VALUES(@tmpAlias, @IdUsuario);

        FETCH NEXT FROM curCoord INTO @tmpAlias;
    END
    CLOSE curCoord;
    DEALLOCATE curCoord;

    -- Coord Becas
    SELECT @IdPersona = Id_Persona FROM @Personas WHERE Alias = 'COORDBECAS1';
    INSERT INTO tbl_usuarios(
        Id_Persona, Usuario, Contrasena,
        Fecha_Creacion, Fecha_Modificacion, Ultima_Sesion, Ultimo_Cambio_Contrasena,
        Id_Creador, Id_Modificador, Id_Transaccion, Id_Estado
    )
    VALUES(
        @IdPersona, 'coorbecas1', @HashPwd,
        GETDATE(), GETDATE(), NULL, NULL,
        @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, @IdEstado_Activo
    );
    SET @IdUsuario = SCOPE_IDENTITY();
    INSERT INTO @Usuarios(Alias, Id_Usuario) VALUES('COORDBECAS1', @IdUsuario);

    -- Secretarias
    DECLARE curSec CURSOR FOR
        SELECT Alias FROM @Personas WHERE Alias IN ('SEC1','SEC2');

    OPEN curSec;
    FETCH NEXT FROM curSec INTO @tmpAlias;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SELECT @IdPersona = Id_Persona FROM @Personas WHERE Alias = @tmpAlias;

        INSERT INTO tbl_usuarios(
            Id_Persona, Usuario, Contrasena,
            Fecha_Creacion, Fecha_Modificacion, Ultima_Sesion, Ultimo_Cambio_Contrasena,
            Id_Creador, Id_Modificador, Id_Transaccion, Id_Estado
        )
        VALUES(
            @IdPersona, LOWER(@tmpAlias), @HashPwd,
            GETDATE(), GETDATE(), NULL, NULL,
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, @IdEstado_Activo
        );

        SET @IdUsuario = SCOPE_IDENTITY();
        INSERT INTO @Usuarios(Alias, Id_Usuario) VALUES(@tmpAlias, @IdUsuario);

        FETCH NEXT FROM curSec INTO @tmpAlias;
    END
    CLOSE curSec;
    DEALLOCATE curSec;

    -- Docentes
    DECLARE curDoc CURSOR FOR
        SELECT Alias FROM @Personas WHERE Alias LIKE 'DOC%';

    OPEN curDoc;
    FETCH NEXT FROM curDoc INTO @tmpAlias;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SELECT @IdPersona = Id_Persona FROM @Personas WHERE Alias = @tmpAlias;

        INSERT INTO tbl_usuarios(
            Id_Persona, Usuario, Contrasena,
            Fecha_Creacion, Fecha_Modificacion, Ultima_Sesion, Ultimo_Cambio_Contrasena,
            Id_Creador, Id_Modificador, Id_Transaccion, Id_Estado
        )
        VALUES(
            @IdPersona, LOWER(@tmpAlias), @HashPwd,
            GETDATE(), GETDATE(), NULL, NULL,
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, @IdEstado_Activo
        );

        SET @IdUsuario = SCOPE_IDENTITY();
        INSERT INTO @Usuarios(Alias, Id_Usuario) VALUES(@tmpAlias, @IdUsuario);

        FETCH NEXT FROM curDoc INTO @tmpAlias;
    END
    CLOSE curDoc;
    DEALLOCATE curDoc;

    -- Estudiantes
    DECLARE curEst CURSOR FOR
        SELECT Alias FROM @Personas WHERE Alias LIKE 'EST[0-9]%';

    OPEN curEst;
    FETCH NEXT FROM curEst INTO @tmpAlias;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SELECT @IdPersona = Id_Persona FROM @Personas WHERE Alias = @tmpAlias;

        INSERT INTO tbl_usuarios(
            Id_Persona, Usuario, Contrasena,
            Fecha_Creacion, Fecha_Modificacion, Ultima_Sesion, Ultimo_Cambio_Contrasena,
            Id_Creador, Id_Modificador, Id_Transaccion, Id_Estado
        )
        VALUES(
            @IdPersona, LOWER(@tmpAlias), @HashPwd,
            GETDATE(), GETDATE(), NULL, NULL,
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, @IdEstado_Activo
        );

        SET @IdUsuario = SCOPE_IDENTITY();
        INSERT INTO @Usuarios(Alias, Id_Usuario) VALUES(@tmpAlias, @IdUsuario);

        FETCH NEXT FROM curEst INTO @tmpAlias;
    END
    CLOSE curEst;
    DEALLOCATE curEst;

    --------------------------------------------------------
    -- 1.3 ROLES POR USUARIO
    --------------------------------------------------------
    PRINT 'Asignando roles a usuarios...';

    -- Admin principal (znr) ya tiene rol admin en tu script previo

    -- Admin2
    INSERT INTO cls_usuarios_roles(Id_Usuario, Id_Rol, Fecha_Creacion, Fecha_Modificacion, 
                                  Id_Creador, Id_Modificador, Id_Transaccion, Activo)
    SELECT Id_Usuario, @IdRol_Admin, GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, 1
    FROM @Usuarios WHERE Alias = 'ADMIN2';

    -- Coord Acad�mico
    INSERT INTO cls_usuarios_roles(Id_Usuario, Id_Rol, Fecha_Creacion, Fecha_Modificacion, 
                                  Id_Creador, Id_Modificador, Id_Transaccion, Activo)
    SELECT Id_Usuario, @IdRol_CoordAcademico, GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, 1
    FROM @Usuarios WHERE Alias IN ('COORD1','COORD2');

    -- Coord Becas
    INSERT INTO cls_usuarios_roles(Id_Usuario, Id_Rol, Fecha_Creacion, Fecha_Modificacion, 
                                  Id_Creador, Id_Modificador, Id_Transaccion, Activo)
    SELECT Id_Usuario, @IdRol_CoordBecas, GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, 1
    FROM @Usuarios WHERE Alias = 'COORDBECAS1';

    -- Secretaria
    INSERT INTO cls_usuarios_roles(Id_Usuario, Id_Rol, Fecha_Creacion, Fecha_Modificacion, 
                                  Id_Creador, Id_Modificador, Id_Transaccion, Activo)
    SELECT Id_Usuario, @IdRol_Secretaria, GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, 1
    FROM @Usuarios WHERE Alias IN ('SEC1','SEC2');

    -- Docentes
    INSERT INTO cls_usuarios_roles(Id_Usuario, Id_Rol, Fecha_Creacion, Fecha_Modificacion, 
                                  Id_Creador, Id_Modificador, Id_Transaccion, Activo)
    SELECT Id_Usuario, @IdRol_Docente, GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, 1
    FROM @Usuarios WHERE Alias LIKE 'DOC%';

    -- Estudiantes
    INSERT INTO cls_usuarios_roles(Id_Usuario, Id_Rol, Fecha_Creacion, Fecha_Modificacion, 
                                  Id_Creador, Id_Modificador, Id_Transaccion, Activo)
    SELECT Id_Usuario, @IdRol_Estudiante, GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, 1
    FROM @Usuarios WHERE Alias LIKE 'EST%';


    ------------------------------------------------------------
    -- FASE 2: MATERIAS
    ------------------------------------------------------------
    PRINT '';
    PRINT '=== FASE 2: CREANDO MATERIAS ===';

    INSERT INTO cls_materias(
        Codigo_Materia, Nombre_Materia,
        Fecha_Creacion, Fecha_Modificacion,
        Id_Creador, Id_Modificador, Id_Transaccion, Activo
    )
    VALUES
    ('MAT-101', 'MATEMATICA BASICA',       GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, 1),
    ('PROG-101','PROGRAMACION I',          GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, 1),
    ('PROG-102','PROGRAMACION II',         GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, 1),
    ('BD-101',  'BASES DE DATOS I',        GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, 1),
    ('BD-102',  'BASES DE DATOS II',       GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, 1),
    ('RED-101', 'REDES I',                 GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, 1),
    ('SO-101',  'SISTEMAS OPERATIVOS',     GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, 1),
    ('ARQ-101', 'ARQUITECTURA DE COMPUTO', GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, 1),
    ('ING-101', 'INGLES I',                GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, 1),
    ('INV-101', 'METODOLOGIA DE LA INVESTIGACION', GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, 1),
    ('ETI-101', 'ETICA PROFESIONAL',       GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, 1),
    ('LOG-101', 'LOGICA DE PROGRAMACION',  GETDATE(), GETDATE(), @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, 1);

    ------------------------------------------------------------
    -- FASE 3: PERIODOS ACADEMICOS (2024-I a 2025-II)
    ------------------------------------------------------------
    PRINT '';
    PRINT '=== FASE 3: CREANDO PERIODOS ACADEMICOS ===';

    DECLARE @Periodos TABLE(
        Codigo_Periodo      VARCHAR(20),
        Nombre_Periodo      NVARCHAR(100),
        Fecha_Inicio        DATE,
        Fecha_Fin           DATE,
        Fecha_Cierre_Calif  DATE,
        Es_Periodo_Actual   BIT,
        Permite_Inscripciones BIT,
        Id_Periodo          INT
    );

    INSERT INTO tbl_periodos_academicos(
        Codigo_Periodo, Nombre_Periodo, Id_Tipo_Periodo,
        Fecha_Inicio, Fecha_Fin, Fecha_Cierre_Calificaciones,
        Es_Periodo_Actual, Permite_Inscripciones,
        Codigo_Integracion, Observaciones,
        Id_Estado, Id_Estado_Publicacion,
        Hash_Configuracion,
        Fecha_Creacion, Fecha_Modificacion,
        Id_Creador, Id_Modificador, Id_Transaccion,
        Codigo_Control
    )
    OUTPUT
        INSERTED.Codigo_Periodo,
        INSERTED.Nombre_Periodo,
        INSERTED.Fecha_Inicio,
        INSERTED.Fecha_Fin,
        INSERTED.Fecha_Cierre_Calificaciones,
        INSERTED.Es_Periodo_Actual,
        INSERTED.Permite_Inscripciones,
        INSERTED.Id_Periodo
    INTO @Periodos(Codigo_Periodo,Nombre_Periodo,Fecha_Inicio,Fecha_Fin,Fecha_Cierre_Calif,Es_Periodo_Actual,Permite_Inscripciones,Id_Periodo)
    VALUES
    ('2024-I',  'PRIMER CUATRIMESTRE 2024', @IdTipoPeriodo_Cuatrimestre,
     '2024-01-08', '2024-04-30', '2024-05-15',
     0, 0,
     'INT-2024-I', 'Periodo historico 2024-I',
     @IdEstado_Inactivo, @IdEstado_Inactivo,
     0x00,
     GETDATE(), GETDATE(),
     @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL,
     NEWID()),

    ('2024-II', 'SEGUNDO CUATRIMESTRE 2024', @IdTipoPeriodo_Cuatrimestre,
     '2024-05-06', '2024-08-31', '2024-09-15',
     0, 0,
     'INT-2024-II', 'Periodo historico 2024-II',
     @IdEstado_Inactivo, @IdEstado_Inactivo,
     0x00,
     GETDATE(), GETDATE(),
     @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL,
     NEWID()),

    ('2024-III','TERCER CUATRIMESTRE 2024', @IdTipoPeriodo_Cuatrimestre,
     '2024-09-02', '2024-12-20', '2025-01-10',
     0, 0,
     'INT-2024-III', 'Periodo historico 2024-III',
     @IdEstado_Inactivo, @IdEstado_Inactivo,
     0x00,
     GETDATE(), GETDATE(),
     @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL,
     NEWID()),

    ('2025-I',  'PRIMER CUATRIMESTRE 2025', @IdTipoPeriodo_Cuatrimestre,
     '2025-01-13', '2025-04-30', '2025-05-15',
     0, 0,
     'INT-2025-I', 'Periodo historico 2025-I',
     @IdEstado_Inactivo, @IdEstado_Inactivo,
     0x00,
     GETDATE(), GETDATE(),
     @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL,
     NEWID()),

    ('2025-II', 'SEGUNDO CUATRIMESTRE 2025', @IdTipoPeriodo_Cuatrimestre,
     '2025-05-05', '2025-07-14', '2025-07-21',
     0, 0,
     'INT-2025-II', 'Periodo historico 2025-II (cierra 14 julio)',
     @IdEstado_Inactivo, @IdEstado_Inactivo,
     0x00,
     GETDATE(), GETDATE(),
     @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL,
     NEWID());

    ------------------------------------------------------------
    -- FASE 4: MATERIAS-PERIODOS
    ------------------------------------------------------------
    PRINT '';
    PRINT '=== FASE 4: CREANDO MATERIAS-PERIODOS ===';

    DECLARE @Materias TABLE(
        Id_Materia INT,
        Codigo_Materia VARCHAR(10)
    );

    INSERT INTO @Materias(Id_Materia, Codigo_Materia)
    SELECT Id_Materia, Codigo_Materia
    FROM cls_materias
    WHERE Codigo_Materia IN ('MAT-101','PROG-101','BD-101','RED-101','SO-101','ING-101');

    DECLARE @MateriasPeriodos TABLE(
        Id_Materia_Periodo   INT,
        Id_Materia           INT,
        Id_Periodo_Academico INT,
        Codigo_Materia       VARCHAR(10),
        Codigo_Periodo       VARCHAR(20)
    );

    DECLARE @IdPeriodo INT, @CodPeriodo VARCHAR(20);
    DECLARE curPer CURSOR FOR
        SELECT Id_Periodo, Codigo_Periodo FROM @Periodos;

    OPEN curPer;
    FETCH NEXT FROM curPer INTO @IdPeriodo, @CodPeriodo;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @Plan VARCHAR(30) = CASE WHEN LEFT(@CodPeriodo,4) = '2024' THEN 'PLAN-2024' ELSE 'PLAN-2025' END;

        INSERT INTO cls_materias_periodos(
            Id_Materia, Id_Periodo_Academico,
            Codigo_Plan, Id_Jornada, Modalidad,
            Horas_Teoricas, Horas_Practicas, 
            Porcentaje_Asistencia_Minima,
            Id_Estado, Fecha_Publicacion, Id_Usuario_Publicador,
            Observaciones,
            Fecha_Creacion, Fecha_Modificacion,
            Id_Creador, Id_Modificador, Id_Transaccion,
            Activo
        )
        OUTPUT INSERTED.Id_Materia_Periodo, INSERTED.Id_Materia, INSERTED.Id_Periodo_Academico
        INTO   @MateriasPeriodos(Id_Materia_Periodo, Id_Materia, Id_Periodo_Academico)
        SELECT TOP 4
            m.Id_Materia, @IdPeriodo,
            @Plan, @IdJornada_Matutina,
            CASE WHEN m.Codigo_Materia IN ('RED-101','SO-101') THEN 'HIBRIDO' ELSE 'PRESENCIAL' END,
            32, 
            CASE WHEN m.Codigo_Materia IN ('RED-101','SO-101') THEN 16 ELSE 8 END,
            75.00,
            @IdEstado_Activo,
            DATEADD(DAY, -7, (SELECT Fecha_Inicio FROM @Periodos WHERE Id_Periodo = @IdPeriodo)),
            @IdUsuario_AdminPrincipal,
            CONCAT('Configuracion ', m.Codigo_Materia, ' para ', @CodPeriodo),
            GETDATE(), GETDATE(),
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL,
            1
        FROM @Materias m
        ORDER BY m.Codigo_Materia;
        
        -- Actualizar Codigo_Materia y Codigo_Periodo en @MateriasPeriodos para los registros reci�n insertados
        UPDATE mp
        SET mp.Codigo_Materia = m.Codigo_Materia,
            mp.Codigo_Periodo = @CodPeriodo
        FROM @MateriasPeriodos mp
        INNER JOIN @Materias m ON mp.Id_Materia = m.Id_Materia
        WHERE mp.Id_Periodo_Academico = @IdPeriodo 
          AND (mp.Codigo_Periodo IS NULL OR mp.Codigo_Periodo = '');

        FETCH NEXT FROM curPer INTO @IdPeriodo, @CodPeriodo;
    END
    CLOSE curPer;
    DEALLOCATE curPer;


    ------------------------------------------------------------
    -- FASE 5: SECCIONES
    ------------------------------------------------------------
    PRINT '';
    PRINT '=== FASE 5: CREANDO SECCIONES ===';

    DECLARE @Secciones TABLE(
        Id_Seccion           INT,
        Id_Materia_Periodo   INT,
        Codigo_Seccion       VARCHAR(50),
        Codigo_Materia       VARCHAR(10),
        Codigo_Periodo       VARCHAR(20)
    );

    -- Tomamos algunos docentes
    DECLARE @Docentes TABLE(Id_Usuario INT);
    INSERT INTO @Docentes(Id_Usuario)
    SELECT u.Id_Usuario
    FROM @Usuarios u
    JOIN cls_usuarios_roles r ON r.Id_Usuario = u.Id_Usuario AND r.Id_Rol = @IdRol_Docente;

    DECLARE @IdMateriaPeriodo INT, @CodMat VARCHAR(10), @CodPer VARCHAR(20),
            @IdDocente INT, @RowNum INT = 0;

    DECLARE curMP CURSOR FOR
        SELECT Id_Materia_Periodo, Codigo_Materia, Codigo_Periodo
        FROM @MateriasPeriodos;

    OPEN curMP;
    FETCH NEXT FROM curMP INTO @IdMateriaPeriodo, @CodMat, @CodPer;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Docente pseudo-random
        SELECT TOP 1 @IdDocente = Id_Usuario 
        FROM (
            SELECT d.Id_Usuario, ROW_NUMBER() OVER(ORDER BY d.Id_Usuario) AS rn
            FROM @Docentes d
        ) x
        WHERE x.rn = ((@RowNum % (SELECT COUNT(*) FROM @Docentes)) + 1);

        SET @RowNum = @RowNum + 1;

        -- Formato compacto para Codigo_Seccion (m�ximo 20 caracteres)
        -- Ejemplo: SEC2024IMAT101A101 (19 chars) o SEC2024IMAT101L201 (19 chars)
        DECLARE @CodSec1 VARCHAR(20) = CONCAT('SEC', REPLACE(@CodPer,'-',''), REPLACE(@CodMat,'-',''), 'A101');
        DECLARE @CodSec2 VARCHAR(20) = CONCAT('SEC', REPLACE(@CodPer,'-',''), REPLACE(@CodMat,'-',''), 'L201');

        -- Seccion teorica
        INSERT INTO tbl_secciones(
            Codigo_Seccion, Id_Materia_Periodo, Id_Docente,
            Id_Tipo_Seccion, Id_Aula,
            Horario_Descripcion, Modalidad,
            Cupo_Maximo, Requiere_Asistencia, Porcentaje_Asistencia_Minima,
            Id_Estado, Id_Estado_Publicacion,
            Fecha_Publicacion, Fecha_Cierre,
            Codigo_Firma, Id_Usuario_Publicador, Observaciones,
            Fecha_Creacion, Fecha_Modificacion,
            Id_Creador, Id_Modificador, Id_Transaccion,
            Activo
        )
        OUTPUT INSERTED.Id_Seccion, INSERTED.Id_Materia_Periodo, INSERTED.Codigo_Seccion, @CodMat, @CodPer
        INTO   @Secciones(Id_Seccion, Id_Materia_Periodo, Codigo_Seccion, Codigo_Materia, Codigo_Periodo)
        SELECT
            @CodSec1, @IdMateriaPeriodo, @IdDocente,
            @IdTipoSeccion_Teorica, @IdAula_A101,
            'Lun-Mie-Vie 08:00-09:30', 'PRESENCIAL',
            30, 1, 70.00,
            @IdEstado_EnRevision, @IdEstado_Activo,
            DATEADD(DAY, -5, p.Fecha_Inicio),
            p.Fecha_Fin,
            CONCAT('F-', @CodSec1), @IdUsuario_AdminPrincipal,
            'Seccion teorica',
            GETDATE(), GETDATE(),
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL,
            1
        FROM @Periodos p WHERE p.Codigo_Periodo = @CodPer;

        -- Seccion practica / laboratorio
        INSERT INTO tbl_secciones(
            Codigo_Seccion, Id_Materia_Periodo, Id_Docente,
            Id_Tipo_Seccion, Id_Aula,
            Horario_Descripcion, Modalidad,
            Cupo_Maximo, Requiere_Asistencia, Porcentaje_Asistencia_Minima,
            Id_Estado, Id_Estado_Publicacion,
            Fecha_Publicacion, Fecha_Cierre,
            Codigo_Firma, Id_Usuario_Publicador, Observaciones,
            Fecha_Creacion, Fecha_Modificacion,
            Id_Creador, Id_Modificador, Id_Transaccion,
            Activo
        )
        OUTPUT INSERTED.Id_Seccion, INSERTED.Id_Materia_Periodo, INSERTED.Codigo_Seccion, @CodMat, @CodPer
        INTO   @Secciones(Id_Seccion, Id_Materia_Periodo, Codigo_Seccion, Codigo_Materia, Codigo_Periodo)
        SELECT
            @CodSec2, @IdMateriaPeriodo, @IdDocente,
            CASE WHEN @CodMat IN ('RED-101','SO-101') THEN @IdTipoSeccion_Lab ELSE @IdTipoSeccion_Practica END,
            CASE WHEN @CodMat IN ('RED-101','SO-101') THEN @IdAula_Lab201 ELSE @IdAula_A102 END,
            'Mar-Jue 10:00-11:30', 
            CASE WHEN @CodMat IN ('RED-101','SO-101') THEN 'HIBRIDO' ELSE 'PRESENCIAL' END,
            25, 1, 70.00,
            @IdEstado_EnRevision, @IdEstado_Activo,
            DATEADD(DAY, -5, p.Fecha_Inicio),
            p.Fecha_Fin,
            CONCAT('F-', @CodSec2), @IdUsuario_AdminPrincipal,
            'Seccion practica / lab',
            GETDATE(), GETDATE(),
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL,
            1
        FROM @Periodos p WHERE p.Codigo_Periodo = @CodPer;

        FETCH NEXT FROM curMP INTO @IdMateriaPeriodo, @CodMat, @CodPer;
    END
    CLOSE curMP;
    DEALLOCATE curMP;


    ------------------------------------------------------------
    -- FASE 6: GRUPOS
    ------------------------------------------------------------
    PRINT '';
    PRINT '=== FASE 6: CREANDO GRUPOS POR PERIODO ===';

    DECLARE @Grupos TABLE(
        Id_Grupo       INT,
        Codigo_Grupo   VARCHAR(20),
        Codigo_Periodo VARCHAR(20)
    );

    DECLARE @LetraGrupo CHAR(1);
    SET @LetraGrupo = 'A';

    DECLARE curPer2 CURSOR FOR
        SELECT Id_Periodo, Codigo_Periodo, Fecha_Inicio
        FROM @Periodos;

    DECLARE @FIni DATE;

    OPEN curPer2;
    FETCH NEXT FROM curPer2 INTO @IdPeriodo, @CodPeriodo, @FIni;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @CodGrupo VARCHAR(20) = CONCAT('GRP-', @CodPeriodo, '-', @LetraGrupo);

        INSERT INTO tbl_grupos(
            Codigo_Grupo, Nombre_Grupo,
            Id_Periodo, Id_Tipo_Grupo, Id_Coordinador,
            Id_Jornada, Id_Estado,
            Fecha_Cierre, Observaciones,
            Codigo_Seguimiento,
            Fecha_Creacion, Fecha_Modificacion,
            Id_Creador, Id_Modificador, Id_Transaccion,
            Activo
        )
        OUTPUT INSERTED.Id_Grupo, INSERTED.Codigo_Grupo, @CodPeriodo
        INTO   @Grupos(Id_Grupo, Codigo_Grupo, Codigo_Periodo)
        SELECT
            @CodGrupo,
            CONCAT('Grupo ', @LetraGrupo, ' ', @CodPeriodo),
            @IdPeriodo, @IdTipoGrupo_Academico,
            (SELECT TOP 1 u.Id_Usuario 
             FROM @Usuarios u JOIN cls_usuarios_roles r ON u.Id_Usuario = r.Id_Usuario AND r.Id_Rol = @IdRol_CoordAcademico
             ORDER BY u.Id_Usuario),
            NULL,
            @IdEstado_EnRevision,
            @FIni,
            'Grupo academico principal',
            CONCAT('SEG-', @CodPeriodo, '-', @LetraGrupo),
            GETDATE(), GETDATE(),
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL,
            1;

        FETCH NEXT FROM curPer2 INTO @IdPeriodo, @CodPeriodo, @FIni;
    END
    CLOSE curPer2;
    DEALLOCATE curPer2;


    ------------------------------------------------------------
    -- FASE 7: GRUPOS-SECCIONES
    ------------------------------------------------------------
    PRINT '';
    PRINT '=== FASE 7: VINCULANDO GRUPOS Y SECCIONES ===';

    INSERT INTO cls_grupos_secciones(
        Id_Grupo, Id_Seccion, Id_Tipo_Vinculo,
        Prioridad, Fecha_Asignacion, Fecha_Desasignacion, Motivo_Desasignacion,
        Fecha_Creacion, Fecha_Modificacion,
        Id_Creador, Id_Modificador, Id_Transaccion,
        Activo
    )
    SELECT 
        g.Id_Grupo, s.Id_Seccion, @IdTipoVinculo_Principal,
        1, p.Fecha_Inicio, NULL, NULL,
        GETDATE(), GETDATE(),
        @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL,
        1
    FROM @Secciones s
    JOIN @MateriasPeriodos mp ON s.Id_Materia_Periodo = mp.Id_Materia_Periodo
    JOIN @Periodos p ON mp.Id_Periodo_Academico = p.Id_Periodo
    JOIN @Grupos g ON g.Codigo_Periodo = p.Codigo_Periodo;


    ------------------------------------------------------------
    -- FASE 8: INSCRIPCIONES (1 por estudiante en toda su vida)
    ------------------------------------------------------------
    PRINT '';
    PRINT '=== FASE 8: CREANDO INSCRIPCIONES (1 POR ESTUDIANTE) ===';

    DECLARE @Inscripciones TABLE(
        Id_Inscripcion INT,
        Id_Estudiante  INT
    );

    DECLARE curEst2 CURSOR FOR
        SELECT u.Id_Usuario
        FROM @Usuarios u
        JOIN cls_usuarios_roles r ON r.Id_Usuario = u.Id_Usuario AND r.Id_Rol = @IdRol_Estudiante
        ORDER BY u.Id_Usuario;

    DECLARE @IdEstudiante INT;
    DECLARE @SeqIns INT = 1;
    DECLARE @CodIns VARCHAR(30);

    OPEN curEst2;
    FETCH NEXT FROM curEst2 INTO @IdEstudiante;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @CodIns = CONCAT('INS-2024-I-', RIGHT('000' + CAST(@SeqIns AS VARCHAR(3)),3));

        INSERT INTO tbl_inscripciones(
            Codigo_Inscripcion, Id_Estudiante, Id_Tipo_Inscripcion,
            Id_Estado,
            Fecha_Creacion, Fecha_Modificacion, Fecha_Validacion, Fecha_Retiro, Motivo_Retiro,
            Id_Creador, Id_Modificador, Id_Usuario_Validador, Id_Transaccion
        )
        OUTPUT INSERTED.Id_Inscripcion, INSERTED.Id_Estudiante
        INTO   @Inscripciones(Id_Inscripcion, Id_Estudiante)
        VALUES(
            @CodIns, @IdEstudiante, @IdTipoInscripcion_Regular,
            @IdEstado_EnRevision,
            DATEFROMPARTS(2024,01,02), DATEFROMPARTS(2024,01,02), NULL, NULL, NULL,
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, NULL
        );

        SET @SeqIns += 1;
        FETCH NEXT FROM curEst2 INTO @IdEstudiante;
    END
    CLOSE curEst2;
    DEALLOCATE curEst2;


    ------------------------------------------------------------
    -- FASE 9: GRUPOS-INSCRIPCIONES
    ------------------------------------------------------------
    PRINT '';
    PRINT '=== FASE 9: VINCULANDO GRUPOS E INSCRIPCIONES ===';

    -- Distribuimos estudiantes entre los 5 periodos (grupos A)
    DECLARE curGI CURSOR FOR
        SELECT i.Id_Inscripcion, i.Id_Estudiante
        FROM @Inscripciones i
        ORDER BY i.Id_Estudiante;

    DECLARE @IdxEst INT = 0;
    DECLARE @IdGrupo2024I INT, @IdGrupo2024II INT, @IdGrupo2024III INT, @IdGrupo2025I INT, @IdGrupo2025II INT;

    SELECT @IdGrupo2024I  = Id_Grupo FROM @Grupos WHERE Codigo_Periodo = '2024-I';
    SELECT @IdGrupo2024II = Id_Grupo FROM @Grupos WHERE Codigo_Periodo = '2024-II';
    SELECT @IdGrupo2024III= Id_Grupo FROM @Grupos WHERE Codigo_Periodo = '2024-III';
    SELECT @IdGrupo2025I  = Id_Grupo FROM @Grupos WHERE Codigo_Periodo = '2025-I';
    SELECT @IdGrupo2025II = Id_Grupo FROM @Grupos WHERE Codigo_Periodo = '2025-II';

    OPEN curGI;
    FETCH NEXT FROM curGI INTO @IdPeriodo, @IdEstudiante; -- reuse vars: @IdPeriodo = Id_Inscripcion
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @IdInsCurr INT = @IdPeriodo;
        DECLARE @IdGrupoDestino INT;
        DECLARE @FechaAsign DATE;

        IF @IdxEst < 6      BEGIN SET @IdGrupoDestino = @IdGrupo2024I;  SET @FechaAsign = '2024-01-08'; END
        ELSE IF @IdxEst < 12 BEGIN SET @IdGrupoDestino = @IdGrupo2024II; SET @FechaAsign = '2024-05-06'; END
        ELSE IF @IdxEst < 18 BEGIN SET @IdGrupoDestino = @IdGrupo2024III;SET @FechaAsign = '2024-09-02'; END
        ELSE IF @IdxEst < 24 BEGIN SET @IdGrupoDestino = @IdGrupo2025I;  SET @FechaAsign = '2025-01-13'; END
        ELSE                 BEGIN SET @IdGrupoDestino = @IdGrupo2025II; SET @FechaAsign = '2025-05-05'; END

        INSERT INTO tbl_grupos_inscripciones(
            Id_Grupo, Id_Inscripcion, Id_Rol_Grupo,
            Id_Estado,
            Fecha_Asignacion, Fecha_Baja, Motivo_Baja,
            Es_Delegado, Observaciones,
            Fecha_Creacion, Fecha_Modificacion,
            Id_Creador, Id_Modificador, Id_Transaccion,
            Activo
        )
        VALUES(
            @IdGrupoDestino, @IdInsCurr, NULL,
            @IdEstado_Activo,
            @FechaAsign, NULL, NULL,
            0, 'Asignacion inicial al grupo',
            GETDATE(), GETDATE(),
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL,
            1
        );

        SET @IdxEst += 1;
        FETCH NEXT FROM curGI INTO @IdPeriodo, @IdEstudiante;
    END
    CLOSE curGI;
    DEALLOCATE curGI;


    ------------------------------------------------------------
    -- FASE 10: MODELOS DE EVALUACION
    ------------------------------------------------------------
    PRINT '';
    PRINT '=== FASE 10: CREANDO MODELOS DE EVALUACION ===';

    DECLARE @ModelosEval TABLE(
        Id_Evaluacion_Modelo INT,
        Id_Materia_Periodo   INT,
        Codigo_Modelo        VARCHAR(50)
    );

    DECLARE curMP2 CURSOR FOR
        SELECT Id_Materia_Periodo, Codigo_Materia, Codigo_Periodo
        FROM @MateriasPeriodos;

    DECLARE @CodModelo VARCHAR(50);

    OPEN curMP2;
    FETCH NEXT FROM curMP2 INTO @IdMateriaPeriodo, @CodMat, @CodPer;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- 3 modelos: Parcial I (30%), Parcial II (30%), Examen Final (40%)
        DECLARE @OrdenModelo INT = 1;
        
        -- Parcial I
        SET @CodModelo = CONCAT('MOD-', REPLACE(@CodMat,'-',''), '-', REPLACE(@CodPer,'-',''), '-PAR1');
        INSERT INTO cls_evaluaciones_modelos(
            Id_Materia_Periodo, Id_Tipo_Evaluacion, Codigo_Modelo,
            Nombre_Evaluacion, Concepto,
            Calificacion_Maxima, Peso_Porcentual, Orden,
            Requiere_Aprobacion, Version_Configuracion,
            Id_Metodo_Calculo, Porcentaje_Minimo_Aprobacion,
            Niveles_Revision, Permite_Recalculo,
            Fecha_Inicio, Fecha_Fin,
            Fecha_Creacion, Fecha_Modificacion,
            Id_Creador, Id_Modificador, Id_Transaccion,
            Activo
        )
        OUTPUT INSERTED.Id_Evaluacion_Modelo, INSERTED.Id_Materia_Periodo, INSERTED.Codigo_Modelo
        INTO   @ModelosEval(Id_Evaluacion_Modelo, Id_Materia_Periodo, Codigo_Modelo)
        SELECT
            @IdMateriaPeriodo, @IdTipoEval_Escrita, @CodModelo,
            'Parcial I', 'Primer examen parcial del cuatrimestre',
            100.00, 30.00, @OrdenModelo,
            0, 1,
            @IdMetodoCalc_Ponderado, 60.00,
            1, 0,
            DATEADD(DAY, 30, p.Fecha_Inicio),
            DATEADD(DAY, 45, p.Fecha_Inicio),
            GETDATE(), GETDATE(),
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL,
            1
        FROM @Periodos p WHERE p.Codigo_Periodo = @CodPer;
        
        SET @OrdenModelo = 2;
        
        -- Parcial II
        SET @CodModelo = CONCAT('MOD-', REPLACE(@CodMat,'-',''), '-', REPLACE(@CodPer,'-',''), '-PAR2');
        INSERT INTO cls_evaluaciones_modelos(
            Id_Materia_Periodo, Id_Tipo_Evaluacion, Codigo_Modelo,
            Nombre_Evaluacion, Concepto,
            Calificacion_Maxima, Peso_Porcentual, Orden,
            Requiere_Aprobacion, Version_Configuracion,
            Id_Metodo_Calculo, Porcentaje_Minimo_Aprobacion,
            Niveles_Revision, Permite_Recalculo,
            Fecha_Inicio, Fecha_Fin,
            Fecha_Creacion, Fecha_Modificacion,
            Id_Creador, Id_Modificador, Id_Transaccion,
            Activo
        )
        OUTPUT INSERTED.Id_Evaluacion_Modelo, INSERTED.Id_Materia_Periodo, INSERTED.Codigo_Modelo
        INTO   @ModelosEval(Id_Evaluacion_Modelo, Id_Materia_Periodo, Codigo_Modelo)
        SELECT
            @IdMateriaPeriodo, @IdTipoEval_Escrita, @CodModelo,
            'Parcial II', 'Segundo examen parcial del cuatrimestre',
            100.00, 30.00, @OrdenModelo,
            0, 1,
            @IdMetodoCalc_Ponderado, 60.00,
            1, 0,
            DATEADD(DAY, 60, p.Fecha_Inicio),
            DATEADD(DAY, 75, p.Fecha_Inicio),
            GETDATE(), GETDATE(),
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL,
            1
        FROM @Periodos p WHERE p.Codigo_Periodo = @CodPer;
        
        SET @OrdenModelo = 3;
        
        -- Examen Final
        SET @CodModelo = CONCAT('MOD-', REPLACE(@CodMat,'-',''), '-', REPLACE(@CodPer,'-',''), '-FIN');
        INSERT INTO cls_evaluaciones_modelos(
            Id_Materia_Periodo, Id_Tipo_Evaluacion, Codigo_Modelo,
            Nombre_Evaluacion, Concepto,
            Calificacion_Maxima, Peso_Porcentual, Orden,
            Requiere_Aprobacion, Version_Configuracion,
            Id_Metodo_Calculo, Porcentaje_Minimo_Aprobacion,
            Niveles_Revision, Permite_Recalculo,
            Fecha_Inicio, Fecha_Fin,
            Fecha_Creacion, Fecha_Modificacion,
            Id_Creador, Id_Modificador, Id_Transaccion,
            Activo
        )
        OUTPUT INSERTED.Id_Evaluacion_Modelo, INSERTED.Id_Materia_Periodo, INSERTED.Codigo_Modelo
        INTO   @ModelosEval(Id_Evaluacion_Modelo, Id_Materia_Periodo, Codigo_Modelo)
        SELECT
            @IdMateriaPeriodo, @IdTipoEval_Escrita, @CodModelo,
            'Examen Final', 'Examen final del cuatrimestre',
            100.00, 40.00, @OrdenModelo,
            1, 1,
            @IdMetodoCalc_Ponderado, 60.00,
            2, 0,
            DATEADD(DAY, -7, p.Fecha_Fin),
            p.Fecha_Fin,
            GETDATE(), GETDATE(),
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL,
            1
        FROM @Periodos p WHERE p.Codigo_Periodo = @CodPer;

        FETCH NEXT FROM curMP2 INTO @IdMateriaPeriodo, @CodMat, @CodPer;
    END
    CLOSE curMP2;
    DEALLOCATE curMP2;


    ------------------------------------------------------------
    -- FASE 11: INSTANCIAS DE EVALUACION
    ------------------------------------------------------------
    PRINT '';
    PRINT '=== FASE 11: CREANDO INSTANCIAS DE EVALUACION ===';

    DECLARE @InstanciasEval TABLE(
        Id_Evaluacion_Instancia INT,
        Id_Seccion              INT,
        Id_Evaluacion_Modelo    INT,
        Codigo_Instancia        VARCHAR(50)
    );

    DECLARE @IdEvalModelo INT, @IdSeccion INT, @CodInst VARCHAR(50);

    DECLARE curInst CURSOR FOR
        SELECT me.Id_Evaluacion_Modelo, s.Id_Seccion, me.Codigo_Modelo, s.Codigo_Seccion
        FROM @ModelosEval me
        JOIN @MateriasPeriodos mp ON me.Id_Materia_Periodo = mp.Id_Materia_Periodo
        JOIN @Secciones s ON s.Id_Materia_Periodo = mp.Id_Materia_Periodo
        JOIN @Periodos p ON mp.Id_Periodo_Academico = p.Id_Periodo;

    OPEN curInst;
    FETCH NEXT FROM curInst INTO @IdEvalModelo, @IdSeccion, @CodModelo, @CodSec1;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Formato compacto para Codigo_Instancia (m�ximo 30 caracteres)
        -- Ejemplo: INST2024IMAT101A101PAR1 (24 chars)
        -- @CodSec1 ya es "SEC2024IMAT101A101", removemos "SEC" y tomamos los �ltimos caracteres del modelo
        DECLARE @CodSecSinPrefijo VARCHAR(20) = STUFF(@CodSec1, 1, 3, ''); -- Remueve "SEC"
        DECLARE @CodModeloSufijo VARCHAR(10) = RIGHT(REPLACE(REPLACE(@CodModelo,'MOD-',''),'-',''), 6); -- Ej: "PAR1" o "FIN"
        SET @CodInst = CONCAT('INST', @CodSecSinPrefijo, @CodModeloSufijo);
        
        -- Asegurar que no exceda 30 caracteres
        IF LEN(@CodInst) > 30
            SET @CodInst = LEFT(@CodInst, 30);

        INSERT INTO tbl_evaluaciones_instancias(
            Codigo_Instancia, Id_Seccion, Id_Evaluacion_Modelo, Id_Periodo,
            Fecha_Programada, Fecha_Limite,
            Requiere_Revision_Interna, Numero_Version, Nivel_Aprobacion_Actual,
            Id_Estado, Id_Estado_Publicacion,
            Id_Responsable_Revision, Fecha_Revision,
            Id_Responsable_Publicacion, Fecha_Publicacion,
            Id_Evaluacion_Padre, Hash_Instancia,
            Observaciones_Revision, Motivo_Rechazo,
            Fecha_Creacion, Fecha_Modificacion,
            Id_Creador, Id_Modificador, Id_Transaccion
        )
        OUTPUT INSERTED.Id_Evaluacion_Instancia, INSERTED.Id_Seccion, INSERTED.Id_Evaluacion_Modelo, INSERTED.Codigo_Instancia
        INTO   @InstanciasEval(Id_Evaluacion_Instancia, Id_Seccion, Id_Evaluacion_Modelo, Codigo_Instancia)
        SELECT
            @CodInst, @IdSeccion, @IdEvalModelo, p.Id_Periodo,
            CASE 
                WHEN me.Codigo_Modelo LIKE '%PAR1%' THEN DATEADD(DAY, 35, p.Fecha_Inicio)
                WHEN me.Codigo_Modelo LIKE '%PAR2%' THEN DATEADD(DAY, 65, p.Fecha_Inicio)
                WHEN me.Codigo_Modelo LIKE '%FIN%' THEN DATEADD(DAY, -5, p.Fecha_Fin)
            END,
            CASE 
                WHEN me.Codigo_Modelo LIKE '%PAR1%' THEN DATEADD(DAY, 40, p.Fecha_Inicio)
                WHEN me.Codigo_Modelo LIKE '%PAR2%' THEN DATEADD(DAY, 70, p.Fecha_Inicio)
                WHEN me.Codigo_Modelo LIKE '%FIN%' THEN p.Fecha_Fin
            END,
            CASE WHEN me.Codigo_Modelo LIKE '%FIN%' THEN 1 ELSE 0 END,
            1, 1,
            @IdEstado_Activo, @IdEstado_Activo,
            CASE WHEN me.Codigo_Modelo LIKE '%FIN%' THEN @IdUsuario_AdminPrincipal ELSE NULL END,
            CASE WHEN me.Codigo_Modelo LIKE '%FIN%' THEN DATEADD(DAY, -3, p.Fecha_Fin) ELSE NULL END,
            @IdUsuario_AdminPrincipal,
            DATEADD(DAY, -7, p.Fecha_Inicio),
            NULL, 0x00,
            NULL, NULL,
            GETDATE(), GETDATE(),
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL
        FROM @Periodos p
        JOIN @MateriasPeriodos mp ON p.Id_Periodo = mp.Id_Periodo_Academico
        JOIN @ModelosEval me ON me.Id_Materia_Periodo = mp.Id_Materia_Periodo
        WHERE me.Id_Evaluacion_Modelo = @IdEvalModelo;

        FETCH NEXT FROM curInst INTO @IdEvalModelo, @IdSeccion, @CodModelo, @CodSec1;
    END
    CLOSE curInst;
    DEALLOCATE curInst;


    ------------------------------------------------------------
    -- FASE 12: EVALUACIONES DE ALUMNOS
    ------------------------------------------------------------
    PRINT '';
    PRINT '=== FASE 12: CREANDO EVALUACIONES DE ALUMNOS ===';

    DECLARE @IdInstancia INT, @IdInscripcion INT, @Puntaje DECIMAL(8,2);
    DECLARE @SeqEval INT = 1;

    DECLARE curEvalAlum CURSOR FOR
        SELECT i.Id_Evaluacion_Instancia, i.Id_Seccion, gi.Id_Inscripcion, i.Codigo_Instancia
        FROM @InstanciasEval i
        JOIN @Secciones s ON i.Id_Seccion = s.Id_Seccion
        JOIN cls_grupos_secciones gs ON s.Id_Seccion = gs.Id_Seccion AND gs.Activo = 1
        JOIN tbl_grupos_inscripciones gi ON gs.Id_Grupo = gi.Id_Grupo AND gi.Activo = 1 AND gi.Id_Estado = @IdEstado_Activo
        JOIN @Inscripciones ins ON gi.Id_Inscripcion = ins.Id_Inscripcion;

    OPEN curEvalAlum;
    FETCH NEXT FROM curEvalAlum INTO @IdInstancia, @IdSeccion, @IdInscripcion, @CodInst;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Puntaje aleatorio entre 60 y 95
        SET @Puntaje = 60.00 + (RAND(CHECKSUM(NEWID())) * 35.00);
        
        -- Formato compacto para Codigo_Registro (m�ximo 30 caracteres)
        -- Ejemplo: EVAL2024IMAT101A101PAR1001 (26 chars)
        DECLARE @CodRegistro VARCHAR(30);
        DECLARE @CodInstSinPrefijo VARCHAR(25) = REPLACE(@CodInst, 'INST', ''); -- Remueve "INST"
        SET @CodRegistro = CONCAT('EVAL', @CodInstSinPrefijo, RIGHT('000' + CAST(@SeqEval AS VARCHAR(3)), 3));
        
        -- Asegurar que no exceda 30 caracteres
        IF LEN(@CodRegistro) > 30
            SET @CodRegistro = LEFT(@CodRegistro, 30);
        
        -- Obtener docente de la secci�n
        DECLARE @IdDocenteEval INT;
        SELECT @IdDocenteEval = Id_Docente FROM tbl_secciones WHERE Id_Seccion = @IdSeccion;

        INSERT INTO tbl_evaluaciones_alumnos(
            Codigo_Registro, Id_Evaluacion_Instancia, Id_Inscripcion,
            Puntaje_Obtenido, Porcentaje_Logrado, Puntaje_Normalizado,
            Es_Recalculo, Numero_Recalculo, Motivo_Ajuste, Observaciones,
            Id_Usuario_Evaluador, Id_Usuario_Validador, Fecha_Validacion,
            Id_Estado, Id_Estado_Publicacion,
            Hash_Resultado, Id_Evaluacion_Reemplazada,
            Firmado_Por_Estudiante, Firma_Digital,
            Fecha_Notificacion, Fecha_Publicacion,
            Fecha_Creacion, Fecha_Modificacion,
            Id_Creador, Id_Modificador, Id_Transaccion
        )
        SELECT
            @CodRegistro, @IdInstancia, @IdInscripcion,
            @Puntaje, @Puntaje, @Puntaje / 100.00,
            0, 0, NULL, 'Evaluacion regular',
            @IdDocenteEval, @IdDocenteEval, GETDATE(),
            @IdEstado_Activo, @IdEstado_Activo,
            0x00, NULL,
            0, NULL,
            GETDATE(), GETDATE(),
            GETDATE(), GETDATE(),
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL;

        SET @SeqEval += 1;
        FETCH NEXT FROM curEvalAlum INTO @IdInstancia, @IdSeccion, @IdInscripcion, @CodInst;
    END
    CLOSE curEvalAlum;
    DEALLOCATE curEvalAlum;


    ------------------------------------------------------------
    -- FASE 13: SANCIONES ACADEMICAS
    ------------------------------------------------------------
    PRINT '';
    PRINT '=== FASE 13: CREANDO SANCIONES ACADEMICAS ===';

    DECLARE @IdSeveridad_Leve INT, @IdSeveridad_Grave INT;
    SELECT @IdSeveridad_Leve = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 7 AND Nombre_Catalogo = 'LEVE';
    SELECT @IdSeveridad_Grave = Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 7 AND Nombre_Catalogo = 'GRAVE';

    DECLARE @SeqSancion INT = 1;
    DECLARE @IdEstudianteSancion INT;
    DECLARE @FechaSancion DATE;

    -- Seleccionar 5 estudiantes aleatorios para sanciones
    DECLARE curSancion CURSOR FOR
        SELECT TOP 5 u.Id_Usuario
        FROM @Usuarios u
        JOIN cls_usuarios_roles r ON r.Id_Usuario = u.Id_Usuario AND r.Id_Rol = @IdRol_Estudiante
        ORDER BY NEWID();

    OPEN curSancion;
    FETCH NEXT FROM curSancion INTO @IdEstudianteSancion;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @FechaSancion = DATEFROMPARTS(2024, 3 + (@SeqSancion % 3), 10 + (@SeqSancion % 15));
        DECLARE @CodSancion VARCHAR(30) = CONCAT('SAN-2024-', RIGHT('000' + CAST(@SeqSancion AS VARCHAR(3)), 3));
        
        DECLARE @TipoSancion INT = CASE WHEN @SeqSancion % 2 = 0 THEN @IdTipoSancion_Plagio ELSE @IdTipoSancion_Copia END;
        DECLARE @Severidad INT = CASE WHEN @SeqSancion % 3 = 0 THEN @IdSeveridad_Grave ELSE @IdSeveridad_Leve END;
        DECLARE @TipoFalta INT = CASE WHEN @SeqSancion % 2 = 0 THEN @IdTipoFalta_Academica ELSE @IdTipoFalta_Disciplinaria END;

        INSERT INTO tbl_sanciones_academicas(
            Codigo_Sancion, Id_Estudiante, Id_Tipo_Sancion, Id_Tipo_Falta, Id_Severidad,
            Id_Estado, Fecha_Registro, Fecha_Fin, Motivo,
            Es_Apelable, Fecha_Apelacion, Resultado_Apelacion, Observaciones_Apelacion,
            Documento_Resolucion, Id_Usuario_Resolucion, Fecha_Resolucion,
            Id_Sancion_Origen, Hash_Sancion,
            Fecha_Creacion, Fecha_Modificacion,
            Id_Creador, Id_Modificador, Id_Transaccion,
            Codigo_Control
        )
        VALUES(
            @CodSancion, @IdEstudianteSancion, @TipoSancion, @TipoFalta, @Severidad,
            CASE WHEN @SeqSancion % 3 = 0 THEN @IdEstado_Inactivo ELSE @IdEstado_Activo END,
            @FechaSancion, 
            CASE WHEN @SeqSancion % 3 = 0 THEN DATEADD(DAY, 30, @FechaSancion) ELSE NULL END,
            CASE 
                WHEN @TipoSancion = @IdTipoSancion_Plagio THEN 'Plagio detectado en trabajo final'
                WHEN @TipoSancion = @IdTipoSancion_Copia THEN 'Copia durante examen parcial'
                ELSE 'Inasistencias excesivas'
            END,
            CASE WHEN @Severidad = @IdSeveridad_Grave THEN 1 ELSE 0 END,
            NULL, NULL, NULL,
            NULL, @IdUsuario_AdminPrincipal, NULL,
            NULL, 0x00,
            GETDATE(), GETDATE(),
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL,
            NEWID()
        );

        SET @SeqSancion += 1;
        FETCH NEXT FROM curSancion INTO @IdEstudianteSancion;
    END
    CLOSE curSancion;
    DEALLOCATE curSancion;


    ------------------------------------------------------------
    -- FASE 14: PROGRAMAS DE BECAS
    ------------------------------------------------------------
    PRINT '';
    PRINT '=== FASE 14: CREANDO PROGRAMAS DE BECAS ===';

    DECLARE @ProgramasBecas TABLE(
        Id_Beca_Programa INT,
        Codigo_Programa   VARCHAR(30)
    );

    INSERT INTO cls_becas_programas(
        Codigo_Programa, Nombre_Programa, Descripcion,
        Id_Tipo_Programa, Id_Modalidad_Programa,
        Monto_Maximo, Id_Moneda,
        Promedio_Minimo, Requiere_Sin_Sanciones,
        Id_Estado_Programa,
        Fecha_Creacion, Fecha_Modificacion,
        Id_Creador, Id_Modificador, Id_Transaccion,
        Codigo_Control
    )
    OUTPUT INSERTED.Id_Beca_Programa, INSERTED.Codigo_Programa
    INTO   @ProgramasBecas(Id_Beca_Programa, Codigo_Programa)
    VALUES
    ('BECA-EXCELENCIA-2024', 'Beca de Excelencia Acad�mica 2024',
     'Programa de becas para estudiantes con excelente rendimiento acad�mico',
     @IdTipoProg_Beca, 
     (SELECT Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 11 AND Nombre_Catalogo = 'PRESENCIAL'),
     5000.00, @IdMoneda_Cordoba,
     90.00,
     1,
     @IdEstado_Activo,
     GETDATE(), GETDATE(),
     @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL,
     NEWID()),
    ('BECA-MERITO-2024', 'Beca por M�rito Acad�mico 2024',
     'Programa de becas para estudiantes con buen promedio y sin sanciones',
     @IdTipoProg_Beca,
     (SELECT Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 11 AND Nombre_Catalogo = 'PRESENCIAL'),
     3000.00, @IdMoneda_Cordoba,
     85.00,
     1,
     @IdEstado_Activo,
     GETDATE(), GETDATE(),
     @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL,
     NEWID());


    ------------------------------------------------------------
    -- FASE 15: CRITERIOS DE BECAS
    ------------------------------------------------------------
    PRINT '';
    PRINT '=== FASE 15: CREANDO CRITERIOS DE BECAS ===';

    DECLARE @IdProgBeca INT, @CodProg VARCHAR(30);
    DECLARE curProg CURSOR FOR
        SELECT Id_Beca_Programa, Codigo_Programa FROM @ProgramasBecas;

    OPEN curProg;
    FETCH NEXT FROM curProg INTO @IdProgBeca, @CodProg;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Criterio: Promedio m�nimo
        INSERT INTO cls_becas_criterios(
            Id_Programa, Codigo, Nombre_Criterio, Clave_Criterio, Valor_Criterio,
            Tipo_Dato_Valor, Id_Tipo_Criterio, Operador_Comparacion,
            Valor_Numerico_Minimo, Valor_Numerico_Maximo, Valor_Texto, Valor_Booleano,
            Peso_Criterio, Prioridad, Es_Excluyente,
            Fuente_Validacion, Expresion_Validacion, Requiere_Soporte,
            Fecha_Creacion, Fecha_Modificacion,
            Id_Creador, Id_Modificador, Activo
        )
        SELECT
            @IdProgBeca, 'CRIT-PROMEDIO', 'Promedio M�nimo Requerido', 'PROMEDIO_MIN',
            CAST(Valor AS VARCHAR(255)),
            'DECIMAL', @IdTipoCriterio_Promedio, '>=',
            Valor, NULL, NULL, NULL,
            50.00, 1, 1,
            'tbl_evaluaciones_alumnos', 'Promedio >= Valor_Numerico_Minimo', 0,
            GETDATE(), GETDATE(),
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, 1
        FROM (
            SELECT CASE WHEN @CodProg LIKE '%EXCELENCIA%' THEN 90.00 ELSE 85.00 END AS Valor
        ) x;

        -- Criterio: Sin sanciones activas
        INSERT INTO cls_becas_criterios(
            Id_Programa, Codigo, Nombre_Criterio, Clave_Criterio, Valor_Criterio,
            Tipo_Dato_Valor, Id_Tipo_Criterio, Operador_Comparacion,
            Valor_Numerico_Minimo, Valor_Numerico_Maximo, Valor_Texto, Valor_Booleano,
            Peso_Criterio, Prioridad, Es_Excluyente,
            Fuente_Validacion, Expresion_Validacion, Requiere_Soporte,
            Fecha_Creacion, Fecha_Modificacion,
            Id_Creador, Id_Modificador, Activo
        )
        VALUES(
            @IdProgBeca, 'CRIT-SANSANCIONES', 'Sin Sanciones Activas', 'SIN_SANCIONES',
            '0',
            'INT', @IdTipoCriterio_Sanciones, '=',
            NULL, NULL, NULL, 1,
            30.00, 2, 1,
            'tbl_sanciones_academicas', 'Total_Sanciones_Activas = 0', 0,
            GETDATE(), GETDATE(),
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, 1
        );

        -- Criterio: Cr�ditos m�nimos
        INSERT INTO cls_becas_criterios(
            Id_Programa, Codigo, Nombre_Criterio, Clave_Criterio, Valor_Criterio,
            Tipo_Dato_Valor, Id_Tipo_Criterio, Operador_Comparacion,
            Valor_Numerico_Minimo, Valor_Numerico_Maximo, Valor_Texto, Valor_Booleano,
            Peso_Criterio, Prioridad, Es_Excluyente,
            Fuente_Validacion, Expresion_Validacion, Requiere_Soporte,
            Fecha_Creacion, Fecha_Modificacion,
            Id_Creador, Id_Modificador, Activo
        )
        SELECT
            @IdProgBeca, 'CRIT-CREDITOS', 'Cr�ditos M�nimos Aprobados', 'CREDITOS_MIN',
            CAST(Valor AS VARCHAR(255)),
            'INT', @IdTipoCriterio_Creditos, '>=',
            Valor, NULL, NULL, NULL,
            20.00, 3, 0,
            'tbl_evaluaciones_alumnos', 'Creditos_Aprobados >= Valor_Numerico_Minimo', 0,
            GETDATE(), GETDATE(),
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, 1
        FROM (
            SELECT CASE WHEN @CodProg LIKE '%EXCELENCIA%' THEN 16 ELSE 12 END AS Valor
        ) x;

        FETCH NEXT FROM curProg INTO @IdProgBeca, @CodProg;
    END
    CLOSE curProg;
    DEALLOCATE curProg;


    ------------------------------------------------------------
    -- FASE 16: CONVOCATORIAS DE BECAS
    ------------------------------------------------------------
    PRINT '';
    PRINT '=== FASE 16: CREANDO CONVOCATORIAS DE BECAS ===';

    DECLARE @Convocatorias TABLE(
        Id_Convocatoria INT,
        Id_Programa      INT,
        Id_Periodo       INT,
        Codigo_Convocatoria VARCHAR(30)
    );

    DECLARE curConv CURSOR FOR
        SELECT pb.Id_Beca_Programa, p.Id_Periodo, p.Codigo_Periodo, pb.Codigo_Programa
        FROM @ProgramasBecas pb
        CROSS JOIN @Periodos p
        WHERE p.Codigo_Periodo IN ('2024-I', '2024-II', '2025-I');

    OPEN curConv;
    FETCH NEXT FROM curConv INTO @IdProgBeca, @IdPeriodo, @CodPeriodo, @CodProg;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @CodConv VARCHAR(30) = CONCAT('CONV-', @CodPeriodo, '-', REPLACE(@CodProg, 'BECA-', ''));

        INSERT INTO tbl_becas_convocatorias(
            Codigo_Convocatoria, Id_Programa, Id_Periodo,
            Nombre_Convocatoria, Descripcion,
            Cupo_Total, Cupo_Reservado, Cupo_Asignado,
            Fecha_Inicio, Fecha_Publicacion, Fecha_Fin, Fecha_Cierre,
            Requiere_Postulacion_Linea, Documentacion_Obligatoria,
            Url_Convocatoria, Observaciones,
            Id_Estado, Id_Estado_Publicacion,
            Id_Creador, Id_Modificador, Id_Usuario_Publicador, Id_Usuario_Cierre, Id_Transaccion,
            Hash_Convocatoria, Codigo_Control,
            Fecha_Creacion, Fecha_Modificacion
        )
        OUTPUT INSERTED.Id_Convocatoria, INSERTED.Id_Programa, INSERTED.Id_Periodo, INSERTED.Codigo_Convocatoria
        INTO   @Convocatorias(Id_Convocatoria, Id_Programa, Id_Periodo, Codigo_Convocatoria)
        SELECT
            @CodConv, @IdProgBeca, @IdPeriodo,
            CONCAT('Convocatoria ', REPLACE(@CodProg, 'BECA-', ''), ' ', @CodPeriodo),
            CONCAT('Convocatoria para el periodo ', @CodPeriodo),
            CASE WHEN @CodProg LIKE '%EXCELENCIA%' THEN 5 ELSE 10 END,
            0, 0,
            DATEADD(DAY, -14, p.Fecha_Inicio),
            DATEADD(DAY, -14, p.Fecha_Inicio),
            DATEADD(DAY, 7, p.Fecha_Inicio),
            DATEADD(DAY, 14, p.Fecha_Inicio),
            1, 'Cedula, Record academico, Carta de compromiso',
            NULL, 'Convocatoria activa',
            @IdEstado_Activo, @IdEstado_Activo,
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL, NULL,
            0x00, NEWID(),
            GETDATE(), GETDATE()
        FROM @Periodos p WHERE p.Id_Periodo = @IdPeriodo;

        FETCH NEXT FROM curConv INTO @IdProgBeca, @IdPeriodo, @CodPeriodo, @CodProg;
    END
    CLOSE curConv;
    DEALLOCATE curConv;


    ------------------------------------------------------------
    -- FASE 17: SOLICITUDES DE BECAS
    ------------------------------------------------------------
    PRINT '';
    PRINT '=== FASE 17: CREANDO SOLICITUDES DE BECAS ===';

    DECLARE @SeqSol INT = 1;
    DECLARE @IdConvocatoria INT, @IdEstudianteSol INT;
    DECLARE @PromedioVigente DECIMAL(5,2);

    -- Seleccionar estudiantes elegibles (sin sanciones activas)
    DECLARE curSol CURSOR FOR
        SELECT TOP 8 u.Id_Usuario
        FROM @Usuarios u
        JOIN cls_usuarios_roles r ON r.Id_Usuario = u.Id_Usuario AND r.Id_Rol = @IdRol_Estudiante
        WHERE NOT EXISTS (
            SELECT 1 FROM tbl_sanciones_academicas sa
            WHERE sa.Id_Estudiante = u.Id_Usuario AND sa.Id_Estado = @IdEstado_Activo
        )
        ORDER BY NEWID();

    OPEN curSol;
    FETCH NEXT FROM curSol INTO @IdEstudianteSol;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Seleccionar convocatoria aleatoria
        SELECT TOP 1 @IdConvocatoria = c.Id_Convocatoria, @IdProgBeca = c.Id_Programa
        FROM @Convocatorias c
        ORDER BY NEWID();

        -- Calcular promedio simulado (85-95)
        SET @PromedioVigente = 85.00 + (RAND(CHECKSUM(NEWID())) * 10.00);

        DECLARE @CodSegSol VARCHAR(30) = CONCAT('SOL-BECA-2024-', RIGHT('000' + CAST(@SeqSol AS VARCHAR(3)), 3));

        INSERT INTO tbl_solicitudes_becas(
            Codigo_Seguimiento, Id_Beca_Programa, Id_Convocatoria,
            Id_Estudiante, Promedio_Vigente, Creditos_Aprobados,
            Total_Sanciones_Activas, Cumple_Criterios,
            Nivel_Aprobacion_Actual, Nivel_Aprobacion_Maximo,
            Id_Usuario_Responsable, Id_Usuario_Supervisor, Id_Tipo_Decision,
            Id_Estado, Id_Estado_Flujo,
            Fecha_Solicitud, Fecha_Ultima_Decision, Fecha_Cierre,
            Motivo_Ultima_Decision, Observaciones,
            Hash_Solicitud, Codigo_Control,
            Fecha_Creacion, Fecha_Modificacion,
            Id_Creador, Id_Modificador, Id_Transaccion,
            Es_Prioritaria
        )
        SELECT
            @CodSegSol, @IdProgBeca, @IdConvocatoria,
            @IdEstudianteSol, @PromedioVigente, 16,
            0, CASE WHEN @PromedioVigente >= 85.00 THEN 1 ELSE 0 END,
            1, 
            CASE WHEN @CodProg LIKE '%EXCELENCIA%' THEN 2 ELSE 1 END,
            (SELECT TOP 1 Id_Usuario FROM @Usuarios WHERE Alias = 'COORDBECAS1'),
            NULL,
            (SELECT Id_Catalogo FROM cls_catalogos WHERE Id_Tipo_Catalogo = 23 AND Nombre_Catalogo = 'PENDIENTE'),
            CASE WHEN @SeqSol % 3 = 0 THEN @IdEstado_Aprobada 
                 WHEN @SeqSol % 3 = 1 THEN @IdEstado_EnRevision 
                 ELSE @IdEstado_Pendiente END,
            @IdEstado_Activo,
            DATEADD(DAY, -10, p.Fecha_Inicio),
            CASE WHEN @SeqSol % 3 = 0 THEN DATEADD(DAY, -5, p.Fecha_Inicio) ELSE NULL END,
            NULL,
            CASE WHEN @SeqSol % 3 = 0 THEN 'Solicitud aprobada por cumplir criterios' ELSE NULL END,
            'Solicitud de beca academica',
            0x00, NEWID(),
            GETDATE(), GETDATE(),
            @IdUsuario_AdminPrincipal, @IdUsuario_AdminPrincipal, NULL,
            0
        FROM @Convocatorias c
        JOIN @Periodos p ON c.Id_Periodo = p.Id_Periodo
        WHERE c.Id_Convocatoria = @IdConvocatoria;

        SET @SeqSol += 1;
        FETCH NEXT FROM curSol INTO @IdEstudianteSol;
    END
    CLOSE curSol;
    DEALLOCATE curSol;


    ------------------------------------------------------------
    -- FINALIZACION
    ------------------------------------------------------------
    PRINT '';
    PRINT '============================================';
    PRINT 'CARGA DE DATOS COMPLETADA EXITOSAMENTE';
    PRINT '============================================';
    PRINT '';
    
    -- Calcular conteos en variables escalares
    DECLARE @CountSecciones INT, @CountGrupos INT, @CountInscripciones INT;
    DECLARE @CountModelosEval INT, @CountInstanciasEval INT;
    DECLARE @CountProgramasBecas INT, @CountConvocatorias INT;
    
    SELECT @CountSecciones = COUNT(*) FROM @Secciones;
    SELECT @CountGrupos = COUNT(*) FROM @Grupos;
    SELECT @CountInscripciones = COUNT(*) FROM @Inscripciones;
    SELECT @CountModelosEval = COUNT(*) FROM @ModelosEval;
    SELECT @CountInstanciasEval = COUNT(*) FROM @InstanciasEval;
    SELECT @CountProgramasBecas = COUNT(*) FROM @ProgramasBecas;
    SELECT @CountConvocatorias = COUNT(*) FROM @Convocatorias;
    
    PRINT 'Resumen:';
    PRINT '- Per�odos acad�micos: 5 (2024-I a 2025-II)';
    PRINT '- Materias: 12';
    PRINT '- Secciones: ' + CAST(@CountSecciones AS VARCHAR(10));
    PRINT '- Grupos: ' + CAST(@CountGrupos AS VARCHAR(10));
    PRINT '- Inscripciones: ' + CAST(@CountInscripciones AS VARCHAR(10));
    PRINT '- Modelos de evaluaci�n: ' + CAST(@CountModelosEval AS VARCHAR(10));
    PRINT '- Instancias de evaluaci�n: ' + CAST(@CountInstanciasEval AS VARCHAR(10));
    PRINT '- Programas de becas: ' + CAST(@CountProgramasBecas AS VARCHAR(10));
    PRINT '- Convocatorias: ' + CAST(@CountConvocatorias AS VARCHAR(10));
    PRINT '- Solicitudes de becas: ' + CAST(@SeqSol - 1 AS VARCHAR(10));
    PRINT '';

    COMMIT TRAN;
    PRINT 'Transaccion completada exitosamente.';

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN;
    
    DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrorLine INT = ERROR_LINE();
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
    DECLARE @ErrorState INT = ERROR_STATE();
    
    PRINT '';
    PRINT '============================================';
    PRINT 'ERROR EN LA CARGA DE DATOS';
    PRINT '============================================';
    PRINT 'Mensaje: ' + @ErrorMsg;
    PRINT 'Linea: ' + CAST(@ErrorLine AS VARCHAR(10));
    PRINT 'Severidad: ' + CAST(@ErrorSeverity AS VARCHAR(10));
    PRINT 'Estado: ' + CAST(@ErrorState AS VARCHAR(10));
    PRINT '';
    PRINT 'Transaccion revertida.';
    
    THROW;
END CATCH;
GO
