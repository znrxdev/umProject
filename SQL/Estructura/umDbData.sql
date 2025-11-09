USE umDb
GO


INSERT INTO cls_estados (Nombre_Estado, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,Id_Transaccion, Activo) VALUES
('ACTIVO',GETDATE(),GETDATE(),NULL,NULL,NULL,1), 
('INACTIVO',GETDATE(),GETDATE(),NULL,NULL,NULL,1), 
('PENDIENTE',GETDATE(),GETDATE(),NULL,NULL,NULL,1),
('EN REVISION',GETDATE(),GETDATE(),NULL,NULL,NULL,1),
('APROBADA', GETDATE(),GETDATE(),NULL,NULL,NULL,1),
('RECHAZADA', GETDATE(), GETDATE(),NULL,NULL,NULL,1),
('PLANIFICADA', GETDATE(), GETDATE(),NULL,NULL,NULL,1);

INSERT INTO cls_tipos_catalogos  (Nombre_Tipo_Catalogo, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,Id_Transaccion, Activo) VALUES 
('TIPO DOCUMENTO' , GETDATE(), GETDATE(), NULL,NULL,NULL,1),   --1
('GENERO PERSONA' , GETDATE(), GETDATE(), NULL,NULL,NULL,1),   --2
('NACIONALIDAD' , GETDATE(), GETDATE(), NULL,NULL,NULL,1),	   --3
('ESTADO CIVIL' , GETDATE(), GETDATE(), NULL,NULL,NULL,1),	   --4
('TIPO PROGRAMA', GETDATE(), GETDATE(), NULL, NULL,NULL,1),	   --5
('TIPO SANCION', GETDATE(), GETDATE(), NULL, NULL,NULL,1),	   --6
('TIPO SEVERIDAD', GETDATE(), GETDATE(),NULL,NULL,NULL,1),	   --7
('TIPO EVALUACION', GETDATE(), GETDATE(),NULL,NULL,NULL,1); --8


INSERT INTO cls_tipos_catalogos  (Nombre_Tipo_Catalogo, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,Id_Transaccion, Activo) VALUES 
('TIPO CONTACTO', GETDATE(), GETDATE(), NULL, NULL,NULL,1); -- 9

INSERT INTO cls_catalogos (Id_Tipo_Catalogo, Nombre_Catalogo, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,Id_Transaccion, Activo) VALUES
(1, 'CEDULA NICARAGUENSE', GETDATE(), GETDATE(), NULL, NULL,NULL,1),  
(2, 'MASCULINO', GETDATE(), GETDATE(), NULL, NULL,NULL,1),
(2, 'FEMENINO', GETDATE(), GETDATE(), NULL, NULL,NULL,1),
(2, 'OTRO', GETDATE(),GETDATE(),NULL,NULL,NULL,1),
(2,'PREFIERO NO DECIRLO',GETDATE(),GETDATE(),NULL,NULL,NULL,1),
(3, 'NICARAGUENSE', GETDATE(), GETDATE(), NULL, NULL,NULL,1),
(3, 'COSTARRICENSE', GETDATE(), GETDATE(), NULL, NULL,NULL,1),
(4, 'SOLTERO/A', GETDATE(), GETDATE(), NULL, NULL,NULL, 1),
(4, 'CASADO/A', GETDATE(), GETDATE(), NULL, NULL,NULL, 1),
(4, 'VIUDO/A', GETDATE(), GETDATE(), NULL, NULL,NULL, 1),
(4, 'DIVORCIADO/A', GETDATE(), GETDATE(), NULL, NULL,NULL,1),
(4, 'SEPARADO/A', GETDATE(), GETDATE(), NULL, NULL,NULL, 1),
(4, 'UNION CIVIL', GETDATE(), GETDATE(), NULL, NULL,NULL, 1),
(5, 'BECA', GETDATE(), GETDATE(), NULL, NULL,NULL,1),
(5, 'MAESTRIA', GETDATE(), GETDATE(), NULL, NULL,NULL,1),
(5, 'POSGRADO', GETDATE(), GETDATE(), NULL, NULL,NULL,1),
(6, 'PLAGIO', GETDATE(), GETDATE(), NULL, NULL,NULL,1),
(6, 'COPIA EN EXAMEN', GETDATE(), GETDATE(), NULL, NULL,NULL,1),
(6, 'SUPLANTACION', GETDATE(), GETDATE(), NULL, NULL,NULL,1),
(6, 'INASISTENCIAS',GETDATE(),GETDATE(),NULL,NULL,NULL,1),
(6, 'ACOSO',GETDATE(),GETDATE(),NULL,NULL,NULL,1),
(6, 'VIOLENCIA',GETDATE(),GETDATE(),NULL,NULL,NULL,1),
(6, 'USO INDEBIDO DE SISTEMAS',GETDATE(),GETDATE(),NULL,NULL,NULL,1),
(6, 'SABOTAJE TI',GETDATE(),GETDATE(),NULL,NULL,NULL,1),
(7, 'LEVE', GETDATE(), GETDATE(),NULL,NULL,NULL,1),
(7, 'GRAVE', GETDATE(), GETDATE(),NULL,NULL,NULL,1),
(7, 'MUY GRAVE', GETDATE(), GETDATE(),NULL,NULL,NULL,1),
(8, 'ESCRITA', GETDATE(), GETDATE(),NULL,NULL,NULL,1),
(8, 'SELECCION MULTIPLE', GETDATE(), GETDATE(),NULL,NULL,NULL,1),
(8, 'DEFENSA', GETDATE(), GETDATE(),NULL,NULL,NULL,1),
(8, 'CALIFICACION EN LINEA',GETDATE(),GETDATE(),NULL,NULL,NULL,1),
(8, 'MERITO DOCENTE',GETDATE(),GETDATE(),NULL,NULL,NULL,1);

