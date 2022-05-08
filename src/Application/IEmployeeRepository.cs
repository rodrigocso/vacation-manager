using Domain;

namespace Application;

public interface IEmployeeRepository
{
    IEnumerable<Employee> FindEmployees();
    Employee? FindEmployeeById(Guid id);
    void AddEmployee(Employee employee);
    void UpdateEmployee(Employee employee);
}
