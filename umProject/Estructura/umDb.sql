/*
-- 1) Se crea la base de datos para el proyecto University Management.

CREATE DATABASE umDb
DROP DATABASE umDb

-- 2) Nota de desarrollador: esta database cada tabla tendr� un prefijo abreviado de alguna palabra para indicar cu�l es su funci�n dentro de esta base de datos,
se define cls_ para indicar que es una tabla de cat�logos est�ticos, es decir que no son recurrentes en cuanto a modificar/insertar se refiere, sirven para complementar
las tablas transaccionales que son las que estan en constante manejo de datos (inserci�n o actualizaci�n).

Se define las tbl_ para indicar que son tablas transaccionales, es decir que SI son recurrentes en cuanto a modificar/insertar se refiere, son la base del sistema o las tablas
"importantes" en s�.
*/
USE umDb
GO

CREATE TABLE [dbo].[cls_estados]
(
	[Id_Estado] INT PRIMARY KEY IDENTITY(1,1), -- Identificador �nico de cada registro.
	[Nombre_Estado] NVARCHAR(255) NOT NULL, -- Nombre de los estados del sistema (en este caso registros donde se vaya a referencia esta tabla) EJ: Activo, Inactivo, Pendiente
	[Fecha_Creacion] DATETIME NOT NULL, -- Fecha de Creaci�n del Registro.
	[Fecha_Modificacion] DATETIME NOT NULL, -- Fecha de Modificaci�n del Registro.
	[Id_Creador] INT, -- Quien creo el registro
	[Id_Modificador] INT, -- Quien lo modifico
	[Id_Transaccion] INT, -- Ultima transacci�n hecha sobre dicho registro.
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
CREATE TABLE [dbo].[tbl_personas] -- Tabla para definir la informaci�n de una persona.
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
	[Contrasena] VARCHAR(100) NOT NULL,
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
    [Codigo_Programa] VARCHAR(30) NOT NULL,
    [Nombre_Programa] NVARCHAR(150) NOT NULL,
    [Descripcion] NVARCHAR(500) NULL,
    [Id_Tipo_Programa] INT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Id_Modalidad_Programa] INT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Fecha_Vigencia_Inicio] DATE NOT NULL,
    [Fecha_Vigencia_Fin] DATE NOT NULL,
    [Monto_Maximo] DECIMAL(14,2) NULL,
    [Id_Moneda] INT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Promedio_Minimo] DECIMAL(5,2) NULL,
    [Creditos_Minimos] INT NULL,
    [Niveles_Aprobacion] TINYINT NOT NULL DEFAULT(1),
    [Requiere_Sin_Sanciones] BIT NOT NULL DEFAULT(1),
    [Requiere_Carta_Compromiso] BIT NOT NULL DEFAULT(0),
    [Id_Estado_Programa] INT NOT NULL REFERENCES dbo.cls_estados(Id_Estado),
    [Activo] BIT NOT NULL DEFAULT(1),
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion]  DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
    [Codigo_Control] UNIQUEIDENTIFIER NOT NULL DEFAULT(NEWID()),
    [RowVersion] ROWVERSION NOT NULL,
    CONSTRAINT UQ_cls_becas_programas_Codigo UNIQUE([Codigo_Programa]),
    CONSTRAINT CK_cls_becas_programas_Fechas CHECK ([Fecha_Vigencia_Fin] > [Fecha_Vigencia_Inicio]),
    CONSTRAINT CK_cls_becas_programas_Promedio CHECK ([Promedio_Minimo] IS NULL OR ([Promedio_Minimo] BETWEEN 0 AND 100)),
    CONSTRAINT CK_cls_becas_programas_Creditos CHECK ([Creditos_Minimos] IS NULL OR [Creditos_Minimos] >= 0),
    CONSTRAINT CK_cls_becas_programas_Niveles CHECK ([Niveles_Aprobacion] BETWEEN 1 AND 5)
);
GO

/* JUSTIN ZAHIR */

