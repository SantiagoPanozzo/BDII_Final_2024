import { Injectable } from '@angular/core';
import { Partido } from '../interfaces/partido';
import { EquipoService } from './equiposervice.service';
import {Equipo} from "../interfaces/equipo";

@Injectable({
  providedIn: 'root'
})
export class PartidoService {
   partidos: Partido[] = [];
  

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
 

  
  
  
 
  

