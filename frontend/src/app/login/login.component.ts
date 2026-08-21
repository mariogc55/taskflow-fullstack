import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  credentials = {
    email: '',
    password: ''
  };
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) {}

  onLogin(): void {
    console.log('Botón de entrar presionado. Datos:', this.credentials);

    if (!this.credentials.email || !this.credentials.password) {
      this.errorMessage = 'Por favor, introduce el correo y la contraseña.';
      return;
    }

    this.authService.login(this.credentials).subscribe({
      next: (response) => {
        console.log('Login exitoso, respuesta del servidor:', response);
        this.router.navigate(['/tasks']);
      },
      error: (err) => {
        console.error('Error detallado en el login:', err);
        this.errorMessage = err.error?.message || 'Error al iniciar sesión. Verifique sus datos.';
      }
    });
  }
}