using HR.Core.Interfaces;

namespace HR.Core.Entities;
public class Company : IEntity
{
    public int Id { get; }
    private static int _id;
    public string Name { get; set; }
    public string About { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedTime { get; set; }
    public Company(string name, string about)
    {
        Id = _id++;
        Name = name;
        About = about;
        CreatedTime = DateTime.Now;
    }
    public override string ToString()
    {
        return $"ID: {Id} || Name: {Name} || Created Time : {CreatedTime}";
    }
}
