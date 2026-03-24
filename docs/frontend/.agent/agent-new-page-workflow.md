# Agent workflow: new Angular page (PitLaneShop)

This document guides **AI agents** (or developers) through creating a **new frontend page** aligned with the structure already implemented in **`frontend/PitLaneShop`**.

---

## Read first

Before creating a page, inspect these files:

- `frontend/PitLaneShop/package.json`: confirms the stack: Angular standalone, PrimeNG, Axios, Tailwind, SSR.
- `frontend/PitLaneShop/src/app/app.config.ts`: application providers and PrimeNG theme setup.
- `frontend/PitLaneShop/src/app/app.routes.ts`: client-side routes.
- `frontend/PitLaneShop/src/app/app.routes.server.ts`: SSR route behavior.
- `frontend/PitLaneShop/src/app/core/environment.ts`: API base URL.
- `frontend/PitLaneShop/src/app/core/api.service.ts`: shared Axios instance.
- `frontend/PitLaneShop/src/app/pages/login/`: reference for forms.
- `frontend/PitLaneShop/src/app/pages/home/`: reference for layout, toolbar, and list/card pages.

---

## Current frontend architecture

The project currently uses:

- **Angular standalone components**: no `AppModule`.
- **PrimeNG** for UI components.
- **Axios** through a shared `api` instance.
- **Tailwind CSS** for global utility support.
- **Angular SSR** with explicit route rendering config.
- **Feature pages under `src/app/pages/`**.
- **Shared API code under `src/app/core/`**.

Current folder conventions:

- `src/app/core/api.service.ts`: Axios instance.
- `src/app/core/environment.ts`: API URL.
- `src/app/core/models/`: response/request interfaces.
- `src/app/core/services/`: API service classes.
- `src/app/pages/{page-name}/`: component `.ts`, `.html`, `.css`.

---

## Design rules

Use these colors consistently in every new page:

- `#F5F4F3`: general page background and top navigation background.
- `#F2F2F2`: vehicle cards, info items, rental form surfaces, and confirmation detail panels.
- `#1a7a4a`: primary brand color: logo, left panel in signup/auth layouts, buttons, prices, active chips, and text accents.
- `#D92525`: fine amount / penalty value (`ValorMulta`).
- `#242123`: primary text color across the system.

### Design application rules

- Default page background should use `#F5F4F3`.
- Cards and contained surfaces should use `#F2F2F2` unless a PrimeNG component already provides a stronger semantic surface.
- Primary actions should use `#1a7a4a`.
- Penalty values should explicitly use `#D92525`.
- Main headings, body text, labels, and navigation text should prefer `#242123`.
- Keep the interface clean, airy, and consistent with the current login and home pages.

---

## Recommended implementation order

### 0. Discovery

1. Read the backend endpoint that will feed the page.
2. Confirm the response DTO shape and any route parameters.
3. Check whether an existing service in `src/app/core/services/` can be reused.
4. Inspect similar pages already implemented:
   - `src/app/pages/login/`
   - `src/app/pages/home/`

---

### 1. Define the data contract

If the page consumes new backend data, create or update models under:

- `frontend/PitLaneShop/src/app/core/models/`

Guidelines:

- Keep interfaces small and aligned to the backend DTO.
- Prefer one interface per API response shape.
- Use `string` for GUIDs and ISO dates unless transformation is required.

Example paths:

- `src/app/core/models/cliente.model.ts`
- `src/app/core/models/carro.model.ts`

---

### 2. Create or extend the API service

Add the API call under:

- `frontend/PitLaneShop/src/app/core/services/`

Rules:

- Reuse `api` from `src/app/core/api.service.ts`.
- Do not create a new Axios instance per page.
- Keep service methods thin: call the endpoint and return `response.data`.
- Use `@Injectable({ providedIn: 'root' })`.

Example pattern:

```ts
@Injectable({ providedIn: 'root' })
export class ExampleService {
  async getAll(): Promise<ExampleResponse[]> {
    const response = await api.get<ExampleResponse[]>('/resource');
    return response.data;
  }
}
```

---

### 3. Create the page component

Create a new folder under:

