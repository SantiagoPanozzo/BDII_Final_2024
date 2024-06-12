
create table Carrera(
	Id serial primary key,
	Nombre varchar(100) not null UNIQUE
);

create table Equipo(
	Abreviatura varchar(3) primary key ,
	Pais varchar(50) not null UNIQUE
);

create table Etapa(
	Id serial primary key,
	Nombre varchar(20) not null UNIQUE
);

create table Partido(
	Fecha timestamp not null,
	Equipo_E1 varchar(3) not null,
	Equipo_E2 varchar(3) not null,
	Resultado_E1 int,
	Resultado_E2 int,
	Etapa int not null,
	constraint pk_partido primary key (Fecha, Equipo_E1, Equipo_E2),
	constraint fk_partido_equipo_1 foreign key (Equipo_E1) references Equipo(Abreviatura),
	constraint fk_partido_equipo_2 foreign key (Equipo_E2) references Equipo(Abreviatura),
	constraint fk_partido_etapa foreign key (Etapa) references Etapa(Id)	
	--hay que hacer un trigger para controlar que no sean el mismo equipo.
);

create table Administrador(
	Cedula int primary key,
    Nombre varchar(20) not null,
	Apellido varchar(20) not null,
	Fecha_Nacimiento date not null,
	Rol_Universidad varchar(100) not null
);

create table Alumno(
	Cedula int primary key,
    Nombre varchar(20) not null,
	Apellido varchar(20) not null,
	Fecha_Nacimiento date not null,
	Anio_Ingreso int not null,
	Semestre_Ingreso int not null,
	Puntaje_Total int not null,
	Campeon varchar(3) not null,
	Subcampeon varchar(3) not null,
	constraint fk_campeon foreign key (Campeon) references Equipo(Abreviatura),
	constraint fk_subcampeon foreign key (Subcampeon) references Equipo(Abreviatura)
	--hay que hacer un trigger para controlar que campeon y subcampeon sean diferentes
);

create table Prediccion(
	Cedula int not null,
	Equipo_E1 varchar(3) not null,
	Equipo_E2 varchar(3) not null,
	Fecha_partido timestamp not null,
	Prediccion_E1 int,
	Prediccion_E2 int,
	Puntaje int,
	constraint pk_prediccion primary key (Cedula, Equipo_E1, Equipo_E2, Fecha_partido),
	constraint fk_prediccion_alumno foreign key (Cedula) references Alumno(Cedula),
	constraint fk_prediccion_partido foreign key (Equipo_E1, Equipo_E2, Fecha_partido) references Partido(Equipo_E1, Equipo_E2, Fecha)
);

create table Estudia(
	Cedula int not null,
	Id_Carrera int not null,
	constraint pk_estudia primary key (Cedula, Id_carrera),
	constraint fk_alumno foreign key (Cedula) references Alumno(Cedula),
	constraint fk_carrera foreign key (Id_carrera) references Carrera(Id)
);











