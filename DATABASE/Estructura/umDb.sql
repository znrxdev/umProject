/*
-- 1) Se crea la base de datos para el proyecto University Management.

CREATE DATABASE umDb
DROP DATABASE umDb

-- 2) Nota de desarrollador: esta database cada tabla tendrá un prefijo abreviado de alguna palabra para indicar cuál es su función dentro de esta base de datos,
se define cls_ para indicar que es una tabla de catálogos estáticos, es decir que no son recurrentes en cuanto a modificar/insertar se refiere, sirven para complementar
las tablas transaccionales que son las que estan en constante manejo de datos (inserción o actualización).

Se define las tbl_ para indicar que son tablas transaccionales, es decir que SI son recurrentes en cuanto a modificar/insertar se refiere, son la base del sistema o las tablas
"importantes" en sí.
*/
USE umDb
GO


-- YA REALIZADO

CREATE TABLE [dbo].[cls_estados]
(
	[Id_Estado] INT PRIMARY KEY IDENTITY(1,1), -- Identificador único de cada registro.
	[Nombre_Estado] NVARCHAR(255) NOT NULL, -- Nombre de los estados del sistema (en este caso registros donde se vaya a referencia esta tabla) EJ: Activo, Inactivo, Pendiente
	[Fecha_Creacion] DATETIME NOT NULL, -- Fecha de Creación del Registro.
	[Fecha_Modificacion] DATETIME NOT NULL, -- Fecha de Modificación del Registro.
	[Id_Creador] INT, -- Quien creo el registro
	[Id_Modificador] INT, -- Quien lo modifico
	[Id_Transaccion] INT, -- Ultima transacción hecha sobre dicho registro.
	[Activo] BIT NOT NULL -- Para el manejo de Soft Delete, indica si el registro esta activo o no.
)
GO


/* LUIS KENNETH -- Este SP debe tener Tipo de Transacciones para AGREGAR - ACTUALIZAR - FILTRAR POR ID TIPO CATALOGO - LISTAR ULTIMOS 10 TIPOS CATALOGOS*/ 
CREATE TABLE [dbo].[cls_tipos_catalogos] -- Tabla de los tipos de catalogos del sistema.
(
	[Id_Tipo_Catalogo] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre_Tipo_Catalogo] NVARCHAR(50) NOT NULL,
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT,
	[Id_Modificador] INT,
	[Id_Transaccion] INT,
	[Activo] BIT NOT NULL
)
GO 

/* FERNANDO MOISES -- Este SP debe tener Tipo de Transacicones para AGREGAR - ACTUALIZAR - FILTRAR POR ID CATALOGO - FILTRAR POR ID TIPO CATALOGO */
CREATE TABLE [dbo].[cls_catalogos] -- Tabla de catalogos estaticos del sistema, es hija de tipos de catalogos
(
	[Id_Catalogo] INT PRIMARY KEY IDENTITY(1,1),
	[Id_Tipo_Catalogo] INT REFERENCES cls_tipos_catalogos(Id_Tipo_Catalogo),
	[Nombre_Catalogo] NVARCHAR(50),
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT,
	[Id_Modificador] INT,
	[Id_Transaccion] INT,
	[Activo] BIT NOT NULL
)
GO

/* JUSTIN ZAHIR */
CREATE TABLE [dbo].[tbl_personas] -- Tabla para definir la información de una persona.
(
	[Id_Persona] INT PRIMARY KEY IDENTITY(1,1),
	[Primer_Nombre] NVARCHAR(100) NOT NULL,
	[Segundo_Nombre] NVARCHAR(100),
	[Primer_Apellido] NVARCHAR(100) NOT NULL,
	[Segundo_Apellido] NVARCHAR(100),
	[Fecha_Nacimiento] DATE NOT NULL,
	[Id_Tipo_Documento] INT REFERENCES cls_catalogos(Id_Catalogo),
	[Valor_Documento] NVARCHAR(100),
	[Id_Genero_Persona] INT REFERENCES cls_catalogos(Id_Catalogo),
	[Id_Nacionalidad] INT REFERENCES cls_catalogos(Id_Catalogo),
	[Id_Estado_Civil] INT REFERENCES cls_catalogos(Id_Catalogo),
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT,
	[Id_Modificador] INT,
	[Id_Transaccion] INT,
	[Id_Estado] INT REFERENCES cls_estados(Id_Estado)
)
GO

