namespace EmployeeCRM.Web.Controllers;

using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeCRM.Core.Entities;

[Authorize]
public class ClientsController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ClientsController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var token = User.Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;
        var client = _httpClientFactory.CreateClient("ApiClient");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync("api/Clients");
        if (response.IsSuccessStatusCode)
        {
            var clients = await response.Content.ReadFromJsonAsync<List<Client>>();
            return View(clients);
        }

        return View(new List<Client>());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new Client());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Client clientData)
    {
        if (!ModelState.IsValid) return View(clientData);

        var token = User.Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;
        var client = _httpClientFactory.CreateClient("ApiClient");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.PostAsJsonAsync("api/Clients", clientData);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError("", "Error creating client.");
        return View(clientData);
    }
}
