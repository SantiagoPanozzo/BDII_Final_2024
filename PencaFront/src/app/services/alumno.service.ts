import { Injectable } from '@angular/core';
import { Alumno } from '../interfaces/alumnoInterface';
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class AlumnoService {
  private alumnos: Alumno[] = [];

  private alumnoAutenticado: Alumno | null = null;

  constructor(
      private http: HttpClient
  ) { }

  registrarUsuario(alumno: Alumno): void {
    this.http.post('http://localhost:8080/auth/register', alumno).subscribe( x => console.log(x));
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
