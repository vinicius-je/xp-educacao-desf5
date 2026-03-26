import { Component, OnInit, signal } from '@angular/core';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterModule } from '@angular/router';

import { ClienteResponse } from '../../core/models/cliente.model';
import { PedidoResponse } from '../../core/models/pedido.model';
import { ClienteService } from '../../core/services/cliente.service';
import { PedidoService } from '../../core/services/pedido.service';

import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { AccordionModule } from 'primeng/accordion';

@Component({
  selector: 'app-perfil',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    CurrencyPipe,
    DatePipe,
    CardModule,
    ButtonModule,
    AccordionModule
  ],
  templateUrl: './perfil.component.html',
  styleUrl: './perfil.component.css',
})
export class PerfilComponent implements OnInit {
  cliente = signal<ClienteResponse | null>(null);
  pedidos = signal<PedidoResponse[]>([]);
  loading = signal(true);
  error = signal<string | null>(null);

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private clienteService: ClienteService,
    private pedidoService: PedidoService,
  ) {}

  async ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) {
      this.error.set('Cliente ID não fornecido na URL.');
      this.loading.set(false);
      return;
    }

    try {
      const [clienteData, pedidosData] = await Promise.all([
        this.clienteService.getById(id),
        this.pedidoService.getByClienteId(id),
      ]);
      this.cliente.set(clienteData);
      
      // Ordena por data mais recente caso nao venha ordenado da api
      this.pedidos.set(pedidosData.sort((a, b) => 
        new Date(b.dataPedido).getTime() - new Date(a.dataPedido).getTime()
      ));
    } catch (err: any) {
      console.error('Erro ao carregar perfil', err);
      this.error.set(
        err.response?.data?.message || err.message || 'Erro inesperado',
      );
    } finally {
      this.loading.set(false);
    }
  }

  voltarHome() {
    const id = this.cliente()?.id || this.route.snapshot.paramMap.get('id');
    if (id) {
      this.router.navigate(['/home', id]);
    } else {
      this.router.navigate(['/']);
    }
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: 'BRL',
    }).format(value);
  }
}
