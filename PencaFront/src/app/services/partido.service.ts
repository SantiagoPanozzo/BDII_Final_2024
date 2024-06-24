import { Injectable } from '@angular/core';
import { Partido } from '../interfaces/partido';
import { EquipoService } from './equiposervice.service';
import {Equipo} from "../interfaces/equipo";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class PartidoService {
<<<<<<< HEAD
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
=======
  partidos: Partido[] = [];
>>>>>>> 3054a36 (Lista de partidos en predicciones anda)
  

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
 

  
  
  
 
  