INSERT INTO cls_catalogos (Id_Tipo_Catalogo, Nombre_Catalogo, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,Id_Transaccion, Activo) VALUES
(9, 'CORREO ELECTRONICO', GETDATE(), GETDATE(), NULL, NULL,NULL,1),
(9, 'TELEFONO MOVIL', GETDATE(), GETDATE(), NULL, NULL,NULL,1),
(9, 'TELEFONO FIJO', GETDATE(), GETDATE(), NULL, NULL,NULL,1);

INSERT INTO tbl_personas(Primer_Nombre,Segundo_Nombre,Primer_Apellido,Segundo_Apellido,Fecha_Nacimiento,Id_Tipo_Documento,Valor_Documento,Id_Genero_Persona,Id_Nacionalidad,Id_Estado_Civil,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Id_Estado)
VALUES ('JUSTIN','ZAHIR','CALDERON','JOYA','2004-08-14',1,'0011408041050U',2,6,8,GETDATE(),GETDATE(),NULL,NULL,NULL,1)

INSERT INTO tbl_usuarios (Usuario,Contrasena,Fecha_Creacion,Fecha_Modificacion,Ultima_Sesion,Ultimo_Cambio_Contrasena,Id_Creador,Id_Modificador,Id_Transaccion,Id_Estado)
VALUES ('znr','$2a$11$lrqmyt.z2dgs1mnBp8InyuT.XJvS49jlTN.iPbGi.VYVlYYfWl22W',GETDATE(),GETDATE(),NULL,NULL,NULL,NULL,NULL,1)

-- ----------------------------------------------------------------
-- INSERTS MASIVOS: tbl_personas + tbl_usuarios (10 registros)
-- ----------------------------------------------------------------

/* PERSONAS */
INSERT INTO tbl_personas
(Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido, Fecha_Nacimiento,
 Id_Tipo_Documento, Valor_Documento, Id_Genero_Persona, Id_Nacionalidad, Id_Estado_Civil,
 Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Id_Estado)