/* JUSTIN ZAHIR */
CREATE TABLE [dbo].[tbl_usuarios] -- Tabla para definir a los usuarios.
(
	[Id_Usuario] INT PRIMARY KEY IDENTITY(1,1),
	[Id_Persona] INT REFERENCES tbl_personas(Id_Persona),
	[Usuario] NVARCHAR(255) NOT NULL,
	[Contrasena] NVARCHAR(256) NOT NULL,
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Ultima_Sesion] DATETIME,
	[Ultimo_Cambio_Contrasena] DATETIME,
	[Id_Creador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Modificador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Transaccion] INT,
	[Id_Estado] INT REFERENCES cls_estados(Id_Estado)
)
GO

/* JANRIS NARETH -- Este SP debe tener tipo de transacciones para AGREGAR - ACTUALIZAR - FILTRAR POR ID TIPO PROGRAMA - FILTRAR POR ID BECA PROGRAMA - FILTRAR POR NOMBRE */
CREATE TABLE [dbo].[cls_becas_programas]
(
    [Id_Beca_Programa] INT IDENTITY(1,1) PRIMARY KEY,
    [Nombre_Programa] NVARCHAR(150) NOT NULL,
    [Descripcion] NVARCHAR(500) NULL,
    [Id_Tipo_Programa] INT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Activo] BIT NOT NULL DEFAULT(1),
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion]  DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL
);
GO

/* JUSTIN ZAHIR */

CREATE TABLE [dbo].[cls_becas_criterios]
(
	[Id_Beca_Criterio] INT PRIMARY KEY IDENTITY(1,1),
	[Id_Programa] INT REFERENCES dbo.cls_becas_programas (Id_Beca_Programa),
	[Codigo] VARCHAR(50) NOT NULL,
    [Nombre_Criterio] NVARCHAR(150) NOT NULL,
    [Porcentaje_Minimo] DECIMAL(8,2) NOT NULL,
	[Sanciones_Maximas] INT NOT NULL,
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion] DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Activo] BIT NOT NULL
);
GO

/* JOSE JOEL Este sp DEBE TENER TIPO DE TRANSACCIONES PARA AGREGAR - ACTUALIZAR - FILTRAR POR ID SOLICITUD BECA - FILTRAR POR ID BECA PROGRAMA - FILTRAR POR ESTDUAINTE */

CREATE TABLE [dbo].[tbl_solicitudes_becas]
(
	[Id_Solicitud_Beca] INT PRIMARY KEY IDENTITY(1,1),
	[Id_Beca_Programa] INT REFERENCES dbo.cls_becas_programas (Id_Beca_Programa),
	[Id_Estudiante] INT REFERENCES dbo.tbl_usuarios(Id_Usuario), -- Usuario con rol de Estudiante.
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Modificador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Estado] INT REFERENCES dbo.cls_estados(Id_Estado)
)
GO

/* HAREL AMARILIS Este SP debe tener tipo de transacciones para agregar-actualizar-filtrar por id materia - filtrar por codigo materia - filtrar por nombre-materioa */
CREATE TABLE [dbo].[cls_materias] 
(
	[Id_Materia] INT PRIMARY KEY IDENTITY(1,1),
	[Codigo_Materia] VARCHAR(10) NOT NULL,
	[Nombre_Materia] NVARCHAR(100) NOT NULL,
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Modificador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Transaccion] INT,
	[Activo] BIT NOT NULL
)
GO

