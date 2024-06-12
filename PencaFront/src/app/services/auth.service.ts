import { Injectable } from '@angular/core';
import { AlumnoService } from './alumno.service';
import { AdministradorService } from './administrador.service';
import { Router } from '@angular/router';
import { Alumno } from '../interfaces/alumnoInterface';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private adminCredentials = { cedula: 999, contrasena: 'admin' };
  private usuarioAutenticado: Alumno | null = null;

  constructor(private alumnoService: AlumnoService, private administradorService: AdministradorService, private router: Router) { }

  autenticarUsuario(cedula: number, contrasena: string): { esAdmin: boolean, usuario?: any } | null {
    // Verificar si las credenciales son del administrador
    if (cedula === this.adminCredentials.cedula && contrasena === this.adminCredentials.contrasena) {
      // Si son del administrador, devolver la información de administrador
      return { esAdmin: true, usuario: this.administradorService.obtenerDatosAdmin() };
    } else {
      // Si no son del administrador, buscar en los alumnos
      const usuario = this.alumnoService.obtenerUsuarioPorCedulaYContrasena(cedula, contrasena);
      if (usuario) {
        // Si se encuentra al alumno, actualizar el usuario autenticado
        this.usuarioAutenticado = usuario;
        // Devolver la información del alumno
        return { esAdmin: false, usuario };
      } else {
        // Si no se encuentra al alumno, devolver null
        return null;
      }
    }
  }

  obtenerUsuarioAutenticado(): Alumno {
    if (!this.usuarioAutenticado) {
      throw new Error('No hay un usuario autenticado actualmente');
    }
    return this.usuarioAutenticado;
  }

  logout(): void {
    this.usuarioAutenticado = null;
  
    this.router.navigate(['/login']);
  }
}