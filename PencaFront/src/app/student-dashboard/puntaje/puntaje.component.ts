import { Component, OnInit } from '@angular/core';
import { AlumnoService } from 'src/app/services/alumno.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-puntaje',
  templateUrl: './puntaje.component.html',
  styleUrls: ['./puntaje.component.css']
})
export class PuntajeComponent implements OnInit {
  puntajeAlumno: number = 0;

  constructor(private alumnoService: AlumnoService, private authService: AuthService) { }

  ngOnInit(): void {
    const usuarioAutenticado = this.authService.obtenerUsuarioAutenticado();
    if (usuarioAutenticado) {
      const puntaje = this.alumnoService.obtenerPuntajePorUsername(usuarioAutenticado.username);
      if (puntaje !== null) {
        this.puntajeAlumno = puntaje;
      }
    }
  }
}

