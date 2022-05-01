using Domain;

namespace Application;

internal static class EmployeeMapper
{
    internal static EmployeeDto ToDto(this Employee employee) =>
        new(employee.Id, employee.FirstName, employee.LastName, employee.StartDate);
}
