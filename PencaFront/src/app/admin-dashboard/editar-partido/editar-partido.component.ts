import {Component, OnInit} from '@angular/core';
import {Equipo} from "../../interfaces/equipo";
import {Etapa} from "../../interfaces/etapa";
import {Partido} from "../../interfaces/partido";
import {EquipoService} from "../../services/equiposervice.service";
import {PartidoService} from "../../services/partido.service";
import {ActivatedRoute, Router} from "@angular/router";
import {EtapaService} from "../../services/etapa.service";

@Component({
  selector: 'app-editar-partido',
  templateUrl: './editar-partido.component.html',
  styleUrls: ['./editar-partido.component.css']
})
export class EditarPartidoComponent implements OnInit {
  equipos: Equipo[] = [];
  etapas: Etapa[] = [];
  partido: Partido = {} as Partido;

  constructor(
      private equipoService: EquipoService,
      private partidoService: PartidoService,
      private router: Router,
      private route: ActivatedRoute,
      private etapaService: EtapaService
  ) {}

  async ngOnInit() {
    this.etapas = await this.etapaService.obtenerEtapas();
    this.equipos = await this.equipoService.obtenerEquipos();

    const fecha = this.route.snapshot.params['fecha'];
    const abreviatura_1 = this.route.snapshot.params['abreviatura_1'];
    const abreviatura_2 = this.route.snapshot.params['abreviatura_2'];

    this.partido = await this.partidoService.obtenerPartidoPorId(fecha, abreviatura_1, abreviatura_2);
  }

  async guardarPartido() {
    console.log("Buscando etapa" + this.partido.etapa.id);
    this.partido.etapa = await this.etapaService.obtenerEtapaPorId(this.partido.etapa.id);
    this.partido.equipo_E1 = await this.equipoService.obtenerEquipoPorAbreviatura(this.partido.equipo_E1.abreviatura);
    this.partido.equipo_E2 = await this.equipoService.obtenerEquipoPorAbreviatura(this.partido.equipo_E2.abreviatura);
    this.partidoService.modificarPartido(this.partido.equipo_E1.abreviatura, this.partido.equipo_E2.abreviatura, this.partido.fecha, this.partido);
    console.log(this.partido);
    console.log(this.partido.equipo_E2)
    await this.router.navigate(['../admin-dashboard/partidos']); //no anda no se por que
  }
}
