using Application;
using Domain;

namespace Infrastructure;

public class EmployeeRepository : IEmployeeRepository
{
    public IEnumerable<Employee> FindEmployees() => throw new NotImplementedException();

    public Employee? FindEmployeeById(Guid id) => throw new NotImplementedException();

    public void AddEmployee(Employee employee) => throw new NotImplementedException();

    public void UpdateEmployee(Employee employee) => throw new NotImplementedException();
}
