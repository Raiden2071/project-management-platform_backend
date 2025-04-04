# Task Manager API

RESTful API for task management (todo lists) using ASP.NET Core, Entity Framework Core, and MySQL.

## Technologies

- C#
- ASP.NET Core 9
- Entity Framework Core
- MySQL (for production) / In-Memory Database (for development)
- Repository Pattern
- Dependency Injection

## API Functionality

- Create tasks (title, due date, priority)
- Retrieve list of tasks (with filtering by date and priority)
- Update tasks (e.g., change status to "completed")
- Delete tasks

## Project Setup

### Prerequisites

- .NET 9 SDK
- MySQL server (for production)

### Operation Modes

#### Development
In development mode, the application uses an In-Memory Database, which simplifies local testing and debugging.

#### Production
For production environments, you need to set up a connection to a MySQL database.

1. Create a database in MySQL:

```sql
CREATE DATABASE TaskManager;
```

2. Update the connection string in the `appsettings.json` file:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TaskManager;User=your_username;Password=your_password;"
}
```

## Running the Application

```bash
dotnet run
```

By default, the API will be available at `https://localhost:5001` and `http://localhost:5000`.

## API Endpoints

- `GET /api/tasks` - Get all tasks
- `GET /api/tasks/{id}` - Get task by ID
- `POST /api/tasks` - Create a new task
- `PUT /api/tasks/{id}` - Update an existing task
- `DELETE /api/tasks/{id}` - Delete a task
- `GET /api/tasks/priority/{priority}` - Get tasks by priority
- `GET /api/tasks/completed/{isCompleted}` - Get tasks by completion status

## Request Examples

### Creating a Task

```json
POST /api/tasks
Content-Type: application/json

{
  "title": "Learn ASP.NET Core",
  "dueDate": "2023-12-31T23:59:59Z",
  "priority": 2
}
```

### Updating a Task

```json
PUT /api/tasks/1
Content-Type: application/json

{
  "title": "Learn ASP.NET Core and Entity Framework",
  "isCompleted": true
}
```

## Testing the API

You can use Postman or curl to test the API. 