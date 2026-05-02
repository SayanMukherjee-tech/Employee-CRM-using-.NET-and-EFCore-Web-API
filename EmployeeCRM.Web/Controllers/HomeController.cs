using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using EmployeeCRM.Web.Models;
using Microsoft.AspNetCore.Authorization;
using EmployeeCRM.Core.Entities;

namespace EmployeeCRM.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var token = User.Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;
        var client = _httpClientFactory.CreateClient("ApiClient");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var employeesResponse = await client.GetAsync("api/Employees");
        var tasksResponse = await client.GetAsync("api/Tasks");
        var clientsResponse = await client.GetAsync("api/Clients");

        var employees = employeesResponse.IsSuccessStatusCode 
            ? await employeesResponse.Content.ReadFromJsonAsync<List<Employee>>() ?? new List<Employee>()
            : new List<Employee>();

        var tasks = tasksResponse.IsSuccessStatusCode
            ? await tasksResponse.Content.ReadFromJsonAsync<List<TaskItem>>() ?? new List<TaskItem>()
            : new List<TaskItem>();

        var clients = clientsResponse.IsSuccessStatusCode
            ? await clientsResponse.Content.ReadFromJsonAsync<List<Client>>() ?? new List<Client>()
            : new List<Client>();

        ViewBag.TotalEmployees = employees.Count;
        ViewBag.ActiveEmployees = employees.Count(e => e.IsActive);
        ViewBag.TotalTasks = tasks.Count;
        ViewBag.CompletedTasks = tasks.Count(t => t.Status == "Completed");
        ViewBag.TotalClients = clients.Count;

        return View();
    }

}
