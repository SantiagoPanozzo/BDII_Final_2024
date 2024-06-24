import { Component, OnInit } from '@angular/core';
import { PartidoService } from 'src/app/services/partido.service';
import { Partido } from 'src/app/interfaces/partido';
import { Router } from '@angular/router';
import {Etapa} from "../../interfaces/etapa";

@Component({
  selector: 'app-lista-partidos',
  templateUrl: './lista-partidos.component.html',
  styleUrls: ['./lista-partidos.component.css']
})
export class ListaPartidosComponent implements OnInit {
  partidosPorEtapa: { etapa: Etapa, partidos: Partido[] }[] = [];

  constructor(private partidoService: PartidoService, private router: Router) {}

  async ngOnInit() {
    const partidos = await this.partidoService.obtenerPartidos();
    this.partidosPorEtapa = this.agruparPartidosPorEtapa(partidos);
  }

  editarPartido(partidoId: number): void {
    this.router.navigate(['/admin-dashboard/admin-partidos', partidoId]);
  }

  private agruparPartidosPorEtapa(partidos: Partido[]): { etapa: Etapa, partidos: Partido[] }[] {
    const partidosAgrupados: { etapa: Etapa, partidos: Partido[] }[] = [];


    partidos.forEach(partido => {
      const index = partidosAgrupados.findIndex(item => item.etapa === partido.etapa);
      if (index !== -1) {
        partidosAgrupados[index].partidos.push(partido);
      } else {
        partidosAgrupados.push({ etapa: partido.etapa, partidos: [partido] });
      }
    });

    return partidosAgrupados;
  }
}