VALUES
('MARIA',    'ELENA',  'LOPEZ',     'GARCIA',    '1990-03-22', 1, 'MLOP9003220001', 1, 6, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
('CARLOS',   'EDUARDO','RUIZ',      'MARTINEZ',  '1985-11-11', 1, 'CRUI8511110002', 2, 6, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
('ANA',      'ISABEL', 'GOMEZ',     'PEREZ',     '1995-06-05', 1, 'AGOM9506050003', 1, 6, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
('LUIS',     'FERNANDO','HERRERA',   'SANCHEZ',   '1988-01-30', 1, 'LHERR8801300004', 2, 6, 3, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
('SOFIA',    'MAR',    'MARTINEZ',  'LOZANO',    '1992-09-09', 1, 'SMART9209090005', 1, 6, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
('DIEGO',    'ANDRES', 'ALVAREZ',   'TORRES',    '1979-12-01', 1, 'DALV7912010006', 2, 6, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
('PAULA',    'ROCIO',  'CASTILLO',  'VARGAS',    '1998-07-17', 1, 'PCAST9807170007', 1, 6, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
('ROBERTO',  'MIGUEL', 'FERNANDEZ', 'OLIVARES',  '1982-04-24', 1, 'RFER8204240008', 2, 6, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
('ELENA',    'MARIA',  'RIVERA',    'DOMINGUEZ', '1997-02-02', 1, 'ERIV9702020009', 1, 6, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1);

-- Nota: ajusta Id_Tipo_Documento, Id_Genero_Persona, Id_Nacionalidad, Id_Estado_Civil según tu catálogo real.
-- ----------------------------------------------------------------

/* USUARIOS (relacionados por Valor_Documento -> Id_Persona obtenido mediante subquery) 
   Las contraseñas están como hashes BCrypt (VARCHAR(100) ok). */

INSERT INTO tbl_usuarios
(Id_Persona, Usuario, Contrasena,
 Fecha_Creacion, Fecha_Modificacion, Ultima_Sesion, Ultimo_Cambio_Contrasena,
 Id_Creador, Id_Modificador, Id_Transaccion, Id_Estado)
VALUES
(
 (SELECT TOP 1 Id_Persona FROM tbl_personas WHERE Valor_Documento = 'MLOP9003220001'),
 'mlopez',
 '$2a$11$yF1h7KQ0Z8uQh6mYv1Pq9uk6dQ8b1aZ2xgEo2h2pQwX9ZJk4uT1e',
 GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Persona FROM tbl_personas WHERE Valor_Documento = 'CRUI8511110002'),
 'cruiz',
 '$2a$11$N8q5dYk3rPz7sT1vM2wXyue6ZfH0b9cQpLk3j2QfR8uVYc5tB0sG',
 GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Persona FROM tbl_personas WHERE Valor_Documento = 'AGOM9506050003'),
 'agomez',
 '$2a$11$Gz7wYb9Lp4Qk3sT1uV8nZeJ2yH5pM0qRrT6cXl8dF3sW1vN9bU0x',
 GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Persona FROM tbl_personas WHERE Valor_Documento = 'LHERR8801300004'),
 'lherrera',
 '$2a$11$Hk4pTz2Wq8Nn5cV7bJ1rYeL3oM9fG6sXcZ2vQw8yU5tR1pO0nB3d',
 GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Persona FROM tbl_personas WHERE Valor_Documento = 'SMART9209090005'),
 'smartinez',
 '$2a$11$Sx9kQw7Lm2Vc8bY1nZ4pEuF6hR3oJ0tWqY5zN8vC2mT1sP6aB7dE',
 GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Persona FROM tbl_personas WHERE Valor_Documento = 'DALV7912010006'),
 'dalvarez',
 '$2a$11$Vz2bQp5Lm8Wc1nR9sT3yEeF4uH7oK6jXzY1qP8vC2mT9sL4oN0gH',
 GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Persona FROM tbl_personas WHERE Valor_Documento = 'PCAST9807170007'),
 'pcastillo',
 '$2a$11$Qw3eRt6Yu9Ii2oP8Lm5VbN7cRf4tG1yHjK0oP8zX2cV7nM5sL1dB',
 GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Persona FROM tbl_personas WHERE Valor_Documento = 'RFER8204240008'),
 'rfernandez',
 '$2a$11$Lp6mNz1Qv8Xs3wT9bR4yCeF2oH5pK7jXzN1qP9vC3tS6uO2gH0dB',
 GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Persona FROM tbl_personas WHERE Valor_Documento = 'ERIV9702020009'),
 'erivera',
 '$2a$11$Tz8vWr2Yn6Qs4pHjM0lBeK9cX5oF1uG3rV7nP2wQ8sM6tL9kA4dB',
 GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1
);


