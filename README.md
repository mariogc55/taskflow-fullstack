# TaskFlow - Gestor de Tareas Full Stack

**TaskFlow** es una aplicación web moderna de gestión de tareas desarrollada con una arquitectura robusta y desacoplada. Utiliza **Angular** en el frontend y **ASP.NET Core Web API** en el backend, aplicando principios de Clean Architecture y conectándose a una base de datos **LocalDB (SQL Server)** mediante Entity Framework Core.

---

## Tecnologías Utilizadas

### Frontend
* **Angular** (Framework SPA)
* **TypeScript**
* **Bootstrap 5** (Diseño UI/UX moderno y responsivo)

### Backend
* **.NET 9 / C#** (ASP.NET Core Web API)
* **Entity Framework Core** (ORM)
* **SQL Server / LocalDB** (Base de datos relacional)

---

## Características Principales

* **Crear Tareas:** Permite añadir nuevas tareas con título y descripción detallada.
* **Listar Tareas:** Visualización en tiempo real de todas las tareas registradas con su estado actual.
* **Actualizar Estado:** Permite marcar/desmarcar tareas como completadas mediante un *checkbox*, actualizando automáticamente el estado (*Pending* / *Completed*) y aplicando un efecto visual tachado.
* **Eliminar Tareas:** Borrado persistente de registros tanto en la interfaz como en la base de datos.
* **Seguridad y CORS:** Configuración de políticas de intercambio de recursos entre Angular y el Backend.

---

## Requisitos Previos

Antes de ejecutar el proyecto en tu máquina local, asegúrate de tener instalado:
* [.NET SDK](https://dotnet.microsoft.com/) (versión recomendada actual)
* [Node.js y npm](https://nodejs.org/)
* SQL Server LocalDB (incluido por defecto con Visual Studio o las herramientas de .NET)

---

## Guía de Instalación y Ejecución

Sigue estos pasos para clonar y poner en marcha el proyecto localmente:

### 1. Clonar el repositorio
```bash
git clone [https://github.com/tu-usuario/taskflow-fullstack.git](https://github.com/tu-usuario/taskflow-fullstack.git)
cd taskflow-fullstack