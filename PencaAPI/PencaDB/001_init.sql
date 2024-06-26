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
	constraint fk_partido_etapa foreign key (Etapa) references Etapa(Id),
	constraint check_equipos_diferentes check (Equipo_E1 <> Equipo_E2)
	--hay que hacer un trigger para controlar que no sean el mismo equipo y que no queden dados vuelta porque significa lo mismo.
);

CREATE UNIQUE INDEX unique_partido_ordenado ON Partido (
    Fecha,
    LEAST(Equipo_E1, Equipo_E2),
    GREATEST(Equipo_E1, Equipo_E2)
);

create table Administrador(
	Cedula int primary key,
    Nombre varchar(20) not null,
	Apellido varchar(20) not null,
	Fecha_Nacimiento date not null,
	Rol_Universidad varchar(100) not null,
	Contrasena varchar not null
	--constraint admin_no_alumno check (Cedula not in (SELECT Cedula FROM Alumno))

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
	Contrasena varchar not null,
	constraint fk_campeon foreign key (Campeon) references Equipo(Abreviatura),
	constraint fk_subcampeon foreign key (Subcampeon) references Equipo(Abreviatura),
	constraint check_campeon_diferente_subcampeon check (Campeon <> Subcampeon)
	--constraint alumno_no_admin check (Cedula not in (SELECT Cedula FROM Administrador))
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
	constraint fk_prediccion_partido foreign key (Equipo_E1, Equipo_E2, Fecha_partido) 
		references Partido(Equipo_E1, Equipo_E2, Fecha)
        ON UPDATE CASCADE ON DELETE CASCADE
);

create table Estudia(
	Cedula int not null,
	Id_Carrera int not null,
	Principal int not null,
	constraint pk_estudia primary key (Cedula, Id_carrera),
	constraint fk_alumno foreign key (Cedula) references Alumno(Cedula),
	constraint fk_carrera foreign key (Id_carrera) references Carrera(Id),
	constraint check_principal check (Principal IN (0, 1))
);

create table Notificacion (
    Id serial PRIMARY KEY,
    AlumnoCedula int not null,
    Mensaje text not null,
    FechaCreacion timestamp not null default CURRENT_TIMESTAMP,
    Visto boolean not null default FALSE,
    constraint fk_alumno_notificacion foreign key (AlumnoCedula) references Alumno(Cedula)
);



create unique index idx_unico_principal
on Estudia (Cedula)
where Principal = 1;

-- Función para verificar la exclusividad de cédula entre Administrador y Alumno
CREATE OR REPLACE FUNCTION check_admin_alumno_exclusivity()
RETURNS TRIGGER AS $$
BEGIN
    
    IF EXISTS (
        SELECT 1 FROM Administrador WHERE Cedula = NEW.Cedula
        INTERSECT
        SELECT 1 FROM Alumno WHERE Cedula = NEW.Cedula
    ) THEN
        RAISE EXCEPTION 'La cedula % no puede estar en ambas tablas Administrador y Alumno simultáneamente', NEW.Cedula;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Función para actualizar el puntaje del alumno
CREATE OR REPLACE FUNCTION actualizar_puntaje_alumno()
RETURNS TRIGGER AS $$
BEGIN
    
    IF TG_OP = 'UPDATE' AND NEW.Puntaje <> OLD.Puntaje THEN
        UPDATE Alumno
        SET Puntaje_Total = COALESCE(Puntaje_Total, 0) + NEW.Puntaje - COALESCE(OLD.Puntaje, 0)
        WHERE Alumno.cedula = NEW.cedula;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;


create trigger trigger_check_admin_alumno_exclusivity
before insert or update on Administrador
for each row
execute function check_admin_alumno_exclusivity();

create trigger trigger_check_admin_alumno_exclusivity
before insert or update on Alumno
for each row
execute function check_admin_alumno_exclusivity();



create trigger trigger_actualizar_puntaje_alumno
after update OF Puntaje ON Prediccion
for each row
execute function actualizar_puntaje_alumno();











