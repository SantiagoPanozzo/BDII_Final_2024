import { Injectable } from '@angular/core';
import { Carrera } from '../interfaces/carrera';
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class CarreraService {
  private carreras: Carrera[] = [];

  constructor(
      private http: HttpClient
  ) { }

  async obtenerCarreras(): Promise<Carrera[]> {
    this.carreras = (await this.http.get<Carrera[]>('http://localhost:8080/carrera').toPromise())!;
    return this.carreras;
  }

  async obtenerCarreraPorId(id: number) : Promise<Carrera> {
    return (await this.http.get<Carrera>('http://localhost:8080/carrera/' + id).toPromise())!;
  }

  async obtenerNombreCarrera(id: number): Promise<string> {
    let carrera = (await this.http.get<Carrera>('http://localhost:8080/carrera/' + id).toPromise());
    return carrera ? carrera.nombre : 'Carrera Desconocida';
  }
 
}
