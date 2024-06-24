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

  async modificarPartido(abreviatura_1: string, abreviatura_2: string, fecha: Date, partido: Partido){
    const ruta = `http://localhost:8080/partido/${abreviatura_1}/${abreviatura_2}/${fecha}`;
    console.log("PUT a: " + ruta);
    console.log(partido);
    await this.http.put(ruta, partido).toPromise();
  }


}
 

  
  
  
 
  

