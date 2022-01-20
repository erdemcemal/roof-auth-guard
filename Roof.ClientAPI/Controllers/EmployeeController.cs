using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Roof.ClientAPI.Models;
using ForbiddenException = Roof.ClientAPI.Exceptions.ForbiddenException;
using UnauthorizedException = Roof.ClientAPI.Exceptions.UnauthorizedException;

namespace Roof.ClientAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _serializerOptions;

    public EmployeeController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var httpClient = _httpClientFactory.CreateClient("EmployeeApi");
        var response = await httpClient.GetAsync("api/Employees");
        if (!response.IsSuccessStatusCode) throw new Exception("Unhandled Error");

        var content = await response.Content.ReadAsStringAsync();
        var employees = JsonSerializer.Deserialize<List<EmployeeVm>>(content, _serializerOptions);
        return Ok(employees);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeVm vm)
    {
        var httpClient = _httpClientFactory.CreateClient("EmployeeApi");
        var requestBody = new StringContent(JsonSerializer.Serialize(vm), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("api/Employees", requestBody);
        if (!response.IsSuccessStatusCode)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedException("Unauthorized request");
                case HttpStatusCode.Forbidden:
                    throw new ForbiddenException("Forbidden request");
            }
        }
        var content = await response.Content.ReadAsStringAsync();
        var employee = JsonSerializer.Deserialize<EmployeeVm>(content, _serializerOptions);
        return Ok(employee);
    }
}