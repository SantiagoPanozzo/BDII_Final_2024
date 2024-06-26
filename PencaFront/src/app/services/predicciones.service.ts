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
    let predicciones = (await this.http.get<Prediccion[]>('http://localhost:8080/prediccion').toPromise());
    console.log("Predicciones obtenidas: ")
    console.log(predicciones);
    if (!predicciones || predicciones.length <= 0){
      console.log("NULO!");
      predicciones = [];
    } else {
      console.log("NO NULO!")
      console.log("Alumno:")
      console.log(alumno)
      predicciones = predicciones.filter(x => x.alumno.cedula == alumno.cedula);
      console.log("Predicciones filtradas")
      console.log(predicciones)
    }

    for (const partido of partidos) {
      let x = predicciones.filter(x => x.partido.fecha === partido.fecha && x.partido.equipo_E1.abreviatura === partido.equipo_E1.abreviatura && x.partido.equipo_E2.abreviatura === partido.equipo_E2.abreviatura);
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

  async obtenerPrediccion(partido: Partido, alumno: Alumno) {
    const abreviatura_1 = partido.equipo_E1.abreviatura;
    const abreviatura_2 = partido.equipo_E2.abreviatura;
    const fecha = partido.fecha;
    const cedula = alumno.cedula;
    const url = `http://localhost:8080/prediccion/${abreviatura_1}/${abreviatura_2}/${fecha}/${cedula}`;
    return (await this.http.get<Prediccion>(url).toPromise())!;
  }

  async agregarOActualizarPrediccion(prediccion: Prediccion) {
    console.log("LLEGA ACA")
    console.log(prediccion)
    let res : Prediccion | null = null;
    try {
      res = (await this.obtenerPrediccion(prediccion.partido, prediccion.alumno));
      console.log("RES: ")
      console.log(res)
    }
    catch (error) {}
    if (res == null) {
      console.log("POST a http://localhost:8080/prediccion con")
      console.log(prediccion)
      await this.http.post('http://localhost:8080/prediccion', prediccion).toPromise();
    } else {
      const abreviatura_1 = prediccion.partido.equipo_E1.abreviatura;
      const abreviatura_2 = prediccion.partido.equipo_E2.abreviatura;
      const fecha = prediccion.partido.fecha;
      const alumno = prediccion.alumno.cedula;
      const url = `http://localhost:8080/prediccion/${abreviatura_1}/${abreviatura_2}/${fecha}/${alumno}`;
      res.prediccion_E1 = prediccion.prediccion_E1;
      res.prediccion_E2 = prediccion.prediccion_E2;
      await this.http.put(url, res).toPromise();
    }
  }


}


