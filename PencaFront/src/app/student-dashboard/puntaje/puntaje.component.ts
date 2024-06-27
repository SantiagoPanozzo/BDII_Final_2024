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

  async ngOnInit() {
    const usuarioAutenticado = (await this.alumnoService.obtenerUsuarioPorCedula(this.authService.obtenerUsuarioAutenticado()))!;
    if (usuarioAutenticado) {
      const puntaje = usuarioAutenticado.puntajeTotal;
      console.log(puntaje);
      if (puntaje !== null) {
        this.puntajeAlumno = puntaje;
      }
    }
  }
}

