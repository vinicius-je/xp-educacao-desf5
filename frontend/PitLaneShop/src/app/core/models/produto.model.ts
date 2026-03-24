export interface ProdutoResponse {
  id: string;
  nome: string;
  imagem: string;
  descricao: string;
  preco: number;
  quantidadeEstoque: number;
  categoria: number;
}

export const CATEGORIA_LABELS: Record<number, string> = {
  0: 'Óleo e Lubrificante',
  1: 'Pneu e Roda',
  2: 'Freio',
  3: 'Suspensão',
  4: 'Elétrica e Bateria',
  5: 'Filtros',
  6: 'Escapamento',
  7: 'Carroceria e Pintura',
  8: 'Acessórios e Interior',
  9: 'Som e Multimídia',
  10: 'Iluminação',
  11: 'Limpeza e Conservação',
  12: 'Ferramentas',
  13: 'Segurança',
  14: 'Performance e Tuning',
};
