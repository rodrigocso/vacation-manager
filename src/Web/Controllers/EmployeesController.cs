using Application;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeFacade _facade;

    public EmployeesController(EmployeeFacade facade)
    {
        _facade = facade;
    }

    [HttpGet]
    public IEnumerable<EmployeeDto> GetEmployees() => _facade.ListEmployees();

    [HttpPost]
    public IActionResult AddEmployee(EmployeeDto employeeDto)
    {
        _facade.AddEmployee(employeeDto);
        return Ok();
    }
}
