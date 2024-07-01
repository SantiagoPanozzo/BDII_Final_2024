---------Inserts Equipo------------------------------------------
INSERT INTO equipo (abreviatura, pais) VALUES ('uru', 'Uruguay');
INSERT INTO equipo (abreviatura, pais) VALUES ('arg', 'Argentina');
INSERT INTO equipo (abreviatura, pais) VALUES ('per', 'Peru');
INSERT INTO equipo (abreviatura, pais) VALUES ('chl', 'Chile');
INSERT INTO equipo (abreviatura, pais) VALUES ('can', 'Canada');
INSERT INTO equipo (abreviatura, pais) VALUES ('mex', 'Mexico');
INSERT INTO equipo (abreviatura, pais) VALUES ('ecu', 'Ecuador');
INSERT INTO equipo (abreviatura, pais) VALUES ('ven', 'Venezuela');
INSERT INTO equipo (abreviatura, pais) VALUES ('jam', 'Jamaica');
INSERT INTO equipo (abreviatura, pais) VALUES ('usa', 'Estados Unidos');
INSERT INTO equipo (abreviatura, pais) VALUES ('pan', 'Panama');
INSERT INTO equipo (abreviatura, pais) VALUES ('bol', 'Bolivia');
INSERT INTO equipo (abreviatura, pais) VALUES ('bra', 'Brasil');
INSERT INTO equipo (abreviatura, pais) VALUES ('col', 'Colombia');
INSERT INTO equipo (abreviatura, pais) VALUES ('pry', 'Paraguay');
INSERT INTO equipo (abreviatura, pais) VALUES ('cri', 'Costa Rica');

-- INSERT INTO Alumno (nombre, apellido, cedula, fecha_nacimiento, anio_ingreso, semestre_ingreso, puntaje_total, campeon, subcampeon) VALUES ('nombre', 'apellido', 123456789, '2021-01-01', 2021, 1, 0, 'uyu', 'arg');
-- INSERT INTO Alumno (nombre, apellido, cedula, fecha_nacimiento, anio_ingreso, semestre_ingreso, puntaje_total, campeon, subcampeon) VALUES ('nombre2', 'apellido2', 987654321, '2022-02-01', 2022, 2, 0, 'uyu', 'arg');
---------Inserts Carrera-----------------------------------------
INSERT INTO carrera(nombre) VALUES ('Abogacia');
INSERT INTO carrera(nombre) VALUES ('Acompañante Terapeutico');
INSERT INTO carrera(nombre) VALUES ('Agronomia');
INSERT INTO carrera(nombre) VALUES ('Analista en Informatica');
INSERT INTO carrera(nombre) VALUES ('Arquitectura');
INSERT INTO carrera(nombre) VALUES ('Artes Escenicas');
INSERT INTO carrera(nombre) VALUES ('Artes Visuales');
INSERT INTO carrera(nombre) VALUES ('Business Analytics');
INSERT INTO carrera(nombre) VALUES ('Ciencia Politica');
INSERT INTO carrera(nombre) VALUES ('Cine');
INSERT INTO carrera(nombre) VALUES ('Comunicacion');
INSERT INTO carrera(nombre) VALUES ('Comunicacion y Marketing');
INSERT INTO carrera(nombre) VALUES ('Contador Publico');
INSERT INTO carrera(nombre) VALUES ('Datos y Negocios');
INSERT INTO carrera(nombre) VALUES ('Desarrollador de Software');
INSERT INTO carrera(nombre) VALUES ('Direccion de Empresas');
INSERT INTO carrera(nombre) VALUES ('Economia');
INSERT INTO carrera(nombre) VALUES ('Educacion Inicial');
INSERT INTO carrera(nombre) VALUES ('Finanzas');
INSERT INTO carrera(nombre) VALUES ('Fisioterapia');
INSERT INTO carrera(nombre) VALUES ('Fonoaudiologia');
INSERT INTO carrera(nombre) VALUES ('Gestion Humana');
INSERT INTO carrera(nombre) VALUES ('Ingenieria Ambiental');
INSERT INTO carrera(nombre) VALUES ('Ingenieria en Alimentos');
INSERT INTO carrera(nombre) VALUES ('Ingenieria en Electronica');
INSERT INTO carrera(nombre) VALUES ('Ingeniería en Informática');
INSERT INTO carrera(nombre) VALUES ('Ingenieria Industrial');
INSERT INTO carrera(nombre) VALUES ('Inteligencia Artifical y Ciencia de Datos');
INSERT INTO carrera(nombre) VALUES ('Licenciatura en Enfermeria');
INSERT INTO carrera(nombre) VALUES ('Licenciatura en Enfermeria (Profesionalizacion)');
INSERT INTO carrera(nombre) VALUES ('Licenciatura en Informatica');
INSERT INTO carrera(nombre) VALUES ('Medicina');
INSERT INTO carrera(nombre) VALUES ('Negocios Internacionales');
INSERT INTO carrera(nombre) VALUES ('Negocios y Economia');
INSERT INTO carrera(nombre) VALUES ('Notariado');
INSERT INTO carrera(nombre) VALUES ('Nutricion');
INSERT INTO carrera(nombre) VALUES ('Odontologia');
INSERT INTO carrera(nombre) VALUES ('Psicologia');
INSERT INTO carrera(nombre) VALUES ('Psicomotricidad');
INSERT INTO carrera(nombre) VALUES ('Psicopedagogia');
INSERT INTO carrera(nombre) VALUES ('Recreacion Educativa');
INSERT INTO carrera(nombre) VALUES ('Sociologia');
INSERT INTO carrera(nombre) VALUES ('Trabajo Social');

--------Inserts Etapa----------------------------------
INSERT INTO Etapa(id, nombre) VALUES (1, 'Grupos');
INSERT INTO Etapa(id,nombre) VALUES (2, 'Cuartos');
INSERT INTO Etapa(id,nombre) VALUES (3, 'SemiFinal');
INSERT INTO Etapa(id,nombre) VALUES (4, 'Final');

--------Insert Administrador-------------------------------
INSERT INTO Administrador (nombre, apellido, cedula, fecha_nacimiento, rol_universidad, contrasena) VALUES ('juan alberto', 'administra', 7777777, '2000-01-01', 'Profesor', '$2a$11$tTJZt0x0.E.BLGv18KAZz./xSr4iyEE4vGSMtK2x0KX3o1N56U4iG');
