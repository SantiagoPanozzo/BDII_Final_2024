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
import {AdminGuard} from "./guards/admin.guard";
import {StudentGuard} from "./guards/student.guard";
import {AppLayoutComponent} from "./layouts/app-layout/app-layout.component";

const routes: Routes = [
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    { path: 'registro', component: RegistroComponent },
    {
        path: 'admin-dashboard',
        canActivate: [AdminGuard],
        component: AppLayoutComponent,
        children: [
            { path: '', component: AdminDashboardComponent, pathMatch: 'full' }, // Default child route
            { path: 'partidos', component: ListaPartidosComponent, pathMatch: 'full'},
            { path: 'admin-partidos/:abreviatura_1/:abreviatura_2/:fecha', component: AdminPartidosComponent, pathMatch: 'full' },
            { path: 'lista-partidos', component: ListaPartidosComponent, pathMatch: 'full' },
            { path: 'alumnos', component: ListaAlumnosComponent, pathMatch: 'full' },
            { path: 'registrar-partido', component: RegistrarPartidoComponent, pathMatch: 'full'},
        ],
    },
    {
        path: 'student-dashboard',
        canActivate: [StudentGuard],
        component: AppLayoutComponent,
        children: [
            { path: '', component: StudentDashboardComponent },
            { path: 'datos', component: DatosComponent, pathMatch: 'full'},
            { path: 'predicciones', component: PrediccionesComponent, pathMatch: 'full'},
            { path: 'puntaje', component: PuntajeComponent, pathMatch: 'full'},
        ]
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }