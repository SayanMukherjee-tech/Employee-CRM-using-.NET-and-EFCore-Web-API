namespace EmployeeCRM.Core.Entities;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    
    public ICollection<Client> Clients { get; set; } = new List<Client>();
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
