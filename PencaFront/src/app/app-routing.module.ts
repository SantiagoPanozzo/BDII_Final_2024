import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { StudentDashboardComponent } from './student-dashboard/student-dashboard.component';
import { RegistroComponent } from './registro/registro.component';
import { ListaAlumnosComponent } from './admin-dashboard/lista-alumnos/lista-alumnos.component';
import { ListaPartidosComponent } from './admin-dashboard/lista-partidos/lista-partidos.component';
import { DatosComponent } from './student-dashboard/datos/datos.component';
import { PrediccionesComponent } from './student-dashboard/predicciones/predicciones.component';
import { PuntajeComponent } from './student-dashboard/puntaje/puntaje.component';
import { AdminPartidosComponent } from './admin-dashboard/admin-partidos/admin-partidos.component';
import { RegistrarPartidoComponent } from './admin-dashboard/registrar-partido/registrar-partido.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'admin-dashboard', component: AdminDashboardComponent},
  { path: 'registro', component: RegistroComponent },
  { path: 'student-dashboard', component: StudentDashboardComponent },
  { path: 'admin-dashboard/alumnos', component: ListaAlumnosComponent },
  { path: 'admin-dashboard/partidos', component: ListaPartidosComponent },
  { path: 'student-dashboard/datos', component: DatosComponent },
  { path: 'student-dashboard/predicciones', component: PrediccionesComponent },
  { path: 'student-dashboard/puntaje', component: PuntajeComponent },
  { path: 'admin-dashboard/admin-partidos/:abreviatura_1/:abreviatura_2/:fecha', component: AdminPartidosComponent },
  { path: '', redirectTo: '/admin-dashboard/lista-partidos', pathMatch: 'full' },
  { path: 'registrar-partido', component: RegistrarPartidoComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }