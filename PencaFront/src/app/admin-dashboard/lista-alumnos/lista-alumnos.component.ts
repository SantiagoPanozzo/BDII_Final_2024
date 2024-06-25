import { Component, OnInit } from '@angular/core';
import { AlumnoService } from 'src/app/services/alumno.service';
import { Alumno } from 'src/app/interfaces/alumnoInterface';

@Component({
  selector: 'app-lista-alumnos',
  templateUrl: './lista-alumnos.component.html',
  styleUrls: ['./lista-alumnos.component.css']
})
export class ListaAlumnosComponent implements OnInit {
  alumnos: Alumno[] = [];
  alumnosFiltrados: Alumno[] = [];
  cedulaABuscar: string = '';

  constructor(private alumnoService: AlumnoService) {
    }
  buscarAlumnosPorCedula(): void {
    if (this.cedulaABuscar.trim() !== '') {
      const cedulaABuscarNumber = Number(this.cedulaABuscar);
      this.alumnosFiltrados = this.alumnos.filter(alumno =>
        alumno.cedula === cedulaABuscarNumber
      );
    } else {
      this.alumnosFiltrados = [...this.alumnos];
    }
  }
  async ngOnInit() {
    this.alumnos = await this.alumnoService.obtenerUsuarios();
    this.alumnosFiltrados = [...this.alumnos];
  }
}