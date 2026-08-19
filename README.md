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

1. Clonar el repositorio
```bash
git clone [https://github.com/tu-usuario/taskflow-fullstack.git](https://github.com/tu-usuario/taskflow-fullstack.git)
cd taskflow-fullstack


2. Configurar y ejecutar el Backend (.NET)
Navega a la carpeta de la API:

Bash
cd backend/TaskFlow.API
Ejecuta la aplicación (las migraciones y el usuario por defecto se crearán automáticamente gracias al seeder configurado):

Bash
dotnet run
El backend correrá por defecto en http://localhost:5287.

3. Configurar y ejecutar el Frontend (Angular)
Abre otra terminal y navega a la carpeta del frontend (según la estructura de tu proyecto):

Bash
cd frontend
Instala las dependencias:

Bash
npm install
Inicia la aplicación de Angular:

Bash
ng serve
Abre tu navegador y accede a: http://localhost:4200

 Arquitectura del Backend
El proyecto backend sigue los principios de Clean Architecture:

TaskFlow.Domain: Entidades principales del negocio (TaskItem, User).

TaskFlow.Application: Lógica de servicios, interfaces y DTOs.

TaskFlow.Infrastructure: Contexto de base de datos (ApplicationDbContext), repositorios y migraciones de EF Core.

TaskFlow.API: Controladores web y configuración de middleware (CORS, Endpoints).

 Contribuciones
¡Las contribuciones son bienvenidas! Si deseas mejorar este proyecto, siéntete libre de hacer un fork, crear una rama con tus cambios y enviar un Pull Request.

---

### ¿Cómo usarlo?
1. Ve a la raíz de tu proyecto en tu computadora.
2. Crea o abre el archivo llamado **`README.md`**.
3. Pega este contenido, ajusta tu nombre de usuario de GitHub en la URL de clonación y tu nombre al final del archivo.
4. Guárdalo y súbelo a tu repositorio con `git add`, `git commit` y `git push`. 

¡Tu repositorio en GitHub se verá sumamente profesional y claro para cualquier reclutador o colega que lo visite!