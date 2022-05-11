namespace Application;

public class EmployeeFacade
{
    private readonly IEmployeeRepository _repository;

    public EmployeeFacade(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EmployeeDto>> ListEmployees() => (await _repository.FindEmployees())
        .Select(employee => employee.ToDto());

    public async Task<EmployeeDto?> FindEmployeeById(Guid id) => (await _repository.FindEmployeeById(id))?.ToDto();

    public async Task<Guid> AddEmployee(EmployeeDto employeeDto)
    {
        var id = Guid.NewGuid();
        await _repository.AddEmployee(employeeDto.ToEntity(id));

        return id;
    }

    public async Task UpdateEmployee(EmployeeDto employeeDto) => await _repository
        .UpdateEmployee(employeeDto.ToEntity());
}
