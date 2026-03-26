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
  {
    path: 'checkout/:id',
    loadComponent: () =>
      import('./pages/checkout/checkout.component').then((m) => m.CheckoutComponent),
  },
  {
    path: 'pedido-confirmacao/:id',
    loadComponent: () =>
      import('./pages/pedido-confirmacao/pedido-confirmacao.component').then(
        (m) => m.PedidoConfirmacaoComponent,
      ),
  },
  {
    path: 'perfil/:id',
    loadComponent: () =>
      import('./pages/perfil/perfil.component').then(
        (m) => m.PerfilComponent,
      ),
  },
  { path: '**', redirectTo: '' },
];
