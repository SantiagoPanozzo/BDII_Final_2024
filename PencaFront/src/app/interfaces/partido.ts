import {Equipo} from "./equipo";
import {Etapa} from "./etapa";

export interface Partido {
    fecha : Date,
	equipo_E1: Equipo,
	equipo_E2 : Equipo,
	resultado_E1: number|null,
	resultado_E2 : number|null,
	etapa : Etapa,
  }