CREATE TABLE [dbo].[cls_becas_criterios]
(
	[Id_Beca_Criterio] INT PRIMARY KEY IDENTITY(1,1),
	[Id_Programa] INT NOT NULL REFERENCES dbo.cls_becas_programas (Id_Beca_Programa),
	[Codigo] VARCHAR(50) NOT NULL,
    [Nombre_Criterio] NVARCHAR(150) NOT NULL,
	[Clave_Criterio] NVARCHAR(100) NOT NULL,
    [Valor_Criterio] NVARCHAR(255) NOT NULL,
    [Tipo_Dato_Valor] NVARCHAR(50) NOT NULL,
    [Id_Tipo_Criterio] INT NOT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Operador_Comparacion] NVARCHAR(10) NOT NULL,
    [Valor_Numerico_Minimo] DECIMAL(10,2) NULL,
    [Valor_Numerico_Maximo] DECIMAL(10,2) NULL,
    [Valor_Texto] NVARCHAR(255) NULL,
    [Valor_Booleano] BIT NULL,
    [Peso_Criterio] DECIMAL(5,2) NOT NULL DEFAULT(0),
    [Prioridad] INT NOT NULL DEFAULT(1),
    [Es_Excluyente] BIT NOT NULL DEFAULT(0),
    [Fuente_Validacion] NVARCHAR(150) NOT NULL,
    [Expresion_Validacion] NVARCHAR(1000) NULL,
    [Requiere_Soporte] BIT NOT NULL DEFAULT(0),
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion] DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Activo] BIT NOT NULL,
    [RowVersion] ROWVERSION NOT NULL,
    CONSTRAINT UQ_cls_becas_criterios_codigo UNIQUE([Id_Programa],[Codigo]),
    CONSTRAINT CK_cls_becas_criterios_Peso CHECK ([Peso_Criterio] BETWEEN 0 AND 100),
    CONSTRAINT CK_cls_becas_criterios_Valores CHECK (
        [Valor_Numerico_Minimo] IS NOT NULL OR
        [Valor_Numerico_Maximo] IS NOT NULL OR
        [Valor_Texto] IS NOT NULL OR
        [Valor_Booleano] IS NOT NULL OR
        [Valor_Criterio] IS NOT NULL
    )
);
GO

/* JUSTIN ZAHIR - MOVIDO ANTES DE tbl_becas_convocatorias PARA RESOLVER DEPENDENCIAS */

CREATE TABLE [dbo].[tbl_periodos_academicos] -- Tabla para definir los periodos
(
    [Id_Periodo] INT IDENTITY(1,1) PRIMARY KEY,
    [Codigo_Periodo] VARCHAR(20) NOT NULL,
    [Nombre_Periodo] NVARCHAR(100) NOT NULL,
    [Id_Tipo_Periodo] INT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Fecha_Inicio] DATE NOT NULL,
    [Fecha_Fin] DATE NOT NULL,
    [Fecha_Cierre_Calificaciones] DATE NULL,
    [Es_Periodo_Actual] BIT NOT NULL DEFAULT(0),
    [Permite_Inscripciones] BIT NOT NULL DEFAULT(0),
    [Codigo_Integracion] VARCHAR(30) NULL,
    [Observaciones] NVARCHAR(255) NULL,
    [Id_Estado] INT NOT NULL REFERENCES dbo.cls_estados (Id_Estado),
    [Id_Estado_Publicacion] INT NULL REFERENCES dbo.cls_estados (Id_Estado),
    [Hash_Configuracion] VARBINARY(64) NULL,
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion] DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
    [Codigo_Control] UNIQUEIDENTIFIER NOT NULL DEFAULT(NEWID()),
    [RowVersion] ROWVERSION NOT NULL,
    CONSTRAINT UQ_tbl_periodos_academicos_codigo UNIQUE([Codigo_Periodo]),
    CONSTRAINT CK_tbl_periodos_academicos_Fechas CHECK ([Fecha_Fin] > [Fecha_Inicio] AND ([Fecha_Cierre_Calificaciones] IS NULL OR [Fecha_Cierre_Calificaciones] >= [Fecha_Inicio]))
);
GO

/* JUSTIN ZAHIR - MOVIDO ANTES DE tbl_solicitudes_becas PARA RESOLVER DEPENDENCIAS */

CREATE TABLE [dbo].[tbl_becas_convocatorias]
(
    [Id_Convocatoria] INT IDENTITY(1,1) PRIMARY KEY,
    [Codigo_Convocatoria] VARCHAR(30) NOT NULL,
    [Id_Programa] INT NOT NULL REFERENCES dbo.cls_becas_programas(Id_Beca_Programa),
    [Id_Periodo] INT NOT NULL REFERENCES dbo.tbl_periodos_academicos(Id_Periodo),
    [Nombre_Convocatoria] NVARCHAR(150) NOT NULL,
    [Descripcion] NVARCHAR(500) NULL,
    [Cupo_Total] INT NOT NULL,
    [Cupo_Reservado] INT NOT NULL DEFAULT(0),
    [Cupo_Asignado] INT NOT NULL DEFAULT(0),
    [Fecha_Inicio] DATE NOT NULL,
    [Fecha_Publicacion] DATE NOT NULL,
    [Fecha_Fin] DATE NOT NULL,
    [Fecha_Cierre] DATE NULL,
    [Requiere_Postulacion_Linea] BIT NOT NULL DEFAULT(1),
    [Documentacion_Obligatoria] NVARCHAR(500) NULL,
    [Url_Convocatoria] NVARCHAR(255) NULL,
    [Observaciones] NVARCHAR(500) NULL,
    [Id_Estado] INT NOT NULL REFERENCES cls_estados(Id_Estado),
    [Id_Estado_Publicacion] INT NOT NULL REFERENCES cls_estados(Id_Estado),
    [Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Usuario_Publicador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Usuario_Cierre] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
    [Hash_Convocatoria] VARBINARY(64) NULL,
    [Codigo_Control] UNIQUEIDENTIFIER NOT NULL DEFAULT(NEWID()),
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion]  DATETIME NOT NULL,
    [RowVersion] ROWVERSION NOT NULL,
    CONSTRAINT UQ_tbl_becas_convocatorias_codigo UNIQUE([Codigo_Convocatoria]),
    CONSTRAINT CK_tbl_becas_convocatorias_Cupos CHECK ([Cupo_Total] >= 0 AND [Cupo_Reservado] >= 0 AND [Cupo_Asignado] >= 0 AND [Cupo_Total] >= [Cupo_Reservado]),
    CONSTRAINT CK_tbl_becas_convocatorias_Fechas CHECK ([Fecha_Fin] >= [Fecha_Inicio] AND ([Fecha_Cierre] IS NULL OR [Fecha_Cierre] >= [Fecha_Inicio]))
);
GO

