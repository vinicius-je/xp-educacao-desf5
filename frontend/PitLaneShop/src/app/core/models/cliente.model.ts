export interface ClienteResponse {
  id: string;
  nome: string;
  email: string;
  telefone: string;
}

export interface CreateCliente {
  nome: string;
  email: string;
  telefone: string;
}
