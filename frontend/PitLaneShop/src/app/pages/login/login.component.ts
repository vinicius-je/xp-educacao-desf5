import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { InputText } from 'primeng/inputtext';
import { Button } from 'primeng/button';
import { Card } from 'primeng/card';
import { Message } from 'primeng/message';
import { ClienteService } from '../../core/services/cliente.service';

@Component({
  selector: 'app-login',
  imports: [FormsModule, InputText, Button, Card, Message],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  nome = signal('');
  email = signal('');
  telefone = signal('');
  loading = signal(false);
  error = signal('');

  constructor(
    private clienteService: ClienteService,
    private router: Router,
  ) {}

  async onSubmit() {
    this.error.set('');

    if (!this.nome() || !this.email() || !this.telefone()) {
      this.error.set('Preencha todos os campos.');
      return;
    }

    this.loading.set(true);
    try {
      const cliente = await this.clienteService.create({
        nome: this.nome(),
        email: this.email(),
        telefone: this.telefone(),
      });
      await this.router.navigate(['/home', cliente.id]);
    } catch {
      this.error.set('Erro ao cadastrar. Tente novamente.');
    } finally {
      this.loading.set(false);
    }
  }
}
