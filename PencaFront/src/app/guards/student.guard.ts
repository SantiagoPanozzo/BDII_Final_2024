import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable({
    providedIn: 'root'
})
export class StudentGuard implements CanActivate {

    constructor(private authService: AuthService, private router: Router) {}

    canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean {
        console.log("StudentGuard: Checking if logged in and student")
        if (this.authService.isLoggedIn() && !this.authService.isAdmin()) {
            console.log("True")
            return true;
        } else {
            console.log("False")
            this.router.navigate(['/login']);
            return false;
        }
    }

}