/* JOSE JOEL Este sp DEBE TENER TIPO DE TRANSACCIONES PARA AGREGAR - ACTUALIZAR - FILTRAR POR ID SOLICITUD BECA - FILTRAR POR ID BECA PROGRAMA - FILTRAR POR ESTDUAINTE */

CREATE TABLE [dbo].[tbl_solicitudes_becas]
(
	[Id_Solicitud_Beca] INT PRIMARY KEY IDENTITY(1,1),
    [Codigo_Seguimiento] VARCHAR(30) NOT NULL,
	[Id_Beca_Programa] INT NOT NULL REFERENCES dbo.cls_becas_programas (Id_Beca_Programa),
    [Id_Convocatoria] INT NULL REFERENCES dbo.tbl_becas_convocatorias(Id_Convocatoria),
	[Id_Estudiante] INT NOT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario), -- Usuario con rol de Estudiante.
    [Promedio_Vigente] DECIMAL(5,2) NULL,
    [Creditos_Aprobados] INT NULL,
    [Total_Sanciones_Activas] INT NOT NULL DEFAULT(0),
    [Cumple_Criterios] BIT NOT NULL DEFAULT(0),
    [Nivel_Aprobacion_Actual] TINYINT NOT NULL DEFAULT(1),
    [Nivel_Aprobacion_Maximo] TINYINT NOT NULL DEFAULT(1),
    [Id_Usuario_Responsable] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Usuario_Supervisor] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Tipo_Decision] INT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
	[Id_Estado] INT NOT NULL REFERENCES dbo.cls_estados(Id_Estado),
    [Id_Estado_Flujo] INT NOT NULL REFERENCES dbo.cls_estados(Id_Estado),
    [Fecha_Solicitud] DATETIME NOT NULL,
    [Fecha_Ultima_Decision] DATETIME NULL,
    [Fecha_Cierre] DATETIME NULL,
    [Motivo_Ultima_Decision] NVARCHAR(500) NULL,
    [Observaciones] NVARCHAR(1000) NULL,
    [Hash_Solicitud] VARBINARY(64) NULL,
    [Codigo_Control] UNIQUEIDENTIFIER NOT NULL DEFAULT(NEWID()),
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Modificador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Transaccion] INT NULL,
    [Es_Prioritaria] BIT NOT NULL DEFAULT(0),
    [RowVersion] ROWVERSION NOT NULL,
    CONSTRAINT UQ_tbl_solicitudes_becas_codigo UNIQUE([Codigo_Seguimiento]),
    CONSTRAINT CK_tbl_solicitudes_becas_Promedio CHECK ([Promedio_Vigente] IS NULL OR ([Promedio_Vigente] BETWEEN 0 AND 100)),
    CONSTRAINT CK_tbl_solicitudes_becas_Nivel CHECK ([Nivel_Aprobacion_Maximo] BETWEEN 1 AND 5 AND [Nivel_Aprobacion_Maximo] >= [Nivel_Aprobacion_Actual]),
    CONSTRAINT CK_tbl_solicitudes_becas_Fechas CHECK ([Fecha_Cierre] IS NULL OR [Fecha_Cierre] >= [Fecha_Solicitud])
)
GO

