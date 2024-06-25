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

  async ngOnInit() {
    // this.usuarioAutenticado = this.authService.obtenerUsuarioAutenticado(); TODO
    await this.obtenerNombreCarrera();
     
  }

  async obtenerNombreCarrera() {
    return this.usuarioAutenticado!.carreraPrincipal.nombre; // TODO deprecar esto
  }
}