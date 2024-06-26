import { Component } from '@angular/core';
import {AuthService} from "../../../services/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-logout-button',
  templateUrl: './logout-button.component.html',
  styleUrls: ['./logout-button.component.css']
})
export class LogoutButtonComponent {
  constructor(
      private authService: AuthService,
      private router: Router
  ) {}

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']).then();
  }
}
