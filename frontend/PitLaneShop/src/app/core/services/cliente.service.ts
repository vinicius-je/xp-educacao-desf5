import { Injectable } from '@angular/core';
import { api } from '../api.service';
import { ClienteResponse, CreateCliente } from '../models/cliente.model';

@Injectable({ providedIn: 'root' })
export class ClienteService {
  async create(data: CreateCliente): Promise<ClienteResponse> {
    const response = await api.post<ClienteResponse>('/clientes', data);
    return response.data;
  }

  async getById(id: string): Promise<ClienteResponse> {
    const response = await api.get<ClienteResponse>(`/clientes/${id}`);
    return response.data;
  }
}
