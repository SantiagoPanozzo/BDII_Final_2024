import {Equipo} from "./equipo";
import {Alumno} from "./alumnoInterface";
import {Partido} from "./partido";

export interface Prediccion {
    alumno: Alumno,
	partido: Partido,
	prediccion_E1: number,
	prediccion_E2: number,
	puntaje: number
}