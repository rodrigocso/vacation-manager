using Domain;

namespace Application;

public static class EmployeeMapper
{
    public static EmployeeDto ToDto(this Employee employee) =>
        new(employee.Id, employee.FirstName, employee.LastName, employee.StartDate);

    public static Employee ToEntity(this EmployeeDto employeeDto) =>
        new(employeeDto.Id, employeeDto.FirstName, employeeDto.LastName, employeeDto.StartDate);
}
