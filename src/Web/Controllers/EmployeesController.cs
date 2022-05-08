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

    [HttpGet("{id:Guid}")]
    public ActionResult<EmployeeDto> GetEmployeeById(Guid id)
    {
        EmployeeDto? employeeDto = _facade.FindEmployeeById(id);

        if (employeeDto == null)
            return NotFound();

        return Ok(employeeDto);
    }

    [HttpPost]
    public ActionResult<long> AddEmployee(EmployeeDto employeeDto) => Ok(_facade.AddEmployee(employeeDto));

    [HttpPut("{id:Guid}")]
    public IActionResult UpdateEmployee(Guid id, EmployeeDto employeeDto)
    {
        if (id != employeeDto.Id)
            return BadRequest();

        _facade.UpdateEmployee(employeeDto);

        return NoContent();
    }
}
