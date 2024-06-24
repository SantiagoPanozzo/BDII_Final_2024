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
<<<<<<< HEAD
>>>>>>> 3054a36 (Lista de partidos en predicciones anda)
  
=======
>>>>>>> 308e0aa (Partido cambiar resultado)

  constructor(
      private equipoService: EquipoService,
      private http: HttpClient
  ) { }

  async obtenerPartidos(): Promise<Partido[]> {
    this.partidos = (await this.http.get<Partido[]>('http://localhost:8080/partido').toPromise())!;
    return this.partidos;
  }

  async actualizarResultado(abreviatura_1: string, abreviatura_2: string, fecha: Date, resultado_E1: number, resultado_E2: number) {
    const url = `http://localhost:8080/partido/${abreviatura_1}/${abreviatura_2}/${fecha}`;
    const x = (await (this.http.get<Partido>(url)).toPromise())!;
    x.resultado_E1 = resultado_E1;
    x.resultado_E2 = resultado_E2;
    console.log("Put to: " + url);
    console.log(x);
    await this.http.put(url, x).toPromise();
  }
  async obtenerPartidoPorId(fecha: Date, abreviatura_1: string, abreviatura_2: string): Promise<Partido> {
    return (await this.http.get<Partido>(`http://localhost:8080/partido/${abreviatura_1}/${abreviatura_2}/${fecha}`).toPromise())!;
  }

  registrarPartido(nuevoPartido: Partido): void {
    console.log("POST de:")
    console.log(nuevoPartido)
    this.http.post('http://localhost:8080/partido', nuevoPartido).subscribe();
  }


}
 

  
  
  
 
  

