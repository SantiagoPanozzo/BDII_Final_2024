import { Injectable } from '@angular/core';
import { predicciones } from '../interfaces/predicciones';

@Injectable({
  providedIn: 'root'
})
export class PrediccionesService {
  private predicciones: predicciones[] = [
    {
      Cedula: 123456789,
      Equipo_E1: 'Equipo 1',
      Equipo_E2: 'Equipo 2',
      Fecha_partido: new Date('2024-06-20'),
      Prediccion_E1: 1,
      Prediccion_E2: 2,
      Puntaje: 0
    },
    {
      Cedula: 987654321,
      Equipo_E1: 'Equipo A',
      Equipo_E2: 'Equipo B',
      Fecha_partido: new Date('2024-06-21'),
      Prediccion_E1: 2,
      Prediccion_E2: 2,
      Puntaje: 0
    },
    
  ];

  constructor() { }

  obtenerTodasPredicciones(): predicciones[] {
    return this.predicciones;
  }

  editarPrediccion_E1(cedula: number, nuevaPrediccion: number): void {
    const prediccion = this.predicciones.find(p => p.Cedula === cedula);
    if (prediccion) {
      prediccion.Prediccion_E1 = nuevaPrediccion;
    }
  }

  editarPrediccion_E2(cedula: number, nuevaPrediccion: number): void {
    const prediccion = this.predicciones.find(p => p.Cedula === cedula);
    if (prediccion) {
      prediccion.Prediccion_E2 = nuevaPrediccion;
    }
  }
}
