import {Equipo} from "./equipo";
import {Etapa} from "./etapa";

export interface Partido {
    Id: number,
    Fecha : Date,
	Equipo_E1: Equipo,
	Equipo_E2 : Equipo,
	Resultado_E1: number|null,
	Resultado_E2 : number|null,
	Etapa : Etapa,
  }