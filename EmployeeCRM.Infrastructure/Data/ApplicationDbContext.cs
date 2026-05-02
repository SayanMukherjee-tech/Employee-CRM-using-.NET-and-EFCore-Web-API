namespace EmployeeCRM.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using EmployeeCRM.Core.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Clients)
            .WithOne(c => c.AssignedEmployee)
            .HasForeignKey(c => c.AssignedEmployeeId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Tasks)
            .WithOne(t => t.AssignedEmployee)
            .HasForeignKey(t => t.AssignedEmployeeId)
            .OnDelete(DeleteBehavior.SetNull);
            
        modelBuilder.Entity<User>()
            .HasOne(u => u.Employee)
            .WithMany()
            .HasForeignKey(u => u.EmployeeId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
