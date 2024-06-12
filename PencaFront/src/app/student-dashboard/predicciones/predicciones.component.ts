import { Component, OnInit } from '@angular/core';
import { PartidoService } from 'src/app/services/partido.service';
import { Partido } from 'src/app/interfaces/partidoInterface';
import { PrediccionesService } from 'src/app/services/predicciones.service';
import { predicciones } from 'src/app/interfaces/predicciones';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-predicciones',
  templateUrl: './predicciones.component.html',
  styleUrls: ['./predicciones.component.css']
})
export class PrediccionesComponent implements OnInit {
  partidos: Partido[] = [];
  predicciones: { [key: number]: { prediccionE1: number, prediccionE2: number } } = {};
  alumno: any;

  constructor(
    private partidoService: PartidoService,
    private prediccionesService: PrediccionesService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.partidos = this.partidoService.obtenerPartidos();
    this.alumno = this.authService.obtenerUsuarioAutenticado();
  }

  guardarPrediccion(partido: Partido): void {
    const prediccion = this.predicciones[partido.Id];
    if (prediccion) {
      const nuevaPrediccion: predicciones = {
        Cedula: this.alumno.cedula,
        Equipo_E1: partido.Equipo_E1,
        Equipo_E2: partido.Equipo_E2,
        Fecha_partido: partido.Fecha,
        Prediccion_E1: prediccion.prediccionE1,
        Prediccion_E2: prediccion.prediccionE2,
        Puntaje: this.alumno.puntajeTotal
      };
      this.prediccionesService.agregarOActualizarPrediccion(nuevaPrediccion);
      alert('Predicción guardada con éxito!');
    }
  }
}
