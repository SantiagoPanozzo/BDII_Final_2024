import { Component, OnInit } from '@angular/core';
import { Alumno } from 'src/app/interfaces/alumnoInterface';
import { AuthService } from 'src/app/services/auth.service';
import { AlumnoService } from 'src/app/services/alumno.service';
import { CarreraService } from 'src/app/services/carrera.service'; 
import { EquipoService } from 'src/app/services/equiposervice.service';

@Component({
  selector: 'app-datos',
  templateUrl: './datos.component.html',
  styleUrls: ['./datos.component.css']
})
export class DatosComponent implements OnInit {
  usuarioAutenticado: Alumno | null = null;
  nombreCarrera: string = ''; 

  constructor(
    private authService: AuthService,
    private carreraService: CarreraService ,
    private equipoService: EquipoService
  ) {}

  ngOnInit(): void {
    this.usuarioAutenticado = this.authService.obtenerUsuarioAutenticado();
    this.obtenerNombreCarrera(); 
     
  }

  obtenerNombreCarrera(): void {
    if (this.usuarioAutenticado) {
      this.nombreCarrera = this.carreraService.obtenerNombreCarrera(this.usuarioAutenticado.carrera);
    }
  }
}