using System.Text.Json;
using System.Xml.Linq;

namespace pusula_may;

public static class PersonFilter
{
    public static string FilterPeopleFromXml(string xmlData)
    {
        XDocument doc = XDocument.Parse(xmlData);

        var filteredPeople = doc.Descendants("Person")
            .Where(p =>
            {
                var age = (int?)p.Element("Age");
                var department = (string)p.Element("Department")!;
                var salary = (decimal?)p.Element("Salary");
                var hireDateStr = (string)p.Element("HireDate")!;

                if (!age.HasValue || !salary.HasValue || string.IsNullOrEmpty(hireDateStr))
                    return false;

                if (!DateTime.TryParse(hireDateStr, out var hireDate))
                    return false;

                return age > 30 &&
                       department == "IT" &&
                       salary > 5000 &&
                       hireDate.Year < 2019;
            })
            .Select(p => new
            {
                Name = (string)p.Element("Name"),
                Salary = (decimal)p.Element("Salary")
            })
            .ToList();

        if (!filteredPeople.Any())
        {
            return JsonSerializer.Serialize(new
            {
                Names = new string[0],
                TotalSalary = 0,
                AverageSalary = 0,
                MaxSalary = 0,
                Count = 0
            });
        }

        var names = filteredPeople.Select(p => p.Name)
            .OrderBy(name => name)
            .ToArray();

        var salaries = filteredPeople.Select(p => p.Salary).ToList();
        var totalSalary = salaries.Sum();
        var averageSalary = salaries.Average();
        var maxSalary = salaries.Max();

        return JsonSerializer.Serialize(new
        {
            Names = names,
            TotalSalary = totalSalary,
            AverageSalary = averageSalary,
            MaxSalary = maxSalary,
            Count = filteredPeople.Count
        });
    }
}