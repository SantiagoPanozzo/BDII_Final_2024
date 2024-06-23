import { Injectable } from '@angular/core';
import { AlumnoService } from './alumno.service';
import { AdministradorService } from './administrador.service';
import { Router } from '@angular/router';
import { Alumno } from '../interfaces/alumnoInterface';
import { CarreraService } from './carrera.service';
import { Carrera } from '../interfaces/carrera';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private adminCredentials = { cedula: 999, contrasena: 'admin' };
  private usuarioAutenticado: Alumno | null = null;
  public isLoggedIn() {
    return this.usuarioAutenticado != null;
  }

  constructor(
    private alumnoService: AlumnoService, 
    private administradorService: AdministradorService, 
    private carreraService: CarreraService,
    private router: Router
  ) {}

  autenticarUsuario(cedula: number, contrasena: string): { esAdmin: boolean, usuario?: any } | null {
    if (cedula === this.adminCredentials.cedula && contrasena === this.adminCredentials.contrasena) {
      return { esAdmin: true, usuario: this.administradorService.obtenerDatosAdmin() };
    } else {
      const usuario = this.alumnoService.obtenerUsuarioPorCedulaYContrasena(cedula, contrasena);
      if (usuario) {
        this.usuarioAutenticado = usuario;
        localStorage.setItem('user', JSON.stringify(usuario)); // Save user data
        return { esAdmin: false, usuario };
      } else {
        return null;
      }
    }
  }

  obtenerUsuarioAutenticado(): Alumno {
    const storedUser = localStorage.getItem('user');
    if (storedUser) {
      this.usuarioAutenticado = JSON.parse(storedUser);
      return this.usuarioAutenticado!;
    } else {
      throw new Error('No hay un usuario autenticado actualmente');
    }
  }

  logout(): void {
    this.usuarioAutenticado = null;
    localStorage.removeItem('user'); // Remove user data
    this.router.navigate(['/login']);
  }
}
