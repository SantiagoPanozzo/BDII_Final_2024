import { Component, OnInit } from '@angular/core';
import { Partido } from 'src/app/interfaces/partido';
import { EquipoService } from 'src/app/services/equiposervice.service';
import { PartidoService } from 'src/app/services/partido.service';
import { Equipo } from 'src/app/interfaces/equipo';
import { Router } from '@angular/router';
import {Etapa} from "../../interfaces/etapa";
import {EtapaService} from "../../services/etapa.service";
@Component({
  selector: 'app-registrar-partido',
  templateUrl: './registrar-partido.component.html',
  styleUrls: ['./registrar-partido.component.css']
})
export class RegistrarPartidoComponent implements OnInit {
  equipos: Equipo[] = [];
  etapas: Etapa[] = [];
  nuevoPartido: Partido = {
    id: 0,
    fecha: new Date(),
    equipo_E1: {} as Equipo,
    equipo_E2: {} as Equipo,
    resultado_E1: null,
    resultado_E2: null,
    etapa: {} as Etapa,
  };

  constructor(
      private equipoService: EquipoService,
      private partidoService: PartidoService,
      private router: Router,
      private etapaService: EtapaService
  ) {}

  async ngOnInit() {
    this.etapas = await this.etapaService.obtenerEtapas();
    this.equipos = await this.equipoService.obtenerEquipos();
  }

  async registrarPartido() {
    console.log("Buscando etapa" + this.nuevoPartido.etapa.id);
    this.nuevoPartido.etapa = await this.etapaService.obtenerEtapaPorId(this.nuevoPartido.etapa.id);
    this.nuevoPartido.equipo_E1 = await this.equipoService.obtenerEquipoPorAbreviatura(this.nuevoPartido.equipo_E1.abreviatura);
    this.nuevoPartido.equipo_E2 = await this.equipoService.obtenerEquipoPorAbreviatura(this.nuevoPartido.equipo_E2.abreviatura);
    this.partidoService.registrarPartido(this.nuevoPartido);
    console.log(this.nuevoPartido);
    console.log(this.nuevoPartido.equipo_E2)
    this.router.navigate(['../admin-dashboard/partidos']); //no anda no se por que
  }
}
