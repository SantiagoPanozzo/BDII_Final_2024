import { Component } from '@angular/core';
import {PartidoSinResultados} from "../../interfaces/partidoSinResultados";
import {Equipo} from "../../interfaces/equipo";
import {Etapa} from "../../interfaces/etapa";
import {PartidoService} from "../../services/partido.service";
import {ActivatedRoute, Router} from "@angular/router";
import {EquipoService} from "../../services/equiposervice.service";
import {EtapaService} from "../../services/etapa.service";

@Component({
  selector: 'app-editar-partido',
  templateUrl: './editar-partido.component.html',
  styleUrls: ['./editar-partido.component.css']
})
export class EditarPartidoComponent {
  partido: PartidoSinResultados | undefined;
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
    this.equipos = await this.equipoService.obtenerEquipos();
    this.etapas = await this.etapaService.obtenerEtapas();
    console.log('Partido obtenido:', this.partido);
  }

  async guardarResultado() {
    if (this.partido && this.abreviatura_1 != "" && this.partido.equipo_E1.abreviatura != this.partido.equipo_E2.abreviatura && this.abreviatura_2 != "") {
      this.partido.etapa = await this.etapaService.obtenerEtapaPorId(this.partido.etapa.id);
      this.partido.equipo_E1 = await this.equipoService.obtenerEquipoPorAbreviatura(this.partido.equipo_E1.abreviatura);
      this.partido.equipo_E2 = await this.equipoService.obtenerEquipoPorAbreviatura(this.partido.equipo_E2.abreviatura);
      await this.partidoService.modificarPartido(this.abreviatura_1, this.abreviatura_2, this.fecha, this.partido);
      await this.router.navigate(['/admin-dashboard/partidos'])
    } else {
      alert("Error")
    }
  }
}
