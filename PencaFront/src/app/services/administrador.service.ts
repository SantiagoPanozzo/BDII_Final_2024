import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AdministradorService {

  private adminDatos = {

    cedula: 999,
    nombre : 'holaa',
    contrasena: 'admin123',
    Fecha_Nacimiento : '6/10/2002',
    Rol_Universidad : 'Profesor',
    
  };

  constructor() { }

  obtenerDatosAdmin() {
    return this.adminDatos;
  }
}