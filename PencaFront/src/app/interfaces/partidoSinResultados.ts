import {Equipo} from "./equipo";
import {Etapa} from "./etapa";

export interface PartidoSinResultados {
    fecha : Date,
    equipo_E1: Equipo,
    equipo_E2 : Equipo,
    etapa : Etapa,
}