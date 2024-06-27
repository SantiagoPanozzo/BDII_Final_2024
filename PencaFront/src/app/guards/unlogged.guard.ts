import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
    providedIn: 'root'
})
export class UnloggedGuard implements CanActivate {

    constructor(private authService: AuthService, private router: Router) {}

    canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean {
        console.log("StudentGuard: Checking if logged in and student")
        if (!this.authService.isLoggedIn()) {
            return true;
        } else {
            if(this.authService.isAdmin()){
                this.router.navigate(['/admin-dashboard']);
            } else {
                this.router.navigate(['/student-dashboard'])
            }
            return false;
        }
    }

}