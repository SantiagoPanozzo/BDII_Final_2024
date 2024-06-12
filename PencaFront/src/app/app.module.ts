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
    AdminPartidosComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule, 
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
