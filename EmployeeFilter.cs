using System.Text.Json;

namespace pusula_may;

public static class EmployeeFilter
{
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        var filteredEmployees = employees
            .Where(e => e.Age >= 25 && e.Age <= 40 &&
                        (e.Department == "IT" || e.Department == "Finance") &&
                        e.Salary >= 5000 && e.Salary <= 9000 &&
                        e.HireDate.Year > 2017)
            .ToList();
        
        if (!filteredEmployees.Any())
        {
            return JsonSerializer.Serialize(new {
                Names = new string[0],
                TotalSalary = 0,
                AverageSalary = 0,
                MinSalary = 0,
                MaxSalary = 0,
                Count = 0
            });
        }
        
        var sortedNames = filteredEmployees
            .OrderByDescending(e => e.Name.Length)
            .ThenBy(e => e.Name)
            .Select(e => e.Name)
            .ToArray();
        
        var salaries = filteredEmployees.Select(e => e.Salary).ToList();
        var totalSalary = salaries.Sum();
        var averageSalary = salaries.Average();
        var minSalary = salaries.Min();
        var maxSalary = salaries.Max();
        
        return JsonSerializer.Serialize(new {
            Names = sortedNames,
            TotalSalary = totalSalary,
            AverageSalary = Math.Round(averageSalary, 2),
            MinSalary = minSalary,
            MaxSalary = maxSalary,
            Count = filteredEmployees.Count
        });
    }
}