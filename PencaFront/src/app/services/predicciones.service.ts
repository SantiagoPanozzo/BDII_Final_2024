import { Injectable } from '@angular/core';
import { predicciones } from '../interfaces/predicciones';

@Injectable({
  providedIn: 'root'
})
export class PrediccionesService {
  private predicciones: predicciones[] = [];

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


