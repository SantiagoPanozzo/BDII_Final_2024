

import { Injectable } from '@angular/core';
import { Equipo } from '../interfaces/equipo';

@Injectable({
  providedIn: 'root'
})
export class EquipoService {
  private equipos: Equipo[] = [
    { abreviatura: 'arg', nombre: 'Argentina' },
    { abreviatura: 'bra', nombre: 'Brasil' },
   
  ];

  constructor() { }

  obtenerEquipos(): Equipo[] {
    return this.equipos;
  }
  obtenerNombreEquipo(abreviatura: string): string {
    const equipo = this.equipos.find(equipo => equipo.abreviatura === abreviatura);
    return equipo ? equipo.nombre : 'Equipo Desconocido'; 
  }
}

