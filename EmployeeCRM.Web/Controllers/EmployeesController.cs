namespace EmployeeCRM.Web.Controllers;

using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeCRM.Core.Entities;

[Authorize]
public class EmployeesController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EmployeesController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var token = User.Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;
        var client = _httpClientFactory.CreateClient("ApiClient");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync("api/Employees");
        if (response.IsSuccessStatusCode)
        {
            var employees = await response.Content.ReadFromJsonAsync<List<Employee>>();
            return View(employees);
        }

        return View(new List<Employee>());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new Employee());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Employee employee)
    {
        if (!ModelState.IsValid) return View(employee);

        var token = User.Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;
        var client = _httpClientFactory.CreateClient("ApiClient");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.PostAsJsonAsync("api/Employees", employee);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError("", "Error creating employee.");
        return View(employee);
    }
}
