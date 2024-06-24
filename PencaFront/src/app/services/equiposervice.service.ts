

import { Injectable } from '@angular/core';
import { Equipo } from '../interfaces/equipo';
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class EquipoService {
  private equipos: Equipo[] = [];

  constructor(
      private http: HttpClient
  ) { }

  async obtenerEquipos(): Promise<Equipo[]> {
    this.equipos = (await this.http.get<Equipo[]>('http://localhost:8080/equipo').toPromise())!;
    return this.equipos;
  }
  async obtenerNombreEquipo(abreviatura: string): Promise<string> {
    const equipo = (await this.http.get<Equipo>('http://localhost:8080/equipo/' + abreviatura).toPromise())!;
    return equipo ? equipo.pais : 'Equipo Desconocido';
  }
}

