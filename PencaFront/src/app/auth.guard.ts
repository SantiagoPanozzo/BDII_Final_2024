import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    console.log("AuthGuard: Checking if logged in")
    if (this.authService.isLoggedIn()) {
      console.log("True")
      return true;
    } else {
      console.log("False")
      this.router.navigate(['/login']);
      return false;
    }
  }
  
}