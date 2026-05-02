namespace EmployeeCRM.Api.Data;

using EmployeeCRM.Core.Entities;
using EmployeeCRM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.Migrate();

        if (context.Users.Any())
        {
            return;
        }

        var adminUser = new User
        {
            Username = "admin",
            PasswordHash = BCrypt.HashPassword("admin123"),
            Role = "Admin"
        };

        context.Users.Add(adminUser);
        context.SaveChanges();
    }
}
