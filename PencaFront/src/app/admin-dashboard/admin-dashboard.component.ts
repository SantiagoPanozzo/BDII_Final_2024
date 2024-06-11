import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  usuario: any; 
  seccionActiva: string = 'alumnos';
  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    const state = navigation?.extras.state as { usuario: any };
    this.usuario = state?.usuario || {};
  }

  ngOnInit(): void {}
  mostrarSeccion(seccion: string): void {
    this.seccionActiva = seccion;
  }

}


  

