import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlumnoService } from '../services/alumno.service';
import { EquipoService } from '../services/equiposervice.service';
import { Alumno } from '../interfaces/alumnoInterface';
import { Equipo } from '../interfaces/equipo';
import { CarreraService } from '../services/carrera.service';
import { Carrera } from '../interfaces/carrera';

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
    carrera: 0,
    anioIngreso: 0,
    semestreIngreso: '',
    puntajeTotal: 0,
    campeon: '', 
    subcampeon: '' 
  };
  equipos: Equipo[] = [];
  carreras: Carrera[] = [];

  constructor(
    private alumnoService: AlumnoService, 
    private equipoService: EquipoService, 
    private router: Router, 
    private carreraService: CarreraService // Inyección correcta del servicio
  ) {}

  async ngOnInit() {
    this.equipos = await this.equipoService.obtenerEquipos();
    console.log(this.equipos[0])
    this.carreras = this.carreraService.obtenerCarreras();
  }

  registrar(): void {
    this.alumnoService.registrarUsuario(this.alumno);
    this.router.navigate(['/login']);
  }
}
