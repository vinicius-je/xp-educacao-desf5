import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Card } from 'primeng/card';
import { Tag } from 'primeng/tag';
import { Button } from 'primeng/button';
import { PedidoService } from '../../core/services/pedido.service';
import { PedidoResponse, STATUS_PEDIDO_LABELS } from '../../core/models/pedido.model';

@Component({
  selector: 'app-pedido-detalhe',
  imports: [Card, Tag, Button],
  templateUrl: './pedido-detalhe.component.html',
})
export class PedidoDetalheComponent implements OnInit {
  pedido = signal<PedidoResponse | null>(null);
  loading = signal(true);
  error = signal('');

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private pedidoService: PedidoService,
  ) {}

  async ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) return;

    try {
      const pedido = await this.pedidoService.getById(id);
      this.pedido.set(pedido);
    } catch {
      this.error.set('Erro ao carregar pedido.');
    } finally {
      this.loading.set(false);
    }
  }

  statusLabel(status: number): string {
    return STATUS_PEDIDO_LABELS[status] ?? 'Desconhecido';
  }

  statusSeverity(status: number): 'info' | 'success' | 'danger' | 'warn' {
    switch (status) {
      case 0: return 'info';
      case 1: return 'success';
      case 2: return 'danger';
      case 3: return 'warn';
      case 4: return 'success';
      default: return 'info';
    }
  }

  formatCurrency(value: number): string {
    return value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
  }

  voltarHome() {
    const clienteId = this.pedido()?.clienteId;
    if (clienteId) {
      this.router.navigate(['/home', clienteId]);
    } else {
      this.router.navigate(['/']);
    }
  }
}
