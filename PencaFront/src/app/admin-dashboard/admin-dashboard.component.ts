import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {AuthService} from "../services/auth.service";
import {AlumnoService} from "../services/alumno.service";
import {Alumno} from "../interfaces/alumnoInterface";

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
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


  

