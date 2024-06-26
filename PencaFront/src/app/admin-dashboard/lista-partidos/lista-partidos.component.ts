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

  editarPartido(fecha: Date, abreviatura_1: string, abreviatura_2: string): void {
    const ruta = `/admin-dashboard/admin-partidos/${abreviatura_1}/${abreviatura_2}/${fecha}`;
    console.log("Editando partido " + ruta)
    this.router.navigate([ruta]);
  }

  resultadosPartido(fecha: Date, abreviatura_1: string, abreviatura_2: string): void {
    const ruta = `/admin-dashboard/partidos/${abreviatura_1}/${abreviatura_2}/${fecha}/editar`;
    console.log("Estableciendo resultados de partido " + ruta)
    this.router.navigate([ruta]);
  }

  private agruparPartidosPorEtapa(partidos: Partido[]): { etapa: Etapa, partidos: Partido[] }[] {
    const partidosAgrupados: { etapa: Etapa, partidos: Partido[] }[] = [];


    partidos.forEach(partido => {
      const index = partidosAgrupados.findIndex(item => item.etapa.id == partido.etapa.id);
      if (index !== -1) {
        partidosAgrupados[index].partidos.push(partido);
      } else {
        partidosAgrupados.push({ etapa: partido.etapa, partidos: [partido] });
      }
    });

    return partidosAgrupados;
  }
}
