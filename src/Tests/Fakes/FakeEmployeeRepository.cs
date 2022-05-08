using System.Collections.Generic;
using Application;
using Domain;

namespace Tests.Fakes;

public class FakeEmployeeRepository : IEmployeeRepository
{
    public static readonly List<Employee> Employees = new();

    public IEnumerable<Employee> FindEmployees() => Employees;

    public Employee? FindEmployeeById(long id) => Employees.Find(employee => employee.Id == id);

    public void AddEmployee(Employee employee) => Employees.Add(employee);

    public static void AddMany(IEnumerable<Employee> employees) => Employees.AddRange(employees);

    public static void Clear() => Employees.Clear();
}
