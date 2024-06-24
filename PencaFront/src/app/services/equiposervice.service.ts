

import { Injectable } from '@angular/core';
import { Equipo } from '../interfaces/equipo';
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class EquipoService {
<<<<<<< HEAD
  private equipos: Equipo[] = [];
=======
  private equipos: Equipo[] = [
    { abreviatura: 'arg', nombre: 'Argentina' },
    { abreviatura: 'bra', nombre: 'Brasil' },
   
  ];
>>>>>>> 1d7c430 (Register funciona)

  constructor(
      private http: HttpClient
  ) { }

  async obtenerEquipos(): Promise<Equipo[]> {
    this.equipos = (await this.http.get<Equipo[]>('http://localhost:8080/equipo').toPromise())!;
    return this.equipos;
  }

  async obtenerEquipoPorAbreviatura(abreviatura: string) : Promise<Equipo> {
    return (await this.http.get<Equipo>('http://localhost:8080/equipo/' + abreviatura).toPromise())!;
  }

  async obtenerNombreEquipo(abreviatura: string): Promise<string> {
    const equipo = (await this.http.get<Equipo>('http://localhost:8080/equipo/' + abreviatura).toPromise())!;
    return equipo ? equipo.pais : 'Equipo Desconocido';
  }
}

