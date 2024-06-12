import { Component, OnInit } from '@angular/core';
import { Alumno } from '../interfaces/alumnoInterface';

@Component({
  selector: 'app-student-dashboard',
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.css']
})
export class StudentDashboardComponent implements OnInit {
  alumno: Alumno | undefined; 

  constructor() { }

  ngOnInit(): void {
   
  }
}
