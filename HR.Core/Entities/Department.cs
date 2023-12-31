using HR.Core.Interfaces;
using System.Data;

namespace HR.Core.Entities;

public class Department : IEntity
{
    public int Id { get; }
    private static int _id;
    public string Name { get; set; }
    public string About { get; set; }
    public int EmployeeLimit { get; set; }
    public int EmployeeCount { get; set; }
    public string CompanyName { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedTime { get; set; }
    public Department(string name, string about, int employeeLimit, string companyName)
    {
        Id = _id++;
        Name = name;
        About = about;
        EmployeeLimit = employeeLimit;
        CompanyName = companyName;
        CreatedTime = DateTime.Now;
    }

    public override string ToString()
    {
        return $"ID: {Id} || Name: {Name} || Company: {CompanyName} || Created Time : {CreatedTime} \n ";
    }

}