CREATE TABLE [dbo].[tbl_solicitudes_becas_historial]
(
    [Id_Historial_Solicitud] INT PRIMARY KEY IDENTITY(1,1),
    [Id_Solicitud_Beca] INT NOT NULL REFERENCES dbo.tbl_solicitudes_becas(Id_Solicitud_Beca),
    [Id_Estado_Anterior] INT NULL REFERENCES dbo.cls_estados(Id_Estado),
    [Id_Estado_Nuevo] INT NOT NULL REFERENCES dbo.cls_estados(Id_Estado),
    [Id_Usuario_Revisor] INT NOT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Fecha_Decision] DATETIME NOT NULL,
    [Motivo_Decision] NVARCHAR(1000) NULL,
    [Observaciones] NVARCHAR(1000) NULL
);
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
    [Codigo_Sancion] VARCHAR(30) NOT NULL,
    [Id_Estudiante] INT NOT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Tipo_Sancion] INT NOT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo), -- tipos en cat�logo
    [Id_Tipo_Falta] INT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Id_Severidad] INT NOT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Id_Estado] INT NOT NULL REFERENCES dbo.cls_estados(Id_Estado),
    [Fecha_Registro] DATETIME NOT NULL,
    [Fecha_Fin] DATETIME NULL,
    [Motivo] NVARCHAR(300) NULL,
    [Es_Apelable] BIT NOT NULL DEFAULT(0),
    [Fecha_Apelacion] DATETIME NULL,
    [Resultado_Apelacion] NVARCHAR(200) NULL,
    [Observaciones_Apelacion] NVARCHAR(500) NULL,
    [Documento_Resolucion] NVARCHAR(255) NULL,
    [Id_Usuario_Resolucion] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Fecha_Resolucion] DATETIME NULL,
    [Id_Sancion_Origen] INT NULL REFERENCES dbo.tbl_sanciones_academicas(Id_Sancion),
    [Hash_Sancion] VARBINARY(64) NULL,
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion] DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
	[Codigo_Control] UNIQUEIDENTIFIER NOT NULL DEFAULT(NEWID()),
    [RowVersion] ROWVERSION NOT NULL,
	CONSTRAINT UQ_tbl_sanciones_academicas_codigo UNIQUE([Codigo_Sancion]),
    CONSTRAINT CK_tbl_sanciones_academicas_Fechas CHECK ([Fecha_Fin] IS NULL OR [Fecha_Fin] >= [Fecha_Registro])
);
GO
/* Esta tabla fue movida antes de tbl_becas_convocatorias para resolver dependencias */

