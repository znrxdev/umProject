USE umDb
GO

-- ============================================================
-- DATOS COMPLEMENTARIOS: PERSONAS Y USUARIOS ESTUDIANTES
-- ============================================================
-- Este archivo crea personas adicionales y les asigna usuarios
-- con la contraseña especificada y el rol de ESTUDIANTE
-- ============================================================

-- ============================================================
-- PERSONAS ADICIONALES
-- ============================================================
INSERT INTO tbl_personas
(Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido, Fecha_Nacimiento,
 Id_Tipo_Documento, Valor_Documento, Id_Genero_Persona, Id_Nacionalidad, Id_Estado_Civil,
 Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Id_Estado)
VALUES
('JUAN',     'CARLOS',   'RAMIREZ',    'MORALES',   '2000-05-15', 1, 'JRAM0005150001', 2, 6, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
('MARIA',    'JOSE',     'SILVA',      'CASTRO',    '2001-08-22', 1, 'MSIL0108220002', 1, 6, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
('PEDRO',    'ANTONIO',  'MENDEZ',     'RODRIGUEZ', '1999-12-10', 1, 'PMEN9912100003', 2, 6, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
('LAURA',    'PATRICIA', 'TORRES',     'VARGAS',    '2002-03-18', 1, 'LTOR0203180004', 1, 6, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
('CARLOS',   'ALBERTO',  'GONZALEZ',   'JIMENEZ',   '2000-07-25', 1, 'CGON0007250005', 2, 6, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
('ANDREA',   'SOFIA',    'HERRERA',    'MARTINEZ',  '2001-11-30', 1, 'AHER0111300006', 1, 6, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
('ROBERTO',  'LUIS',     'CAMPOS',     'SALAZAR',   '1999-04-12', 1, 'RCAM9904120007', 2, 6, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
('DANIELA',  'FERNANDA', 'ORTEGA',     'NAVARRO',   '2002-09-05', 1, 'DORT0209050008', 1, 6, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
('FERNANDO', 'JOSE',     'VASQUEZ',    'CRUZ',      '2000-01-20', 1, 'FVAS0001200009', 2, 6, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1),
('VALERIA',  'ALEJANDRA','MORALES',    'FLORES',    '2001-06-14', 1, 'VMOR0106140010', 1, 6, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1);

-- ============================================================
-- USUARIOS PARA LAS PERSONAS CREADAS
-- ============================================================
-- Todos los usuarios tienen la misma contraseña: $2a$11$lrqmyt.z2dgs1mnBp8InyuT.XJvS49jlTN.iPbGi.VYVlYYfWl22W
INSERT INTO tbl_usuarios
(Id_Persona, Usuario, Contrasena,
 Fecha_Creacion, Fecha_Modificacion, Ultima_Sesion, Ultimo_Cambio_Contrasena,
 Id_Creador, Id_Modificador, Id_Transaccion, Id_Estado)
VALUES
(
 (SELECT TOP 1 Id_Persona FROM tbl_personas WHERE Valor_Documento = 'JRAM0005150001'),
 'jramirez',
 '$2a$11$lrqmyt.z2dgs1mnBp8InyuT.XJvS49jlTN.iPbGi.VYVlYYfWl22W',
 GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Persona FROM tbl_personas WHERE Valor_Documento = 'MSIL0108220002'),
 'msilva',
 '$2a$11$lrqmyt.z2dgs1mnBp8InyuT.XJvS49jlTN.iPbGi.VYVlYYfWl22W',
 GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Persona FROM tbl_personas WHERE Valor_Documento = 'PMEN9912100003'),
 'pmendez',
 '$2a$11$lrqmyt.z2dgs1mnBp8InyuT.XJvS49jlTN.iPbGi.VYVlYYfWl22W',
 GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Persona FROM tbl_personas WHERE Valor_Documento = 'LTOR0203180004'),
 'ltorres',
 '$2a$11$lrqmyt.z2dgs1mnBp8InyuT.XJvS49jlTN.iPbGi.VYVlYYfWl22W',
 GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Persona FROM tbl_personas WHERE Valor_Documento = 'CGON0007250005'),
 'cgonzalez',
 '$2a$11$lrqmyt.z2dgs1mnBp8InyuT.XJvS49jlTN.iPbGi.VYVlYYfWl22W',
 GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Persona FROM tbl_personas WHERE Valor_Documento = 'AHER0111300006'),
 'aherrera',
 '$2a$11$lrqmyt.z2dgs1mnBp8InyuT.XJvS49jlTN.iPbGi.VYVlYYfWl22W',
 GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Persona FROM tbl_personas WHERE Valor_Documento = 'RCAM9904120007'),
 'rcampos',
 '$2a$11$lrqmyt.z2dgs1mnBp8InyuT.XJvS49jlTN.iPbGi.VYVlYYfWl22W',
 GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Persona FROM tbl_personas WHERE Valor_Documento = 'DORT0209050008'),
 'dortega',
 '$2a$11$lrqmyt.z2dgs1mnBp8InyuT.XJvS49jlTN.iPbGi.VYVlYYfWl22W',
 GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Persona FROM tbl_personas WHERE Valor_Documento = 'FVAS0001200009'),
 'fvasquez',
 '$2a$11$lrqmyt.z2dgs1mnBp8InyuT.XJvS49jlTN.iPbGi.VYVlYYfWl22W',
 GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Persona FROM tbl_personas WHERE Valor_Documento = 'VMOR0106140010'),
 'vmorales',
 '$2a$11$lrqmyt.z2dgs1mnBp8InyuT.XJvS49jlTN.iPbGi.VYVlYYfWl22W',
 GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1
);

