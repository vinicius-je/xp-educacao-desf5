import { Component, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Card } from 'primeng/card';
import { Tag } from 'primeng/tag';
import { InputText } from 'primeng/inputtext';
import { Button } from 'primeng/button';
import { Badge } from 'primeng/badge';
import { ClienteService } from '../../core/services/cliente.service';
import { ProdutoService } from '../../core/services/produto.service';
import { CartService } from '../../core/services/cart.service';
import { ClienteResponse } from '../../core/models/cliente.model';
import { ProdutoResponse, CATEGORIA_LABELS } from '../../core/models/produto.model';

@Component({
  selector: 'app-home',
  imports: [FormsModule, RouterModule, Card, Tag, InputText, Button, Badge],
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  cliente = signal<ClienteResponse | null>(null);
  produtos = signal<ProdutoResponse[]>([]);
  loading = signal(true);
  filtro = signal('');
  cartOpen = signal(false);

  private searchTimeout: any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private clienteService: ClienteService,
    private produtoService: ProdutoService,
    public cart: CartService,
  ) {}

  async ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) return;

    try {
      const [cliente, produtos] = await Promise.all([
        this.clienteService.getById(id),
        this.produtoService.getAll(),
      ]);
      this.cliente.set(cliente);
      this.produtos.set(produtos);
    } finally {
      this.loading.set(false);
    }
  }

  onFiltroChange(value: string) {
    this.filtro.set(value);
    
    if (this.searchTimeout) {
      clearTimeout(this.searchTimeout);
    }

    this.searchTimeout = setTimeout(async () => {
      const termo = value.trim();
      const produtos = await this.produtoService.getAll(termo);
      this.produtos.set(produtos);
    }, 400); // 400ms debounce
  }

  addToCart(produto: ProdutoResponse) {
    this.cart.addItem(produto);
    this.cartOpen.set(true);
  }

  toggleCart() {
    this.cartOpen.update((v) => !v);
  }

  async finalizarPedido() {
    const cliente = this.cliente();
    if (!cliente || this.cart.items().length === 0) return;

    this.cartOpen.set(false);
    await this.router.navigate(['/checkout', cliente.id]);
  }

  categoriaLabel(cat: number): string {
    return CATEGORIA_LABELS[cat] ?? 'Outros';
  }

  estoqueSeverity(qtd: number): 'success' | 'warn' | 'danger' {
    if (qtd > 10) return 'success';
    if (qtd > 0) return 'warn';
    return 'danger';
  }

  estoqueLabel(qtd: number): string {
    if (qtd === 0) return 'Esgotado';
    if (qtd === 1) return '1 em estoque';
    return `${qtd} em estoque`;
  }

  formatCurrency(value: number): string {
    return value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
  }
}
