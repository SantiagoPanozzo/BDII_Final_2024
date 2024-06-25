import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PartidoService } from 'src/app/services/partido.service';
import { Partido } from 'src/app/interfaces/partido';
import {EquipoService} from "../../services/equiposervice.service";
import {Equipo} from "../../interfaces/equipo";
import {EtapaService} from "../../services/etapa.service";
import {Etapa} from "../../interfaces/etapa";

@Component({
  selector: 'app-admin-partidos',
  templateUrl: './admin-partidos.component.html',
  styleUrls: ['./admin-partidos.component.css']
})
export class AdminPartidosComponent implements OnInit {
  partido: Partido | undefined;
  today: Date = new Date();
  equipos: Equipo[] = [];
  etapas: Etapa[] = [];
  abreviatura_1: string = "";
  abreviatura_2: string = "";
  fecha: Date = {} as Date;

  constructor(
    private partidoService: PartidoService,
    private route: ActivatedRoute,
    private router: Router,
    private equipoService: EquipoService,
    private etapaService: EtapaService
  ) {}

  async ngOnInit() {
    this.fecha = this.route.snapshot.params['fecha'];
    this.abreviatura_1 = this.route.snapshot.params['abreviatura_1'];
    this.abreviatura_2 = this.route.snapshot.params['abreviatura_2'];
    this.partido = await this.partidoService.obtenerPartidoPorId(this.fecha, this.abreviatura_1, this.abreviatura_2);
    if(this.partido.resultado_E1 == null || this.partido.resultado_E1 < 0) this.partido.resultado_E1 = 0;
    if(this.partido.resultado_E2 == null || this.partido.resultado_E2 < 0) this.partido.resultado_E2 = 0;
    this.equipos = await this.equipoService.obtenerEquipos();
    this.etapas = await this.etapaService.obtenerEtapas();
    console.log('Partido obtenido:', this.partido);
  }

  async guardarResultado() {
    if (this.partido && this.abreviatura_1 != "" && this.partido.equipo_E1.abreviatura != this.partido.equipo_E2.abreviatura && this.abreviatura_2 != "") {
      this.partido.etapa = await this.etapaService.obtenerEtapaPorId(this.partido.etapa.id);
      this.partido.equipo_E1 = await this.equipoService.obtenerEquipoPorAbreviatura(this.partido.equipo_E1.abreviatura);
      this.partido.equipo_E2 = await this.equipoService.obtenerEquipoPorAbreviatura(this.partido.equipo_E2.abreviatura);
      await this.partidoService.actualizarResultado(this.abreviatura_1, this.abreviatura_2, this.fecha, this.partido.resultado_E1!, this.partido.resultado_E2!);
      await this.router.navigate(['/admin-dashboard/partidos'])
    } else {
      alert("Error")
    }
  }
}






