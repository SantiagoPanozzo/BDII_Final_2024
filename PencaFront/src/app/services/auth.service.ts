import { Injectable } from '@angular/core';
import { AlumnoService } from './alumno.service';
import { AdministradorService } from './administrador.service';
import { Router } from '@angular/router';
import { Alumno } from '../interfaces/alumnoInterface';
import { CarreraService } from './carrera.service';
import { Carrera } from '../interfaces/carrera';
import {HttpClient} from "@angular/common/http";
import {jwtDecode} from "jwt-decode";
import {UserLogin} from "../interfaces/UserLogin";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public isLoggedIn() {
    return localStorage.getItem('userToken') != null;
  }

  public isAdmin() {
    const token = localStorage.getItem('userToken');
    if (token)
      // @ts-ignore
      return jwtDecode(token).role !== "alumno";
    return false;
  }

  constructor(
    private router: Router,
    private http: HttpClient
  ) {}

  async autenticarUsuario(cedula: number, contrasena: string): Promise<{ esAdmin: boolean, usuario?: any } | null> {

    const userLogin: UserLogin = { Cedula: cedula, Contrasena: contrasena };
    console.log("Loggin in as:")
    console.log(userLogin)
    const response = await this.http.post('http://localhost:8080/auth/login', userLogin).toPromise();

    // @ts-ignore
    const token = response.token.result;
    localStorage.setItem('userToken', JSON.stringify(token));

    // @ts-ignore
    const user = jwtDecode(token).nameid;
    localStorage.setItem('user', JSON.stringify(user));

    return { esAdmin: this.isAdmin(), usuario: this.obtenerUsuarioAutenticado() };
  }

 
  obtenerUsuarioAutenticado(): string {
    const storedUser = localStorage.getItem('user');
    if (storedUser) {
      return storedUser!;
    } else {
      throw new Error('No hay un usuario autenticado actualmente');
    }
  }

  logout(): void {
    localStorage.removeItem('userToken'); // Remove user data
    this.router.navigate(['/login']);
  }
}
