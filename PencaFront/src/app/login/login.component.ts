import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    const resultado = this.authService.login(this.username, this.password);
    if (resultado === 'admin') {
      this.router.navigate(['/admin-dashboard']); // Redirige al admin dashboard
    } else if (resultado === 'alumno') {
      this.router.navigate(['/student-dashboard']); // Redirige al student dashboard
    } else {
      alert('Datos incorrectos'); // Muestra un mensaje de error si las credenciales son incorrectas
    }
  }
  }
