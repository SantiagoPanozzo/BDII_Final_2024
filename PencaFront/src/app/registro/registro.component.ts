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
    carreraPrincipal: {} as Carrera,
    anioIngreso: 0,
    semestreIngreso: '',
    puntajeTotal: 0,
    campeon: {} as Equipo,
    subCampeon: {} as Equipo
  };
  equipos: Equipo[] = [];
  carreras: Carrera[] = [];

  constructor(
    private alumnoService: AlumnoService, 
    private equipoService: EquipoService, 
    private router: Router, 
    private carreraService: CarreraService // Inyección correcta del servicio
  ) {}

  async ngOnInit(): Promise<void> {
    this.equipos = await this.equipoService.obtenerEquipos();
    this.carreras = await this.carreraService.obtenerCarreras();
  }

  async registrar() {
    this.alumno.carreraPrincipal = await this.carreraService.obtenerCarreraPorId(this.alumno.carreraPrincipal.id);
    this.alumno.campeon = await this.equipoService.obtenerEquipoPorAbreviatura(this.alumno.campeon.abreviatura);
    this.alumno.subCampeon = await this.equipoService.obtenerEquipoPorAbreviatura(this.alumno.subCampeon.abreviatura);
    console.log("Registrando: ")
    console.log(this.alumno);
    this.alumnoService.registrarUsuario(this.alumno);
    //this.router.navigate(['/login']);
  }
}
