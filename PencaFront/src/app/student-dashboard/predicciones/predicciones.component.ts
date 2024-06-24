import { Component, OnInit } from '@angular/core';
import { PartidoService } from 'src/app/services/partido.service';
import { Partido } from 'src/app/interfaces/partido';
import { PrediccionesService } from 'src/app/services/predicciones.service';
import { predicciones } from 'src/app/interfaces/predicciones';
import { AuthService } from 'src/app/services/auth.service';
import {Etapa} from "../../interfaces/etapa";

@Component({
  selector: 'app-predicciones',
  templateUrl: './predicciones.component.html',
  styleUrls: ['./predicciones.component.css']
})
export class PrediccionesComponent implements OnInit {
  partidos: Partido[] = [];
  predicciones: { [key: number]: { prediccionE1: number, prediccionE2: number } } = {};
  alumno: any;
  partidosPorEtapa: { etapa: Etapa, partidos: Partido[] }[] = [];

  constructor(
    private partidoService: PartidoService,
    private prediccionesService: PrediccionesService,
    private authService: AuthService
  ) {}

  async ngOnInit() {
    this.partidos = await this.partidoService.obtenerPartidos();
    if(this.partidos && this.partidos.length > 0) {
      console.log(this.partidos[0])
      this.alumno = this.authService.obtenerUsuarioAutenticado();
      this.partidosPorEtapa = this.agruparPartidosPorEtapa(this.partidos);
      console.log(this.partidosPorEtapa[0])
    }
  }

  guardarPrediccion(partido: Partido): void {
    const prediccion = this.predicciones[partido.id];
    if (prediccion) {
      const nuevaPrediccion: predicciones = {
        Cedula: this.alumno.cedula,
        Equipo_E1: partido.equipo_E1,
        Equipo_E2: partido.equipo_E2,
        Fecha_partido: partido.fecha,
        Prediccion_E1: prediccion.prediccionE1,
        Prediccion_E2: prediccion.prediccionE2,
        Puntaje: this.alumno.puntajeTotal
      };
      console.log("Guardando prediccion:");
      console.log(nuevaPrediccion);
      this.prediccionesService.agregarOActualizarPrediccion(nuevaPrediccion);
      alert('Predicción guardada con éxito!');
    }
  }
  private agruparPartidosPorEtapa(partidos: Partido[]): { etapa: Etapa, partidos: Partido[] }[] {
    const partidosAgrupados: { etapa: Etapa, partidos: Partido[] }[] = [];
    partidos.forEach(partido => {
      console.log(partido);
      console.log("etapa: ")
      // @ts-ignore
      console.log(partido.etapa);
      const index = partidosAgrupados.findIndex(item => item.etapa === partido.etapa);
      if (index !== -1) {
        partidosAgrupados[index].partidos.push(partido);
      } else {
        partidosAgrupados.push({ etapa: partido.etapa, partidos: [partido] });
      }
    });

    return partidosAgrupados;
  }
}
