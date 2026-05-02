# Employee CRM Application

A comprehensive Employee CRM system built with .NET, Entity Framework Core, and ASP.NET MVC.

## Project Structure

- **EmployeeCRM.Api** - RESTful API backend with JWT authentication
- **EmployeeCRM.Web** - ASP.NET MVC web interface
- **EmployeeCRM.Core** - Core entities and DTOs
- **EmployeeCRM.Infrastructure** - Database context and migrations

## Features

- User authentication and JWT token-based authorization
- Employee management
- Client management
- Task tracking
- Role-based access control
- Real-time data synchronization between web and API

## Local Setup

### Prerequisites
- .NET 8 SDK or later
- Visual Studio 2022 or VS Code
- Git

### Installation

1. Clone the repository:
```bash
git clone https://github.com/SayanMukherjee-tech/Employee-CRM-using-.NET-and-EFCore-Web-API.git
cd EmployeeCRM
```

2. Build the project:
```bash
dotnet build
```

3. Start the API (in one terminal):
```bash
cd EmployeeCRM.Api
dotnet run --launch-profile https
```

4. Start the Web application (in another terminal):
```bash
cd EmployeeCRM.Web
dotnet run --launch-profile http
```

## Access the Application

- **Web Interface**: http://localhost:5205
- **API**: https://localhost:7120
- **Swagger API Docs**: https://localhost:7120/swagger

### Default Credentials
- **Username**: admin
- **Password**: admin123

## Deployment Options

### Option 1: Deploy to Azure App Service (Recommended)

1. Create an Azure account at https://portal.azure.com
2. Create a new App Service for the API
3. Create another App Service for the Web application
4. Configure connection strings for the database
5. Use GitHub Actions for CI/CD pipeline

### Option 2: Deploy to Railway.app (Easiest)

1. Go to https://railway.app and sign up
2. Connect your GitHub repository
3. Railway will automatically detect .NET projects
4. Configure environment variables
5. Deploy with one click

### Option 3: Deploy to Heroku (Free tier alternative)

```bash
heroku login
heroku create your-app-name
git push heroku main
```

### Option 4: Self-hosted Deployment

1. Use a VPS provider (DigitalOcean, Linode, AWS EC2)
2. Install .NET runtime on the server
3. Publish the application:
```bash
dotnet publish -c Release
```
4. Configure reverse proxy with Nginx or IIS
5. Set up SSL certificate with Let's Encrypt

## Database

The application uses SQLite by default. To use a different database:

1. Install the appropriate NuGet package (e.g., `Microsoft.EntityFrameworkCore.SqlServer`)
2. Update the connection string in `appsettings.json`
3. Update the DbContext configuration in `Program.cs`
4. Create and apply migrations

## Environment Variables

Create an `appsettings.Development.json` with the following:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=employeecrm.db"
  },
  "Jwt": {
    "Key": "your-secret-key-at-least-32-characters-long",
    "Issuer": "EmployeeCRM",
    "Audience": "EmployeeCRM"
  }
}
```

## API Endpoints

### Authentication
- POST `/api/Auth/register` - Register new user
- POST `/api/Auth/login` - Login user

### Employees
- GET `/api/Employees` - Get all employees
- POST `/api/Employees` - Create employee
- PUT `/api/Employees/{id}` - Update employee
- DELETE `/api/Employees/{id}` - Delete employee

### Clients
- GET `/api/Clients` - Get all clients
- POST `/api/Clients` - Create client
- PUT `/api/Clients/{id}` - Update client
- DELETE `/api/Clients/{id}` - Delete client

### Tasks
- GET `/api/Tasks` - Get all tasks
- POST `/api/Tasks` - Create task
- PUT `/api/Tasks/{id}` - Update task
- DELETE `/api/Tasks/{id}` - Delete task

## Technologies Used

- .NET 8
- ASP.NET Core
- Entity Framework Core
- SQLite
- JWT Authentication
- Bootstrap 5
- jQuery

## License

This project is open source and available under the MIT License.

## Support

For issues and feature requests, please open an issue on the GitHub repository.
