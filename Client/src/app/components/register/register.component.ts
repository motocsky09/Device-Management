import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { RegisterRequest } from '../../models/auth.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  data: RegisterRequest = {
    name: '',
    email: '',
    password: '',
    location: ''
  };
  errorMessage = '';
  successMessage = '';

  constructor(private authService: AuthService, private router: Router) {}

  register(): void {
    if (!this.data.name || !this.data.email || !this.data.password || !this.data.location) {
      this.errorMessage = 'All fields are required.';
      return;
    }

    this.authService.register(this.data).subscribe({
      next: () => {
        this.successMessage = 'Account created successfully. Redirecting to login...';
        setTimeout(() => this.router.navigate(['/login']), 2000);
      },
      error: (err) => {
        this.errorMessage = err.error || 'Registration failed.';
      }
    });
  }

  goToLogin(): void {
    this.router.navigate(['/login']);
  }
}