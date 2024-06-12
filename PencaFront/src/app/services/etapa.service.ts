import { Injectable } from '@angular/core';
import { Etapa } from '../interfaces/etapa';

@Injectable({
  providedIn: 'root'
})
export class EtapaService {
  private etapas: Etapa[] = [
    { Id: 1, nombre: 'Fase de Grupos' },
    { Id: 2, nombre: 'Cuartos de Final' },
    { Id: 3, nombre: 'Semifinal' },
    { Id: 4, nombre: 'Final' }
  ];

  constructor() { }

  obtenerEtapas(): Etapa[] {
    return this.etapas;
  }

  obtenerNombreEtapa(Id: number): string {
    const etapa = this.etapas.find(etapa => etapa.Id === Id);
    return etapa ? etapa.nombre : 'Etapa Desconocida';
  }
}