CREATE TABLE [dbo].[cls_materias_periodos] -- Tabla para definir que materias van dentro de que periodo
(
	[Id_Materia_Periodo] INT IDENTITY(1,1) PRIMARY KEY,
	[Id_Materia] INT NOT NULL REFERENCES dbo.cls_materias (Id_Materia),
	[Id_Periodo_Academico] INT NOT NULL REFERENCES dbo.tbl_periodos_academicos (Id_Periodo),
    [Codigo_Plan] VARCHAR(30) NOT NULL,
    [Id_Jornada] INT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Modalidad] NVARCHAR(50) NULL,
    [Horas_Teoricas] INT NOT NULL DEFAULT(0),
    [Horas_Practicas] INT NOT NULL DEFAULT(0),
    [Porcentaje_Asistencia_Minima] DECIMAL(5,2) NULL,
    [Id_Estado] INT NOT NULL REFERENCES dbo.cls_estados(Id_Estado),
    [Fecha_Publicacion] DATETIME NULL,
    [Id_Usuario_Publicador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Observaciones] NVARCHAR(255) NULL,
	[Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion] DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
    [Activo] BIT NOT NULL,
    [RowVersion] ROWVERSION NOT NULL,
    CONSTRAINT UQ_cls_materias_periodos UNIQUE([Id_Materia], [Id_Periodo_Academico], [Codigo_Plan]),
    CONSTRAINT CK_cls_materias_periodos_Horas CHECK ([Horas_Teoricas] >= 0 AND [Horas_Practicas] >= 0),
    CONSTRAINT CK_cls_materias_periodos_Porcentaje CHECK ([Porcentaje_Asistencia_Minima] IS NULL OR ([Porcentaje_Asistencia_Minima] BETWEEN 0 AND 100))
)
GO
/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[tbl_secciones] -- Tabla para definir las secciones a las cuales un docente esta asignado y que materia se imparte.
(
    [Id_Seccion] INT IDENTITY(1,1) PRIMARY KEY,
    [Codigo_Seccion] VARCHAR(20) NOT NULL,
    [Id_Materia_Periodo] INT NOT NULL REFERENCES dbo.cls_materias_periodos(Id_Materia_Periodo),
    [Id_Docente] INT NOT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario), -- Usuario con Rol de Docente.
    [Id_Tipo_Seccion] INT NOT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Id_Aula] INT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Horario_Descripcion] NVARCHAR(255) NULL,
    [Modalidad] NVARCHAR(50) NULL,
    [Cupo_Maximo] INT NULL,
    [Requiere_Asistencia] BIT NOT NULL DEFAULT(1),
    [Porcentaje_Asistencia_Minima] DECIMAL(5,2) NULL,
    [Id_Estado] INT NOT NULL REFERENCES dbo.cls_estados(Id_Estado),
    [Id_Estado_Publicacion] INT NULL REFERENCES dbo.cls_estados(Id_Estado),
    [Fecha_Publicacion] DATETIME NULL,
    [Fecha_Cierre] DATETIME NULL,
    [Codigo_Firma] NVARCHAR(100) NULL,
    [Id_Usuario_Publicador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Observaciones] NVARCHAR(255) NULL,
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion] DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
    [Activo] BIT NOT NULL,
    [RowVersion] ROWVERSION NOT NULL,
    CONSTRAINT UQ_tbl_secciones_codigo UNIQUE([Codigo_Seccion], [Id_Materia_Periodo]),
    CONSTRAINT CK_tbl_secciones_Asistencia CHECK ([Porcentaje_Asistencia_Minima] IS NULL OR ([Porcentaje_Asistencia_Minima] BETWEEN 0 AND 100)),
    CONSTRAINT CK_tbl_secciones_Cupos CHECK ([Cupo_Maximo] IS NULL OR [Cupo_Maximo] > 0)
)
GO
/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[tbl_grupos]
(
	[Id_Grupo] INT IDENTITY(1,1) PRIMARY KEY,
	[Codigo_Grupo] VARCHAR(20) NOT NULL,
    [Nombre_Grupo] NVARCHAR(100) NOT NULL,
	[Id_Periodo] INT NOT NULL REFERENCES dbo.tbl_periodos_academicos(Id_Periodo),
    [Id_Tipo_Grupo] INT NOT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Id_Coordinador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Jornada] INT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Id_Estado] INT NOT NULL REFERENCES dbo.cls_estados(Id_Estado),
    [Fecha_Cierre] DATETIME NULL,
    [Observaciones] NVARCHAR(255) NULL,
    [Codigo_Seguimiento] VARCHAR(30) NOT NULL,
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
	[Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
	[Id_Transaccion] INT NULL,
	[Activo] BIT NOT NULL,
    [RowVersion] ROWVERSION NOT NULL,
    CONSTRAINT UQ_tbl_grupos_codigo UNIQUE([Codigo_Grupo], [Id_Periodo]),
    CONSTRAINT UQ_tbl_grupos_seguimiento UNIQUE([Codigo_Seguimiento]),
    CONSTRAINT CK_tbl_grupos_Fechas CHECK ([Fecha_Cierre] IS NULL OR [Fecha_Cierre] >= [Fecha_Creacion])
)
GO
/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[cls_grupos_secciones]
(
	[Id_Grupo_Seccion] INT PRIMARY KEY IDENTITY(1,1),
	[Id_Grupo] INT NOT NULL REFERENCES dbo.tbl_grupos(Id_Grupo),
	[Id_Seccion] INT NOT NULL REFERENCES dbo.tbl_secciones(Id_Seccion),
    [Id_Tipo_Vinculo] INT NOT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Prioridad] INT NOT NULL DEFAULT(1),
    [Fecha_Asignacion] DATETIME NOT NULL DEFAULT(GETDATE()),
    [Fecha_Desasignacion] DATETIME NULL,
    [Motivo_Desasignacion] NVARCHAR(255) NULL,
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
	[Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
	[Id_Transaccion] INT NULL,
	[Activo] BIT NOT NULL,
    [RowVersion] ROWVERSION NOT NULL,
    CONSTRAINT UQ_cls_grupos_secciones UNIQUE([Id_Grupo], [Id_Seccion], [Fecha_Asignacion]),
    CONSTRAINT CK_cls_grupos_secciones_Fechas CHECK ([Fecha_Desasignacion] IS NULL OR [Fecha_Desasignacion] >= [Fecha_Asignacion])
)
GO
/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[tbl_inscripciones]
(
    [Id_Inscripcion] INT IDENTITY(1,1) PRIMARY KEY,
    [Codigo_Inscripcion] VARCHAR(30) NOT NULL,
    [Id_Seccion] INT NOT NULL REFERENCES dbo.tbl_secciones(Id_Seccion),
    [Id_Estudiante] INT NOT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario), -- Usuario con rol Estudiante
    [Id_Tipo_Inscripcion] INT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Id_Estado] INT NOT NULL REFERENCES dbo.cls_estados(Id_Estado),
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion] DATETIME NOT NULL,
    [Fecha_Validacion] DATETIME NULL,
    [Fecha_Retiro] DATETIME NULL,
    [Motivo_Retiro] NVARCHAR(500) NULL,
    [Promedio_Acumulado] DECIMAL(5,2) NULL,
    [Total_Faltas] INT NOT NULL DEFAULT(0),
    [Es_Repetidor] BIT NOT NULL DEFAULT(0),
    [En_Riesgo_Academico] BIT NOT NULL DEFAULT(0),
    [Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Usuario_Validador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
    [Activo] BIT NOT NULL,
    [RowVersion] ROWVERSION NOT NULL,
    CONSTRAINT UQ_tbl_inscripciones_codigo UNIQUE([Codigo_Inscripcion]),
    CONSTRAINT UQ_tbl_inscripciones_unica UNIQUE([Id_Seccion], [Id_Estudiante]),
    CONSTRAINT CK_tbl_inscripciones_Promedio CHECK ([Promedio_Acumulado] IS NULL OR ([Promedio_Acumulado] BETWEEN 0 AND 100)),
    CONSTRAINT CK_tbl_inscripciones_Fechas CHECK ([Fecha_Retiro] IS NULL OR [Fecha_Retiro] >= [Fecha_Creacion])
);
GO
/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[tbl_grupos_inscripciones]
(
    [Id_Grupo_Inscripcion] INT IDENTITY(1,1) PRIMARY KEY,
    [Id_Grupo] INT NOT NULL REFERENCES dbo.tbl_grupos(Id_Grupo),
    [Id_Inscripcion] INT NOT NULL REFERENCES dbo.tbl_inscripciones(Id_Inscripcion),
    [Id_Rol_Grupo] INT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Id_Estado] INT NOT NULL REFERENCES dbo.cls_estados(Id_Estado),
    [Fecha_Asignacion] DATETIME NOT NULL DEFAULT(GETDATE()),
    [Fecha_Baja] DATETIME NULL,
    [Motivo_Baja] NVARCHAR(255) NULL,
    [Es_Delegado] BIT NOT NULL DEFAULT(0),
    [Observaciones] NVARCHAR(255) NULL,
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion] DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
    [Activo] BIT NOT NULL,
    [RowVersion] ROWVERSION NOT NULL,
    CONSTRAINT UQ_tbl_grupos_inscripciones UNIQUE([Id_Grupo], [Id_Inscripcion]),
    CONSTRAINT CK_tbl_grupos_inscripciones_Fechas CHECK ([Fecha_Baja] IS NULL OR [Fecha_Baja] >= [Fecha_Asignacion])
);
GO
/* ANGEL JOSUE - MOVIDO ANTES DE cls_evaluaciones_modelos PARA RESOLVER DEPENDENCIAS */
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

