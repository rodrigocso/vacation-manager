using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    [HttpGet]
    public IEnumerable<EmployeeDto> GetEmployees()
    {
        return Array.Empty<EmployeeDto>();
    }
}
