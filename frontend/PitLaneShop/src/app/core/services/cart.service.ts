import { Injectable, computed, signal } from '@angular/core';
import { CartItem } from '../models/cart.model';
import { ProdutoResponse } from '../models/produto.model';

@Injectable({ providedIn: 'root' })
export class CartService {
  private _items = signal<CartItem[]>([]);

  readonly items = this._items.asReadonly();

  readonly totalItens = computed(() =>
    this._items().reduce((sum, i) => sum + i.quantidade, 0),
  );

  readonly totalValor = computed(() =>
    this._items().reduce((sum, i) => sum + i.produto.preco * i.quantidade, 0),
  );

  addItem(produto: ProdutoResponse) {
    const current = this._items();
    const existing = current.find((i) => i.produto.id === produto.id);

    if (existing) {
      this._items.set(
        current.map((i) =>
          i.produto.id === produto.id
            ? { ...i, quantidade: i.quantidade + 1 }
            : i,
        ),
      );
    } else {
      this._items.set([...current, { produto, quantidade: 1 }]);
    }
  }

  removeItem(produtoId: string) {
    this._items.set(this._items().filter((i) => i.produto.id !== produtoId));
  }

  updateQuantidade(produtoId: string, quantidade: number) {
    if (quantidade <= 0) {
      this.removeItem(produtoId);
      return;
    }
    this._items.set(
      this._items().map((i) =>
        i.produto.id === produtoId ? { ...i, quantidade } : i,
      ),
    );
  }

  clear() {
    this._items.set([]);
  }
}
