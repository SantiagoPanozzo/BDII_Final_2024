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
  private adminCredentials = { cedula: 999, contrasena: 'admin' };
  private usuarioAutenticado: Alumno | null = null;
<<<<<<< HEAD
  private token: string | null = null;

=======
>>>>>>> 1fd6e41 (Guardar usuario en el browser storage)
  public isLoggedIn() {
    return this.usuarioAutenticado != null;
  }

  constructor(
    private alumnoService: AlumnoService, 
    private administradorService: AdministradorService, 
    private carreraService: CarreraService,
    private router: Router,
    private http: HttpClient
  ) {}

  async autenticarUsuario(cedula: number, contrasena: string): Promise<{ esAdmin: boolean, usuario?: any } | null> {

    const userLogin: UserLogin = { Cedula: cedula, Contrasena: contrasena };
    console.log("Loggin in as:")
    console.log(userLogin)
    const response = await this.http.post('http://localhost:8080/auth/login', userLogin).toPromise();

    // @ts-ignore
    this.token = response.token.result;
    localStorage.setItem('userToken', JSON.stringify(this.token));
    // @ts-ignore
    let esAdmin = jwtDecode(this.token).role !== "alumno";
    // @ts-ignore
    if(!esAdmin) console.log("no es admin!!!!!");
    return { esAdmin: esAdmin, usuario: null };
  }

    /*if (cedula === this.adminCredentials.cedula && contrasena === this.adminCredentials.contrasena) {
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
    }*/


 
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
