import { Injectable } from '@angular/core';
import { Carrera } from '../interfaces/carrera';

@Injectable({
  providedIn: 'root'
})
export class CarreraService {
  private carreras: Carrera[] = [
    { id: 1, nombre: 'Ingeniería de Sistemas' },
    { id: 2, nombre: 'Medicina' },
    { id: 3, nombre: 'Derecho' },
    { id: 4, nombre: 'Arquitectura' }
  ];

  constructor() { }

  obtenerCarreras(): Carrera[] {
    return this.carreras;
  }

  obtenerNombreCarrera(id: number): string {
    const carrera = this.carreras.find(carrera => carrera.id === id);
    return carrera ? carrera.nombre : 'Carrera Desconocida';
  }
 
}