/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[cls_evaluaciones_modelos]
(
    [Id_Evaluacion_Modelo] INT IDENTITY(1,1) PRIMARY KEY,
    [Id_Materia_Periodo] INT NOT NULL REFERENCES dbo.cls_materias_periodos(Id_Materia_Periodo),
    [Id_Tipo_Evaluacion] INT NOT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Codigo_Modelo] VARCHAR(30) NOT NULL,
    [Nombre_Evaluacion] NVARCHAR(100) NOT NULL,
    [Concepto] NVARCHAR(255) NULL,
    [Calificacion_Maxima] DECIMAL(6,2) NOT NULL, 
    [Peso_Porcentual] DECIMAL(6,2) NOT NULL,
    [Orden] INT NOT NULL DEFAULT(1),
    [Requiere_Aprobacion] BIT NOT NULL DEFAULT(0),
    [Version_Configuracion] INT NOT NULL DEFAULT(1),
    [Id_Metodo_Calculo] INT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Rubrica_Detalle] NVARCHAR(MAX) NULL,
    [Porcentaje_Minimo_Aprobacion] DECIMAL(5,2) NULL,
    [Niveles_Revision] TINYINT NOT NULL DEFAULT(1),
    [Id_Rol_Aprobador] INT NULL REFERENCES dbo.cls_roles(Id_Rol),
    [Permite_Recalculo] BIT NOT NULL DEFAULT(0),
    [Tiempo_Maximo_Carga] INT NULL,
    [Hash_Rubrica] VARBINARY(64) NULL,
    [Fecha_Inicio] DATETIME NULL,
    [Fecha_Fin] DATETIME NULL,
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion] DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
    [Activo] BIT NOT NULL,
    [RowVersion] ROWVERSION NOT NULL,
    CONSTRAINT UQ_cls_evaluaciones_modelos UNIQUE([Id_Materia_Periodo], [Codigo_Modelo], [Version_Configuracion]),
    CONSTRAINT CK_cls_evaluaciones_modelos_Calificacion CHECK ([Calificacion_Maxima] > 0),
    CONSTRAINT CK_cls_evaluaciones_modelos_Peso CHECK ([Peso_Porcentual] BETWEEN 0 AND 100),
    CONSTRAINT CK_cls_evaluaciones_modelos_Porcentaje CHECK ([Porcentaje_Minimo_Aprobacion] IS NULL OR ([Porcentaje_Minimo_Aprobacion] BETWEEN 0 AND 100))
);
GO
/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[tbl_evaluaciones_instancias]
(
    [Id_Evaluacion_Instancia] INT IDENTITY(1,1) PRIMARY KEY,
    [Codigo_Instancia] VARCHAR(30) NOT NULL,
    [Id_Seccion] INT NOT NULL REFERENCES dbo.tbl_secciones(Id_Seccion),
    [Id_Evaluacion_Modelo] INT NOT NULL REFERENCES dbo.cls_evaluaciones_modelos(Id_Evaluacion_Modelo),
    [Id_Periodo] INT NOT NULL REFERENCES dbo.tbl_periodos_academicos(Id_Periodo),
    [Fecha_Programada] DATETIME NULL,
    [Fecha_Limite] DATETIME NULL,
    [Requiere_Revision_Interna] BIT NOT NULL DEFAULT(0),
    [Numero_Version] INT NOT NULL DEFAULT(1),
    [Nivel_Aprobacion_Actual] TINYINT NOT NULL DEFAULT(1),
    [Id_Estado] INT NOT NULL REFERENCES dbo.cls_estados(Id_Estado),
    [Id_Estado_Publicacion] INT NOT NULL REFERENCES dbo.cls_estados(Id_Estado),
    [Id_Responsable_Revision] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Fecha_Revision] DATETIME NULL,
    [Id_Responsable_Publicacion] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Fecha_Publicacion] DATETIME NULL,
    [Id_Evaluacion_Padre] INT NULL REFERENCES dbo.tbl_evaluaciones_instancias(Id_Evaluacion_Instancia),
    [Hash_Instancia] VARBINARY(64) NULL,
    [Observaciones_Revision] NVARCHAR(500) NULL,
    [Motivo_Rechazo] NVARCHAR(500) NULL,
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion] DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
    [RowVersion] ROWVERSION NOT NULL,
    CONSTRAINT UQ_tbl_evaluaciones_instancias_codigo UNIQUE([Codigo_Instancia]),
    CONSTRAINT CK_tbl_evaluaciones_instancias_Fechas CHECK ([Fecha_Limite] IS NULL OR [Fecha_Limite] >= [Fecha_Programada])
);
GO
/* JUSTIN ZAHIR*/

