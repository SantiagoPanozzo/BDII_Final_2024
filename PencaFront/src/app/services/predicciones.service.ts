import { Injectable } from '@angular/core';
import { predicciones } from '../interfaces/predicciones';

@Injectable({
  providedIn: 'root'
})
export class PrediccionesService {
  private predicciones: predicciones[] = [{
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
  }];

  constructor() { }

  obtenerPrediccionesPorCedula(cedula: number): predicciones[] {
    return this.predicciones.filter(pred => pred.Cedula === cedula);
  }

  agregarOActualizarPrediccion(prediccion: predicciones): void {
    const index = this.predicciones.findIndex(p => p.Cedula === prediccion.Cedula && p.Equipo_E1 === prediccion.Equipo_E1 && p.Equipo_E2 === prediccion.Equipo_E2 && p.Fecha_partido === prediccion.Fecha_partido);
    if (index > -1) {
      this.predicciones[index] = prediccion;
    } else {
      this.predicciones.push(prediccion);
    }
  }

  obtenerTodasPredicciones(): predicciones[] {
    return this.predicciones;
  }
}


