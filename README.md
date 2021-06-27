# letscode-kanban
A simple Kanban web app.

## TL;DR

### 1. Launch backend:
```bash
cd backend
dotnet run --launch-profile "LetsCode.Kanban.WebApi" --project "./src/LetsCode.Kanban.WebApi"
```

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
You may also visit API Explorer on http://localhost:5000/swagger

## Backend (.NET 5)

### Configuration
Configuration can be done either by using an `appsettings.json` on `src/LetsCode.Kanban.WebApi`, or setting environment variables:
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
```

## Frontend (Angular 12)