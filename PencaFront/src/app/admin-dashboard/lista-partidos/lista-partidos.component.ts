import { Component, OnInit } from '@angular/core';
import { PartidoService } from 'src/app/services/partido.service';
import { Partido } from 'src/app/interfaces/partidoInterface';
import { Router } from '@angular/router';

@Component({
  selector: 'app-lista-partidos',
  templateUrl: './lista-partidos.component.html',
  styleUrls: ['./lista-partidos.component.css']
})
export class ListaPartidosComponent implements OnInit {
  partidosPorEtapa: { etapa: number, partidos: Partido[] }[] = [];

  constructor(private partidoService: PartidoService, private router: Router) {}

  ngOnInit(): void {
    const partidos = this.partidoService.obtenerPartidos();
    this.partidosPorEtapa = this.agruparPartidosPorEtapa(partidos);
  }

  editarPartido(partidoId: number): void {
    this.router.navigate(['/admin-dashboard/admin-partidos', partidoId]);
  }

  private agruparPartidosPorEtapa(partidos: Partido[]): { etapa: number, partidos: Partido[] }[] {
    const partidosAgrupados: { etapa: number, partidos: Partido[] }[] = [];


    partidos.forEach(partido => {
      const index = partidosAgrupados.findIndex(item => item.etapa === partido.Etapa);
      if (index !== -1) {
        partidosAgrupados[index].partidos.push(partido);
      } else {
        partidosAgrupados.push({ etapa: partido.Etapa, partidos: [partido] });
      }
    });

    return partidosAgrupados;
  }
}
