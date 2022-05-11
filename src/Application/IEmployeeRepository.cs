using Domain;

namespace Application;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> FindEmployees();
    Task<Employee?> FindEmployeeById(Guid id);
    Task AddEmployee(Employee employee);
    Task UpdateEmployee(Employee employee);
}
