namespace EmployeeCRM.Core.Entities;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    
    public int? AssignedEmployeeId { get; set; }
    public Employee? AssignedEmployee { get; set; }
}
