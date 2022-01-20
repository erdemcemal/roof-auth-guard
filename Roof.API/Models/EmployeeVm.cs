namespace Roof.API.Models;

public class EmployeeVm
{
    public EmployeeVm(int id, string firstName, string lastName, Gender gender, int age)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Age = age;
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public int Age { get; set; }
}
