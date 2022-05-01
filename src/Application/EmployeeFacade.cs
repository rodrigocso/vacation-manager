using Domain;

namespace Application;

public class EmployeeFacade
{
    private readonly IRepository<Employee> _repository;

    public EmployeeFacade(IRepository<Employee> repository)
    {
        _repository = repository;
    }

    public IEnumerable<EmployeeDto> ListEmployees() => _repository.GetAll()
        .Select(employee => employee.ToDto());
}
