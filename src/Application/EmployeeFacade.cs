using Domain;

namespace Application;

public class EmployeeFacade
{
    private readonly IEmployeeRepository _repository;

    public EmployeeFacade(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<EmployeeDto> ListEmployees() => _repository.FindEmployees()
        .Select(employee => employee.ToDto());

    public EmployeeDto? FindEmployeeById(long id) => _repository.FindEmployeeById(id).ToDto();

    public void AddEmployee(EmployeeDto employeeDto) => _repository.AddEmployee(employeeDto.ToEntity());
}
