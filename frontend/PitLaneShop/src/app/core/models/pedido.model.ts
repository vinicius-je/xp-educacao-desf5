export interface CreateItemPedido {
  produtoId: string;
  quantidade: number;
}

export interface CreatePedido {
  clienteId: string;
  codigoPromocionalId?: string | null;
  itens: CreateItemPedido[];
}

export interface ItemPedidoResponse {
  id: string;
  produtoId: string;
  descricao: string;
  valorUnitario: number;
  quantidade: number;
  valorTotal: number;
}

export interface PedidoResponse {
  id: string;
  dataPedido: string;
  valorTotal: number;
  valorDesconto: number;
  status: number;
  clienteId: string;
  codigoPromocionalId: string | null;
  itens: ItemPedidoResponse[];
}

export const STATUS_PEDIDO_LABELS: Record<number, string> = {
  0: 'Em andamento',
  1: 'Pago',
  2: 'Cancelado',
  3: 'Em rota',
  4: 'Entregue',
};
