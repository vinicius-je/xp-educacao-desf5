import { Injectable } from '@angular/core';
import { api } from '../api.service';
import { CodigoPromocionalResponse } from '../models/codigo-promocional.model';

@Injectable({ providedIn: 'root' })
export class CodigoPromocionalService {
  async getAll(): Promise<CodigoPromocionalResponse[]> {
    const response = await api.get<CodigoPromocionalResponse[]>('/codigos-promocionais');
    return response.data;
  }

  async getByCodigo(codigo: string): Promise<CodigoPromocionalResponse | null> {
    const todos = await this.getAll();
    const codigoEncontrado = todos.find(
      (c) => c.codigo.toUpperCase() === codigo.toUpperCase()
    );
    return codigoEncontrado || null;
  }
}