-- ============================================================
-- ASIGNACIÓN DEL ROL ESTUDIANTE A LOS USUARIOS CREADOS
-- ============================================================
-- El rol ESTUDIANTE tiene Id_Rol = 2
INSERT INTO cls_usuarios_roles
(Id_Usuario, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo)
VALUES
(
 (SELECT TOP 1 Id_Usuario FROM tbl_usuarios WHERE Usuario = 'jramirez'),
 2, -- ESTUDIANTE
 GETDATE(), GETDATE(), NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Usuario FROM tbl_usuarios WHERE Usuario = 'msilva'),
 2, -- ESTUDIANTE
 GETDATE(), GETDATE(), NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Usuario FROM tbl_usuarios WHERE Usuario = 'pmendez'),
 2, -- ESTUDIANTE
 GETDATE(), GETDATE(), NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Usuario FROM tbl_usuarios WHERE Usuario = 'ltorres'),
 2, -- ESTUDIANTE
 GETDATE(), GETDATE(), NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Usuario FROM tbl_usuarios WHERE Usuario = 'cgonzalez'),
 2, -- ESTUDIANTE
 GETDATE(), GETDATE(), NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Usuario FROM tbl_usuarios WHERE Usuario = 'aherrera'),
 2, -- ESTUDIANTE
 GETDATE(), GETDATE(), NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Usuario FROM tbl_usuarios WHERE Usuario = 'rcampos'),
 2, -- ESTUDIANTE
 GETDATE(), GETDATE(), NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Usuario FROM tbl_usuarios WHERE Usuario = 'dortega'),
 2, -- ESTUDIANTE
 GETDATE(), GETDATE(), NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Usuario FROM tbl_usuarios WHERE Usuario = 'fvasquez'),
 2, -- ESTUDIANTE
 GETDATE(), GETDATE(), NULL, NULL, NULL, 1
),
(
 (SELECT TOP 1 Id_Usuario FROM tbl_usuarios WHERE Usuario = 'vmorales'),
 2, -- ESTUDIANTE
 GETDATE(), GETDATE(), NULL, NULL, NULL, 1
);

-- ============================================================
-- SANCIONES ACADÉMICAS
-- ============================================================
-- Sanciones académicas para algunos de los estudiantes creados
INSERT INTO tbl_sanciones_academicas (
    Codigo_Sancion, Id_Estudiante, Id_Tipo_Sancion, Id_Tipo_Falta, Id_Severidad,
    Id_Estado, Fecha_Registro, Fecha_Fin, Motivo, Es_Apelable, Fecha_Apelacion,
    Resultado_Apelacion, Observaciones_Apelacion, Documento_Resolucion,
    Id_Usuario_Resolucion, Fecha_Resolucion, Id_Sancion_Origen, Fecha_Creacion,
    Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion
) VALUES
(
 'SAN-2024-003',
 (SELECT TOP 1 Id_Usuario FROM tbl_usuarios WHERE Usuario = 'jramirez'),
 45, -- PLAGIO
 24, -- ACADEMICA
 53, -- LEVE
 1, -- ACTIVO
 '2024-09-10',
 '2024-12-10',
 'Inasistencias repetidas sin justificación',
 1,
 NULL,
 NULL,
 NULL,
 'RES-2024-003.pdf',
 (SELECT TOP 1 Id_Usuario FROM tbl_usuarios WHERE Usuario = 'znr'), -- Usuario administrador
 '2024-09-15',
 NULL,
 GETDATE(),
 GETDATE(),
 (SELECT TOP 1 Id_Usuario FROM tbl_usuarios WHERE Usuario = 'znr'),
 (SELECT TOP 1 Id_Usuario FROM tbl_usuarios WHERE Usuario = 'znr'),
 NULL
),
(
 'SAN-2024-004',
 (SELECT TOP 1 Id_Usuario FROM tbl_usuarios WHERE Usuario = 'msilva'),
 46, -- COPIA EN EXAMEN
 24, -- ACADEMICA
 54, -- GRAVE
 1, -- ACTIVO
 '2024-09-05',
 '2024-10-05',
 'Copia en examen parcial',
 1,
 NULL,
 NULL,
 NULL,
 'RES-2024-004.pdf',
 (SELECT TOP 1 Id_Usuario FROM tbl_usuarios WHERE Usuario = 'znr'), -- Usuario administrador
 '2024-09-08',
 NULL,
 GETDATE(),
 GETDATE(),
 (SELECT TOP 1 Id_Usuario FROM tbl_usuarios WHERE Usuario = 'znr'),
 (SELECT TOP 1 Id_Usuario FROM tbl_usuarios WHERE Usuario = 'znr'),
 NULL
);

