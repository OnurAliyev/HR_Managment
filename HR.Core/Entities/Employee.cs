using HR.Core.Interfaces;

namespace HR.Core.Entities;

public class Employee : IEntity
{
    public int Id { get; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Age { get; set; }
    public string? Gender { get; set; }
    public string? Role { get; set; }
    public decimal Salary { get; set; }
    public Department Departmentİd { get; set; }

    public DateTime CreatedTime { get; set; }

    private static int _id;

    public Employee(string? name, string? surname, int age, string? gender, string? role,
        decimal salary,Department departmentId)
    {
        Id = _id++;
        Name = name;
        Surname = surname;
        Age = age;
        Gender = gender;
        Role = role;
        Salary = salary;
        Departmentİd = departmentId;
        CreatedTime = DateTime.Now;
    }

    public override string ToString()
    {
        return $"ID: {Id} || Full Name: {Name} {Surname} || Joined Time : {CreatedTime} \n ";
    }
}
