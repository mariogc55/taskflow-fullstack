# TaskFlow — Gestor de Tareas Full Stack

TaskFlow es una aplicación web de gestión de tareas construida con una arquitectura desacoplada: **Angular** en el frontend y **ASP.NET Core Web API** en el backend, aplicando principios de **Clean Architecture** y persistencia mediante **Entity Framework Core** sobre SQL Server LocalDB.

Es un proyecto personal de aprendizaje, en desarrollo activo, orientado a practicar el stack .NET + Angular en un caso de uso real de gestión de tareas (similar en concepto a Trello/Jira, pero simplificado).

## Estado del proyecto

 En desarrollo activo. Las funcionalidades básicas de CRUD de tareas están implementadas; se seguirá escalando en funcionalidad (autenticación de usuarios, prioridades, filtros, etc.). Ver la sección [Roadmap](#roadmap) para el detalle de lo que falta.

## Tecnologías utilizadas

**Frontend**
- Angular (framework SPA)
- TypeScript
- Bootstrap 5 (UI/UX responsivo)

**Backend**
- .NET 9 / C# (ASP.NET Core Web API)
- Entity Framework Core (ORM)
- SQL Server / LocalDB (base de datos relacional)

## Características actuales

- **Crear tareas**: añadir nuevas tareas con título y descripción.
- **Listar tareas**: visualización de todas las tareas registradas con su estado actual.
- **Actualizar estado**: marcar/desmarcar tareas como completadas mediante checkbox, con actualización automática (Pending / Completed) y efecto visual tachado.
- **Eliminar tareas**: borrado persistente tanto en la interfaz como en la base de datos.
- **CORS configurado**: políticas de intercambio de recursos entre Angular y el backend.

## Arquitectura del backend

El backend sigue los principios de Clean Architecture, separado en 4 capas:

| Capa | Responsabilidad |
|---|---|
| `TaskFlow.Domain` | Entidades principales del negocio (`TaskItem`, `User`) |
| `TaskFlow.Application` | Lógica de servicios, interfaces y DTOs |
| `TaskFlow.Infrastructure` | Contexto de base de datos (`ApplicationDbContext`), repositorios y migraciones de EF Core |
| `TaskFlow.API` | Controladores web y configuración de middleware (CORS, endpoints) |

## Requisitos previos

Antes de ejecutar el proyecto en tu máquina local, asegúrate de tener instalado:

- [.NET SDK](https://dotnet.microsoft.com/) (versión 9 o superior)
- [Node.js y npm](https://nodejs.org/)
- SQL Server LocalDB (incluido por defecto con Visual Studio o las herramientas de .NET)

## Guía de instalación y ejecución

### 1. Clonar el repositorio

```bash
git clone https://github.com/mariogc55/taskflow-fullstack.git
cd taskflow-fullstack
```

### 2. Configurar y ejecutar el backend (.NET)

```bash
cd backend/TaskFlow.API
dotnet run
```

El backend correrá por defecto en `http://localhost:5287`.

### 3. Configurar y ejecutar el frontend (Angular)

En otra terminal:

```bash
cd frontend
npm install
ng serve
```

Abre tu navegador en `http://localhost:4200`.

## Roadmap

- [ ] Autenticación y autorización de usuarios
- [ ] Prioridades y filtros de tareas
- [ ] Tests unitarios (backend) y tests e2e (frontend)
- [ ] Despliegue en la nube (demo pública)

## Contribuciones

Este es un proyecto personal de aprendizaje, pero si tienes sugerencias eres bienvenido a abrir un issue o un pull request.

## Autor

**Mario Guerrero Castillo**
[GitHub](https://github.com/mariogc55) · [LinkedIn](https://linkedin.com/in/mario-guerrero-castillo-b19214283)