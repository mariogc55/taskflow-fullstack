# TaskFlow — Gestor de Tareas Full Stack

TaskFlow es una aplicación web de gestión de tareas construida con una arquitectura desacoplada: **Angular** en el frontend y **ASP.NET Core Web API** en el backend, aplicando principios de **Clean Architecture** y persistencia mediante **Entity Framework Core** sobre **PostgreSQL**.

Es un proyecto personal de aprendizaje, orientado a practicar el stack .NET + Angular en un caso de uso real de gestión de tareas, con despliegue completo en la nube (producción).

## Demo en vivo

**Prueba la aplicación aquí:** [taskflow-fullstack-chi.vercel.app](https://taskflow-fullstack-chi.vercel.app/tasks)

## Estado del proyecto

En producción. Las funcionalidades principales, incluyendo el sistema completo de autenticación de usuarios (JWT) y el CRUD de tareas persistente en la nube, se encuentran implementadas y operativas.

## Tecnologías utilizadas

**Frontend**
- Angular (framework SPA)
- TypeScript
- Bootstrap 5 (UI/UX responsivo)
- Interceptores HTTP para gestión de autenticación (JWT)

**Backend**
- .NET 9 / C# (ASP.NET Core Web API)
- Entity Framework Core (ORM)
- PostgreSQL (base de datos relacional en la nube mediante Railway)
- Autenticación JWT (JSON Web Tokens) y hashing de contraseñas con BCrypt

**Infraestructura y despliegue**
- Vercel (despliegue del frontend SPA)
- Railway (despliegue del backend y base de datos PostgreSQL)
- GitHub (control de versiones)

## Características actuales

- **Autenticación completa**: registro e inicio de sesión de usuarios mediante tokens JWT y contraseñas cifradas.
- **Crear tareas**: añadir nuevas tareas asociadas al usuario autenticado con título y descripción.
- **Listar tareas**: visualización de todas las tareas del usuario con su estado actual.
- **Actualizar estado**: marcar/desmarcar tareas como completadas, con actualización automática y efecto visual tachado.
- **Eliminar tareas**: borrado persistente tanto en la interfaz como en la base de datos de producción.
- **CORS configurado**: políticas de intercambio de recursos seguras entre el cliente en Vercel y el servidor en Railway.

## Arquitectura del backend

El backend sigue los principios de Clean Architecture, separado en 4 capas:

| Capa | Responsabilidad |
|---|---|
| `TaskFlow.Domain` | Entidades principales del negocio (`TaskItem`, `User`) |
| `TaskFlow.Application` | Lógica de servicios, interfaces y DTOs |
| `TaskFlow.Infrastructure` | Contexto de base de datos (`ApplicationDbContext`), repositorios y migraciones de EF Core para PostgreSQL |
| `TaskFlow.API` | Controladores web y configuración de middleware (JWT, CORS, endpoints) |

## Requisitos previos

Antes de ejecutar el proyecto en tu máquina local, asegúrate de tener instalado:

- [.NET SDK](https://dotnet.microsoft.com/) (versión 9 o superior)
- [Node.js y npm](https://nodejs.org/)
- PostgreSQL (opcional, si deseas probar de forma local con la misma base de datos)

## Guía de instalación y ejecución local

### 1. Clonar el repositorio

```bash
git clone https://github.com/mariogc55/taskflow-fullstack.git
cd taskflow-fullstack
```

### 2. Configurar el entorno

**Backend:**
En `backend/TaskFlow.API/appsettings.json`, configura:
- `ConnectionStrings:DefaultConnection`: tu cadena de conexión a PostgreSQL.
- `Jwt:Key`: una clave secreta de al menos 32 caracteres para firmar los tokens.

**Frontend:**
En `frontend/src/environments/environment.ts` (o `environment.development.ts`), define la URL de la API:

```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5287/api'
};
```

### 3. Ejecutar el backend (.NET)

```bash
cd backend/TaskFlow.API
dotnet run
```

### 4. Ejecutar el frontend (Angular)

En otra terminal:

```bash
cd frontend
npm install
ng serve
```

Abre tu navegador en `http://localhost:4200`.

## Roadmap

- [x] Autenticación y autorización de usuarios con JWT
- [x] Despliegue en la nube (Vercel & Railway con PostgreSQL)
- [ ] Prioridades y filtros avanzados de tareas
- [ ] Tests unitarios (backend) y tests e2e (frontend)

## Contribuciones

Este es un proyecto personal de aprendizaje, pero si tienes sugerencias eres bienvenido a abrir un issue o un pull request.

## Autor

**Mario Guerrero Castillo**
[GitHub](https://github.com/mariogc55) · [LinkedIn](https://linkedin.com/in/mario-guerrero-castillo-b19214283)