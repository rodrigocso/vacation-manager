using System;
using System.Collections.Generic;
using System.Linq;
using Application;
using Domain;

namespace Tests.Fakes;

public class FakeEmployeeRepository : IEmployeeRepository
{
    public static readonly Dictionary<Guid, Employee> Employees = new();

    public IEnumerable<Employee> FindEmployees() => Employees.Values;

    public Employee? FindEmployeeById(Guid id) => Employees.GetValueOrDefault(id);

    public void AddEmployee(Employee employee) => Employees.Add(employee.Id, employee);

    public void UpdateEmployee(Employee employee) => Employees[employee.Id] = employee;

    public static void AddMany(IEnumerable<Employee> employees) =>
        employees
            .ToList()
            .ForEach(employee => Employees.Add(employee.Id, employee));

    public static void Clear() => Employees.Clear();
}
