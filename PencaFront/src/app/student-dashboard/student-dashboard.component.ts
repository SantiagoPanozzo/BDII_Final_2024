import { Component } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-student-dashboard',
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.css']
})
export class StudentDashboardComponent {

  constructor(private authService: AuthService) {}

  logout() {
    this.authService.logout();
  }
}