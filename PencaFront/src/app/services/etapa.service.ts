import { Injectable } from '@angular/core';
import { Etapa } from '../interfaces/etapa';
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class EtapaService {
  private etapas: Etapa[] = [];

  constructor(
      private http: HttpClient
  ) { }

  async obtenerEtapas(): Promise<Etapa[]> {
    this.etapas = (await this.http.get<Etapa[]>('http://localhost:8080/etapa').toPromise())!;
    return this.etapas;
  }

  async obtenerEtapaPorId(id: number) : Promise<Etapa> {
    return (await this.http.get<Etapa>('http://localhost:8080/etapa/' + id).toPromise())!;
  }

  obtenerNombreEtapa(Id: number): string {
    const etapa = this.etapas.find(etapa => etapa.id === Id);
    return etapa ? etapa.nombre : 'Etapa Desconocida';
  }
}
