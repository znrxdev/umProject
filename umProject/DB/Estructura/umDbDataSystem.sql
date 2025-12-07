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
(2, 'OTRO', GETDATE(),GETDATE(),NULL,NULL,NULL,0),
(2,'PREFIERO NO DECIRLO',GETDATE(),GETDATE(),NULL,NULL,NULL,0),
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

INSERT INTO tbl_usuarios (Id_Persona, Usuario,Contrasena,Fecha_Creacion,Fecha_Modificacion,Ultima_Sesion,Ultimo_Cambio_Contrasena,Id_Creador,Id_Modificador,Id_Transaccion,Id_Estado)
VALUES (1, 'znr','$2a$11$lrqmyt.z2dgs1mnBp8InyuT.XJvS49jlTN.iPbGi.VYVlYYfWl22W',GETDATE(),GETDATE(),NULL,NULL,NULL,NULL,NULL,1)



INSERT INTO cls_roles (Nombre_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador,Id_Transaccion,Activo) VALUES
('ADMINISTRADOR', GETDATE(), GETDATE(), 1, 1, NULL, 1);
INSERT INTO cls_usuarios_roles(Id_Usuario,Id_Rol,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo)
VALUES (1,1,GETDATE(),GETDATE(),1,1,NULL,1)

-- Insertar los roles restantes del sistema
INSERT INTO cls_roles (Nombre_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('ESTUDIANTE', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),                    -- Id_Rol = 2
('DOCENTE', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),                       -- Id_Rol = 3
('COORDINADOR ACADEMICO', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),         -- Id_Rol = 4
('COORDINADOR DE BECAS', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),          -- Id_Rol = 5
('SECRETARIA ACADEMICA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1);          -- Id_Rol = 6





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

INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('AGREGAR BECA PROGRAMA', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 59
('ACTUALIZAR BECA PROGRAMA', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 60
('FILTRAR BECA PROGRAMA POR TIPO PROGRAMA', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 61
('FILTRAR BECA PROGRAMA POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 62
('FILTRAR BECA PROGRAMA POR NOMBRE', GETDATE(), GETDATE(), NULL,NULL,NULL,1); -- 63

INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('AGREGAR BECA CRITERIO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 64
('FILTRAR BECA CRITERIO POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 65
('FILTRAR BECA CRITERIO POR ID PROGRAMA', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 66
('ACTUALIZAR BECA CRITERIO', GETDATE(), GETDATE(), NULL,NULL,NULL,1); -- 67

INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('AGREGAR SOLICITUD BECA', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 68
('ACTUALIZAR SOLICITUD BECA', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 69
('FILTRAR SOLICITUD BECA POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 70
('FILTRAR SOLICITUD BECA POR ID PROGRAMA', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 71
('FILTRAR SOLICITUD BECA POR ESTUDIANTE', GETDATE(), GETDATE(), NULL,NULL,NULL,1); -- 72

INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('AGREGAR MATERIA', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 73
('ACTUALIZAR MATERIA', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 74
('FILTRAR MATERIA POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 75
('FILTRAR MATERIA POR CODIGO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 76
('FILTRAR MATERIA POR NOMBRE', GETDATE(), GETDATE(), NULL,NULL,NULL,1); -- 77

INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('AGREGAR PERIODO ACADEMICO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 78
('ACTUALIZAR PERIODO ACADEMICO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 79
('FILTRAR PERIODO POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 80
('FILTRAR PERIODO POR CODIGO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 81
('AGREGAR CONVOCATORIA BECA', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 82
('ACTUALIZAR CONVOCATORIA BECA', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 83
('FILTRAR CONVOCATORIA POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 84
('FILTRAR CONVOCATORIA POR ID PROGRAMA', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 85
('FILTRAR CONVOCATORIA POR ID PERIODO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 86
('AGREGAR SANCION ACADEMICA', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 87
('FILTRAR SANCION POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 88
('FILTRAR SANCION POR ESTUDIANTE', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 89
('ACTUALIZAR SANCION ACAD�MICA', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 90
('AGREGAR MATERIA PERIODO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 91
('ACTUALIZAR MATERIA PERIODO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 92
('FILTRAR MATERIA PERIODO POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 93
('FILTRAR MATERIA PERIODO POR MATERIA', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 94
('FILTRAR MATERIA PERIODO POR PERIODO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 95
('AGREGAR SECCION', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 96
('ACTUALIZAR SECCION', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 97
('FILTRAR SECCION POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 98
('FILTRAR SECCION POR DOCENTE', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 99
('FILTRAR SECCION POR MATERIA PERIODO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 100
('AGREGAR GRUPO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 101
('ACTUALIZAR GRUPO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 102
('FILTRAR GRUPO POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 103
('FILTRAR GRUPO POR PERIODO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 104
('AGREGAR GRUPO SECCION', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 105
('ACTUALIZAR GRUPO SECCION', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 106
('FILTRAR GRUPO SECCION POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 107
('FILTRAR GRUPO SECCION POR GRUPO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 108
('FILTRAR GRUPO SECCION POR SECCION', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 109
('AGREGAR INSCRIPCION', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 110
('ACTUALIZAR INSCRIPCION', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 111
('FILTRAR INSCRIPCION POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 112
('FILTRAR INSCRIPCION POR ESTUDIANTE', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 113
('FILTRAR INSCRIPCION POR SECCION', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 114
('AGREGAR GRUPO INSCRIPCION', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 115
('ACTUALIZAR GRUPO INSCRIPCION', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 116
('FILTRAR GRUPO INSCRIPCION POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 117
('FILTRAR GRUPO INSCRIPCION POR GRUPO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 118
('FILTRAR GRUPO INSCRIPCION POR INSCRIPCION', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 119
('AGREGAR EVALUACION MODELO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 120
('ACTUALIZAR EVALUACION MODELO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 121
('FILTRAR EVALUACION MODELO POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 122
('FILTRAR EVALUACION MODELO POR MATERIA PERIODO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 123
('AGREGAR EVALUACION INSTANCIA', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 124
('ACTUALIZAR EVALUACION INSTANCIA', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 125
('FILTRAR EVALUACION INSTANCIA POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 126
('FILTRAR EVALUACION INSTANCIA POR SECCION', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 127
('AGREGAR EVALUACION ALUMNO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 128
('ACTUALIZAR EVALUACION ALUMNO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 129
('FILTRAR EVALUACION ALUMNO POR ID', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 130
('FILTRAR EVALUACION ALUMNO POR ESTUDIANTE', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 131
('FILTRAR EVALUACION ALUMNO POR INSTANCIA', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 132
('OBTENER ESTUDIANTE POR NUMERO DE DOCUMENTO', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 133
('LISTAR ESTUDIANTES CON SANCIONES', GETDATE(), GETDATE(), NULL,NULL,NULL,1); -- 134

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
(58, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(59, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(60, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(61, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(62, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(63, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(64, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(65, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(66, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(67, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(68, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(69, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(70, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(71, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(72, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(73, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(74, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(75, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(76, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(77, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(78, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(79, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(80, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(81, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(82, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(83, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(84, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(85, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(86, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(87, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(88, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(89, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(90, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(91, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(92, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(93, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(94, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(95, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(96, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(97, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(98, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(99, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(100, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(101, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(102, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(103, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(104, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(105, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(106, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(107, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(108, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(109, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(110, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(111, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(112, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(113, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(114, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(115, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(116, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(117, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(118, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(119, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(120, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(121, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(122, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(123, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(124, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(125, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(126, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(127, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(128, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(129, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(130, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(131, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(132, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(133, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(134, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1);


-- ============================================================
-- TIPO DE TRANSACCION: FILTRAR SANCION POR ESTUDIANTE Y ESTADO
-- ============================================================
INSERT INTO cls_tipos_transacciones (Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('FILTRAR SANCION POR ESTUDIANTE Y ESTADO', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 135

-- ============================================================
-- ESTADOS LOGICOS PARA FILTRAR SANCION POR ESTUDIANTE Y ESTADO
-- ============================================================
-- Todos los estados posibles para sanciones academicas
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(135,1,GETDATE(),GETDATE(),1), -- ACTIVO
(135,2,GETDATE(),GETDATE(),1), -- INACTIVO
(135,3,GETDATE(),GETDATE(),1), -- PENDIENTE
(135,4,GETDATE(),GETDATE(),1), -- EN REVISION
(135,5,GETDATE(),GETDATE(),1), -- APROBADA
(135,6,GETDATE(),GETDATE(),1); -- RECHAZADA


-- ============================================================
-- PERMISOS DE ROLES PARA FILTRAR SANCION POR ESTUDIANTE Y ESTADO
-- ============================================================
-- Asignar la TRANSACCION 135 a los mismos roles que tienen acceso a las otras transacciones de sanciones academicas
-- Rol 1 = ADMINISTRADOR (tiene acceso a todas las transacciones de sanciones academicas: 87, 88, 89, 90)
-- Rol 6 = SECRETARIA ACADEMICA (tiene acceso al menu de Sanciones academicas)
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(135, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR
(135, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- SECRETARIA ACADEMICA


-- ============================================================
-- menuS DEL SISTEMA
-- ============================================================
INSERT INTO cls_menus (Menu, Nombre_Boton, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('Usuarios', 'btn_UsuarioMenu', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),                    -- Id_Menu = 1
('Estudiantes', 'btn_EstudiantesMenu', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),            -- Id_Menu = 2
('Docentes', 'btn_DocentesMenu', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),                  -- Id_Menu = 3
('Materias', 'btn_MateriasMenu', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),                  -- Id_Menu = 4
('Periodos Academicos', 'btn_PeriodosMenu', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),       -- Id_Menu = 5
('Secciones', 'btn_SeccionesMenu', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),                -- Id_Menu = 6
('Grupos', 'btn_GruposMenu', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),                      -- Id_Menu = 7
('Inscripciones', 'btn_InscripcionesMenu', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),         -- Id_Menu = 8
('Evaluaciones', 'btn_EvaluacionesMenu', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),          -- Id_Menu = 9
('Configuracion Evaluaciones', 'btn_ConfiguracionEvaluacionesMenu', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- Id_Menu = 10
('Revision de Evaluaciones', 'btn_RevisionEvaluacionesMenu', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- Id_Menu = 11
('Solicitudes Becas', 'btn_BecasMenu', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),            -- Id_Menu = 12 (solo estudiante)
('Sanciones Academicas', 'btn_SancionesMenu', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),     -- Id_Menu = 13
('Reportes', 'btn_ReportesMenu', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),                  -- Id_Menu = 14
('Gestion de Becas', 'btn_ProgramasBecasMenu', GETDATE(), GETDATE(), NULL, NULL, NULL, 1);   -- Id_Menu = 15 (admin/coord becas/secretaria)

-- ============================================================
-- RELACIONES menuS-ROLES
-- ============================================================
-- ADMINISTRADOR (Id_Rol = 1): Acceso a todos los menus
INSERT INTO cls_menus_roles (Id_Menu, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(1, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- Usuarios
(2, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- Estudiantes
(3, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- Docentes
(4, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- Materias
(5, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- PERIODOs ACADEMICOs
(6, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- Secciones
(7, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- Grupos
(8, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- Inscripciones
(9, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- Evaluaciones
(10, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- Configuracion
(11, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- Revision de Evaluaciones
(12, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 0),  -- Solicitudes Becas (solo estudiante)
(13, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),  -- Sanciones academicas
(14, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1);  -- Reportes



-- ESTUDIANTE (Id_Rol = 2): Acceso a evaluaciones, becas (solicitudes), inscripciones propias
INSERT INTO cls_menus_roles (Id_Menu, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(8, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 0),   -- Inscripciones (propias)
(9, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 0),   -- Evaluaciones (propias)
(12, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1);  -- Solicitudes Becas (solo estudiante)

-- DOCENTE (Id_Rol = 3): Acceso a evaluaciones, secciones propias, grupos propios
INSERT INTO cls_menus_roles (Id_Menu, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(6, 3, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- Secciones (propias)
(7, 3, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- Grupos (propios)
(9, 3, GETDATE(), GETDATE(), NULL, NULL, NULL, 1);  -- Evaluaciones (de sus secciones)

-- COORDINADOR ACADEMICO (Id_Rol = 4): Acceso a materias, PERIODOs, secciones, grupos, inscripciones, evaluaciones
INSERT INTO cls_menus_roles (Id_Menu, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(4, 4, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- Materias
(5, 4, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- PERIODOs ACADEMICOs
(6, 4, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- Secciones
(7, 4, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- Grupos
(8, 4, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- Inscripciones
(9, 4, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),  -- Evaluaciones
(14, 4, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- Reportes

-- COORDINADOR DE BECAS (Id_Rol = 5): Acceso a becas (programas, convocatorias, solicitudes)
INSERT INTO cls_menus_roles (Id_Menu, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(12, 5, GETDATE(), GETDATE(), NULL, NULL, NULL, 0), -- Solicitudes Becas (no acceso)
(14, 5, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- Reportes

-- SECRETARIA ACADEMICA (Id_Rol = 6): Acceso a estudiantes, inscripciones, sanciones, PERIODOs
INSERT INTO cls_menus_roles (Id_Menu, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(2, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- Estudiantes
(5, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- PERIODOs ACADEMICOs
(8, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- Inscripciones
(13, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),  -- Sanciones academicas
(14, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1);  -- Reportes

-- ADMINISTRADOR (Id_Rol = 1): Acceso a menus adicionales (Programas de Becas y Solicitudes de Becas)
INSERT INTO cls_menus_roles (Id_Menu, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(15, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),  -- Gestion de Becas (Admin)
(15, 5, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),  -- Gestion de Becas (Coord Becas)
(15, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1);  -- Gestion de Becas (Secretaria Academica)


-- ============================================================
-- menu: MI HISTORIAL (Exclusivo para estudiantes)
-- ============================================================

INSERT INTO cls_menus (Menu, Nombre_Boton, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('Mi Historial', 'btn_MiHistorialMenu', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- Id_Menu = 16

-- Asignar el menu al rol ESTUDIANTE (Id_Rol = 2)
INSERT INTO cls_menus_roles (Id_Menu, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(16, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- ESTUDIANTE

-- ============================================================
-- menu: AUDITOR�A (Exclusivo para administrador)
-- ============================================================
INSERT INTO cls_menus (Menu, Nombre_Boton, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('AUDITORIA', 'btn_AuditoriaMenu', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- Id_Menu = 17

-- Asignar el menu al rol ADMINISTRADOR (Id_Rol = 1)
INSERT INTO cls_menus_roles (Id_Menu, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(17, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- ADMINISTRADOR



-- Asignar el menu a ADMINISTRADOR (Id_Rol = 1), COORDINADOR ACADEMICO (Id_Rol = 4) y SECRETARIA ACADEMICA (Id_Rol = 6)
INSERT INTO cls_menus_roles (Id_Menu, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(11, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR
(11, 4, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- COORDINADOR ACADEMICO
(11, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- SECRETARIA ACADEMICA

-- ============================================================
-- TIPOS DE TRANSACCIONES PARA MI HISTORIAL
-- ============================================================
-- Obtener sanciones academicas del estudiante actual
INSERT INTO cls_tipos_transacciones (Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('OBTENER MIS SANCIONES academicas', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 136
('OBTENER MIS EVALUACIONES PUBLICADAS', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 137
('OBTENER MIS SOLICITUDES DE BECAS', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 138
('APELAR SANCION ACAD�MICA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 139

INSERT INTO cls_tipos_transacciones (Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('OBTENER SANCIONES EN ESPERA DE APELACI�N', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 140
('RESPONDER APELACI�N', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 141

-- ============================================================
-- PERMISOS DE ROLES PARA LAS NUEVAS TRANSACCIONES
-- ============================================================
-- Asignar las transacciones al rol ESTUDIANTE (Id_Rol = 2)
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(26, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- LISTAR MENU POR ROL DE USUARIO
(88, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- FILTRAR SANCION ID
(136, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER MIS SANCIONES academicas
(137, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER MIS EVALUACIONES PUBLICADAS
(138, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER MIS SOLICITUDES DE BECAS
(139, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- APELAR SANCION ACAD�MICA

-- ============================================================
-- ESTADOS LOGICOS PARA RESPONDER APELACI�N
-- ============================================================
-- Estados posibles al responder una apelaci�n: APROBADA (5) o RECHAZADA (6)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(141,5,GETDATE(),GETDATE(),1), -- APROBADA
(141,6,GETDATE(),GETDATE(),1); -- RECHAZADA

-- ============================================================
-- PERMISOS DE ROLES PARA REVISAR APELACIONES
-- ============================================================
-- Asignar las transacciones a ADMINISTRADOR (Id_Rol = 1) y SECRETARIA ACADEMICA (Id_Rol = 6)
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(140, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER SANCIONES EN ESPERA DE APELACI�N - ADMINISTRADOR
(140, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER SANCIONES EN ESPERA DE APELACI�N - SECRETARIA ACADEMICA
(141, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- RESPONDER APELACI�N - ADMINISTRADOR
(141, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- RESPONDER APELACI�N - SECRETARIA ACADEMICA

-- ============================================================
-- TIPO DE TRANSACCION: LISTAR TODOS LOS MODELOS DE EVALUACION
-- ============================================================
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('LISTAR TODOS LOS MODELOS DE EVALUACION', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 142

-- Permisos para LISTAR TODOS LOS MODELOS DE EVALUACION (ID 142)
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(142, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR
(142, 3, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- DOCENTE
(142, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- SECRETARIA ACADEMICA

-- Estados LOGICOS para LISTAR TODOS LOS MODELOS DE EVALUACION (ID 142)
-- Esta TRANSACCION es de solo lectura, no requiere estados espec�ficos, pero agregamos ACTIVO por consistencia
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion, Id_Estado, Fecha_Creacion, Fecha_Modificacion, Activo) VALUES
(142, 1, GETDATE(), GETDATE(), 1); -- ACTIVO

-- ============================================================
-- TIPO DE TRANSACCION: FILTRAR USUARIOS POR ROL
-- ============================================================
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('FILTRAR USUARIOS POR ROL', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 143

-- Permisos para FILTRAR USUARIOS POR ROL (ID 143)
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(143, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- ADMINISTRADOR

-- Estados LOGICOS para FILTRAR USUARIOS POR ROL (ID 143)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion, Id_Estado, Fecha_Creacion, Fecha_Modificacion, Activo) VALUES
(143, 1, GETDATE(), GETDATE(), 1); -- ACTIVO

-- ============================================================
-- TIPO DE TRANSACCION: LISTAR AUDITOR�A DEL SISTEMA
-- ============================================================
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('LISTAR AUDITOR�A DEL SISTEMA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 144

-- Permisos para LISTAR AUDITOR�A DEL SISTEMA (ID 144) - Solo ADMINISTRADOR
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(144, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- ADMINISTRADOR

-- Estados LOGICOS para LISTAR AUDITOR�A DEL SISTEMA (ID 144)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion, Id_Estado, Fecha_Creacion, Fecha_Modificacion, Activo) VALUES
(144, 1, GETDATE(), GETDATE(), 1); -- ACTIVO

-- ============================================================
-- TIPO DE TRANSACCION: LISTAR TODAS LAS MATERIAS PERIODOS ACTIVAS
-- ============================================================
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('LISTAR TODAS LAS MATERIAS PERIODOS ACTIVAS', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 145



-- Permisos para LISTAR TODAS LAS MATERIAS PERIODOS ACTIVAS (ID 96)
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(145, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR
(145, 3, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- DOCENTE
(145, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- SECRETARIA ACADEMICA

-- Estados LOGICOS para LISTAR TODAS LAS MATERIAS PERIODOS ACTIVAS (ID 145)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion, Id_Estado, Fecha_Creacion, Fecha_Modificacion, Activo) VALUES
(145, 1, GETDATE(), GETDATE(), 1); -- ACTIVO

-- ============================================================
-- TIPO DE TRANSACCION: LISTAR TODAS LAS INSTANCIAS DE EVALUACION
-- ============================================================
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('LISTAR TODAS LAS INSTANCIAS DE EVALUACION', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 146

-- Permisos para LISTAR TODAS LAS INSTANCIAS DE EVALUACION (ID 146)
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(146, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR
(146, 3, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- DOCENTE
(146, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- SECRETARIA ACADEMICA

-- Estados LOGICOS para LISTAR TODAS LAS INSTANCIAS DE EVALUACION (ID 146)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion, Id_Estado, Fecha_Creacion, Fecha_Modificacion, Activo) VALUES
(146, 1, GETDATE(), GETDATE(), 1); -- ACTIVO

-- ============================================================
-- TIPO DE TRANSACCION: LISTAR TODAS LAS CALIFICACIONES DE ALUMNOS
-- ============================================================
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('LISTAR TODAS LAS CALIFICACIONES DE ALUMNOS', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 147

-- Permisos para LISTAR TODAS LAS CALIFICACIONES DE ALUMNOS (ID 147)
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(147, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR
(147, 3, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- DOCENTE
(147, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- SECRETARIA ACADEMICA

-- Estados LOGICOS para LISTAR TODAS LAS CALIFICACIONES DE ALUMNOS (ID 147)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion, Id_Estado, Fecha_Creacion, Fecha_Modificacion, Activo) VALUES
(147, 1, GETDATE(), GETDATE(), 1); -- ACTIVO


INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('EVALUAR ELEGIBILIDAD BECA', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                 -- 148
('LISTAR SOLICITUDES BECAS PENDIENTES POR NIVEL', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 149
('REGISTRAR DECISI�N SOLICITUD BECA', GETDATE(), GETDATE(), NULL,NULL,NULL,1);        -- 150

-- Permisos de roles para los nuevos tipos de TRANSACCION de becas
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion,Id_Rol,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
(148, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR puede evaluar elegibilidad
(148, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ESTUDIANTE puede evaluar su propia elegibilidad
(148, 5, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- COORDINADOR DE BECAS
(149, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR lista pendientes
(149, 5, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- COORDINADOR DE BECAS lista pendientes
(150, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR registra decisi�n
(150, 5, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- COORDINADOR DE BECAS registra decisi�n

-- Estados LOGICOS para los nuevos tipos de TRANSACCION
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(148,1,GETDATE(),GETDATE(),1), -- EVALUAR ELEGIBILIDAD BECA: ACTIVO (consulta)
(149,1,GETDATE(),GETDATE(),1), -- LISTAR SOLICITUDES PENDIENTES POR NIVEL: ACTIVO (consulta)
(150,5,GETDATE(),GETDATE(),1), -- REGISTRAR DECISI�N: APROBADA
(150,6,GETDATE(),GETDATE(),1); -- REGISTRAR DECISI�N: RECHAZADA

-- ============================================================
-- REPORTES DEL SISTEMA
-- ============================================================
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('REPORTE USUARIOS ACTIVOS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                 -- 151
('REPORTE USUARIOS INACTIVOS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),               -- 152
('REPORTE AUDITOR�A POR FECHAS', GETDATE(), GETDATE(), NULL,NULL,NULL,1);            -- 153

-- Permisos de roles para los reportes
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion,Id_Rol,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
(151, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR
(151, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- SECRETARIA ACADEMICA
(152, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR
(152, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- SECRETARIA ACADEMICA
(153, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- ADMINISTRADOR (solo admin para auditor�a)

-- Estados LOGICOS para los reportes
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(151,1,GETDATE(),GETDATE(),1), -- REPORTE USUARIOS ACTIVOS: ACTIVO (consulta)
(152,1,GETDATE(),GETDATE(),1), -- REPORTE USUARIOS INACTIVOS: ACTIVO (consulta)
(153,1,GETDATE(),GETDATE(),1); -- REPORTE AUDITOR�A: ACTIVO (consulta)

-- ============================================================
-- TIPOS DE TRANSACCIONES PARA ERRORES SQL Y REPORTES ADICIONALES
-- ============================================================
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('LISTAR ERRORES SQL', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                    -- 154
('REPORTE PERSONAS REGISTRADAS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),          -- 155
('REPORTE MATERIAS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                      -- 156
('REPORTE PERIODOS ACADEMICOS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),           -- 157
('REPORTE SECCIONES', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                     -- 158
('REPORTE GRUPOS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                        -- 159
('REPORTE INSCRIPCIONES', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                 -- 160
('REPORTE EVALUACIONES', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                  -- 161
('REPORTE PROGRAMAS DE BECAS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),            -- 162
('REPORTE CONVOCATORIAS DE BECAS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),        -- 163
('REPORTE SOLICITUDES DE BECAS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),          -- 164
('REPORTE SANCIONES academicas', GETDATE(), GETDATE(), NULL,NULL,NULL,1),          -- 165
('REPORTE TRANSACCIONES', GETDATE(), GETDATE(), NULL,NULL,NULL,1);                 -- 166

-- Permisos de roles para LISTAR ERRORES SQL (ID 154) - Solo ADMINISTRADOR
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion,Id_Rol,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
(154, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- ADMINISTRADOR

-- Permisos de roles para todos los reportes adicionales (ID 155-166) - Solo ADMINISTRADOR
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion,Id_Rol,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
(155, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- REPORTE PERSONAS REGISTRADAS - ADMINISTRADOR
(156, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- REPORTE MATERIAS - ADMINISTRADOR
(157, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- REPORTE PERIODOS ACADEMICOS - ADMINISTRADOR
(158, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- REPORTE SECCIONES - ADMINISTRADOR
(159, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- REPORTE GRUPOS - ADMINISTRADOR
(160, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- REPORTE INSCRIPCIONES - ADMINISTRADOR
(161, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- REPORTE EVALUACIONES - ADMINISTRADOR
(162, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- REPORTE PROGRAMAS DE BECAS - ADMINISTRADOR
(163, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- REPORTE CONVOCATORIAS DE BECAS - ADMINISTRADOR
(164, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- REPORTE SOLICITUDES DE BECAS - ADMINISTRADOR
(165, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- REPORTE SANCIONES academicas - ADMINISTRADOR
(166, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- REPORTE TRANSACCIONES - ADMINISTRADOR

-- Estados LOGICOS para LISTAR ERRORES SQL (ID 154)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(154,1,GETDATE(),GETDATE(),1); -- LISTAR ERRORES SQL: ACTIVO (consulta)

-- Estados LOGICOS para todos los reportes adicionales (ID 155-166)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(155,1,GETDATE(),GETDATE(),1), -- REPORTE PERSONAS REGISTRADAS: ACTIVO (consulta)
(156,1,GETDATE(),GETDATE(),1), -- REPORTE MATERIAS: ACTIVO (consulta)
(157,1,GETDATE(),GETDATE(),1), -- REPORTE PERIODOS ACADEMICOS: ACTIVO (consulta)
(158,1,GETDATE(),GETDATE(),1), -- REPORTE SECCIONES: ACTIVO (consulta)
(159,1,GETDATE(),GETDATE(),1), -- REPORTE GRUPOS: ACTIVO (consulta)
(160,1,GETDATE(),GETDATE(),1), -- REPORTE INSCRIPCIONES: ACTIVO (consulta)
(161,1,GETDATE(),GETDATE(),1), -- REPORTE EVALUACIONES: ACTIVO (consulta)
(162,1,GETDATE(),GETDATE(),1), -- REPORTE PROGRAMAS DE BECAS: ACTIVO (consulta)
(163,1,GETDATE(),GETDATE(),1), -- REPORTE CONVOCATORIAS DE BECAS: ACTIVO (consulta)
(164,1,GETDATE(),GETDATE(),1), -- REPORTE SOLICITUDES DE BECAS: ACTIVO (consulta)
(165,1,GETDATE(),GETDATE(),1), -- REPORTE SANCIONES academicas: ACTIVO (consulta)
(166,1,GETDATE(),GETDATE(),1); -- REPORTE TRANSACCIONES: ACTIVO (consulta)

-- ============================================================
-- M�DULO DE ESTUDIANTES
-- ============================================================
-- Tipos de transacciones para el m�dulo de Estudiantes
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('LISTAR ESTUDIANTES', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                                    -- 167
('OBTENER DETALLE ESTUDIANTE', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                            -- 168
('OBTENER INSCRIPCIONES ESTUDIANTE', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                      -- 169
('OBTENER GRUPOS ESTUDIANTE', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                             -- 170
('OBTENER SECCIONES ESTUDIANTE', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                          -- 171
('OBTENER PERIODOS ESTUDIANTE', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                          -- 172
('OBTENER EVALUACIONES ESTUDIANTE', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                       -- 173
('OBTENER DESEMPENO POR PERIODO ESTUDIANTE', GETDATE(), GETDATE(), NULL,NULL,NULL,1),             -- 174
('OBTENER SANCIONES ESTUDIANTE', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                         -- 175
('OBTENER SOLICITUDES BECAS ESTUDIANTE', GETDATE(), GETDATE(), NULL,NULL,NULL,1);                 -- 176

-- Permisos de roles para el modulo de Estudiantes - ADMINISTRADOR y SECRETARIA ACADEMICA
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion,Id_Rol,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
(167, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- LISTAR ESTUDIANTES - ADMINISTRADOR
(167, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- LISTAR ESTUDIANTES - SECRETARIA ACADEMICA
(168, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER DETALLE ESTUDIANTE - ADMINISTRADOR
(168, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER DETALLE ESTUDIANTE - SECRETARIA ACADEMICA
(169, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER INSCRIPCIONES ESTUDIANTE - ADMINISTRADOR
(169, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER INSCRIPCIONES ESTUDIANTE - SECRETARIA ACADEMICA
(170, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER GRUPOS ESTUDIANTE - ADMINISTRADOR
(170, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER GRUPOS ESTUDIANTE - SECRETARIA ACADEMICA
(171, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER SECCIONES ESTUDIANTE - ADMINISTRADOR
(171, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER SECCIONES ESTUDIANTE - SECRETARIA ACADEMICA
(172, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER PERIODOS ESTUDIANTE - ADMINISTRADOR
(172, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER PERIODOS ESTUDIANTE - SECRETARIA ACADEMICA
(173, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER EVALUACIONES ESTUDIANTE - ADMINISTRADOR
(173, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER EVALUACIONES ESTUDIANTE - SECRETARIA ACADEMICA
(174, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER DESEMPENO POR PERIODO ESTUDIANTE - ADMINISTRADOR
(174, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER DESEMPENO POR PERIODO ESTUDIANTE - SECRETARIA ACADEMICA
(175, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER SANCIONES ESTUDIANTE - ADMINISTRADOR
(175, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER SANCIONES ESTUDIANTE - SECRETARIA ACADEMICA
(176, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER SOLICITUDES BECAS ESTUDIANTE - ADMINISTRADOR
(176, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- OBTENER SOLICITUDES BECAS ESTUDIANTE - SECRETARIA ACADEMICA

-- Estados LOGICOS para el modulo de Estudiantes (todas son consultas, solo ACTIVO)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(167,1,GETDATE(),GETDATE(),1), -- LISTAR ESTUDIANTES: ACTIVO (consulta)
(168,1,GETDATE(),GETDATE(),1), -- OBTENER DETALLE ESTUDIANTE: ACTIVO (consulta)
(169,1,GETDATE(),GETDATE(),1), -- OBTENER INSCRIPCIONES ESTUDIANTE: ACTIVO (consulta)
(170,1,GETDATE(),GETDATE(),1), -- OBTENER GRUPOS ESTUDIANTE: ACTIVO (consulta)
(171,1,GETDATE(),GETDATE(),1), -- OBTENER SECCIONES ESTUDIANTE: ACTIVO (consulta)
(172,1,GETDATE(),GETDATE(),1), -- OBTENER PERIODOS ESTUDIANTE: ACTIVO (consulta)
(173,1,GETDATE(),GETDATE(),1), -- OBTENER EVALUACIONES ESTUDIANTE: ACTIVO (consulta)
(174,1,GETDATE(),GETDATE(),1), -- OBTENER DESEMPENO POR PERIODO ESTUDIANTE: ACTIVO (consulta)
(175,1,GETDATE(),GETDATE(),1), -- OBTENER SANCIONES ESTUDIANTE: ACTIVO (consulta)
(176,1,GETDATE(),GETDATE(),1); -- OBTENER SOLICITUDES BECAS ESTUDIANTE: ACTIVO (consulta)

-- ============================================================
-- MODULO DE DOCENTES
-- ============================================================
-- Tipos de transacciones para el Modulo de Docentes
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('LISTAR DOCENTES', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                                       -- 177
('OBTENER DETALLE DOCENTE', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                               -- 178
('OBTENER EVALUACIONES REALIZADAS DOCENTE', GETDATE(), GETDATE(), NULL,NULL,NULL,1),               -- 179
('OBTENER DETALLE EVALUACION', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                            -- 180
('OBTENER SECCIONES ASIGNADAS DOCENTE', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                   -- 181
('OBTENER ESTUDIANTES DE SECCION', GETDATE(), GETDATE(), NULL,NULL,NULL,1);                        -- 182

-- Permisos de roles para el modulo de Docentes - ADMINISTRADOR y SECRETARIA ACADEMICA
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion,Id_Rol,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
(177, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- LISTAR DOCENTES - ADMINISTRADOR
(177, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- LISTAR DOCENTES - SECRETARIA ACADEMICA
(178, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER DETALLE DOCENTE - ADMINISTRADOR
(178, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER DETALLE DOCENTE - SECRETARIA ACADEMICA
(179, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER EVALUACIONES REALIZADAS DOCENTE - ADMINISTRADOR
(179, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER EVALUACIONES REALIZADAS DOCENTE - SECRETARIA ACADEMICA
(180, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER DETALLE EVALUACION - ADMINISTRADOR
(180, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER DETALLE EVALUACION - SECRETARIA ACADEMICA
(181, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER SECCIONES ASIGNADAS DOCENTE - ADMINISTRADOR
(181, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER SECCIONES ASIGNADAS DOCENTE - SECRETARIA ACADEMICA
(182, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER ESTUDIANTES DE SECCION - ADMINISTRADOR
(182, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- OBTENER ESTUDIANTES DE SECCION - SECRETARIA ACADEMICA

-- Estados LOGICOS para el m�dulo de Docentes (todas son consultas, solo ACTIVO)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(177,1,GETDATE(),GETDATE(),1), -- LISTAR DOCENTES: ACTIVO (consulta)
(178,1,GETDATE(),GETDATE(),1), -- OBTENER DETALLE DOCENTE: ACTIVO (consulta)
(179,1,GETDATE(),GETDATE(),1), -- OBTENER EVALUACIONES REALIZADAS DOCENTE: ACTIVO (consulta)
(180,1,GETDATE(),GETDATE(),1), -- OBTENER DETALLE EVALUACION: ACTIVO (consulta)
(181,1,GETDATE(),GETDATE(),1), -- OBTENER SECCIONES ASIGNADAS DOCENTE: ACTIVO (consulta)
(182,1,GETDATE(),GETDATE(),1); -- OBTENER ESTUDIANTES DE SECCION: ACTIVO (consulta)

-- ============================================================
-- M�DULO DE SANCIONES academicas - LISTAR TODAS
-- ============================================================
-- Tipo de TRANSACCION para listar todas las sanciones academicas
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('LISTAR TODAS LAS SANCIONES academicas', GETDATE(), GETDATE(), NULL,NULL,NULL,1); -- 183

-- Permisos de roles para LISTAR TODAS LAS SANCIONES academicas - ADMINISTRADOR y SECRETARIA ACADEMICA
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion,Id_Rol,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
(183, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR
(183, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- SECRETARIA ACADEMICA

-- Estados LOGICOS para LISTAR TODAS LAS SANCIONES academicas (consulta, solo ACTIVO)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(183,1,GETDATE(),GETDATE(),1); -- LISTAR TODAS LAS SANCIONES academicas: ACTIVO (consulta)


-- ============================================================
-- M�DULO DE PROGRAMAS DE BECAS - LISTAR TODOS
-- ============================================================
-- Tipo de TRANSACCION para listar todos los programas de becas
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('LISTAR TODOS LOS PROGRAMAS DE BECAS', GETDATE(), GETDATE(), NULL,NULL,NULL,1); -- 184

-- Permisos de roles para LISTAR TODOS LOS PROGRAMAS DE BECAS - ADMINISTRADOR y COORDINADOR DE BECAS
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion,Id_Rol,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
(184, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR
(184, 5, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- COORDINADOR DE BECAS

-- Estados LOGICOS para LISTAR TODOS LOS PROGRAMAS DE BECAS (consulta, solo ACTIVO)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(184,1,GETDATE(),GETDATE(),1); -- LISTAR TODOS LOS PROGRAMAS DE BECAS: ACTIVO (consulta)

-- ============================================================
-- VALIDAR ACTIVACION PERIODO ACADEMICO
-- ============================================================
-- Tipo de TRANSACCION para validar la activaci�n de un PERIODO ACADEMICO
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('VALIDAR ACTIVACION PERIODO ACADEMICO', GETDATE(), GETDATE(), NULL,NULL,NULL,1); -- 185

-- Permisos de roles para VALIDAR ACTIVACION PERIODO ACADEMICO - ADMINISTRADOR
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion,Id_Rol,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
(185, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- ADMINISTRADOR

-- Estados LOGICOS para VALIDAR ACTIVACION PERIODO ACADEMICO (solo cuando el PERIODO est� en EN REVISION)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(185,4,GETDATE(),GETDATE(),1); -- VALIDAR ACTIVACION: EN REVISION (solo se puede ejecutar cuando el PERIODO est� en EN REVISION)

-- ============================================================
-- VALIDAR ACTIVACION SECCION
-- ============================================================
-- Tipo de TRANSACCION para validar la activaci�n de una SECCION
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('VALIDAR ACTIVACION SECCION', GETDATE(), GETDATE(), NULL,NULL,NULL,1); -- 186

-- Permisos de roles para VALIDAR ACTIVACION SECCION - ADMINISTRADOR y COORDINADOR ACADEMICO
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion,Id_Rol,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
(186, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR
(186, 4, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- COORDINADOR ACADEMICO

-- Estados LOGICOS para VALIDAR ACTIVACION SECCION (solo cuando la SECCION est� en EN REVISION)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(186,4,GETDATE(),GETDATE(),1); -- VALIDAR ACTIVACION: EN REVISION (solo se puede ejecutar cuando la SECCION est� en EN REVISION)


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

-- ============================================================
-- ESTADOS LOGICOS PARA TRANSACCIONES DE AGREGAR Y ACTUALIZAR
-- ============================================================
-- AGREGAR TIPOS CATALOGOS (6) y ACTUALIZAR TIPOS CATALOGOS (7)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(6,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(6,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(7,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(7,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR CATALOGOS (11) y ACTUALIZAR CATALOGOS (12)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(11,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(11,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(12,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(12,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR PERSONAS (16) y ACTUALIZAR PERSONAS (17)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(16,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(16,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(17,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(17,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR CONTACTO (28) y ACTUALIZAR CONTACTO (29)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(28,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(28,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(29,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(29,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR ROL (31) y ACTUALIZAR ROL (32)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(31,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(31,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(32,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(32,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR TIPO TRANSACCION (35) y ACTUALIZAR TIPO TRANSACCION (36)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(35,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(35,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(36,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(36,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR TIPO TRANSACCION ROL (39) y ACTUALIZAR TIPO TRANSACCION ROL (40)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(39,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(39,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(40,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(40,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR TIPO TRANSACCION ESTADO (43) y ACTUALIZAR TIPO TRANSACCION ESTADO (44)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(43,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(43,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(44,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(44,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR USUARIO ROL (47) y ACTUALIZAR USUARIO ROL (48)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(47,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(47,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(48,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(48,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR MENU (51) y ACTUALIZAR MENU (52)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(51,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(51,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(52,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(52,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR MENU ROL (55) y ACTUALIZAR MENU ROL (56)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(55,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(55,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(56,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(56,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR BECA PROGRAMA (59) y ACTUALIZAR BECA PROGRAMA (60)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(59,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(59,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(59,4,GETDATE(),GETDATE(),1), -- AGREGAR: EN REVISION
(60,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(60,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR BECA CRITERIO (64) y ACTUALIZAR BECA CRITERIO (67)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(64,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(64,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(67,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(67,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR SOLICITUD BECA (68) y ACTUALIZAR SOLICITUD BECA (69)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(68,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(68,4,GETDATE(),GETDATE(),1), -- AGREGAR: EN REVISION
(69,3,GETDATE(),GETDATE(),1), -- ACTUALIZAR: PENDIENTE
(69,4,GETDATE(),GETDATE(),1), -- ACTUALIZAR: EN REVISION
(69,5,GETDATE(),GETDATE(),1), -- ACTUALIZAR: APROBADA
(69,6,GETDATE(),GETDATE(),1); -- ACTUALIZAR: RECHAZADA

-- AGREGAR MATERIA (73) y ACTUALIZAR MATERIA (74)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(73,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(73,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(74,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(74,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR PERIODO ACADEMICO (78) y ACTUALIZAR PERIODO ACADEMICO (79)
-- AGREGAR (78): Solo permite estado "EN REVISION" (4)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(78,4,GETDATE(),GETDATE(),1); -- AGREGAR: EN REVISION

-- ACTUALIZAR (79): Permite PENDIENTE (3), ACTIVO (1), e INACTIVO/FINALIZADO (2)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(79,3,GETDATE(),GETDATE(),1), -- ACTUALIZAR: PENDIENTE
(79,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(79,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO (FINALIZADO)

-- FILTRAR PERIODO POR CODIGO (81): Permite todos los estados para mostrar todos los períodos
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(81,1,GETDATE(),GETDATE(),1), -- FILTRAR: ACTIVO
(81,2,GETDATE(),GETDATE(),1), -- FILTRAR: INACTIVO
(81,3,GETDATE(),GETDATE(),1), -- FILTRAR: PENDIENTE
(81,4,GETDATE(),GETDATE(),1), -- FILTRAR: EN REVISION
(81,5,GETDATE(),GETDATE(),1), -- FILTRAR: APROBADA
(81,6,GETDATE(),GETDATE(),1), -- FILTRAR: RECHAZADA
(81,7,GETDATE(),GETDATE(),1); -- FILTRAR: PLANIFICADA

-- AGREGAR CONVOCATORIA BECA (82) y ACTUALIZAR CONVOCATORIA BECA (83)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(82,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(82,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(82,4,GETDATE(),GETDATE(),1), -- AGREGAR: EN REVISION
(82,7,GETDATE(),GETDATE(),1), -- AGREGAR: PLANIFICADA
(83,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(83,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR SANCION ACAD�MICA (87) y ACTUALIZAR SANCION ACAD�MICA (90)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(87,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO (SANCION aplicada inmediatamente)
(87,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE (requiere validaci�n)
(87,4,GETDATE(),GETDATE(),1), -- AGREGAR: EN REVISION (en proceso de revisi�n)
(90,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO (activar SANCION)
(90,2,GETDATE(),GETDATE(),1), -- ACTUALIZAR: INACTIVO (anular/desactivar SANCION)
(90,3,GETDATE(),GETDATE(),1), -- ACTUALIZAR: PENDIENTE (poner en pendiente)
(90,4,GETDATE(),GETDATE(),1), -- ACTUALIZAR: EN REVISION (poner en revisi�n/apelaci�n)
(90,5,GETDATE(),GETDATE(),1), -- ACTUALIZAR: APROBADA (aprobada despu�s de revisi�n)
(90,6,GETDATE(),GETDATE(),1); -- ACTUALIZAR: RECHAZADA (rechazada despu�s de revisi�n/apelaci�n)

-- AGREGAR MATERIA PERIODO (91) y ACTUALIZAR MATERIA PERIODO (92)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(91,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(91,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(91,4,GETDATE(),GETDATE(),1), -- AGREGAR: EN REVISION
(92,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(92,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR SECCION (96) y ACTUALIZAR SECCION (97)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(96,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(96,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(96,4,GETDATE(),GETDATE(),1), -- AGREGAR: EN REVISION
(97,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(97,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR GRUPO (101) y ACTUALIZAR GRUPO (102)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(101,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(101,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(102,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(102,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR GRUPO SECCION (105) y ACTUALIZAR GRUPO SECCION (106)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(105,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(105,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(106,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(106,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR INSCRIPCION (110) y ACTUALIZAR INSCRIPCION (111)
-- Eliminar estados anteriores si existen
DELETE FROM cls_transacciones_estados WHERE Id_Tipo_Transaccion IN (110, 111);

INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(110,4,GETDATE(),GETDATE(),1), -- AGREGAR: Solo EN REVISION (estado inicial)
(111,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO (para aprobar)
(111,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO (para desactivar)

-- AGREGAR GRUPO INSCRIPCION (115) y ACTUALIZAR GRUPO INSCRIPCION (116)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(115,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(115,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(116,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(116,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR EVALUACION MODELO (120) y ACTUALIZAR EVALUACION MODELO (121)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(120,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(120,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(120,4,GETDATE(),GETDATE(),1), -- AGREGAR: EN REVISION
(121,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(121,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- AGREGAR EVALUACION INSTANCIA (124) y ACTUALIZAR EVALUACION INSTANCIA (125)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(124,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(124,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(124,4,GETDATE(),GETDATE(),1), -- AGREGAR: EN REVISION
(125,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(125,3,GETDATE(),GETDATE(),1); -- ACTUALIZAR: PENDIENTE

-- AGREGAR EVALUACION ALUMNO (128) y ACTUALIZAR EVALUACION ALUMNO (129)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(128,1,GETDATE(),GETDATE(),1), -- AGREGAR: ACTIVO
(128,3,GETDATE(),GETDATE(),1), -- AGREGAR: PENDIENTE
(128,4,GETDATE(),GETDATE(),1), -- AGREGAR: EN REVISION
(129,1,GETDATE(),GETDATE(),1), -- ACTUALIZAR: ACTIVO
(129,2,GETDATE(),GETDATE(),1); -- ACTUALIZAR: INACTIVO

-- ============================================================
-- INSERTS ADICIONALES DE CATALOGOS NECESARIOS
-- ============================================================

INSERT INTO cls_tipos_catalogos (Nombre_Tipo_Catalogo, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('TIPO PERIODO', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 10
('MODALIDAD PROGRAMA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 11
('MONEDA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 12
('TIPO CRITERIO', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 13
('JORNADA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 14
('TIPO SECCION', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 15
('AULA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 16
('TIPO GRUPO', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 17
('TIPO INSCRIPCION', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 18
('ROL GRUPO', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 19
('TIPO VINCULO', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 20
('METODO CALCULO', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 21
('TIPO APROBACION', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 22
('TIPO DECISION', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 23
('TIPO FALTA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 24

INSERT INTO cls_catalogos (Id_Tipo_Catalogo, Nombre_Catalogo, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
-- TIPO PERIODO (10)
(10, 'CUATRIMESTRE', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(10, 'SEMESTRE', GETDATE(), GETDATE(), NULL, NULL, NULL, 0),
(10, 'ANUAL', GETDATE(), GETDATE(), NULL, NULL, NULL, 0),
-- MODALIDAD PROGRAMA (11)
(11, 'PRESENCIAL', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(11, 'VIRTUAL', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(11, 'HIBRIDO', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
-- MONEDA (12)
(12, 'CORDOBA NIO', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(12, 'DOLAR USD', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
-- TIPO CRITERIO (13)
(13, 'PROMEDIO', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(13, 'CREDITOS', GETDATE(), GETDATE(), NULL, NULL, NULL, 0),
(13, 'SANCIONES', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(13, 'ASISTENCIA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(13, 'MATERIAS APROBADAS', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),

-- JORNADA (14)
(14, 'MATUTINA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(14, 'VESPERTINA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(14, 'NOCTURNA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(14, 'SABATINA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
-- TIPO SECCION (15)
(15, 'TEORICA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(15, 'PRACTICA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(15, 'LABORATORIO', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(15, 'TALLER', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
-- AULA (16)
(16, 'A-101', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(16, 'A-102', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(16, 'LAB-201', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(16, 'LAB-202', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(16, 'VIRTUAL', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
-- TIPO GRUPO (17)
(17, 'ACADEMICO', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(17, 'INVESTIGACION', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(17, 'EXTENSION', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
-- TIPO INSCRIPCION (18)
(18, 'REGULAR', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(18, 'ESPECIAL', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(18, 'AUDITORIA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
-- ROL GRUPO (19)
(19, 'ESTUDIANTE', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(19, 'DELEGADO', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(19, 'SUBDELEGADO', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
-- TIPO VINCULO (20)
(20, 'PRINCIPAL', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(20, 'SECUNDARIO', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
-- METODO CALCULO (21)
(21, 'PROMEDIO SIMPLE', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(21, 'PROMEDIO PONDERADO', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(21, 'RUBRICA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
-- TIPO APROBACION (22)
(22, 'CREACION', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(22, 'REVISION', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(22, 'PUBLICACION', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
-- TIPO DECISION (23)
(23, 'APROBADA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(23, 'RECHAZADA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(23, 'PENDIENTE', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
-- TIPO FALTA (24)
(24, 'ACADEMICA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(24, 'DISCIPLINARIA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1);

-- ============================================================
-- PERMISOS ADICIONALES PARA ROL ESTUDIANTE (Id_Rol = 2)
-- Accesos de solo lectura a catálogos y menús para la navegación
-- ============================================================
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(8,  2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- FILTRAR TIPOS CATALOGOS POR ID
(9,  2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- LISTAR TIPOS CATALOGOS
(12, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- FILTRAR CATALOGOS POR TIPO
(13, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- FILTRAR CATALOGO ID
(14, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- LISTAR ULTIMOS 10 CATALOGOS
(173,2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- OBTENER EVALUACIONES ESTUDIANTE (Mi Historial)

-- Accesos adicionales becas (elegibilidad y gestión) para ESTUDIANTE / COORD. BECAS / SECRETARÍA / ADMIN
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(148, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- Evaluar elegibilidad beca (estudiante)
(149, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- Listar pendientes (consulta estudiante)
(150, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- Registrar decisión (no debería usarla, pero se concede según requerimiento)
(149, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- Listar pendientes (secretaría académica)
(150, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- Registrar decisión (secretaría académica)


-- ============================================================
-- TIPO DE TRANSACCION: ACTIVAR PROGRAMA DE BECA
-- ============================================================
INSERT INTO cls_tipos_transacciones (Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('ACTIVAR PROGRAMA DE BECA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 187


-- ============================================================
-- ESTADOS PARA ACTIVAR PROGRAMA DE BECA (solo desde EN REVISION)
-- ============================================================
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(187,4,GETDATE(),GETDATE(),1);


-- ============================================================
-- PERMISOS DE ROLES PARA ACTIVAR PROGRAMA DE BECA
-- ============================================================
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(187, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR
(187, 5, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- COORDINADOR DE BECAS
(187, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- SECRETARIA ACADEMICA

-- ============================================================
-- TRANSACCIONES ESTUDIANTE: SOLICITUDES DE BECAS
-- Nota: el último Id_Tipo_Transaccion vigente es 187; estos quedan 188-192
-- ============================================================
INSERT INTO cls_tipos_transacciones (Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('LISTAR PROGRAMAS DE BECAS (ESTUDIANTE)', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 188
('APLICAR SOLICITUD BECA (ESTUDIANTE)', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),    -- 189
('MIS SOLICITUDES DE BECA (ESTUDIANTE)', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),   -- 190
('HISTORIAL SOLICITUDES BECA (ESTUDIANTE)', GETDATE(), GETDATE(), NULL, NULL, NULL, 1),-- 191
('CRITERIOS PROGRAMA BECA (ESTUDIANTE)', GETDATE(), GETDATE(), NULL, NULL, NULL, 1);   -- 192

-- Estados permitidos para estas transacciones (consulta / creación en revisión)
INSERT INTO cls_transacciones_estados (Id_Tipo_Transaccion, Id_Estado, Fecha_Creacion, Fecha_Modificacion, Activo) VALUES
(188, 4, GETDATE(), GETDATE(), 1),
(189, 4, GETDATE(), GETDATE(), 1),
(190, 4, GETDATE(), GETDATE(), 1),
(191, 4, GETDATE(), GETDATE(), 1),
(192, 4, GETDATE(), GETDATE(), 1);

-- Roles con permiso (ESTUDIANTE)
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(188, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(189, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(190, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(191, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
(192, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1);

