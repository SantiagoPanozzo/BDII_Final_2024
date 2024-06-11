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
    this.alumnos = this.alumnoService.obtenerUsuarios();
    this.alumnosFiltrados = [...this.alumnos];
  }
  buscarAlumnosPorCedula(): void {
    if (this.cedulaABuscar.trim() !== '') {
      this.alumnosFiltrados = this.alumnos.filter(alumno =>
        alumno.cedula.includes(this.cedulaABuscar)
      );
    } else {
      
      this.alumnosFiltrados = [...this.alumnos];
    }
  }

  ngOnInit(): void {
    this.alumnos = this.alumnoService.obtenerUsuarios();
  }
}