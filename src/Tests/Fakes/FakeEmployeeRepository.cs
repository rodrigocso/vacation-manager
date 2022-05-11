using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Domain;

namespace Tests.Fakes;

public class FakeEmployeeRepository : IEmployeeRepository
{
    public static readonly Dictionary<Guid, Employee> Employees = new();

    public Task<IEnumerable<Employee>> FindEmployees() => Task.FromResult(Employees.Values.AsEnumerable());

    public Task<Employee?> FindEmployeeById(Guid id) => Task.FromResult(Employees.GetValueOrDefault(id));

    public Task AddEmployee(Employee employee)
    {
        Employees.Add(employee.Id, employee);
        return Task.CompletedTask;
    }

    public Task UpdateEmployee(Employee employee)
    {
        Employees[employee.Id] = employee;
        return Task.CompletedTask;
    }

    public static void AddMany(IEnumerable<Employee> employees) =>
        employees
            .ToList()
            .ForEach(employee => Employees.Add(employee.Id, employee));

    public static void Clear() => Employees.Clear();
}