INSERT INTO cls_usuarios_roles(Id_Usuario,Id_Rol,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo)
VALUES (1,1,GETDATE(),GETDATE(),1,1,NULL,1)


INSERT INTO cls_roles (Nombre_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,Id_Transaccion,Activo) VALUES
('ADMINISTRADOR', GETDATE(), GETDATE(), 1, 1, NULL, 1),
('DOCENTE', GETDATE(), GETDATE(), 1, 1, NULL, 1),
('ALUMNO', GETDATE(), GETDATE(), 1, 1, NULL, 1),
('AUDITOR', GETDATE(), GETDATE(), 1, 1, NULL, 1),
('COORDINADOR ACADEMICO', GETDATE(), GETDATE(), 1, 1, NULL, 1),
('GESTOR BECAS', GETDATE(), GETDATE(), 1, 1, NULL, 1);


INSERT INTO cls_menus(Menu, Nombre_Boton,Fecha_Creacion,Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo)VALUES 
('USUARIOS','btn_UsuarioMenu',GETDATE(),GETDATE(),1,1,NULL,1),		-- 1
('MIS EVALUACIONES','btn_MisEvaluacionesMenu',GETDATE(),GETDATE(),1,1,NULL,1), -- 2
('MIS MATERIAS','btn_MisMateriasMenu',GETDATE(),GETDATE(),1,1,NULL,1),-- 3
('CONVOCATORIAS','btn_ConvocatoriasMenu',GETDATE(),GETDATE(),1,1,NULL,1), -- 4
('MIS SECCIONES','btn_MisSeccionesMenu',GETDATE(),GETDATE(),1,1,NULL,1), -- 5
('HISTORIAL','btn_HistorialMenu',GETDATE(),GETDATE(),1,1,NULL,1),-- 6
('CONFIGURACIONES SISTEMA','btn_CfgSistemaMenu' ,GETDATE(), GETDATE(),1,1,NULL,1),  -- 7
('GESTIONAR BECAS','btn_GestionarBecasMenu' ,GETDATE(), GETDATE(),1,1,NULL,1); -- 8

