
# Task Backend API

API REST para gestionar tareas desarrollada con:

- C#
- ASP.NET Core 9
- Entity Framework Core 9
- SQLite
- xUnit

## Requisitos

- SDK de .NET 9
- Visual Studio 2022
- Entity Framework Core Tools 9

## Instalación

Clonar o descargar el proyecto.

Restaurar dependencias:

```bash
dotnet restore
```

Crear la base de datos:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Ejecutar:

```bash
dotnet run
```

## Endpoints

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | /api/tasks | Listar tareas |
| GET | /api/tasks/{id} | Consultar tarea |
| POST | /api/tasks | Crear tarea |
| PUT | /api/tasks/{id} | Actualizar tarea |
| DELETE | /api/tasks/{id} | Eliminar tarea |

## Filtros

Se puede filtrar por estado:

`/api/tasks?status=Pendiente`

Por responsable:

`/api/tasks?responsible=Ana`

También se pueden combinar:

`/api/tasks?status=EnCurso&responsible=Ana&page=1&pageSize=10`

## Estados disponibles

- Pendiente
- EnCurso
- Hecha

## Códigos HTTP

- 200: operación correcta.
- 201: tarea creada.
- 204: tarea eliminada.
- 400: datos inválidos.
- 404: tarea no encontrada.
- 500: error interno.

## Base de datos

Se utiliza SQLite con Entity Framework Core.

La base de datos se crea mediante migraciones.

## Pruebas

Para ejecutar los tests:

```bash
dotnet test
```

Se incluyen pruebas de creación, consulta,
eliminación y filtrado de tareas.

## Decisiones técnicas

- SQLite permite ejecutar el proyecto sin instalar
  un servidor de base de datos.
- Entity Framework Core facilita las consultas
  y las migraciones.
- Los DTO separan los datos de entrada y salida
  de las entidades de base de datos.
- Se utiliza paginación para evitar devolver
  demasiados registros en una sola petición.
- Se utilizan enumeraciones para controlar
  los estados permitidos.

## Mejoras futuras

- Incorporar autenticación JWT.
- Añadir roles y permisos.
- Documentar la API con Swagger/OpenAPI.
- Incorporar Docker.
- Añadir pruebas de integración.
- Implementar logging y monitorización.
- Incorporar ordenamiento configurable.
