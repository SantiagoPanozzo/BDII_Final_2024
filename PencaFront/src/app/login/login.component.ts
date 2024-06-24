import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  cedula: number = 0;
  password: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  async login(): Promise<void> {
    const resultado = await this.authService.autenticarUsuario(this.cedula, this.password);
    if (resultado) {
      if (resultado.esAdmin) {
        this.router.navigate(['/admin-dashboard'], { state: { usuario: resultado.usuario } });
      } else {
        this.router.navigate(['/student-dashboard'], { state: { usuario: resultado.usuario } });
      }
    } else {
      alert('Credenciales incorrectas');
    }
  }
  

  registrar(): void {
    this.router.navigate(['/register']);
  }
}