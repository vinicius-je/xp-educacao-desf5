import { Injectable } from '@angular/core';
import { api } from '../api.service';
import { ProdutoResponse } from '../models/produto.model';

@Injectable({ providedIn: 'root' })
export class ProdutoService {
  async getAll(nome?: string): Promise<ProdutoResponse[]> {
    const params = nome ? { nome } : {};
    const response = await api.get<ProdutoResponse[]>('/produtos', { params });
    console.log(response)
    return response.data;
  }

  async getById(id: string): Promise<ProdutoResponse> {
    const response = await api.get<ProdutoResponse>(`/produtos/${id}`);
    return response.data;
  }
}
