# Task Manager API

RESTful API для управления задачами (туду-листами) с использованием ASP.NET Core, Entity Framework Core и MySQL.

## Технологии

- C#
- ASP.NET Core 9
- Entity Framework Core
- MySQL (для продакшн) / In-Memory Database (для разработки)
- Repository Pattern
- Dependency Injection

## Функциональность API

- Создание задачи (название, дата выполнения, приоритет)
- Получение списка задач (с возможностью фильтрации по дате и приоритету)
- Обновление задачи (например, изменение статуса на "выполнено")
- Удаление задачи

## Настройка проекта

### Предварительные требования

- .NET 9 SDK
- MySQL сервер (для продакшн)

### Режимы работы

#### Разработка
В режиме разработки приложение использует базу данных в памяти (In-Memory Database), что упрощает локальное тестирование и отладку.

#### Продакшн
Для продакшн-среды необходимо настроить подключение к MySQL базе данных.

1. Создайте базу данных в MySQL:

```sql
CREATE DATABASE TaskManager;
```

2. Обновите строку подключения в файле `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TaskManager;User=your_username;Password=your_password;"
}
```

### Миграции Entity Framework

Выполните следующие команды из директории проекта для создания и применения миграций:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Запуск приложения

```bash
dotnet run
```

По умолчанию API будет доступен по адресу `https://localhost:5001` и `http://localhost:5000`.

## API endpoints

- `GET /api/tasks` - Получить список всех задач
- `GET /api/tasks/{id}` - Получить задачу по ID
- `POST /api/tasks` - Создать новую задачу
- `PUT /api/tasks/{id}` - Обновить существующую задачу
- `DELETE /api/tasks/{id}` - Удалить задачу
- `GET /api/tasks/priority/{priority}` - Получить задачи по приоритету
- `GET /api/tasks/completed/{isCompleted}` - Получить задачи по статусу завершения

## Примеры запросов

### Создание задачи

```json
POST /api/tasks
Content-Type: application/json

{
  "title": "Изучить ASP.NET Core",
  "dueDate": "2023-12-31T23:59:59Z",
  "priority": 2
}
```

### Обновление задачи

```json
PUT /api/tasks/1
Content-Type: application/json

{
  "title": "Изучить ASP.NET Core и Entity Framework",
  "isCompleted": true
}
```

## Тестирование API

Вы можете использовать Postman или curl для тестирования API. 