import { Injectable } from '@angular/core';
import { Alumno } from '../interfaces/alumnoInterface';

@Injectable({
  providedIn: 'root'
})
export class AlumnoService {
  private alumnos: Alumno[] = [
    {
      username: 'alumno1',
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
      username: 'alumno2',
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

  obtenerUsuarioPorUsuarioYContrasena(username: string, contrasena: string): Alumno | null {
    const usuario = this.alumnos.find(u => u.username === username && u.contrasena === contrasena) || null;
    if (usuario) {
      this.alumnoAutenticado = usuario;
    }
    return usuario;
  }

  obtenerUsuarios(): Alumno[] {
    return this.alumnos;
  }

  obtenerPuntajePorUsername(username: string): number | null {
    const alumno = this.alumnos.find(u => u.username === username);
    return alumno ? alumno.puntajeTotal : null;
  }

  obtenerUsuarioAutenticado(): Alumno | null {
    return this.alumnoAutenticado;
  }
}
