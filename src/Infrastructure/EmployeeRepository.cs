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

    public async Task<IEnumerable<Employee>> FindEmployees()
    {
        await using var connection = new SqliteConnection(_connectionString);

        return await connection.QueryAsync<Employee>("SELECT * FROM Employee;");
    }

    public async Task<Employee?> FindEmployeeById(Guid id)
    {
        await using var connection = new SqliteConnection(_connectionString);

        return await connection.QueryFirstOrDefaultAsync<Employee>(
            "SELECT * FROM Employee WHERE Id = @id;",
            new { id });
    }

    public async Task AddEmployee(Employee employee)
    {
        await using var connection = new SqliteConnection(_connectionString);

        await connection.ExecuteAsync(@"
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

    public async Task UpdateEmployee(Employee employee)
    {
        await using var connection = new SqliteConnection(_connectionString);

        await connection.ExecuteAsync(@"
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