/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[tbl_sanciones_academicas]
(
    [Id_Sancion] INT IDENTITY(1,1) PRIMARY KEY,
    [Id_Estudiante] INT NOT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Tipo_Sancion] INT NOT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo), -- tipos en catálogo
    [Fecha_Registro] DATETIME NOT NULL,
    [Fecha_Fin] DATETIME NULL,
    [Motivo] NVARCHAR(300) NULL,
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion] DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
	[Id_Estado] INT REFERENCES dbo.cls_estados(Id_Estado)
);
GO
/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[tbl_periodos_academicos] -- Tabla para definir los periodos
(
    [Id_Periodo] INT IDENTITY(1,1) PRIMARY KEY,
    [Codigo_Periodo] VARCHAR(20) NOT NULL,
    [Nombre_Periodo] NVARCHAR(100) NOT NULL,
    [Fecha_Inicio] DATE NOT NULL,
    [Fecha_Fin] DATE NOT NULL,
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion] DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
    [Id_Estado] INT REFERENCES dbo.cls_estados (Id_Estado)
);
GO
/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[tbl_becas_convocatorias]
(
    [Id_Convocatoria] INT IDENTITY(1,1) PRIMARY KEY,
    [Id_Programa] INT NOT NULL REFERENCES dbo.cls_becas_programas(Id_Beca_Programa),
    [Id_Periodo] INT NOT NULL REFERENCES dbo.tbl_periodos_academicos(Id_Periodo),
    [Nombre_Convocatoria] NVARCHAR(150) NOT NULL,
    [Fecha_Inicio] DATE NOT NULL,
    [Fecha_Fin] DATE NOT NULL,
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion]  DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
    [Id_Estado] INT REFERENCES cls_estados(Id_Estado)
);
GO
/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[cls_materias_periodos] -- Tabla para definir que materias van dentro de que periodo
(
	[Id_Materia_Periodo] INT IDENTITY(1,1) PRIMARY KEY,
	[Id_Materia] INT REFERENCES dbo.cls_materias (Id_Materia),
	[Id_Periodo_Academico] INT REFERENCES dbo.tbl_periodos_academicos (Id_Periodo),
	[Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion] DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
    [Activo] BIT NOT NULL
)
GO
/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[tbl_secciones] -- Tabla para definri las secciones a las cuales un docente esta asignado y que materia se imparte.
(
    [Id_Seccion] INT IDENTITY(1,1) PRIMARY KEY,
    [Codigo_Seccion] VARCHAR(20) NOT NULL,
    [Id_Materia_Periodo] INT NOT NULL REFERENCES dbo.cls_materias_periodos(Id_Materia_Periodo),
    [Id_Docente] INT NOT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario), -- Usuario con Rol de Docente.
    [Cupo_Maximo] INT NULL,
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion] DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
    [Activo] BIT NOT NULL
)
GO
/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[tbl_grupos]
(
	[Id_Grupo] INT IDENTITY(1,1) PRIMARY KEY,
	[Id_Periodo] INT NOT NULL REFERENCES dbo.tbl_periodos_academicos(Id_Periodo),
	[Codigo_Grupo] VARCHAR(20) NOT NULL,
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
	[Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
	[Id_Transaccion] INT NULL,
	[Activo] BIT NOT NULL
)
GO
/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[cls_grupos_secciones]
(
	[Id_Grupo_Seccion] INT PRIMARY KEY IDENTITY(1,1),
	[Id_Grupo] INT NOT NULL REFERENCES dbo.tbl_grupos(Id_Grupo),
	[Id_Seccion] INT NOT NULL REFERENCES dbo.tbl_secciones(Id_Seccion),
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
	[Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
	[Id_Transaccion] INT NULL,
	[Activo] BIT NOT NULL
)
GO
/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[tbl_inscripciones]
(
    [Id_Inscripcion] INT IDENTITY(1,1) PRIMARY KEY,
    [Id_Seccion] INT NOT NULL REFERENCES dbo.tbl_secciones(Id_Seccion),
    [Id_Estudiante] INT NOT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario), -- Usuario con rol Estudiante
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion] DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
    [Activo] BIT NOT NULL
);
GO
/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[tbl_grupos_inscripciones]
(
	[Id_Grupo_Inscripcion] INT IDENTITY(1,1) PRIMARY KEY,
	[Id_Grupo] INT NOT NULL REFERENCES dbo.tbl_grupos(Id_Grupo),
	[Id_Inscripcion] INT NOT NULL REFERENCES dbo.tbl_inscripciones(Id_Inscripcion),
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
	[Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
	[Id_Transaccion] INT NULL,
	[Activo] BIT NOT NULL
)
GO
/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[cls_evaluaciones_modelos]
(
    [Id_Evaluacion_Modelo] INT IDENTITY(1,1) PRIMARY KEY,
    [Id_Materia_Periodo] INT NOT NULL REFERENCES dbo.cls_materias_periodos(Id_Materia_Periodo),
    [Id_Tipo_Evaluacion] INT NOT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Nombre_Evaluacion] NVARCHAR(100) NOT NULL,
    [Concepto] NVARCHAR(255) NULL,
    [Calificacion_Maxima] DECIMAL(6,2) NOT NULL, 
    [Peso_Porcentual] DECIMAL(6,2) NOT NULL,
    [Orden] INT NOT NULL DEFAULT(1),
    [Requiere_Aprobacion] BIT NOT NULL DEFAULT(0),
    [Fecha_Inicio] DATETIME NULL,
    [Fecha_Fin] DATETIME NULL,
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion] DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
    [Activo] BIT NOT NULL
);
GO
/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[tbl_evaluaciones_instancias]
(
    [Id_Evaluacion_Instancia] INT IDENTITY(1,1) PRIMARY KEY,
    [Id_Seccion] INT NOT NULL REFERENCES dbo.tbl_secciones(Id_Seccion),
    [Id_Evaluacion_Modelo] INT NOT NULL REFERENCES dbo.cls_evaluaciones_modelos(Id_Evaluacion_Modelo),
    [Fecha_Programada] DATETIME NULL,
    [Fecha_Limite] DATETIME NULL,
    [Requiere_Revision_Interna] BIT NOT NULL DEFAULT(0),  -- pasó revisión interna
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion] DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
    [Id_Estado] INT NOT NULL REFERENCES dbo.cls_estados(Id_Estado)
);
GO
/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[tbl_evaluaciones_alumnos]
(
    [Id_Evaluacion_Alumno] INT IDENTITY(1,1) PRIMARY KEY,
    [Id_Evaluacion_Instancia] INT NOT NULL REFERENCES dbo.tbl_evaluaciones_instancias(Id_Evaluacion_Instancia),
    [Id_Inscripcion] INT NOT NULL REFERENCES dbo.tbl_inscripciones(Id_Inscripcion),
    [Puntaje_Obtenido] DECIMAL(8,2) NOT NULL DEFAULT(0),
    [Observaciones] NVARCHAR(255) NULL,
    [Fecha_Publicacion] DATETIME NULL,
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion] DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
	[Id_Estado] INT REFERENCES cls_estados(Id_Estado)
);
GO


