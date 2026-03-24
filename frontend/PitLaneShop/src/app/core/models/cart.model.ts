import { ProdutoResponse } from './produto.model';

export interface CartItem {
  produto: ProdutoResponse;
  quantidade: number;
}