INSERT INTO cls_menus_roles(Id_Menu, Id_Rol,Fecha_Creacion,Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(1,1,GETDATE(),GETDATE(),1,1,NULL,1),
(2,3,GETDATE(),GETDATE(),1,1,NULL,1),
(3,3,GETDATE(),GETDATE(),1,1,NULL,1),
(4,3,GETDATE(),GETDATE(),1,1,NULL,1),
(5,4,GETDATE(),GETDATE(),1,1,NULL,1),
(6,1,GETDATE(),GETDATE(),1,1,NULL,1),
(7,1,GETDATE(),GETDATE(),1,1,NULL,1),
(6,4,GETDATE(),GETDATE(),1,1,NULL,1),
(8,6,GETDATE(),GETDATE(),1,1,NULL,1);

INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('AGREGAR ESTADOS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('ACTUALIZAR ESTADOS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('FILTRAR ESTADOS ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('FILTRAR ESTADOS POR TIPO TRANSACCION', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('LISTAR ULTIMOS 10 ESTADOS', GETDATE(), GETDATE(), NULL,NULL,NULL,1);

INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('AGREGAR TIPOS CATALOGOS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('ACTUALIZAR TIPOS CATALOGOS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('FILTRAR TIPOS DE CATALOGOS POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('LISTAR ULTIMOS 10 TIPOS DE CATALOGOS', GETDATE(), GETDATE(), NULL,NULL,NULL,1);

INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('AGREGAR CATALOGOS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('ACTUALIZAR  CATALOGOS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('FILTRAR CATALOGOS POR TIPO', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('FILTRAR CATALOGO ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('LISTAR ULTIMOS 10 CATALOGOS', GETDATE(), GETDATE(), NULL,NULL,NULL,1);

INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('AGREGAR PERSONAS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('ACTUALIZAR  PERSONAS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('FILTRAR PERSONAS POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('FILTRAR PERSONAS POR NUMERO DOCUMENTO', GETDATE(), GETDATE(), NULL,NULL,NULL,1); 

INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('INICIAR SESION', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 19
('AGREGAR USUARIOS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('ACTUALIZAR USUARIOS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('LISTAR USUARIOS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('FILTRAR USUARIOS POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('FILTRAR USUARIOS POR USUARIO', GETDATE(),GETDATE(),NULL,NULL,NULL,1),
('FILTRAR USUARIOS POR ID PERSONA', GETDATE(),GETDATE(),NULL,NULL,NULL,1),
('LISTAR MENU POR ROL DE USUARIO', GETDATE(),GETDATE(),NULL,NULL,NULL,1);

INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('AGREGAR CONTACTO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 27
('ACTUALIZAR CONTACTO', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('FILTRAR CONTACTO POR PERSONA', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('FILTRAR CONTACTO POR ID CONTACTO', GETDATE(), GETDATE(), NULL,NULL,NULL,1);

INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('AGREGAR ROL', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 31
('ACTUALIZAR ROL', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('FILTRAR ROL POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('LISTAR ULTIMOS 10 ROLES', GETDATE(), GETDATE(), NULL,NULL,NULL,1);

INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('AGREGAR TIPO TRANSACCION', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 35
('ACTUALIZAR TIPO TRANSACCION', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('FILTRAR TIPO TRANSACCION POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('LISTAR ULTIMOS 10 TIPOS TRANSACCIONES', GETDATE(), GETDATE(), NULL,NULL,NULL,1);


INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('AGREGAR TIPO TRANSACCION ROL', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 39
('ACTUALIZAR TIPO TRANSACCION ROL', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('FILTRAR TIPO TRANSACCION ROL POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('LISTAR ULTIMOS 10 TIPOS TRANSACCIONES ROLES', GETDATE(), GETDATE(), NULL,NULL,NULL,1);


INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('AGREGAR TIPO TRANSACCION ESTADO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 43
('ACTUALIZAR TIPO TRANSACCION ESTADO', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('FILTRAR TIPO TRANSACCION ESTADO POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('LISTAR ULTIMOS 10 TIPOS TRANSACCIONES ESTADOS', GETDATE(), GETDATE(), NULL,NULL,NULL,1);

INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('AGREGAR USUARIO ROL', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 47
('ACTUALIZAR USUARIO ROL', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('FILTRAR USUARIO ROL ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1),
('LISTAR ROLES DE USUARIO', GETDATE(), GETDATE(), NULL,NULL,NULL,1);

INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('AGREGAR MENU', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 51
('ACTUALIZAR MENU', GETDATE(), GETDATE(), NULL,NULL,NULL,1), --52
('FILTRAR MENU ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1),-- 53
('LISTAR ULTIMOS 10 MENUS', GETDATE(), GETDATE(), NULL,NULL,NULL,1); --54

INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('AGREGAR MENU ROL', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 55
('ACTUALIZAR MENU ROL', GETDATE(), GETDATE(), NULL,NULL,NULL,1), --56
('FILTRAR MENU ROL ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1),-- 57
('LISTAR ULTIMOS 10 MENUS ROLES', GETDATE(), GETDATE(), NULL,NULL,NULL,1); --58

/* */
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(1, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(2, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(3, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(4, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(5, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(6, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(7, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(8, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(9, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(10, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(11, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(12, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(13, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(14, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(15, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(16, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(17, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(18, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(19, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(20, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(21, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(22, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(23, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(24, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(25, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(26, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(27, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(28, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(29, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(30, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(31, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(32, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(33, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(34, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(35, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(36, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(37, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(38, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(39, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(40, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(41, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(42, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(43, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(44, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(45, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(46, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(47, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(48, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(49, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(50, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(51, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(52, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(53, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(54, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(55, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(56, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(57, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(58, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1);



INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(15,1,GETDATE(),GETDATE(),1),
(15,3,GETDATE(),GETDATE(),1),
(16,1,GETDATE(),GETDATE(),1),
(16,2,GETDATE(),GETDATE(),1); 

INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(20,1,GETDATE(),GETDATE(),1),
(20,3,GETDATE(),GETDATE(),1),
(21,1,GETDATE(),GETDATE(),1),
(21,2,GETDATE(),GETDATE(),1); 


