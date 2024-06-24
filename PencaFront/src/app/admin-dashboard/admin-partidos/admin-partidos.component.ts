import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PartidoService } from 'src/app/services/partido.service';
import { Partido } from 'src/app/interfaces/partido';

@Component({
  selector: 'app-admin-partidos',
  templateUrl: './admin-partidos.component.html',
  styleUrls: ['./admin-partidos.component.css']
})
export class AdminPartidosComponent implements OnInit {
  partido: Partido | undefined;
  today: Date = new Date();

  constructor(
    private partidoService: PartidoService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const partidoId = +this.route.snapshot.params['id']; 
    if (!isNaN(partidoId)) { 
      this.partido = this.partidoService.obtenerPartidoPorId(partidoId);
      console.log('Partido obtenido:', this.partido);
    } else {
      console.error('ID de partido no válido:', partidoId);
    }
  }

  guardarResultado(): void {
    if (this.partido && this.partido.Resultado_E1 !== null && this.partido.Resultado_E2 !== null) {
      this.partidoService.actualizarResultado(this.partido.Id, this.partido.Resultado_E1, this.partido.Resultado_E2);
      this.router.navigate(['/admin-dashboard/lista-partidos']);
    } else {
      console.error('Resultados no válidos.');
    }
  }
}






