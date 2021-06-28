# letscode-kanban
A simple Kanban web app.

## TL;DR

### 1. Launch backend:
```bash
cd backend
dotnet run --launch-profile "LetsCode.Kanban.WebApi" --project "./src/LetsCode.Kanban.WebApi"
```
You may visit Swagger API Explorer on http://localhost:5000/swagger

### 2. Launch frontend:
```bash
cd frontend
npm install
npm start
```

### 3. http://localhost:4200
```
Username: letscode
Password: lets@123
```


## Docker-compose
You may also run the application with docker-compose on repository root folder:
```bash
docker-compose up -d
```
or with rebuild
```bash
docker-compose up -d --build
```

## Backend (.NET 5)

### Configuration
Configuration can be done either by using an `appsettings.json` on `src/LetsCode.Kanban.WebApi`, or setting environment variables. See the examples bellow:
```js
{
  "Authentication": {
    "Username": "letscode",
    "Password": "lets@123"
  },
  "Jwt": {
    "Issuer": "letscode",
    "ExpiresAfter": "05:00:00",
    "Secret": "xxxxxxxxxxyyyyyyyyyyyyzzzzzzzzzz"
    "Audience": "http://letscode-kanban.xyz"
  },
  "Postgre": {
    "ConnectionString": "Host=localhost;Database=letscode_kanban;Username=victor;Password=123456"
  }
}
```
```
Authentication__Username = "letscode"
Authentication__Password = "lets@123"
Jwt__Issuer = "letscode"
Jwt__ExpiresAfter = "05:00:00"
Jwt__Secret = "xxxxxxxxxxyyyyyyyyyyyyzzzzzzzzzz"
Jwt__Audience = "http://letscode-kanban.xyz"
Postgre__ConnectionString = "Host=localhost;Database=letscode_kanban;Username=victor;Password=123456"
```
By default the application will run with an InMemory database.
If you set the Postgre ConnectionString, it will run with PostgreSQL.
### Migrations
To work with migrations, you'll have to install dotnet ef tools:
```
dotnet tool install --global dotnet-ef
```
The PostgreSQL migrations are in the folder
`src/LetsCode.Kanban.Persistence.EntityFrameworkCore/PostgreSql/Migrations`

To run the database management commands as described here (migrations and update) you have to set the `Postgre__ConnectionString` variable and run them from directory `src/LetsCode.Kanban.Persistence.EntityFrameworkCore`. See the example bellow: 
```bash
export Postgre__ConnectionString="your_connection_string_here"
cd src/LetsCode.Kanban.Persistence.EntityFrameworkCore
```

To add a migration, run:
```bash
dotnet ef migrations add NewMigrationName -c PostgreSqlApplicationDbContext -o PostgreSql/Migrations -s ../LetsCode.Kanban.WebApi
```
To remove a migration, run:
```bash
dotnet ef migrations remove NewMigrationName -c PostgreSqlApplicationDbContext -s ../LetsCode.Kanban.WebApi
```
To update database, run:
```bash
dotnet ef database update NewMigrationName -c PostgreSqlApplicationDbContext -s ../LetsCode.Kanban.WebApi
```

## Frontend (Angular 12)
There are no specific configurations yet.