import { Injectable } from '@angular/core';
import { api } from '../api.service';
import { CreatePedido, PedidoResponse } from '../models/pedido.model';

@Injectable({ providedIn: 'root' })
export class PedidoService {
  async create(data: CreatePedido): Promise<PedidoResponse> {
    const response = await api.post<PedidoResponse>('/pedidos', data);
    return response.data;
  }

  async getById(id: string): Promise<PedidoResponse> {
    const response = await api.get<PedidoResponse>(`/pedidos/${id}`);
    return response.data;
  }
}
