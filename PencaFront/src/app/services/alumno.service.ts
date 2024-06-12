import { Injectable } from '@angular/core';
import { Alumno } from '../interfaces/alumnoInterface';

@Injectable({
  providedIn: 'root'
})
export class AlumnoService {
  private alumnos: Alumno[] = [
    {
      cedula: 123,
      contrasena: 'clave123',
      nombre: 'Juan',
      apellido: 'Pérez',
      fechaNacimiento: new Date('1995-05-15'),
      anioIngreso: 2020,
      semestreIngreso: 'Agosto',
      puntajeTotal: 100,
      campeon: 'ARG',
      subcampeon: 'BRA'
    },
    {
      cedula: 987,
      contrasena: 'abc123',
      nombre: 'María',
      apellido: 'García',
      fechaNacimiento: new Date('1998-10-20'),
      anioIngreso: 2019,
      semestreIngreso: 'Marzo',
      puntajeTotal: 85,
      campeon: 'BRA',
      subcampeon: 'ARG'
    }
  ];

  private alumnoAutenticado: Alumno | null = null;

  constructor() { }

  registrarUsuario(alumno: Alumno): void {
    this.alumnos.push(alumno);
  }

  obtenerUsuarioPorCedulaYContrasena(cedula: number, contrasena: string): Alumno | null {
    const usuario = this.alumnos.find(u => u.cedula === cedula && u.contrasena === contrasena) || null;
    if (usuario) {
      this.alumnoAutenticado = usuario;
    }
    return usuario;
  }

  obtenerUsuarios(): Alumno[] {
    return this.alumnos;
  }

  obtenerPuntajePorCedula(cedula: number): number | null {
    const alumno = this.alumnos.find(u => u.cedula === cedula);
    return alumno ? alumno.puntajeTotal : null;
  }

  obtenerUsuarioAutenticado(): Alumno | null {
    return this.alumnoAutenticado;
  }
}