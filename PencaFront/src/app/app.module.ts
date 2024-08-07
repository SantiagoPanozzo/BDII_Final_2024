import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module'; // Importa AppRoutingModule
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { StudentDashboardComponent } from './student-dashboard/student-dashboard.component';
import { RegistroComponent } from './registro/registro.component';
import { ListaAlumnosComponent } from './admin-dashboard/lista-alumnos/lista-alumnos.component';
import { ListaPartidosComponent } from './admin-dashboard/lista-partidos/lista-partidos.component';
import { PrediccionesComponent } from './student-dashboard/predicciones/predicciones.component';
import { DatosComponent } from './student-dashboard/datos/datos.component';
import { PuntajeComponent } from './student-dashboard/puntaje/puntaje.component';
import { AdminPartidosComponent } from './admin-dashboard/admin-partidos/admin-partidos.component';
import { RegistrarPartidoComponent } from './admin-dashboard/registrar-partido/registrar-partido.component';
import {HttpClientModule} from "@angular/common/http";
import { AppLayoutComponent } from './layouts/app-layout/app-layout.component';
import { LogoutButtonComponent } from './components/navbar/logout-button/logout-button.component';
import { EditarPartidoComponent } from './admin-dashboard/editar-partido/editar-partido.component';
import { RankingComponent } from './student-dashboard/ranking/ranking.component';
import { PartidoPrediccionComponent } from './student-dashboard/predicciones/partido-prediccion/partido-prediccion.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { NavbarLinkComponent } from './components/navbar/navbar-link/navbar-link.component';
import { LoginButtonComponent } from './components/navbar/login-button/login-button.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AdminDashboardComponent,
    StudentDashboardComponent,
    RegistroComponent,
    ListaAlumnosComponent,
    ListaPartidosComponent,
    PrediccionesComponent,
    DatosComponent,
    PuntajeComponent,
    AdminPartidosComponent,
    RegistrarPartidoComponent,
    AppLayoutComponent,
    LogoutButtonComponent,
    EditarPartidoComponent,
    RankingComponent,
    PartidoPrediccionComponent,
    NavbarComponent,
    NavbarLinkComponent,
    LoginButtonComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule, 
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
