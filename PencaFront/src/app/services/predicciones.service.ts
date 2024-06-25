import { Injectable } from '@angular/core';
import {Prediccion} from '../interfaces/prediccion';
import {HttpClient} from "@angular/common/http";
import {Alumno} from "../interfaces/alumnoInterface";
import {Partido} from "../interfaces/partido";
import {PartidoService} from "./partido.service";

@Injectable({
  providedIn: 'root'
})
export class PrediccionesService {
  constructor(
      private partidoService: PartidoService,
      private http: HttpClient
  ) { }

  async obtenerPrediccionesByAlumno(alumno: Alumno): Promise<Prediccion[]> {
    console.log("Obteniendo partidos");
    const partidos: Partido[] = await this.partidoService.obtenerPartidos()
    console.log(partidos);
    const predicciones: Prediccion[] = (await this.http.get<Prediccion[]>('http://localhost:8080/prediccion').toPromise())!.filter(x => x.alumno === alumno);
    for (const partido of partidos) {
      let x = predicciones.filter(x => x.partido === partido);
      if(!x || x.length == 0){
        predicciones.push({
          partido: partido,
          alumno: alumno,
          puntaje: 0,
        } as Prediccion)
      }
    }
    console.log("Predicciones obtenidas o creadas")
    console.log(predicciones)
    return predicciones;
  }

  async obtenerPrediccion(partido: Partido) {
    const abreviatura_1 = partido.equipo_E1.abreviatura;
    const abreviatura_2 = partido.equipo_E2.abreviatura;
    const fecha = partido.fecha;
    const url = `http://localhost:8080/predicciones/${abreviatura_1}/${abreviatura_2}/${fecha}`;
    return (await this.http.get<Prediccion>(url).toPromise())!;
  }

  async agregarOActualizarPrediccion(prediccion: Prediccion) {
    let res : Prediccion | null = null;
    try {
      res = (await this.obtenerPrediccion(prediccion.partido));
    }
    catch (error) {}
    if (res == null) {
      await this.http.post('http://localhost:8080/predicciones', prediccion).toPromise();
    } else {
      const abreviatura_1 = prediccion.partido.equipo_E1.abreviatura;
      const abreviatura_2 = prediccion.partido.equipo_E2.abreviatura;
      const fecha = prediccion.partido.fecha;
      const url = `http://localhost:8080/predicciones/${abreviatura_1}/${abreviatura_2}/${fecha}`;
      res.prediccion_E1 = prediccion.prediccion_E1;
      res.prediccion_E2 = prediccion.prediccion_E2;
      await this.http.put(url, res).toPromise();
    }
  }


}