/* ALAN ALFONSO */
CREATE TABLE [dbo].[tbl_contactos] -- Tabla para definir los contactos de las personas.
(
	[Id_Contacto] INT PRIMARY KEY IDENTITY(1,1),
	[Id_Persona] INT REFERENCES tbl_personas (Id_Persona) ON DELETE CASCADE,
	[Id_Tipo_Contacto] INT REFERENCES cls_catalogos (Id_Catalogo),
	[Valor_Contacto] NVARCHAR(MAX) NOT NULL,
	[Principal] BIT NOT NULL,
	[Id_Estado] INT REFERENCES cls_estados (Id_Estado),
	[Id_Transaccion] INT,
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Modificador] INT REFERENCES tbl_usuarios(Id_Usuario)
)
GO

/* ANGEL JOSUE */
CREATE TABLE [dbo].[cls_roles] -- Tabla para definir los roles del sistema.
(
	[Id_Rol] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre_Rol] NVARCHAR(50) NOT NULL,
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Modificador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Transaccion] INT,
	[Activo] BIT NOT NULL
)
GO


/* JOSE JOEL */
CREATE TABLE [dbo].[cls_tipos_transacciones] -- Tabla para definir los tipos de transacciones disponibles en el sistema (Agregar persona, Actualizar persona, Agregar nota etc)
(
	[Id_Tipo_Transaccion] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre_Tipo_Transaccion] NVARCHAR(80) NOT NULL,
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Modificador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Transaccion] INT,
	[Activo] BIT NOT NULL
)
GO

/* ALAN ALFONSO */

CREATE TABLE [dbo].[cls_transacciones_roles] -- Tabla para definir que rol tiene alcance a que tipo de transacción.
(
	[Id_Transaccion_Rol] INT PRIMARY KEY IDENTITY(1,1),
	[Id_Tipo_Transaccion] INT REFERENCES cls_tipos_transacciones (Id_Tipo_Transaccion),
	[Id_Rol] INT REFERENCES cls_roles(Id_Rol),
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Modificador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Transaccion] INT,
	[Activo] BIT NOT NULL
)
GO

