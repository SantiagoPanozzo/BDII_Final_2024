import { Component, OnInit } from '@angular/core';
import { Partido } from 'src/app/interfaces/partido';
import { EquipoService } from 'src/app/services/equiposervice.service';
import { PartidoService } from 'src/app/services/partido.service';
import { Equipo } from 'src/app/interfaces/equipo';
import { Router } from '@angular/router';
import {Etapa} from "../../interfaces/etapa";
@Component({
  selector: 'app-registrar-partido',
  templateUrl: './registrar-partido.component.html',
  styleUrls: ['./registrar-partido.component.css']
})
export class RegistrarPartidoComponent implements OnInit {
  equipos: Equipo[] = [];
  nuevoPartido: Partido = {
    Id: 0,
    Fecha: new Date(),
    Equipo_E1: {} as Equipo,
    Equipo_E2: {} as Equipo,
    Resultado_E1: null,
    Resultado_E2: null,
    Etapa: {} as Etapa,
  };

  constructor(private equipoService: EquipoService, private partidoService: PartidoService, private router: Router) {}

  async ngOnInit() {
    this.equipos = await this.equipoService.obtenerEquipos();
  }

  registrarPartido(): void {
    this.partidoService.registrarPartido(this.nuevoPartido);
    this.nuevoPartido = {
      Id: 0,
      Fecha: new Date(),
      Equipo_E1: {} as Equipo,
      Equipo_E2: {} as Equipo,
      Resultado_E1: null,
      Resultado_E2: null,
      Etapa: {} as Etapa,
    };
    this.router.navigate(['../lista-partidos']); //no anda no se por que 
  }
}
