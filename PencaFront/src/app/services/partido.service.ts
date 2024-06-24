import { Injectable } from '@angular/core';
import { Partido } from '../interfaces/partido';
import { EquipoService } from './equiposervice.service';
import {Equipo} from "../interfaces/equipo";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class PartidoService {
  partidos: Partido[] = [];
  

  constructor(
      private equipoService: EquipoService,
      private http: HttpClient
  ) { }

  async obtenerPartidos(): Promise<Partido[]> {
    this.partidos = (await this.http.get<Partido[]>('http://localhost:8080/partido').toPromise())!;
    return this.partidos;
  }

  actualizarResultado(id: number, resultado_E1: number, resultado_E2: number): void {
    const partido = this.partidos.find(p => p.id === id);
    if (partido) {
      partido.resultado_E1 = resultado_E1;
      partido.resultado_E2 = resultado_E2;
    }
  }
  obtenerPartidoPorId(id: number): Partido | undefined {
    return this.partidos.find(partido => partido.id === id)
  }
  registrarPartido(nuevoPartido: Partido): void {
    const nuevoId = this.partidos.length > 0 ? this.partidos[this.partidos.length - 1].id + 1 : 1;
    nuevoPartido.id = nuevoId;
    this.partidos.push(nuevoPartido);
}


}
 

  
  
  
 
  

