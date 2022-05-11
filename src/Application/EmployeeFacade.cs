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

    public EmployeeDto? FindEmployeeById(Guid id) => _repository.FindEmployeeById(id)?.ToDto();

    public Guid AddEmployee(EmployeeDto employeeDto)
    {
        var id = Guid.NewGuid();
        _repository.AddEmployee(employeeDto.ToEntity(id));

        return id;
    }

    public void UpdateEmployee(EmployeeDto employeeDto) => _repository.UpdateEmployee(employeeDto.ToEntity());
}
