import {Equipo} from "./equipo";
import {Carrera} from "./carrera";

export interface Alumno {
    cedula : number;
    contrasena: string;
    nombre: string;
    apellido: string;
    fechaNacimiento: Date;
    carreraPrincipal: Carrera;
    anioIngreso: number;
    semestreIngreso: string;
    puntajeTotal: number;
    campeon: Equipo;
    subCampeon: Equipo;
  }