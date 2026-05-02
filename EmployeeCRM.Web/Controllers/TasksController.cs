namespace EmployeeCRM.Web.Controllers;

using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeCRM.Core.Entities;

[Authorize]
public class TasksController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public TasksController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var token = User.Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;
        var client = _httpClientFactory.CreateClient("ApiClient");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync("api/Tasks");
        if (response.IsSuccessStatusCode)
        {
            var tasks = await response.Content.ReadFromJsonAsync<List<TaskItem>>();
            return View(tasks);
        }

        return View(new List<TaskItem>());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new TaskItem { DueDate = DateTime.Today });
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskItem taskItem)
    {
        if (!ModelState.IsValid) return View(taskItem);

        var token = User.Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;
        var client = _httpClientFactory.CreateClient("ApiClient");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.PostAsJsonAsync("api/Tasks", taskItem);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError("", "Error creating task.");
        return View(taskItem);
    }
}
