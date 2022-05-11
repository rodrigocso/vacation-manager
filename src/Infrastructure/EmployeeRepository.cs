using Application;
using Dapper;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly string _connectionString;

    public EmployeeRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("VacationDb");
    }

    public IEnumerable<Employee> FindEmployees()
    {
        using var connection = new SqliteConnection(_connectionString);

        return connection.Query<Employee>("SELECT * FROM Employee;");
    }

    public Employee? FindEmployeeById(Guid id)
    {
        using var connection = new SqliteConnection(_connectionString);

        return connection.QueryFirstOrDefault<Employee>(
            "SELECT * FROM Employee WHERE Id = @id;",
            new { id });
    }

    public void AddEmployee(Employee employee)
    {
        using var connection = new SqliteConnection(_connectionString);

        connection.Execute(@"
            INSERT INTO Employee (Id, FirstName, LastName, StartDate)
            VALUES (@Id, @FirstName, @LastName, @StartDate);",
            new
            {
                employee.Id,
                employee.FirstName,
                employee.LastName,
                StartDate = employee.StartDate.ToString("yyyy-MM-dd")
            });
    }

    public void UpdateEmployee(Employee employee)
    {
        using var connection = new SqliteConnection(_connectionString);

        connection.Execute(@"
            UPDATE Employee
            SET
                FirstName = @FirstName,
                LastName = @LastName,
                StartDate = @StartDate
            WHERE
                Id = @Id;",
            new
            {
                employee.Id,
                employee.FirstName,
                employee.LastName,
                StartDate = employee.StartDate.ToString("yyyy-MM-dd")
            });
    }
}
