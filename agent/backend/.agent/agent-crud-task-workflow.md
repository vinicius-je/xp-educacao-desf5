# Agent workflow — full CRUD (PitLaneShop)

This document guides **AI agents** (or developers) through implementing a **complete REST CRUD** aligned with the architecture in **`agent/architecture.md`**.

---

## Reference documents (read first)

| Document | Repository path | Purpose |
|----------|-----------------|---------|
| **API architecture** | `agent/architecture.md` | Layers, HTTP → Controller → Service → Repository → DB flow, folder conventions, DI. |
| **Data model (ERD + enums)** | **`agent/diagram.md`** | Entities, fields, C# types, FKs, enums, nullability. **Read before** creating or changing entities, DTOs, or EF mappings. |

Typical paths from repo root (adjust to your clone):

- Architecture: `agent/architecture.md`
- Diagram: **`agent/diagram.md`**

---

## Recommended implementation order

Following this sequence avoids broken compile cycles and matches the pattern already used for **Cliente** (reference implementation).

### 0. Discovery

1. Open **`agent/diagram.md`** and confirm whether the entity already exists, its relationships, enums, and types (`Guid`, `DateOnly`, etc.).
2. Open **`agent/architecture.md`** to review the request flow and folder layout.
3. Inspect the reference implementation:
   - `Controllers/ClientesController.cs`
   - `Services/Features/Cliente/` (`Dtos`, `Interfaces`, `Implementation`)
   - `Model/Repositories/IClienteRepository.cs`
   - `Persistence/Repositories/ClienteRepository.cs`
   - `Persistence/EntitiesMapping/ClienteConfiguration.cs`

Replace `X` / `EntidadeX` with the resource name (e.g. `Carro`, `VeiculoModelo`).

---

### 1. Domain model (`Model/`)

- If the entity **does not exist** or needs changes: add or update it under **`Model/Entities/`**, inheriting **`EntidadeBase`** (`Id`, `DataCriacao`, `DataAtualizacao`).
- New or updated enums: **`Model/Enums/`**.
- Include a **parameterless constructor** (for EF) and, when it fits the domain, a parameterized constructor for explicit creation (as in `Cliente`).

**Checklist:** fields and relationships match **`agent/diagram.md`**.

---

### 2. Persistence — EF mapping (`Persistence/`)

1. **`Persistence/EntitiesMapping/{Entity}Configuration.cs`** — `IEntityTypeConfiguration<T>`: table name, key, string lengths, unique indexes, FKs, `DeleteBehavior`, `decimal` precision, enum conversions if needed.
2. **`PitLaneShopDbContext.cs`** — add **`DbSet<T>`** for a new entity.
3. Configurations are picked up via **`ApplyConfigurationsFromAssembly`**; no manual registration per class.

---

### 3. Repository

1. **`Model/Repositories/I{X}Repository.cs`** — empty interface extending **`IBaseRepository<{Entity}>`** (already defined in the project).
2. **`Persistence/Repositories/{X}Repository.cs`** — class inheriting **`BaseRepository<{Entity}>`** and implementing **`I{X}Repository`**, constructor taking **`PitLaneShopDbContext`** and calling `base(context)`.

---

### 4. EF Core migration

From the API project (`backend/PitLaneShop/PitLaneShop/`):

```bash
dotnet ef migrations add PitLaneShopV{N} --output-dir Persistence/Migrations
```

Align migration name/id with the repo convention if required (see existing files under **`Persistence/Migrations/`**).

On startup the app runs **`Database.Migrate()`** (`Program.cs`); in development use `dotnet ef database update` when you need to apply manually.

---

### 5. Application service (`Services/Features/{X}/`)

Create **`Services/Features/{X}/`** with:

| Subfolder | Contents |
|-----------|----------|
| **`Dtos/`** | `{X}ResponseDto`, `Create{X}Dto`, `Update{X}Dto` — mirror what the API exposes (do not expose the entity directly). |
| **`Interfaces/`** | **`I{X}Service : IBaseCrudService<{X}ResponseDto, Create{X}Dto, Update{X}Dto>`** |
| **`Implementation/`** | **`{X}Service`** inheriting **`BaseCrudService<Entity, …>`** and **`I{X}Service`**, implementing **`MapToResponse`**, **`MapFromCreate`**, **`ApplyUpdate`**. |

**Namespace clash:** if the feature folder name equals the entity class name (e.g. `Cliente`), use a **type alias** in the service (`using EntityX = PitLaneShop.Model.Entities.X`).

---

### 6. Composition (`Program.cs`)

- **`builder.Services.AddScoped<I{X}Repository, {X}Repository>();`**
- **`builder.Services.AddScoped<I{X}Service, {X}Service>();`**

---

### 7. REST API (`Controllers/`)

- Add **`{X}sController`** (or another plural name that matches the resource) following the **REST** style of **`ClientesController`**:
  - **`[Route("api/{lowercase-plural-resource}")]`**
  - **`[Produces("application/json")]`**; **`[Consumes]`** on POST/PUT
  - **Route names** (`Name = nameof(...)`) for **`CreatedAtRoute`** on POST
  - **GET** collection and **GET** by `id:guid`; **POST** → 201 + body + `Location`; **PUT** full replacement; **DELETE** → 204 or 404

---

### 8. Final verification

- `dotnet build` on the API `.csproj`.
- Run the API and verify verbs and status codes in **Swagger** (Development).
- Ensure **`agent/diagram.md`** still matches the model (update the diagram if the domain changed).

---

## One-line flow summary

**`agent/diagram.md`** (domain) → **Entity + mapping + DbSet** → **Repository (interface + impl)** → **Migration** → **DTOs + Service (BaseCrudService)** → **`Program.cs`** → **REST controller** → **build + Swagger**.

---

## Cross-references

- Full architecture: **`agent/architecture.md`**
- ERD and enums: **`agent/diagram.md`**
