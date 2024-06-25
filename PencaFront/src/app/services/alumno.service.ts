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

  async obtenerUsuarioPorCedulaYContrasena(cedula: number, contrasena: string) : Promise<Alumno | undefined> {
    return (await this.http.get<Alumno>('http://localhost:8080/alumno/' + cedula).toPromise());
  }

  async obtenerUsuarios() {
    return (await this.http.get<Alumno[]>('http://localhost:8080/alumno').toPromise())!;
  }

  async obtenerRanking() {
    return (await this.http.get<Alumno[]>('http://localhost:8080/alumno/ranking').toPromise())!;
  }

  obtenerPuntajePorCedula(cedula: number): number | null {
    const alumno = this.alumnos.find(u => u.cedula === cedula);
    return alumno ? alumno.puntajeTotal : null;
  }

  obtenerUsuarioAutenticado(): Alumno | null {
    return this.alumnoAutenticado;
  }
}
