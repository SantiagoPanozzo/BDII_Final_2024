import {Equipo} from "./equipo";

export interface predicciones {
    Cedula : number,
	Equipo_E1 : Equipo,
	Equipo_E2 : Equipo,
	Fecha_partido : Date,
	Prediccion_E1 : number,
	Prediccion_E2 : number,
	Puntaje : number,
}