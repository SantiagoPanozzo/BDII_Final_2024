import { Injectable } from '@angular/core';
import { Partido } from '../interfaces/partido';
import { EquipoService } from './equiposervice.service';
import {Equipo} from "../interfaces/equipo";

@Injectable({
  providedIn: 'root'
})
export class PartidoService {
<<<<<<< HEAD
   partidos: Partido[] = [];
=======
   partidos: Partido[] = [
    
    {
    Id: 1,
    Fecha: new Date('2024-06-7'),
	  Equipo_E1:'arg',
	  Equipo_E2 : 'bra',
	  Resultado_E1: null,
	  Resultado_E2 : null,
	  Etapa : 1,
    Hora: '18:00'
     
    },
    {
    Id: 2,
    Fecha: new Date('2023-06-9'),
	  Equipo_E1:'arg',
	  Equipo_E2 : 'bra',
	  Resultado_E1: null,
	  Resultado_E2 : null,
	  Etapa : 2,
    Hora: '17:00'
    },
    {
      Id: 3,
      Fecha: new Date('2024-07-7'),
      Equipo_E1:'arg',
      Equipo_E2 : 'bra',
      Resultado_E1: null,
      Resultado_E2 : null,
      Etapa : 1,
      Hora: '18:00'
       
      }
  ];
>>>>>>> 1d7c430 (Register funciona)
  

  constructor(private equipoService: EquipoService) { }

  obtenerPartidos(): Partido[] {
    return this.partidos;
  }

  actualizarResultado(id: number, resultado_E1: number, resultado_E2: number): void {
    const partido = this.partidos.find(p => p.Id === id);
    if (partido) {
      partido.Resultado_E1 = resultado_E1;
      partido.Resultado_E2 = resultado_E2;
    }
  }
  obtenerPartidoPorId(id: number): Partido | undefined {
    return this.partidos.find(partido => partido.Id === id)  
  }
  registrarPartido(nuevoPartido: Partido): void {
    const nuevoId = this.partidos.length > 0 ? this.partidos[this.partidos.length - 1].Id + 1 : 1;
    nuevoPartido.Id = nuevoId;
    this.partidos.push(nuevoPartido);
}


}
 

  
  
  
 
  

