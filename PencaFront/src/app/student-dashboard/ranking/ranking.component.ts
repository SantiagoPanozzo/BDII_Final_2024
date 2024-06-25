import {Component, OnInit} from '@angular/core';
import {AlumnoService} from "../../services/alumno.service";
import {Alumno} from "../../interfaces/alumnoInterface";
import {PuntajeComponent} from "../puntaje/puntaje.component";

@Component({
  selector: 'app-ranking',
  templateUrl: './ranking.component.html',
  styleUrls: ['./ranking.component.css']
})
export class RankingComponent implements OnInit {
  constructor(
      private alumnoService: AlumnoService
  ) {}

  alumnos: Alumno[] = [];

  async ngOnInit() {
    this.alumnos = await this.alumnoService.obtenerRanking();
  }
}
