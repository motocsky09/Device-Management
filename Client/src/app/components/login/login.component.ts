import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { LoginRequest } from '../../models/auth.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  credentials: LoginRequest = {
    email: '',
    password: ''
  };
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) {}

  login(): void {
    if (!this.credentials.email || !this.credentials.password) {
      this.errorMessage = 'All fields are required.';
      return;
    }

    this.authService.login(this.credentials).subscribe({
      next: (response) => {
        this.authService.saveSession(response);
        this.router.navigate(['/devices']);
      },
      error: () => {
        this.errorMessage = 'Invalid email or password.';
      }
    });
  }

  goToRegister(): void {
    this.router.navigate(['/register']);
  }
}