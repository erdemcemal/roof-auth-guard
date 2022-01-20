using Roof.API.Models;

namespace Roof.API;

internal static class DummyData
{
    static List<EmployeeVm> _employees = new()
    {
        new EmployeeVm(1, "Yasmeen", "Phelps", Gender.Female, 26),
        new EmployeeVm(2, "Anwar", "Lawson", Gender.Male, 25),
        new EmployeeVm(3, "Libbi", "Mckenna", Gender.Female, 47),
        new EmployeeVm(4, "Winston", "Pugh", Gender.Male, 35)
    };
    public static List<EmployeeVm> GetEmployeeList()
    {
        return _employees;
    }

    public static void AddEmployee(EmployeeVm employee)
    {
        _employees.Add(employee);
    }
}