/* LEONARDO 7665 8465 -- SP DEBE TENER TIPO DE TRANSACCION PARA AGREGAR - ACTUALIZAR - FILTRAR POR ID TRANSACCION ESTADO - FILTRAR POR ID TIPO TRANSACCION - FILTRAR POR ID ESTADO */

CREATE TABLE [dbo].[cls_transacciones_estados] -- Tabla para definir que tipo de transacción tiene alcance a que estado. por ejemplo AGREGAR PERSONA tiene alcance al estado ACTIVO / PENDIENTE
(
	[Id_Transaccion_Estado] INT PRIMARY KEY IDENTITY(1,1),
	[Id_Tipo_Transaccion] INT REFERENCES cls_tipos_transacciones (Id_Tipo_Transaccion),
	[Id_Estado] INT REFERENCES cls_estados (Id_Estado),
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Modificador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Transaccion] INT,
	[Activo] BIT NOT NULL
)
GO

/* JOSE JOEL */
CREATE TABLE [dbo].[cls_usuarios_roles] -- Tabla para definir que usuarios tiene que rol.
(
	[Id_Usuario_Rol] INT PRIMARY KEY IDENTITY(1,1),
	[Id_Usuario] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Rol] INT REFERENCES cls_roles(Id_Rol),
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Modificador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Transaccion] INT,
	[Activo] BIT NOT NULL,
)
GO

/* KENNETH2.0 8417 7991 ESTE SP DEBE TENER TIPO DE TRANSACCION PARA AGREGAR - ACTUALIZAR - FILTRAR POR ID MENU - FILTRAR POR MENU */
CREATE TABLE[dbo].[cls_menus] -- Tabla de Menus disponibles del sistema
(
	[Id_Menu] INT PRIMARY KEY IDENTITY(1,1),
	[Menu] VARCHAR(50) NOT NULL,
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Modificador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Transaccion] INT,
	[Activo] BIT NOT NULL,
)
GO

/* ALAN ALFONSO */
CREATE TABLE[dbo].[cls_menus_roles] -- Tabla para definir que rol tiene acceso a que menú.
(
	[Id_Menu_Rol] INT PRIMARY KEY IDENTITY(1,1),
	[Id_Menu] INT REFERENCES cls_menus(Id_Menu),
	[Id_Rol] INT REFERENCES cls_roles(Id_Rol),
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Modificador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Transaccion] INT,
	[Activo] BIT NOT NULL,
)
GO

-- YA REALIZADO
CREATE TABLE [dbo].[tbl_transacciones] -- Tabla de Logs para tablas transaccionales.
(
	[Id_Transaccion] INT PRIMARY KEY IDENTITY(1,1),
	[Id_Tipo_Transaccion] INT REFERENCES cls_tipos_transacciones(Id_Tipo_Transaccion),
	[Concepto] NVARCHAR(255) NOT NULL,
	[Id_Persona] INT REFERENCES tbl_personas(Id_Persona),
	[Id_Usuario] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Contacto] INT REFERENCES dbo.tbl_contactos(Id_Contacto),
	[Id_Evaluacion] INT REFERENCES dbo.tbl_evaluaciones_alumnos(Id_Evaluacion_Alumno),
	[Id_Solicitud_Beca] INT REFERENCES dbo.tbl_solicitudes_becas(Id_Solicitud_Beca),
	[Id_Inscripcion] INT REFERENCES dbo.tbl_inscripciones(Id_Inscripcion),
	[Id_Autor] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Fecha_Creacion] DATETIME NOT NULL,
	[Completado] BIT NOT NULL,
)
GO

-- YA REALIZADO
CREATE TABLE [dbo].[log_errores_sql] -- Tabla de Logs de Errores que ocurran en T-SQL
(
	[Id_Error] INT PRIMARY KEY IDENTITY(1,1),
	[Origen_Error] NVARCHAR(50),
	[Linea_Error] INT,
	[Numero_Error] INT,
	[Mensaje_Error] NVARCHAR(255),
	[Fecha_Error] DATETIME
)
GO
