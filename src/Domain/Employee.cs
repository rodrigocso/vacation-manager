namespace Domain;

public class Employee
{
    public Guid Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly StartDate { get; set; }

    public Employee(Guid id, string firstName, string lastName, DateOnly startDate)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        StartDate = startDate;
    }

    public Employee(string id, string firstName, string lastName, string startDate)
    {
        Id = Guid.Parse(id);
        FirstName = firstName;
        LastName = lastName;
        StartDate = DateOnly.Parse(startDate);
    }
}
