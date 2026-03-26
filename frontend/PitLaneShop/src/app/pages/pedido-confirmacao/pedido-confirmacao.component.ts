import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Card } from 'primeng/card';
import { Button } from 'primeng/button';
import { PedidoService } from '../../core/services/pedido.service';
import { PedidoResponse } from '../../core/models/pedido.model';

@Component({
  selector: 'app-pedido-confirmacao',
  imports: [Card, Button],
  templateUrl: './pedido-confirmacao.component.html',
  styleUrl: './pedido-confirmacao.component.css',
})
export class PedidoConfirmacaoComponent implements OnInit {
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
      this.error.set('Erro ao carregar os dados do pedido.');
    } finally {
      this.loading.set(false);
    }
  }

  formatCurrency(value: number): string {
    return value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
  }

  voltarParaLoja() {
    const clienteId = this.pedido()?.clienteId;
    if (clienteId) {
      this.router.navigate(['/home', clienteId]);
    } else {
      this.router.navigate(['/']);
    }
  }

  verDetalhes() {
    const id = this.pedido()?.id;
    if (id) {
      this.router.navigate(['/pedido', id]);
    }
  }
}
