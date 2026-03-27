# Arquitetura do Frontend

Este documento descreve as decisões arquiteturais, as tecnologias utilizadas e a estrutura do projeto para a aplicação frontend **PitLaneShop**.

## Tecnologias Utilizadas

- **Framework**: Angular 21 (Componentes Standalone)
- **Renderização no Servidor (SSR)**: Habilitada através do `@angular/ssr` e Express
- **Interface e Estilização**: 
  - [Tailwind CSS v4](https://tailwindcss.com/) (integrado via PostCSS)
  - [PrimeNG](https://primeng.org/) para componentes de UI, combinado com os temas do `@primeuix/themes`
- **Cliente HTTP**: [Axios](https://axios-http.com/)
- **Testes**: [Vitest](https://vitest.dev/) com o `jsdom` (substituindo a configuração padrão do Jasmine/Karma)
- **Formatação de Código**: Prettier

## Estrutura do Projeto

O projeto adota uma estrutura de pastas orientada ao domínio para separar as configurações principais da lógica específica das páginas.

```text
src/
└── app/
    ├── core/                # Lógica principal da aplicação e configurações
    │   ├── api.service.ts   # Configuração da instância do Axios
    │   ├── environment.ts   # Variáveis de ambiente
    │   ├── models/          # Interfaces TypeScript globais e tipagens
    │   └── services/        # Serviços globais da aplicação
    ├── pages/               # Componentes inteligentes (smart components) servindo como páginas roteáveis
    │   ├── home/            # Página inicial
    │   ├── login/           # Página de autenticação
    │   └── pedido-detalhe/  # Página de detalhes do pedido
    ├── app.config.ts        # Configuração global da aplicação Angular (providers)
    ├── app.config.server.ts # Configuração global para o servidor (SSR)
    ├── app.routes.ts        # Definições das rotas da aplicação
    └── app.component.ts     # Componente raiz
```

## Principais Decisões Arquiteturais

### 1. Componentes Standalone
A aplicação utiliza estritamente os Componentes Standalone (Independentes) do Angular. Não há `NgModules` (`app.module.ts`), reduzindo o código repetitivo (boilerplate) e tornando os componentes passíveis de tree-shaking (remoção de código não utilizado) e mais fáceis para o carregamento sob demanda (lazy-load).

### 2. Renderização no Servidor (SSR)
A Renderização no Servidor (SSR) está implementada para melhorar a performance inicial de carregamento (First Contentful Paint) e a otimização para motores de busca (SEO). A aplicação executa um servidor Node/Express (`server.mjs`) em produção, que é o responsável pela renderização inicial da página.

### 3. Axios para Requisições HTTP
Ao invés de utilizar o `HttpClient` embutido no padrão do Angular, a aplicação faz o uso do **Axios**. 
A instância centralizada do Axios é configurada no `src/app/core/api.service.ts`:
- Define a `baseURL` a partir das configurações de ambiente.
- Configura os cabeçalhos padrões (`Content-Type: application/json`).

### 4. Estratégia de Estilização
A aplicação combina o **Tailwind CSS** com sua abordagem baseada em classes utilitárias para o design das estruturas e o layout, juntamente com o **PrimeNG** para a aplicação de componentes de interface iterativos e mais complexos (ex., tabelas, caixas de seleção).

### 5. Execução Rápida de Testes Unitários com o Vitest
Para oferecer uma experiência de desenvolvimento mais moderna e rápida, a aplicação também foi estruturada para rodar seus testes unitários utilizando o **Vitest** ao invés do executor tradicional do Angular que possui o Jasmine/Karma. O pacote `jsdom` é utilizado durante o ciclo de testes para prover a simulação do ambiente visual do navegador.
