import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlumnoService } from '../services/alumno.service';
import { EquipoService } from '../services/equiposervice.service';
import { Alumno } from '../interfaces/alumnoInterface';
import { Equipo } from '../interfaces/equipo';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {
  alumno: Alumno = {
    cedula: 0,
    contrasena: '',
    nombre: '',
    apellido: '',
    fechaNacimiento: new Date(),
    anioIngreso: 0,
    semestreIngreso: '',
    puntajeTotal: 0,
    campeon: '', 
    subcampeon: '' 
  };
  equipos: Equipo[] = [];

  constructor(private alumnoService: AlumnoService, private equipoService: EquipoService, private router: Router) {}

  ngOnInit(): void {
    this.equipos = this.equipoService.obtenerEquipos();
  }

  registrar(): void {
    this.alumnoService.registrarUsuario(this.alumno);
    this.router.navigate(['/login']);
  }
}
