import { Component, OnInit } from '@angular/core';
import { Alumno } from 'src/app/interfaces/alumnoInterface';
import { AuthService } from 'src/app/services/auth.service';
import { AlumnoService } from 'src/app/services/alumno.service';
import { CarreraService } from 'src/app/services/carrera.service'; 
import { EquipoService } from 'src/app/services/equiposervice.service';
import {Equipo} from "../../interfaces/equipo";
import { Carrera } from 'src/app/interfaces/carrera';

@Component({
  selector: 'app-datos',
  templateUrl: './datos.component.html',
  styleUrls: ['./datos.component.css']
})
export class DatosComponent implements OnInit {
  usuarioAutenticado: Alumno | null = null;

  equipos: Equipo[] = [];
  carreras: Carrera[] = [];

  constructor(
    private authService: AuthService,
    private carreraService: CarreraService ,
    private equipoService: EquipoService,
    private alumnoService: AlumnoService
  ) {}

  async ngOnInit() {
    this.usuarioAutenticado = (await this.alumnoService.obtenerUsuarioPorCedula(this.authService.obtenerUsuarioAutenticado()))!;
    this.carreras = await this.carreraService.obtenerCarreras();
    this.equipos = await this.equipoService.obtenerEquipos();
  }

  async actualizar() {
    try{
      const res = await this.alumnoService.actualizarUsuario(this.usuarioAutenticado!);
      alert("Usuario editado con éxito!")
    } catch (err) {
      console.error(err);
      alert("Error al actualizar usuario");
    }
  }

}