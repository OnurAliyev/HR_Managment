using HR.Core.Entities;

namespace HR.DataAcces.Contexts;

public class HRDbContext
{
    public static List<Company> Companies { get; set; } = new List<Company>();
    public static List<Department> Departments { get; set; } = new List<Department>();
    public static List<Employee> Employees { get; set; } = new List<Employee>();
}
