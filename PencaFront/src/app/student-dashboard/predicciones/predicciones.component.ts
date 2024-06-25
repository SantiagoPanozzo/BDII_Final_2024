import { Component, OnInit } from '@angular/core';
import { PartidoService } from 'src/app/services/partido.service';
import { Partido } from 'src/app/interfaces/partido';
import { PrediccionesService } from 'src/app/services/predicciones.service';
import { Prediccion } from 'src/app/interfaces/prediccion';
import { AuthService } from 'src/app/services/auth.service';
import {Etapa} from "../../interfaces/etapa";
import {Alumno} from "../../interfaces/alumnoInterface";
import {AlumnoService} from "../../services/alumno.service";

@Component({
  selector: 'app-predicciones',
  templateUrl: './predicciones.component.html',
  styleUrls: ['./predicciones.component.css']
})
export class PrediccionesComponent implements OnInit {
  predicciones: Prediccion[] = [];
  alumno: any;
  partidosPorEtapa: { etapa: Etapa, predicciones: Prediccion[] }[] = [];

  constructor(
    private partidoService: PartidoService,
    private prediccionesService: PrediccionesService,
    private authService: AuthService,
    private alumnoService: AlumnoService
  ) {}

  async ngOnInit() {
    console.log("Autenticado como " + this.authService.obtenerUsuarioAutenticado());
    this.alumno = await this.alumnoService.obtenerUsuarioPorCedula(this.authService.obtenerUsuarioAutenticado());
    this.predicciones = await this.prediccionesService.obtenerPrediccionesByAlumno(this.alumno);

    if(this.predicciones && this.predicciones.length > 0) {
      this.agruparPartidosPorEtapa();
    }
  }

  guardarPrediccion(prediccion: Prediccion): void {
    console.log("Prediccion a guardar:")
    console.log(prediccion)
    this.prediccionesService.agregarOActualizarPrediccion(prediccion);
  }

  private agruparPartidosPorEtapa() {
    this.predicciones.forEach(prediccion => {
      const index = this.partidosPorEtapa.findIndex(item => item.etapa === prediccion.partido.etapa);
      if (index !== -1) {
        this.partidosPorEtapa[index].predicciones.push(prediccion);
      } else {
        this.partidosPorEtapa.push({ etapa: prediccion.partido.etapa, predicciones: [prediccion] });
      }
    });
  }
}
