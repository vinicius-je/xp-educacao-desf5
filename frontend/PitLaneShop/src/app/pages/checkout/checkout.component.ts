import { Component, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Card } from 'primeng/card';
import { Button } from 'primeng/button';
import { InputText } from 'primeng/inputtext';
import { Message } from 'primeng/message';
import { CartService } from '../../core/services/cart.service';
import { PedidoService } from '../../core/services/pedido.service';
import { CodigoPromocionalService } from '../../core/services/codigo-promocional.service';
import { CodigoPromocionalResponse } from '../../core/models/codigo-promocional.model';

@Component({
  selector: 'app-checkout',
  imports: [FormsModule, Card, Button, InputText, Message],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css',
})
export class CheckoutComponent implements OnInit {
  clienteId = signal<string | null>(null);
  
  codigoInput = signal('');
  cupomAplicado = signal<CodigoPromocionalResponse | null>(null);
  cupomErro = signal('');

  finalizando = signal(false);
  erroPedido = signal('');

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public cart: CartService,
    private pedidoService: PedidoService,
    private promoService: CodigoPromocionalService,
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.clienteId.set(id);
    }
    
    // Se carrinho vazio, volta para home
    if (this.cart.items().length === 0 && id) {
      this.router.navigate(['/home', id]);
    }
  }

  async aplicarCupom() {
    this.cupomErro.set('');
    this.cupomAplicado.set(null);
    
    const codigo = this.codigoInput().trim();
    if (!codigo) return;

    try {
      const promocao = await this.promoService.getByCodigo(codigo);
      if (!promocao) {
        this.cupomErro.set('Cupom inválido ou não encontrado.');
        return;
      }
      if (!promocao.ehValido) {
        this.cupomErro.set('Este cupom não é mais válido.');
        return;
      }
      this.cupomAplicado.set(promocao);
    } catch {
      this.cupomErro.set('Erro ao validar cupom.');
    }
  }

  get valorDesconto() {
    const cupom = this.cupomAplicado();
    if (!cupom) return 0;
    return cupom.desconto; // Valor fixo assumido aqui base na API (pode ser porcentagem no seu sistema, ajusto se for o caso)
  }

  get totalFinal() {
    const subtotal = this.cart.totalValor();
    const total = subtotal - this.valorDesconto;
    return total > 0 ? total : 0;
  }

  formatCurrency(value: number): string {
    return value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
  }

  async finalizarPedido() {
    const id = this.clienteId();
    if (!id || this.cart.items().length === 0) return;

    this.finalizando.set(true);
    this.erroPedido.set('');

    try {
      const pedido = await this.pedidoService.create({
        clienteId: id,
        codigoPromocionalId: this.cupomAplicado()?.id || null,
        itens: this.cart.items().map((i) => ({
          produtoId: i.produto.id,
          quantidade: i.quantidade,
        })),
      });
      this.cart.clear();
      await this.router.navigate(['/pedido-confirmacao', pedido.id]);
    } catch {
      this.erroPedido.set('Erro ao finalizar pedido. Tente novamente.');
    } finally {
      this.finalizando.set(false);
    }
  }

  voltar() {
    const id = this.clienteId();
    if (id) {
      this.router.navigate(['/home', id]);
    } else {
      this.router.navigate(['/']);
    }
  }
}
