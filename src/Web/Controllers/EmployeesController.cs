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
    public async Task<IEnumerable<EmployeeDto>> GetEmployees() => await _facade.ListEmployees();

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<EmployeeDto>> GetEmployeeById(Guid id)
    {
        EmployeeDto? employeeDto = await _facade.FindEmployeeById(id);

        if (employeeDto == null)
            return NotFound();

        return Ok(employeeDto);
    }

    [HttpPost]
    public async Task<ActionResult<long>> AddEmployee(EmployeeDto employeeDto) =>
        Ok(await _facade.AddEmployee(employeeDto));

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateEmployee(Guid id, EmployeeDto employeeDto)
    {
        if (id != employeeDto.Id)
            return BadRequest();

        await _facade.UpdateEmployee(employeeDto);

        return NoContent();
    }
}
