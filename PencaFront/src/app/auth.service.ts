import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private isAuthenticated = false;
  private userRole: 'admin' | 'student' | null = null;

  // Hardcodear datos de alumnos
  alumnos = [
    {
      username: 'alumno1',
      password: 'password1',
      cedula: '123',
      nombre: 'al',
      apellido: 'ap',
      fechanacimiento: '12/12/1900',
      carrera: 'nada',
      anodeingreso: '2023',
      semestredeingreso: '2'
    },
    {
      username: 'alumno2',
      password: 'password2',
      cedula: '124',
      nombre: 'al2',
      apellido: 'ap2',
      fechanacimiento: '12/12/1901',
      carrera: 'algo',
      anodeingreso: '2022',
      semestredeingreso: '1'
    },
    {
      username: 'alumno3',
      password: 'password3',
      cedula: '125',
      nombre: 'al3',
      apellido: 'ap3',
      fechanacimiento: '12/12/1902',
      carrera: 'algo más',
      anodeingreso: '2021',
      semestredeingreso: '2'
    }
  ];

  adminUser = { username: 'admin', password: 'admin123' };

  constructor(private router: Router) { }

  login(username: string, password: string): string {
    // Verifica si las credenciales corresponden al admin
    if (username === this.adminUser.username && password === this.adminUser.password) {
      this.isAuthenticated = true;
      this.userRole = 'admin';
      return 'admin'; // Retorna 'admin' si las credenciales son del admin
    }

    // Verifica si las credenciales corresponden a algún alumno registrado
    const alumno = this.alumnos.find(alumno => alumno.username === username && alumno.password === password);
    if (alumno) {
      this.isAuthenticated = true;
      this.userRole = 'student';
      return 'alumno'; // Retorna 'alumno' si las credenciales son de un alumno registrado
    }

    return 'invalido'; // Retorna 'invalido' si las credenciales son incorrectas
  }

  getUsers(): { username: string, password: string, cedula: string, nombre: string, apellido: string, fechanacimiento: string, carrera: string, anodeingreso: string, semestredeingreso: string }[] {
    return this.alumnos;
  }

  logout(): void {
    this.isAuthenticated = false;
    this.userRole = null;
    this.router.navigate(['/login']);
  }

  isLoggedIn(): boolean {
    return this.isAuthenticated;
  }

  registrarAlumno(datos: any): void {
    this.alumnos.push(datos);
  }
}