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

  async ngOnInit() {
    const fecha = this.route.snapshot.params['fecha'];
    const abreviatura_1 = this.route.snapshot.params['abreviatura_1'];
    const abreviatura_2 = this.route.snapshot.params['abreviatura_2'];
    this.partido = await this.partidoService.obtenerPartidoPorId(fecha, abreviatura_1, abreviatura_2);
    if(this.partido.resultado_E1 == null || this.partido.resultado_E1 < 0) this.partido.resultado_E1 = 0;
    if(this.partido.resultado_E2 == null || this.partido.resultado_E2 < 0) this.partido.resultado_E2 = 0;
    console.log('Partido obtenido:', this.partido);
  }

  async guardarResultado() {
    if (this.partido && this.partido.resultado_E1 !== null && this.partido.resultado_E2 !== null) {
      this.partidoService.actualizarResultado(this.partido.equipo_E1.abreviatura, this.partido.equipo_E2.abreviatura, this.partido.fecha, this.partido.resultado_E1, this.partido.resultado_E2)
          .then(async () => {
            await this.router.navigate(['/admin-dashboard/partidos'])
          });
    } else {
      console.error('Resultados no válidos.');
    }
  }
}






