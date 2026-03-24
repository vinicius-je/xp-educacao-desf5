import { Injectable } from '@angular/core';
import { api } from '../api.service';
import { ProdutoResponse } from '../models/produto.model';

@Injectable({ providedIn: 'root' })
export class ProdutoService {
  async getAll(): Promise<ProdutoResponse[]> {
    const response = await api.get<ProdutoResponse[]>('/produtos');
    return response.data;
  }

  async getById(id: string): Promise<ProdutoResponse> {
    const response = await api.get<ProdutoResponse>(`/produtos/${id}`);
    return response.data;
  }
}
