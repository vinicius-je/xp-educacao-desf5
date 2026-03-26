import { RenderMode, ServerRoute } from '@angular/ssr';

export const serverRoutes: ServerRoute[] = [
  {
    path: 'home/:id',
    renderMode: RenderMode.Client,
  },
  {
    path: 'pedido/:id',
    renderMode: RenderMode.Client,
  },
  {
    path: 'checkout/:id',
    renderMode: RenderMode.Client,
  },
  {
    path: 'pedido-confirmacao/:id',
    renderMode: RenderMode.Client,
  },
  {
    path: '**',
    renderMode: RenderMode.Prerender,
  },
];
