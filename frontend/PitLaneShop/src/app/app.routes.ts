import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./pages/login/login.component').then((m) => m.LoginComponent),
  },
  {
    path: 'home/:id',
    loadComponent: () =>
      import('./pages/home/home.component').then((m) => m.HomeComponent),
  },
  {
    path: 'pedido/:id',
    loadComponent: () =>
      import('./pages/pedido-detalhe/pedido-detalhe.component').then(
        (m) => m.PedidoDetalheComponent,
      ),
  },
  { path: '**', redirectTo: '' },
];
