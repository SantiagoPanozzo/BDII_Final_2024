
import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router'; // Importar el Router

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent {
  username: string = '';
password: string = '';
  cedula: string = '';
  nombre: string = '';
  apellido: string = '';
  fechanacimiento: string = '';
  carrera: string = '';
  anodeingreso: string = '';
  semestredeingreso: string = '';


  constructor(private authService: AuthService, private router: Router) {}

  registrar() {
    // Crear un objeto con los datos del alumno
    const alumno = {
      username: this.username,
      password: this.password,
      cedula: this.cedula,
      nombre: this.nombre,
      apellido: this.apellido,
      fechanacimiento: this.fechanacimiento,
      carrera: this.carrera,
      anodeingreso: this.anodeingreso,
      semestredeingreso: this.semestredeingreso

    };
    // Registrar al alumno llamando al método en el servicio AuthService
    this.authService.registrarAlumno(alumno);

    // Después de registrar, navegar de vuelta a la página de inicio de sesión
    this.router.navigate(['/login']);
  }
}