CREATE TABLE [dbo].[tbl_evaluaciones_alumnos]
(
    [Id_Evaluacion_Alumno] INT IDENTITY(1,1) PRIMARY KEY,
    [Codigo_Registro] VARCHAR(30) NOT NULL,
    [Id_Evaluacion_Instancia] INT NOT NULL REFERENCES dbo.tbl_evaluaciones_instancias(Id_Evaluacion_Instancia),
    [Id_Inscripcion] INT NOT NULL REFERENCES dbo.tbl_inscripciones(Id_Inscripcion),
    [Puntaje_Obtenido] DECIMAL(8,2) NOT NULL DEFAULT(0),
    [Porcentaje_Logrado] DECIMAL(6,2) NULL,
    [Puntaje_Normalizado] DECIMAL(6,4) NULL,
    [Es_Recalculo] BIT NOT NULL DEFAULT(0),
    [Numero_Recalculo] INT NOT NULL DEFAULT(0),
    [Motivo_Ajuste] NVARCHAR(500) NULL,
    [Observaciones] NVARCHAR(255) NULL,
    [Id_Usuario_Evaluador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Usuario_Validador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Fecha_Validacion] DATETIME NULL,
    [Id_Estado] INT NOT NULL REFERENCES cls_estados(Id_Estado),
    [Id_Estado_Publicacion] INT NULL REFERENCES cls_estados(Id_Estado),
    [Hash_Resultado] VARBINARY(64) NULL,
    [Id_Evaluacion_Reemplazada] INT NULL REFERENCES dbo.tbl_evaluaciones_alumnos(Id_Evaluacion_Alumno),
    [Firmado_Por_Estudiante] BIT NOT NULL DEFAULT(0),
    [Firma_Digital] NVARCHAR(255) NULL,
    [Fecha_Notificacion] DATETIME NULL,
    [Fecha_Publicacion] DATETIME NULL,
    [Fecha_Creacion] DATETIME NOT NULL,
    [Fecha_Modificacion] DATETIME NOT NULL,
    [Id_Creador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Modificador] INT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Id_Transaccion] INT NULL,
    [RowVersion] ROWVERSION NOT NULL,
    CONSTRAINT UQ_tbl_evaluaciones_alumnos_codigo UNIQUE([Codigo_Registro]),
    CONSTRAINT CK_tbl_evaluaciones_alumnos_Puntaje CHECK ([Puntaje_Obtenido] >= 0),
    CONSTRAINT CK_tbl_evaluaciones_alumnos_Porcentaje CHECK ([Porcentaje_Logrado] IS NULL OR ([Porcentaje_Logrado] BETWEEN 0 AND 100))
);
GO

CREATE TABLE [dbo].[tbl_evaluaciones_aprobaciones]
(
    [Id_Aprobacion] INT PRIMARY KEY IDENTITY(1,1),
    [Id_Evaluacion_Instancia] INT NULL REFERENCES dbo.tbl_evaluaciones_instancias(Id_Evaluacion_Instancia),
    [Id_Evaluacion_Alumno] INT NULL REFERENCES dbo.tbl_evaluaciones_alumnos(Id_Evaluacion_Alumno),
    [Id_Tipo_Aprobacion] INT NOT NULL REFERENCES dbo.cls_catalogos(Id_Catalogo),
    [Id_Usuario_Aprobador] INT NOT NULL REFERENCES dbo.tbl_usuarios(Id_Usuario),
    [Fecha_Aprobacion] DATETIME NOT NULL DEFAULT GETDATE(),
    [Id_Estado_Aprobacion] INT NOT NULL REFERENCES dbo.cls_estados(Id_Estado),
    [Observaciones] NVARCHAR(1000) NULL
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

/* Esta tabla fue movida antes de cls_evaluaciones_modelos para resolver dependencias */


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

CREATE TABLE [dbo].[cls_transacciones_roles] -- Tabla para definir que rol tiene alcance a que tipo de transacci�n.
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

CREATE TABLE [dbo].[cls_transacciones_estados] -- Tabla para definir que tipo de transacci�n tiene alcance a que estado. por ejemplo AGREGAR PERSONA tiene alcance al estado ACTIVO / PENDIENTE
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
	[Nombre_Boton] VARCHAR(50) NOT NULL,
	[Fecha_Creacion] DATETIME NOT NULL,
	[Fecha_Modificacion] DATETIME NOT NULL,
	[Id_Creador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Modificador] INT REFERENCES tbl_usuarios(Id_Usuario),
	[Id_Transaccion] INT,
	[Activo] BIT NOT NULL,
)
GO


