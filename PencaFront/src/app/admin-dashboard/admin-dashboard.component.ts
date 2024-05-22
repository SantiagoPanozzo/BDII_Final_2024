import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {


  alumnos: any[] = [];

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.alumnos = this.authService.getUsers();
    
  }

  
  logout(): void {
    this.authService.logout();
  }
}

  

