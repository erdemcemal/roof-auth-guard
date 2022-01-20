using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roof.API.Models;

namespace Roof.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    [Authorize(Policy = "Read")]
    [HttpGet]
    public IActionResult GetEmployees()
    {
        var employees = DummyData.GetEmployeeList();
        return Ok(employees);
    }

    [Authorize(Policy = "Crate")]
    [HttpPost]
    public IActionResult CreateEmployee(EmployeeVm employee)
    {
        DummyData.AddEmployee(employee);
        return Ok(employee);
    }
}