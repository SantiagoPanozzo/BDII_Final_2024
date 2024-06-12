import { Component, OnInit } from '@angular/core';
import { PartidoService } from 'src/app/services/partido.service';
import { Partido } from 'src/app/interfaces/partidoInterface';
import {  Router } from '@angular/router';

@Component({
  selector: 'app-lista-partidos',
  templateUrl: './lista-partidos.component.html',
  styleUrls: ['./lista-partidos.component.css']
})
export class ListaPartidosComponent implements OnInit {
  partidos: Partido[] = [];

  constructor(private partidoService: PartidoService,private router:Router) {}

  ngOnInit(): void {
    this.partidos = this.partidoService.obtenerPartidos();
  }
  editarPartido(partidoId: number): void {
    this.router.navigate(['/admin-dashboard/admin-partidos', partidoId]);
  }
  
  
}
