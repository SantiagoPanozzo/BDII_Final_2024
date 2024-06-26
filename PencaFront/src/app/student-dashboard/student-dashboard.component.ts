import { Component, OnInit } from '@angular/core';
import { Alumno } from '../interfaces/alumnoInterface';
import {Router} from "@angular/router";
import {AuthService} from "../services/auth.service";
import {AlumnoService} from "../services/alumno.service";

@Component({
  selector: 'app-student-dashboard',
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.css']
})
export class StudentDashboardComponent implements OnInit {
  usuario: Alumno = {} as Alumno;

  constructor(
      private router: Router,
      private authService: AuthService,
      private alumnoService: AlumnoService
  ) {}

  async ngOnInit() {
    this.usuario = (await this.alumnoService.obtenerUsuarioPorCedula(this.authService.obtenerUsuarioAutenticado()))!;
  }
}
