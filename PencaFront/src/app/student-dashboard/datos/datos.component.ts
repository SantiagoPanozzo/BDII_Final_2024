import { Component,OnInit } from '@angular/core';
import { Alumno } from 'src/app/interfaces/alumnoInterface';
import { AuthService } from 'src/app/services/auth.service';
import { AlumnoService } from 'src/app/services/alumno.service';


@Component({
  selector: 'app-datos',
  templateUrl: './datos.component.html',
  styleUrls: ['./datos.component.css']
})
export class DatosComponent implements OnInit {
  usuarioAutenticado: Alumno | null = null;
  

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.usuarioAutenticado = this.authService.obtenerUsuarioAutenticado();
  }
}


