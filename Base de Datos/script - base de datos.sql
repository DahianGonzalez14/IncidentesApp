CREATE DATABASE IncidentesDB;

GO

USE IncidentesDB;

GO

CREATE TABLE Departamento
(
DepartamentoId int not null identity (1,1),
Nombre varchar(100),
Estatus varchar(2),
Borrado bit,
FechaRegistro datetimeoffset,
FechaModificacion datetimeoffset,
CreadoPor int,
ModificadoPor int
);

GO

CREATE TABLE Puesto
(
PuestoId int not null identity (1,1),
DepartamentoId int,
Nombre varchar(100),
Estatus varchar(2),
Borrado bit,
FechaRegistro datetimeoffset,
FechaModificacion datetimeoffset,
CreadoPor int,
ModificadoPor int
);

GO

CREATE TABLE Usuario
(
UsuarioId int not null identity (1,1),
PuestoId int,
Nombre varchar(100),
Apellido varchar(100),
Cedula varchar(11),
Correo varchar(50),
Telefono varchar(15),
FechaNacimiento date,
NombreUsuario varchar(100),
Contrasena varchar(500),
Estatus varchar(2),
Borrado bit,
FechaRegistro datetimeoffset,
FechaModificacion datetimeoffset,
CreadoPor int,
ModificadoPor int
);

GO

CREATE TABLE Sla
(
SlaId int not null identity (1,1),
Descripcion varchar(200),
CantidadHoras int,
Estatus varchar(2),
Borrado bit,
FechaRegistro datetimeoffset,
FechaModificacion datetimeoffset,
CreadoPor int,
ModificadoPor int
);

GO

CREATE TABLE Prioridad
(
PrioridadId int not null identity (1,1),
SlaId int,
Nombre varchar(50),
Estatus varchar(2),
Borrado bit,
FechaRegistro datetimeoffset,
FechaModificacion datetimeoffset,
CreadoPor int,
ModificadoPor int
);

GO

CREATE TABLE Incidente
(
IncidenteId int not null identity (1,1),
UsuarioReportaId int,
UsuarioAsignadoId int,
PrioridadId int,
DepartamentoId int,
Titulo varchar(200),
Descripcion varchar(max),
FechaCierre datetimeoffset,
ComentarioCierre varchar(500),
Estatus varchar(2),
Borrado bit,
FechaRegistro datetimeoffset,
FechaModificacion datetimeoffset,
CreadoPor int,
ModificadoPor int
);

GO

CREATE TABLE HistorialIncidente
(
HistorialIncidenteId int not null identity (1,1),
IncidenteId int,
Comentario varchar(500),
Estatus varchar(2),
Borrado bit,
FechaRegistro datetimeoffset,
FechaModificacion datetimeoffset,
CreadoPor int,
ModificadoPor int
);

GO

CREATE TABLE Comentario
(
	ComentarioId int IDENTITY(1,1) NOT NULL,
	IncidenteId int NULL,
	UsuarioComentaId int NULL,
	Descripcion varchar (500) NULL,
	Estatus varchar(2) NULL,
	Borrado bit NULL,
	FechaRegistro datetimeoffset(7) NULL,
	FechaModificacion datetimeoffset(7) NULL,
	CreadoPor int NULL,
	ModificadoPor int NULL
)

GO

-----CLAVES PRIMARIAS------

ALTER TABLE Usuario
ADD PRIMARY KEY (UsuarioId);

GO

ALTER TABLE Departamento
ADD PRIMARY KEY (DepartamentoId);

GO 

ALTER TABLE Sla
ADD PRIMARY KEY (SlaId);

GO

ALTER TABLE Prioridad
ADD PRIMARY KEY (PrioridadId);

GO

ALTER TABLE Incidente
ADD PRIMARY KEY (IncidenteId);

GO

ALTER TABLE HistorialIncidente
ADD PRIMARY KEY (HistorialIncidenteId);

GO

ALTER TABLE Puesto
ADD PRIMARY KEY (PuestoId);

GO

ALTER TABLE Comentario
ADD PRIMARY KEY (ComentarioId);

-----CLAVES FORANEAS-----

--ALTER TABLE Usuario
--ADD FOREIGN KEY (PuestoId) REFERENCES Puesto(PuestoId);

--GO

--ALTER TABLE Usuario
--ADD FOREIGN KEY (CreadoPor) REFERENCES Usuario(UsuarioId);

--GO

--ALTER TABLE Usuario
--ADD FOREIGN KEY (ModificadoPor) REFERENCES Usuario(UsuarioId);

--GO

--ALTER TABLE Departamento
--ADD FOREIGN KEY (CreadoPor) REFERENCES Usuario(UsuarioId);

--GO

--ALTER TABLE Departamento
--ADD FOREIGN KEY (ModificadoPor) REFERENCES Usuario(UsuarioId);

--GO

--ALTER TABLE Puesto
--ADD FOREIGN KEY (DepartamentoId) REFERENCES Departamento(DepartamentoId);

--GO

--ALTER TABLE Puesto
--ADD FOREIGN KEY (CreadoPor) REFERENCES Usuario(UsuarioId);

--GO

--ALTER TABLE Puesto
--ADD FOREIGN KEY (ModificadoPor) REFERENCES Usuario(UsuarioId);

--GO

--ALTER TABLE Sla
--ADD FOREIGN KEY (CreadoPor) REFERENCES Usuario(UsuarioId);

--GO

--ALTER TABLE Sla
--ADD FOREIGN KEY (ModificadoPor) REFERENCES Usuario(UsuarioId);

--GO

--ALTER TABLE Prioridad
--ADD FOREIGN KEY (SlaId) REFERENCES Sla(SlaId);

--GO

--ALTER TABLE Prioridad
--ADD FOREIGN KEY (CreadoPor) REFERENCES Usuario(UsuarioId);

--GO

--ALTER TABLE Prioridad
--ADD FOREIGN KEY (ModificadoPor) REFERENCES Usuario(UsuarioId);

--GO

--ALTER TABLE Incidente
--ADD FOREIGN KEY (UsuarioReportaId) REFERENCES Usuario(UsuarioId);

--GO

--ALTER TABLE Incidente
--ADD FOREIGN KEY (UsuarioAsignadoId) REFERENCES Usuario(UsuarioId);

--GO

--ALTER TABLE Incidente
--ADD FOREIGN KEY (PrioridadId) REFERENCES Prioridad(PrioridadId);

--GO

--ALTER TABLE Incidente
--ADD FOREIGN KEY (DepartamentoId) REFERENCES Departamento(DepartamentoId);

--GO

--ALTER TABLE Incidente
--ADD FOREIGN KEY (CreadoPor) REFERENCES Usuario(UsuarioId);

--GO

--ALTER TABLE Incidente
--ADD FOREIGN KEY (ModificadoPor) REFERENCES Usuario(UsuarioId);

--GO

--ALTER TABLE HistorialIncidente
--ADD FOREIGN KEY (IncidenteId) REFERENCES Incidente(IncidenteId);

--GO

--ALTER TABLE HistorialIncidente
--ADD FOREIGN KEY (CreadoPor) REFERENCES Usuario(UsuarioId);

--GO

--ALTER TABLE HistorialIncidente
--ADD FOREIGN KEY (ModificadoPor) REFERENCES Usuario(UsuarioId);