- `frontend/PitLaneShop/src/app/pages/{page-name}/`

Expected files:

- `{page-name}.component.ts`
- `{page-name}.component.html`
- `{page-name}.component.css`

Rules for the component:

- Use a **standalone component**.
- Import only the Angular and PrimeNG pieces actually needed.
- Prefer `signal()` for local state, matching the current implementation.
- Use `async` methods with `try/finally` for loading states.
- Keep API orchestration in the component, but keep mapping-heavy logic in services/helpers when it becomes complex.

Example patterns already used:

- `signal('')` for form state.
- `signal(false)` / `signal(true)` for loading.
- `Promise.all([...])` when multiple API calls can run in parallel.

---

### 4. Build the HTML structure

Use PrimeNG components already present in the app style:

- `p-card` for grouped content.
- `p-toolbar` for top navigation/header areas.
- `p-button` for actions.
- `p-message` for inline errors.
- `p-tag` for statuses.
- `pInputText` for text inputs.

Rules:

- Keep top-level page layout simple.
- Put page content inside a main container with predictable spacing.
- Prefer semantic sections: toolbar, main content, cards/list/form area.
- Use Angular control flow (`@if`, `@for`) as already implemented.

---

### 5. Add styling

Style the page in the local component CSS file first.

Guidelines:

- Keep `src/styles.css` for global defaults only.
- Put page-specific layout and visual treatment in the page CSS file.
- Use the project color palette above.
- Use soft shadows, moderate border radii, and spacious gaps.
- Favor readable layout over heavy decoration.

Recommended styling checklist:

- Page background uses `#F5F4F3`.
- Cards/surfaces use `#F2F2F2`.
- Primary buttons and accents use `#1a7a4a`.
- Text uses `#242123`.
- `ValorMulta` or penalty text uses `#D92525`.

---

### 6. Register the route

Add the page to:

- `frontend/PitLaneShop/src/app/app.routes.ts`

Use lazy-loaded standalone route definitions:

```ts
{
  path: 'example',
  loadComponent: () =>
    import('./pages/example/example.component').then((m) => m.ExampleComponent),
}
```

If the route contains dynamic parameters or should not be prerendered, also update:

- `frontend/PitLaneShop/src/app/app.routes.server.ts`

Rule:

- Use `RenderMode.Client` for dynamic routes such as `details/:id`.
- Keep static routes prerendered when possible.

---

### 7. Integrate navigation flow

If the page is reached from another page:

- Use `Router.navigate(...)` in the source component.
- Read route params with `ActivatedRoute` in the destination component.

Existing pattern:

- Login creates a client, then redirects to `['/home', cliente.id]`.
- Home reads `id` from `ActivatedRoute.snapshot.paramMap`.

---

### 8. Error and loading states

Every new page should handle at least:

- initial loading state
- empty state when relevant
- request failure state for user feedback

Guidelines:

- Use a simple inline message or text block for errors.
- Avoid leaving the page blank during loading.
- Do not swallow failed API calls silently.

---

### 9. Final verification

Run from:

- `frontend/PitLaneShop/`

Commands:

```bash
npm install
npx ng build
```

Verify:

- the page builds successfully
- route registration works
- API calls hit the configured backend URL from `src/app/core/environment.ts`
- PrimeNG imports are correct
- SSR route behavior is correct for dynamic pages
- colors match the design rules in this document

---

## One-line flow summary

**Read backend DTO/endpoint** ? **create/update model** ? **create/update core service** ? **create standalone page under `pages/`** ? **style with the project palette** ? **register route** ? **adjust SSR route mode if needed** ? **build and verify**.

---

## Reference paths

- Frontend app root: `frontend/PitLaneShop`
- Shared API instance: `frontend/PitLaneShop/src/app/core/api.service.ts`
- Environment config: `frontend/PitLaneShop/src/app/core/environment.ts`
- Client routes: `frontend/PitLaneShop/src/app/app.routes.ts`
- SSR routes: `frontend/PitLaneShop/src/app/app.routes.server.ts`
- Login reference: `frontend/PitLaneShop/src/app/pages/login/`
- Home reference: `frontend/PitLaneShop/src/app/pages/home/`
