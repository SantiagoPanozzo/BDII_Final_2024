export interface Partido {
    Id: number,
    Fecha : Date,
	Equipo_E1: string,
	Equipo_E2 : string,
	Resultado_E1: number|null,
	Resultado_E2 : number|null,
	Etapa : number,
    Hora : string
  }