-- ============================================================
-- TIPO DE TRANSACCIÓN: FILTRAR SANCIÓN POR ESTUDIANTE Y ESTADO
-- ============================================================
INSERT INTO cls_tipos_transacciones (Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('FILTRAR SANCIÓN POR ESTUDIANTE Y ESTADO', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 135

-- ============================================================
-- ESTADOS LÓGICOS PARA FILTRAR SANCIÓN POR ESTUDIANTE Y ESTADO
-- ============================================================
-- Todos los estados posibles para sanciones académicas
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(135,1,GETDATE(),GETDATE(),1), -- ACTIVO
(135,2,GETDATE(),GETDATE(),1), -- INACTIVO
(135,3,GETDATE(),GETDATE(),1), -- PENDIENTE
(135,4,GETDATE(),GETDATE(),1), -- EN REVISION
(135,5,GETDATE(),GETDATE(),1), -- APROBADA
(135,6,GETDATE(),GETDATE(),1); -- RECHAZADA

-- ============================================================
-- PERMISOS DE ROLES PARA FILTRAR SANCIÓN POR ESTUDIANTE Y ESTADO
-- ============================================================
-- Asignar la transacción 135 a los mismos roles que tienen acceso a las otras transacciones de sanciones académicas
-- Rol 1 = ADMINISTRADOR (tiene acceso a todas las transacciones de sanciones académicas: 87, 88, 89, 90)
-- Rol 6 = SECRETARÍA ACADÉMICA (tiene acceso al menú de Sanciones Académicas)
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(135, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR
(135, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- SECRETARÍA ACADÉMICA

-- ============================================================
-- MENÚ: MI HISTORIAL (Exclusivo para estudiantes)
-- ============================================================
INSERT INTO cls_menus (Menu, Nombre_Boton, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('MI HISTORIAL', 'btn_MiHistorialMenu', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- Id_Menu = 15

-- Asignar el menú al rol ESTUDIANTE (Id_Rol = 2)
INSERT INTO cls_menus_roles (Id_Menu, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(15, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- ESTUDIANTE

-- ============================================================
-- MENÚ: AUDITORÍA (Exclusivo para administrador)
-- ============================================================
INSERT INTO cls_menus (Menu, Nombre_Boton, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('AUDITORÍA', 'btn_AuditoriaMenu', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- Id_Menu = 16

-- Asignar el menú al rol ADMINISTRADOR (Id_Rol = 1)
INSERT INTO cls_menus_roles (Id_Menu, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(16, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- ADMINISTRADOR

-- ============================================================
-- TIPOS DE TRANSACCIONES PARA MI HISTORIAL
-- ============================================================
-- Obtener sanciones académicas del estudiante actual
INSERT INTO cls_tipos_transacciones (Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('OBTENER MIS SANCIONES ACADÉMICAS', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 136
('OBTENER MIS EVALUACIONES PUBLICADAS', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 137
('OBTENER MIS SOLICITUDES DE BECAS', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 138
('APELAR SANCIÓN ACADÉMICA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 139

INSERT INTO cls_tipos_transacciones (Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('OBTENER SANCIONES EN ESPERA DE APELACIÓN', GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- 140
('RESPONDER APELACIÓN', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 141

-- ============================================================
-- PERMISOS DE ROLES PARA LAS NUEVAS TRANSACCIONES
-- ============================================================
-- Asignar las transacciones al rol ESTUDIANTE (Id_Rol = 2)
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(26, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- LISTAR MENU POR ROL DE USUARIO
(88, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- FILTRAR SANCION ID
(136, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER MIS SANCIONES ACADÉMICAS
(137, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER MIS EVALUACIONES PUBLICADAS
(138, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER MIS SOLICITUDES DE BECAS
(139, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- APELAR SANCIÓN ACADÉMICA

-- ============================================================
-- ESTADOS LÓGICOS PARA RESPONDER APELACIÓN
-- ============================================================
-- Estados posibles al responder una apelación: APROBADA (5) o RECHAZADA (6)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(141,5,GETDATE(),GETDATE(),1), -- APROBADA
(141,6,GETDATE(),GETDATE(),1); -- RECHAZADA

-- ============================================================
-- PERMISOS DE ROLES PARA REVISAR APELACIONES
-- ============================================================
-- Asignar las transacciones a ADMINISTRADOR (Id_Rol = 1) y SECRETARÍA ACADÉMICA (Id_Rol = 6)
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(140, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER SANCIONES EN ESPERA DE APELACIÓN - ADMINISTRADOR
(140, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- OBTENER SANCIONES EN ESPERA DE APELACIÓN - SECRETARÍA ACADÉMICA
(141, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- RESPONDER APELACIÓN - ADMINISTRADOR
(141, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- RESPONDER APELACIÓN - SECRETARÍA ACADÉMICA

-- ============================================================
-- TIPO DE TRANSACCIÓN: LISTAR TODOS LOS MODELOS DE EVALUACIÓN
-- ============================================================
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('LISTAR TODOS LOS MODELOS DE EVALUACIÓN', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 142

-- Permisos para LISTAR TODOS LOS MODELOS DE EVALUACIÓN (ID 142)
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(142, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR
(142, 3, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- DOCENTE
(142, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- SECRETARÍA ACADÉMICA

-- Estados lógicos para LISTAR TODOS LOS MODELOS DE EVALUACIÓN (ID 142)
-- Esta transacción es de solo lectura, no requiere estados específicos, pero agregamos ACTIVO por consistencia
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion, Id_Estado, Fecha_Creacion, Fecha_Modificacion, Activo) VALUES
(142, 1, GETDATE(), GETDATE(), 1); -- ACTIVO

-- ============================================================
-- TIPO DE TRANSACCIÓN: FILTRAR USUARIOS POR ROL
-- ============================================================
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('FILTRAR USUARIOS POR ROL', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 143

-- Permisos para FILTRAR USUARIOS POR ROL (ID 143)
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(143, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- ADMINISTRADOR

-- Estados lógicos para FILTRAR USUARIOS POR ROL (ID 143)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion, Id_Estado, Fecha_Creacion, Fecha_Modificacion, Activo) VALUES
(143, 1, GETDATE(), GETDATE(), 1); -- ACTIVO

-- ============================================================
-- TIPO DE TRANSACCIÓN: LISTAR AUDITORÍA DEL SISTEMA
-- ============================================================
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('LISTAR AUDITORÍA DEL SISTEMA', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 144

-- Permisos para LISTAR AUDITORÍA DEL SISTEMA (ID 144) - Solo ADMINISTRADOR
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(144, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- ADMINISTRADOR

-- Estados lógicos para LISTAR AUDITORÍA DEL SISTEMA (ID 144)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion, Id_Estado, Fecha_Creacion, Fecha_Modificacion, Activo) VALUES
(144, 1, GETDATE(), GETDATE(), 1); -- ACTIVO

-- ============================================================
-- TIPO DE TRANSACCIÓN: LISTAR TODAS LAS MATERIAS PERÍODOS ACTIVAS
-- ============================================================
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('LISTAR TODAS LAS MATERIAS PERÍODOS ACTIVAS', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 145



-- Permisos para LISTAR TODAS LAS MATERIAS PERÍODOS ACTIVAS (ID 96)
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(145, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR
(145, 3, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- DOCENTE
(145, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- SECRETARÍA ACADÉMICA

-- Estados lógicos para LISTAR TODAS LAS MATERIAS PERÍODOS ACTIVAS (ID 145)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion, Id_Estado, Fecha_Creacion, Fecha_Modificacion, Activo) VALUES
(145, 1, GETDATE(), GETDATE(), 1); -- ACTIVO

-- ============================================================
-- TIPO DE TRANSACCIÓN: LISTAR TODAS LAS INSTANCIAS DE EVALUACIÓN
-- ============================================================
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('LISTAR TODAS LAS INSTANCIAS DE EVALUACIÓN', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 146

-- Permisos para LISTAR TODAS LAS INSTANCIAS DE EVALUACIÓN (ID 146)
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(146, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR
(146, 3, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- DOCENTE
(146, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- SECRETARÍA ACADÉMICA

-- Estados lógicos para LISTAR TODAS LAS INSTANCIAS DE EVALUACIÓN (ID 146)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion, Id_Estado, Fecha_Creacion, Fecha_Modificacion, Activo) VALUES
(146, 1, GETDATE(), GETDATE(), 1); -- ACTIVO

-- ============================================================
-- TIPO DE TRANSACCIÓN: LISTAR TODAS LAS CALIFICACIONES DE ALUMNOS
-- ============================================================
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
('LISTAR TODAS LAS CALIFICACIONES DE ALUMNOS', GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- 147

-- Permisos para LISTAR TODAS LAS CALIFICACIONES DE ALUMNOS (ID 147)
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion, Id_Rol, Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion, Activo) VALUES
(147, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR
(147, 3, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- DOCENTE
(147, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- SECRETARÍA ACADÉMICA

-- Estados lógicos para LISTAR TODAS LAS CALIFICACIONES DE ALUMNOS (ID 147)
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion, Id_Estado, Fecha_Creacion, Fecha_Modificacion, Activo) VALUES
(147, 1, GETDATE(), GETDATE(), 1); -- ACTIVO

-- ============================================================
-- DATOS DE PRUEBA PARA EVALUACIONES ACADÉMICAS
-- ============================================================
-- Modelos de evaluación adicionales para pruebas
INSERT INTO cls_evaluaciones_modelos (
    Id_Materia_Periodo, Id_Tipo_Evaluacion, Codigo_Modelo, Nombre_Evaluacion,
    Concepto, Calificacion_Maxima, Peso_Porcentual, Orden, Requiere_Aprobacion,
    Version_Configuracion, Id_Metodo_Calculo, Rubrica_Detalle, Porcentaje_Minimo_Aprobacion,
    Niveles_Revision, Id_Rol_Aprobador, Permite_Recalculo, Tiempo_Maximo_Carga,
    Fecha_Inicio, Fecha_Fin, Fecha_Creacion, Fecha_Modificacion, Id_Creador,
    Id_Modificador, Id_Transaccion, Activo
) VALUES
-- Modelos para PROG-201 (Id_Materia_Periodo = 2) - Evaluaciones adicionales
(2, 31, 'MOD-PROG-201-004', 'Laboratorio I', 'Evaluación práctica de laboratorio', 100.00, 20.00, 4, 0, 1, NULL, NULL, 50.00, 1, NULL, 0, NULL, '2024-10-20', '2024-10-30', GETDATE(), GETDATE(), 2, 2, NULL, 1),
(2, 31, 'MOD-PROG-201-005', 'Laboratorio II', 'Evaluación práctica avanzada', 100.00, 20.00, 5, 0, 1, NULL, NULL, 50.00, 1, NULL, 0, NULL, '2024-11-20', '2024-11-30', GETDATE(), GETDATE(), 2, 2, NULL, 1);

-- Instancias de evaluación adicionales para pruebas
INSERT INTO tbl_evaluaciones_instancias (
    Codigo_Instancia, Id_Seccion, Id_Evaluacion_Modelo, Id_Periodo, Fecha_Programada,
    Fecha_Limite, Requiere_Revision_Interna, Numero_Version, Nivel_Aprobacion_Actual,
    Id_Estado, Id_Estado_Publicacion, Id_Responsable_Revision, Fecha_Revision,
    Id_Responsable_Publicacion, Fecha_Publicacion, Id_Evaluacion_Padre,
    Observaciones_Revision, Motivo_Rechazo, Fecha_Creacion, Fecha_Modificacion,
    Id_Creador, Id_Modificador, Id_Transaccion
) VALUES
-- Instancias para PROG-201-A (Id_Seccion = 2) - Evaluaciones adicionales
('INST-PROG-201-A-003', 2, 5, 3, '2024-10-25 14:00:00', '2024-10-25 16:00:00', 0, 1, 1, 1, 3, NULL, NULL, NULL, NULL, NULL, NULL, NULL, GETDATE(), GETDATE(), 2, 2, NULL),
('INST-PROG-201-A-004', 2, 6, 3, '2024-11-25 14:00:00', '2024-11-25 16:00:00', 0, 1, 1, 1, 3, NULL, NULL, NULL, NULL, NULL, NULL, NULL, GETDATE(), GETDATE(), 2, 2, NULL);

-- Evaluaciones de alumnos adicionales para pruebas
INSERT INTO tbl_evaluaciones_alumnos (
    Codigo_Registro, Id_Evaluacion_Instancia, Id_Inscripcion, Puntaje_Obtenido,
    Porcentaje_Logrado, Puntaje_Normalizado, Es_Recalculo, Numero_Recalculo,
    Motivo_Ajuste, Observaciones, Id_Usuario_Evaluador, Id_Usuario_Validador,
    Fecha_Validacion, Id_Estado, Id_Estado_Publicacion, Id_Evaluacion_Reemplazada,
    Firmado_Por_Estudiante, Firma_Digital, Fecha_Notificacion, Fecha_Publicacion,
    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion
) VALUES
-- Evaluaciones para estudiante 4 (Id_Inscripcion = 4) en PROG-201-A - Laboratorio I
('EVAL-2024-005', 4, 4, 90.00, 90.00, 0.9000, 0, 0, NULL, 'Excelente trabajo práctico', 2, 3, '2024-10-26', 1, 5, NULL, 1, 'FIRMA-DIG-004', '2024-10-26', '2024-10-26', GETDATE(), GETDATE(), 2, 2, NULL),
-- Evaluaciones para estudiante 5 (Id_Inscripcion = 5) en PROG-201-A - Laboratorio I
('EVAL-2024-006', 4, 5, 88.00, 88.00, 0.8800, 0, 0, NULL, 'Buen desempeño', 2, 3, '2024-10-26', 1, 5, NULL, 1, 'FIRMA-DIG-005', '2024-10-26', '2024-10-26', GETDATE(), GETDATE(), 2, 2, NULL),
-- Evaluaciones para estudiante 9 (Id_Inscripcion = 6) en PROG-201-A - Laboratorio I
('EVAL-2024-007', 4, 6, 75.00, 75.00, 0.7500, 0, 0, NULL, 'Necesita mejorar práctica', 2, 3, '2024-10-26', 1, 5, NULL, 0, NULL, '2024-10-26', '2024-10-26', GETDATE(), GETDATE(), 2, 2, NULL);

-- ============================================================
-- DATOS HISTÓRICOS ADICIONALES PARA EVALUACIONES (Simular datos antiguos)
-- ============================================================
-- Modelos de evaluación históricos (períodos anteriores)
INSERT INTO cls_evaluaciones_modelos (
    Id_Materia_Periodo, Id_Tipo_Evaluacion, Codigo_Modelo, Nombre_Evaluacion,
    Concepto, Calificacion_Maxima, Peso_Porcentual, Orden, Requiere_Aprobacion,
    Version_Configuracion, Id_Metodo_Calculo, Rubrica_Detalle, Porcentaje_Minimo_Aprobacion,
    Niveles_Revision, Id_Rol_Aprobador, Permite_Recalculo, Tiempo_Maximo_Carga,
    Fecha_Inicio, Fecha_Fin, Fecha_Creacion, Fecha_Modificacion, Id_Creador,
    Id_Modificador, Id_Transaccion, Activo
) VALUES
-- Modelos históricos para PROG-201 (Id_Materia_Periodo = 2) - Períodos anteriores (usando códigos únicos)
(2, 28, 'MOD-PROG-201-HIST-001', 'Parcial I Histórico', 'Primer examen parcial del curso - Período anterior', 100.00, 30.00, 1, 0, 1, NULL, NULL, 60.00, 1, NULL, 0, NULL, '2024-03-15', '2024-03-20', '2024-03-01', '2024-03-01', 2, 2, NULL, 1),
(2, 28, 'MOD-PROG-201-HIST-002', 'Parcial II Histórico', 'Segundo examen parcial del curso - Período anterior', 100.00, 30.00, 2, 0, 1, NULL, NULL, 60.00, 1, NULL, 0, NULL, '2024-06-15', '2024-06-20', '2024-06-01', '2024-06-01', 2, 2, NULL, 1),
(2, 30, 'MOD-PROG-201-HIST-003', 'Proyecto Final Histórico', 'Proyecto integrador del curso - Período anterior', 100.00, 20.00, 3, 1, 1, NULL, NULL, 70.00, 2, NULL, 0, NULL, '2024-08-01', '2024-08-15', '2024-07-15', '2024-07-15', 2, 2, NULL, 1),
-- Modelos para otras materias (Id_Materia_Periodo = 1, 3, etc.)
(1, 28, 'MOD-MATE-101-001', 'Examen Parcial', 'Evaluación parcial de matemáticas', 100.00, 40.00, 1, 0, 1, NULL, NULL, 60.00, 1, NULL, 0, NULL, '2024-04-10', '2024-04-15', '2024-04-01', '2024-04-01', 2, 2, NULL, 1),
(1, 28, 'MOD-MATE-101-002', 'Trabajo Práctico', 'Trabajo práctico de ejercicios', 100.00, 30.00, 2, 0, 1, NULL, NULL, 50.00, 1, NULL, 0, NULL, '2024-05-10', '2024-05-20', '2024-05-01', '2024-05-01', 2, 2, NULL, 1),
(3, 31, 'MOD-FIS-201-001', 'Laboratorio I', 'Primera práctica de laboratorio', 100.00, 25.00, 1, 0, 1, NULL, NULL, 50.00, 1, NULL, 0, NULL, '2024-03-20', '2024-03-25', '2024-03-10', '2024-03-10', 2, 2, NULL, 1),
(3, 28, 'MOD-FIS-201-002', 'Examen Teórico', 'Examen teórico de física', 100.00, 35.00, 2, 0, 1, NULL, NULL, 60.00, 1, NULL, 0, NULL, '2024-05-15', '2024-05-20', '2024-05-01', '2024-05-01', 2, 2, NULL, 1);

-- Instancias históricas de evaluación
INSERT INTO tbl_evaluaciones_instancias (
    Codigo_Instancia, Id_Seccion, Id_Evaluacion_Modelo, Id_Periodo, Fecha_Programada,
    Fecha_Limite, Requiere_Revision_Interna, Numero_Version, Nivel_Aprobacion_Actual,
    Id_Estado, Id_Estado_Publicacion, Id_Responsable_Revision, Fecha_Revision,
    Id_Responsable_Publicacion, Fecha_Publicacion, Id_Evaluacion_Padre,
    Observaciones_Revision, Motivo_Rechazo, Fecha_Creacion, Fecha_Modificacion,
    Id_Creador, Id_Modificador, Id_Transaccion
) VALUES
-- Instancias históricas para PROG-201-A (Id_Seccion = 2) - Usando modelos históricos creados arriba
('INST-PROG-201-A-HIST-001', 2, (SELECT TOP 1 Id_Evaluacion_Modelo FROM cls_evaluaciones_modelos WHERE Codigo_Modelo = 'MOD-PROG-201-HIST-001' ORDER BY Id_Evaluacion_Modelo DESC), 3, '2024-03-18 10:00:00', '2024-03-18 12:00:00', 0, 1, 1, 1, 5, NULL, NULL, 2, '2024-03-19', NULL, NULL, NULL, '2024-03-15', '2024-03-15', 2, 2, NULL),
('INST-PROG-201-A-HIST-002', 2, (SELECT TOP 1 Id_Evaluacion_Modelo FROM cls_evaluaciones_modelos WHERE Codigo_Modelo = 'MOD-PROG-201-HIST-002' ORDER BY Id_Evaluacion_Modelo DESC), 3, '2024-06-18 10:00:00', '2024-06-18 12:00:00', 0, 1, 1, 1, 5, NULL, NULL, 2, '2024-06-19', NULL, NULL, NULL, '2024-06-15', '2024-06-15', 2, 2, NULL),
-- Instancias para otras secciones
('INST-MATE-101-A-HIST-001', 1, (SELECT TOP 1 Id_Evaluacion_Modelo FROM cls_evaluaciones_modelos WHERE Codigo_Modelo = 'MOD-MATE-101-001' ORDER BY Id_Evaluacion_Modelo DESC), 3, '2024-04-12 08:00:00', '2024-04-12 10:00:00', 0, 1, 1, 1, 5, NULL, NULL, 2, '2024-04-13', NULL, NULL, NULL, '2024-04-10', '2024-04-10', 2, 2, NULL),
('INST-FIS-201-A-HIST-001', 3, (SELECT TOP 1 Id_Evaluacion_Modelo FROM cls_evaluaciones_modelos WHERE Codigo_Modelo = 'MOD-FIS-201-001' ORDER BY Id_Evaluacion_Modelo DESC), 3, '2024-03-22 14:00:00', '2024-03-22 16:00:00', 0, 1, 1, 1, 5, NULL, NULL, 2, '2024-03-23', NULL, NULL, NULL, '2024-03-20', '2024-03-20', 2, 2, NULL);

-- Evaluaciones históricas de alumnos (usando las instancias históricas creadas arriba)
INSERT INTO tbl_evaluaciones_alumnos (
    Codigo_Registro, Id_Evaluacion_Instancia, Id_Inscripcion, Puntaje_Obtenido,
    Porcentaje_Logrado, Puntaje_Normalizado, Es_Recalculo, Numero_Recalculo,
    Motivo_Ajuste, Observaciones, Id_Usuario_Evaluador, Id_Usuario_Validador,
    Fecha_Validacion, Id_Estado, Id_Estado_Publicacion, Id_Evaluacion_Reemplazada,
    Firmado_Por_Estudiante, Firma_Digital, Fecha_Notificacion, Fecha_Publicacion,
    Fecha_Creacion, Fecha_Modificacion, Id_Creador, Id_Modificador, Id_Transaccion
) VALUES
-- Evaluaciones históricas para varios estudiantes (usando IDs de instancias históricas)
('EVAL-2024-HIST-001', (SELECT TOP 1 Id_Evaluacion_Instancia FROM tbl_evaluaciones_instancias WHERE Codigo_Instancia = 'INST-PROG-201-A-HIST-001' ORDER BY Id_Evaluacion_Instancia DESC), 4, 85.00, 85.00, 0.8500, 0, 0, NULL, 'Buen desempeño en el parcial', 2, 3, '2024-03-19', 1, 5, NULL, 1, 'FIRMA-DIG-HIST-001', '2024-03-19', '2024-03-19', '2024-03-19', '2024-03-19', 2, 2, NULL),
('EVAL-2024-HIST-002', (SELECT TOP 1 Id_Evaluacion_Instancia FROM tbl_evaluaciones_instancias WHERE Codigo_Instancia = 'INST-PROG-201-A-HIST-001' ORDER BY Id_Evaluacion_Instancia DESC), 5, 92.00, 92.00, 0.9200, 0, 0, NULL, 'Excelente trabajo', 2, 3, '2024-03-19', 1, 5, NULL, 1, 'FIRMA-DIG-HIST-002', '2024-03-19', '2024-03-19', '2024-03-19', '2024-03-19', 2, 2, NULL),
('EVAL-2024-HIST-003', (SELECT TOP 1 Id_Evaluacion_Instancia FROM tbl_evaluaciones_instancias WHERE Codigo_Instancia = 'INST-PROG-201-A-HIST-002' ORDER BY Id_Evaluacion_Instancia DESC), 4, 78.00, 78.00, 0.7800, 0, 0, NULL, 'Aprobado con buen desempeño', 2, 3, '2024-06-19', 1, 5, NULL, 1, 'FIRMA-DIG-HIST-003', '2024-06-19', '2024-06-19', '2024-06-19', '2024-06-19', 2, 2, NULL),
('EVAL-2024-HIST-004', (SELECT TOP 1 Id_Evaluacion_Instancia FROM tbl_evaluaciones_instancias WHERE Codigo_Instancia = 'INST-PROG-201-A-HIST-002' ORDER BY Id_Evaluacion_Instancia DESC), 5, 88.00, 88.00, 0.8800, 0, 0, NULL, 'Muy buen desempeño', 2, 3, '2024-06-19', 1, 5, NULL, 1, 'FIRMA-DIG-HIST-004', '2024-06-19', '2024-06-19', '2024-06-19', '2024-06-19', 2, 2, NULL),
('EVAL-2024-HIST-005', (SELECT TOP 1 Id_Evaluacion_Instancia FROM tbl_evaluaciones_instancias WHERE Codigo_Instancia = 'INST-MATE-101-A-HIST-001' ORDER BY Id_Evaluacion_Instancia DESC), 1, 90.00, 90.00, 0.9000, 0, 0, NULL, 'Excelente en matemáticas', 2, 3, '2024-04-13', 1, 5, NULL, 1, 'FIRMA-DIG-HIST-005', '2024-04-13', '2024-04-13', '2024-04-13', '2024-04-13', 2, 2, NULL),
('EVAL-2024-HIST-006', (SELECT TOP 1 Id_Evaluacion_Instancia FROM tbl_evaluaciones_instancias WHERE Codigo_Instancia = 'INST-FIS-201-A-HIST-001' ORDER BY Id_Evaluacion_Instancia DESC), 2, 82.00, 82.00, 0.8200, 0, 0, NULL, 'Buen trabajo práctico', 2, 3, '2024-03-23', 1, 5, NULL, 1, 'FIRMA-DIG-HIST-006', '2024-03-23', '2024-03-23', '2024-03-23', '2024-03-23', 2, 2, NULL);

-- ============================================================
-- MÓDULO DE BECAS: ELEGIBILIDAD Y FLUJOS DE APROBACIÓN
-- ============================================================
-- Nuevos tipos de transacción para evaluar elegibilidad de becas,
-- listar solicitudes pendientes por nivel y registrar decisiones.

INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('EVALUAR ELEGIBILIDAD BECA', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                 -- 148
('LISTAR SOLICITUDES BECAS PENDIENTES POR NIVEL', GETDATE(), GETDATE(), NULL,NULL,NULL,1), -- 149
('REGISTRAR DECISIÓN SOLICITUD BECA', GETDATE(), GETDATE(), NULL,NULL,NULL,1);        -- 150

-- Permisos de roles para los nuevos tipos de transacción de becas
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion,Id_Rol,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
(148, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR puede evaluar elegibilidad
(148, 2, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ESTUDIANTE puede evaluar su propia elegibilidad
(148, 5, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- COORDINADOR DE BECAS
(149, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR lista pendientes
(149, 5, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- COORDINADOR DE BECAS lista pendientes
(150, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR registra decisión
(150, 5, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- COORDINADOR DE BECAS registra decisión

-- Estados lógicos para los nuevos tipos de transacción
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(148,1,GETDATE(),GETDATE(),1), -- EVALUAR ELEGIBILIDAD BECA: ACTIVO (consulta)
(149,1,GETDATE(),GETDATE(),1), -- LISTAR SOLICITUDES PENDIENTES POR NIVEL: ACTIVO (consulta)
(150,5,GETDATE(),GETDATE(),1), -- REGISTRAR DECISIÓN: APROBADA
(150,6,GETDATE(),GETDATE(),1); -- REGISTRAR DECISIÓN: RECHAZADA

-- ============================================================
-- REPORTES DEL SISTEMA
-- ============================================================
INSERT INTO cls_tipos_transacciones(Nombre_Tipo_Transaccion,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
('REPORTE USUARIOS ACTIVOS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),                 -- 151
('REPORTE USUARIOS INACTIVOS', GETDATE(), GETDATE(), NULL,NULL,NULL,1),               -- 152
('REPORTE AUDITORÍA POR FECHAS', GETDATE(), GETDATE(), NULL,NULL,NULL,1);            -- 153

-- Permisos de roles para los reportes
INSERT INTO cls_transacciones_roles (Id_Tipo_Transaccion,Id_Rol,Fecha_Creacion,Fecha_Modificacion,Id_Creador,Id_Modificador,Id_Transaccion,Activo) VALUES
(151, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR
(151, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- SECRETARÍA ACADÉMICA
(152, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- ADMINISTRADOR
(152, 6, GETDATE(), GETDATE(), NULL, NULL, NULL, 1), -- SECRETARÍA ACADÉMICA
(153, 1, GETDATE(), GETDATE(), NULL, NULL, NULL, 1); -- ADMINISTRADOR (solo admin para auditoría)

-- Estados lógicos para los reportes
INSERT INTO cls_transacciones_estados(Id_Tipo_Transaccion,Id_Estado,Fecha_Creacion,Fecha_Modificacion,Activo) VALUES
(151,1,GETDATE(),GETDATE(),1), -- REPORTE USUARIOS ACTIVOS: ACTIVO (consulta)
(152,1,GETDATE(),GETDATE(),1), -- REPORTE USUARIOS INACTIVOS: ACTIVO (consulta)
(153,1,GETDATE(),GETDATE(),1); -- REPORTE AUDITORÍA: ACTIVO (consulta)