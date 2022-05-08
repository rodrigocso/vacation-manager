using Domain;

namespace Application;

public interface IEmployeeRepository
{
    IEnumerable<Employee> FindEmployees();
    Employee? FindEmployeeById(long id);
    void AddEmployee(Employee employee);
}
