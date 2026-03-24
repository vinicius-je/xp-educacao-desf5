# Modelo de dados (entidades)

Diagrama alinhado ao código em `backend/PitLaneShop/PitLaneShop/Model/`.

Todas as entidades herdam **`EntidadeBase`**: `Guid Id`, `DateTime DataCriacao`, `DateTime DataAtualizacao` (abaixo repetido em cada bloco para leitura no diagrama).

```erDiagram
    Cliente ||--o{ Aluguel : "realiza"
    Carro ||--o{ Aluguel : "objeto do"
    VeiculoModelo ||--|{ Carro : "modelo de"
    VeiculoModelo ||--|{ TarifaDiaria : "tarifa por"

    Cliente {
        guid Id PK
        datetime DataCriacao
        datetime DataAtualizacao
        string Nome
        string Email
        string Telefone
    }

    Aluguel {
        guid Id PK
        datetime DataCriacao
        datetime DataAtualizacao
        date DataRetirada
        date DataDevolucaoPrevista
        date DataDevolucao
        int Diarias
        decimal ValorTotal
        decimal ValorMulta
        guid ClienteId FK
        guid CarroId FK
    }

    Carro {
        guid Id PK
        datetime DataCriacao
        datetime DataAtualizacao
        string Placa
        string Quilometragem
        StatusCarro Status
        guid VeiculoModeloId FK
    }

    VeiculoModelo {
        guid Id PK
        datetime DataCriacao
        datetime DataAtualizacao
        string Marca
        string Modelo
        CategoriaVeiculo Categoria
    }

    TarifaDiaria {
        guid Id PK
        datetime DataCriacao
        datetime DataAtualizacao
        decimal ValorDiaria
        decimal ValorMulta
        bool EhValorDiariaVigente
        date DataInicioVigencia
        date DataFimVigencia
        guid VeiculoModeloId FK
    }
```

## Tipos no código C#

| No diagrama | Tipo em C# |
|-------------|------------|
| `guid` | `Guid` |
| `date` | `DateOnly` |
| `datetime` | `DateTime` |

**Nuláveis no código:** `Aluguel.DataDevolucao` (`DateOnly?`), `TarifaDiaria.DataFimVigencia` (`DateOnly?`). Navegações (`Cliente`, `Carro`, `VeiculoModelo`) são referências opcionais nas FKs.

## Enums (`Model/Enums`)

### `StatusCarro`

- `Disponivel` (0)
- `Alugado` (1)

### `CategoriaVeiculo`

- `Compacto` (0)
- `Sedan` (1)
- `Hatch` (2)
- `Esportivo` (3)
- `Suv` (4)
- `Pickup` (5)
- `Minivan` (6)
- `Coupe` (7)
- `Conversivel` (8)
- `Utilitario` (9)