/* ALAN ALFONSO */
CREATE TABLE[dbo].[cls_menus_roles] -- Tabla para definir que rol tiene acceso a que men�.
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

CREATE UNIQUE NONCLUSTERED INDEX UX_tbl_usuarios_Usuario
ON dbo.tbl_usuarios(Usuario);

CREATE NONCLUSTERED INDEX IX_tbl_usuarios_Id_Persona_Id_Estado
ON dbo.tbl_usuarios(Id_Persona, Id_Estado);

CREATE NONCLUSTERED INDEX IX_tbl_personas_Valor_Documento
ON dbo.tbl_personas(Valor_Documento);

CREATE NONCLUSTERED INDEX IX_tbl_personas_Id_Tipo_Documento_Id_Nacionalidad
ON dbo.tbl_personas(Id_Tipo_Documento, Id_Nacionalidad);

CREATE NONCLUSTERED INDEX IX_cls_materias_Codigo_Materia
ON dbo.cls_materias(Codigo_Materia);

CREATE NONCLUSTERED INDEX IX_tbl_periodos_IdTipoEstado
ON dbo.tbl_periodos_academicos(Id_Tipo_Periodo, Id_Estado, Es_Periodo_Actual);

CREATE NONCLUSTERED INDEX IX_tbl_becas_convocatorias_ProgramaPeriodo
ON dbo.tbl_becas_convocatorias(Id_Programa, Id_Periodo, Id_Estado, Id_Estado_Publicacion);

CREATE NONCLUSTERED INDEX IX_tbl_solicitudes_becas_ProgramaConvocatoriaEstado
ON dbo.tbl_solicitudes_becas(Id_Beca_Programa, Id_Convocatoria, Id_Estado, Id_Estado_Flujo, Id_Estudiante);

CREATE NONCLUSTERED INDEX IX_tbl_sanciones_academicas_EstudianteEstado
ON dbo.tbl_sanciones_academicas(Id_Estudiante, Id_Estado, Fecha_Registro);

CREATE NONCLUSTERED INDEX IX_cls_catalogos_Id_Tipo_Catalogo
ON dbo.cls_catalogos(Id_Tipo_Catalogo);

CREATE NONCLUSTERED INDEX IX_cls_becas_programas_Id_Tipo_Programa
ON dbo.cls_becas_programas(Id_Tipo_Programa, Id_Estado_Programa);

CREATE NONCLUSTERED INDEX IX_cls_materias_periodos_MateriaPeriodo
ON dbo.cls_materias_periodos(Id_Materia, Id_Periodo_Academico, Activo);

CREATE NONCLUSTERED INDEX IX_tbl_secciones_MateriaPeriodoEstado
ON dbo.tbl_secciones(Id_Materia_Periodo, Id_Estado, Id_Docente);

CREATE NONCLUSTERED INDEX IX_tbl_grupos_PeriodoEstado
ON dbo.tbl_grupos(Id_Periodo, Id_Estado);

CREATE NONCLUSTERED INDEX IX_tbl_grupos_inscripciones_GrupoEstado
ON dbo.tbl_grupos_inscripciones(Id_Grupo, Id_Estado);

CREATE NONCLUSTERED INDEX IX_tbl_grupos_inscripciones_InscripcionEstado
ON dbo.tbl_grupos_inscripciones(Id_Inscripcion, Id_Estado);

CREATE NONCLUSTERED INDEX IX_tbl_inscripciones_EstudianteEstado
ON dbo.tbl_inscripciones(Id_Estudiante, Id_Estado);

CREATE NONCLUSTERED INDEX IX_tbl_inscripciones_SeccionEstado
ON dbo.tbl_inscripciones(Id_Seccion, Id_Estado);

CREATE NONCLUSTERED INDEX IX_tbl_transacciones_Id_Tipo_Transaccion_Fecha_Creacion
ON dbo.tbl_transacciones(Id_Tipo_Transaccion, Fecha_Creacion);

CREATE NONCLUSTERED INDEX IX_cls_tipos_catalogos_Activo
ON dbo.cls_tipos_catalogos(Activo);

CREATE NONCLUSTERED INDEX IX_cls_catalogos_Activo_Fecha_Creacion
ON dbo.cls_catalogos(Activo, Fecha_Creacion);

CREATE NONCLUSTERED INDEX IX_cls_evaluaciones_modelos_MateriaPeriodo
ON dbo.cls_evaluaciones_modelos(Id_Materia_Periodo, Version_Configuracion, Activo);

CREATE NONCLUSTERED INDEX IX_tbl_evaluaciones_instancias_SeccionEstado
ON dbo.tbl_evaluaciones_instancias(Id_Seccion, Id_Estado_Publicacion, Fecha_Programada);

CREATE NONCLUSTERED INDEX IX_tbl_evaluaciones_alumnos_InstanciaEstado
ON dbo.tbl_evaluaciones_alumnos(Id_Evaluacion_Instancia, Id_Estado, Id_Estado_Publicacion);
