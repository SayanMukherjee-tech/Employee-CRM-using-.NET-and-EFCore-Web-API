namespace EmployeeCRM.Core.Entities;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending"; // Pending, In Progress, Completed
    public DateTime DueDate { get; set; }
    
    public int? AssignedEmployeeId { get; set; }
    public Employee? AssignedEmployee { get; set; }
}
