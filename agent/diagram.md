# Modelo de dados (entidades)

Diagrama alinhado ao código em `backend/PitLaneShop/PitLaneShop/Model/`.

Todas as entidades herdam **`EntidadeBase`**: `Guid Id`, `DateTime DataCriacao`, `DateTime? DataAtualizacao` (abaixo repetido em cada bloco para leitura no diagrama).

```classDiagram
  direction TB

  class Cliente {
    +Guid Id
    +string Nome
    +string Email
    +string Telefone
  }

  class Pedido {
    +Guid Id
    +DateOnly DataPedido
    +decimal ValorTotal
    +StatusPedido Status
    +Guid ClienteId
    +Guid? CodigoPromocionalId
  }

  class CodigoPromocional {
    +Guid Id
    +string Codigo
    +decimal Desconto
    +bool EhValido
  }

  class ItemPedido {
    +Guid Id
    +decimal ValorUnitario
    +decimal ValorTotal
    +int Quantidade
    +string Descricao
    +Guid PedidoId
    +Guid ProdutoId
  }

  class Produto {
    +Guid Id
    +string Nome
    +string Imagem
    +string Descricao
    +decimal Preco
    +int QuantidadeEstoque
    +CategoriaProduto Categoria
  }

  Cliente "1" --> "0..*" Pedido : Realiza um
  Pedido "1" --> "1..*" ItemPedido : contém
  Pedido "0..*" --> "0..1" CodigoPromocional : aplica
  ItemPedido "0..*" --> "1" Produto : referencia
```

## Tipos no código C#

| No diagrama | Tipo em C# |
|-------------|------------|
| `Guid` | `Guid` |
| `DateOnly` | `DateOnly` |
| `DateTime` | `DateTime` |
| `decimal` | `decimal` |

**Nuláveis no código:** `Pedido.CodigoPromocionalId` (`Guid?`), `EntidadeBase.DataAtualizacao` (`DateTime?`). Navegações (`Cliente`, `Pedido`, `Produto`, `CodigoPromocional`) são referências opcionais nas FKs.

## Enums (`Model/Enums`)

### `StatusPedido`

- `Em_andamento` (0)
- `Pago` (1)
- `Cancelado` (2)
- `Em_rota` (3)
- `Entregue` (4)

### `CategoriaProduto`

- `OleoLubrificante` (0)
- `PneuRoda` (1)
- `Freio` (2)
- `Suspensao` (3)
- `EletricaBateria` (4)
- `Filtros` (5)
- `Escapamento` (6)
- `CarroceriaPintura` (7)
- `AcessoriosInterior` (8)
- `SomMultimidia` (9)
- `Iluminacao` (10)
- `LimpezaConservacao` (11)
- `Ferramentas` (12)
- `Seguranca` (13)
- `PerformanceTuning` (14)
