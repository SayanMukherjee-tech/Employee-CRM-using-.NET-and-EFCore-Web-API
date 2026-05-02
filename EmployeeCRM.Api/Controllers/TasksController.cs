namespace EmployeeCRM.Api.Controllers;

using EmployeeCRM.Core.Entities;
using EmployeeCRM.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TasksController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        return Ok(await _context.Tasks.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var taskItem = await _context.Tasks.FindAsync(id);
        if (taskItem == null) return NotFound();
        return Ok(taskItem);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] TaskItem taskItem)
    {
        _context.Tasks.Add(taskItem);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTask), new { id = taskItem.Id }, taskItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItem taskItem)
    {
        if (id != taskItem.Id) return BadRequest();
        _context.Entry(taskItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var taskItem = await _context.Tasks.FindAsync(id);
        if (taskItem == null) return NotFound();
        _context.Tasks.Remove(taskItem);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
