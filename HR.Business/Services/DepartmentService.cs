using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAcces.Contexts;

namespace HR.Business.Services;

public class DepartmentService : IDepartmentServices
{
    private IDepartmentServices? departmentService { get; }
    private ICompanyServices? companyService { get; }
    public DepartmentService()
    {
        companyService = new CompanyService();
    }
    public void Create(string departmentName, string departmentAbout, int employeeLimit, string companyName)
    {
        if (String.IsNullOrEmpty(departmentName)) throw new ArgumentNullException();
        if (String.IsNullOrEmpty(companyName)) throw new ArgumentNullException();
        Department? dbDepartment =
            HRDbContext.Departments.Find(d => d.Name.ToLower() == departmentName.ToLower());
        Company? dbCompany =
            HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is null) throw new NotFoundException($"{companyName} is not found");
        if (dbDepartment is not null && dbDepartment.CompanyName == companyName)
            throw new AlreadyExistException($"{dbDepartment.Name} is already exist");
        if (employeeLimit < 10) throw new EmployeeLimitException($"{departmentName} should be minimum 10 employees");
        Department department = new(departmentName, departmentAbout, employeeLimit, companyName);
        department.Company = dbCompany;
        HRDbContext.Departments.Add(department);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{department.Name} succesfully created");
        Console.ResetColor();
    }
    public void AddEmployee(Employee employee, int departmentId)
    {
        Department? dbDepartment =
            HRDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null)
            throw new NotFoundException($"Department {departmentId} ID is not found");
        if (dbDepartment.EmployeeCount >= dbDepartment.EmployeeLimit)
            throw new EmployeeLimitException($"Employee can't be added. Because this department is already full.\n" +
                                             $"Try another department please.");
        if (dbDepartment.EmployeeCount < dbDepartment.EmployeeLimit)
        {
            employee.Departmentİd = dbDepartment;
            dbDepartment.EmployeeCount++;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Employee succesfully added");
            Console.ResetColor();
        }

    }
    public void ActivateDepartment(int departmentId)
    {
        Department? dbDepartment = HRDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null) throw new NotFoundException($"Department ID: {departmentId} is not found");
        dbDepartment.IsActive = true;
    }

    public void DeactivateDepartment(int departmentId)
    {
        Department? dbDepartment = HRDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null) throw new NotFoundException($"Department ID: {departmentId} is not found");
        dbDepartment.IsActive = false;
    }

    public Department GetDepartmentById(int departmentId)
    {
        if (departmentId < 0) throw new ArgumentOutOfRangeException();
        Department? dbDepartment =
            HRDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null) throw new NotFoundException($"Department ID: {departmentId} is not found");
        return dbDepartment;
    }

    public void GetDepartmentEmployees(int departmentId)
    {
        if (departmentId < 0) throw new ArgumentOutOfRangeException();
        Department? dbDepartmentId =
            HRDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartmentId is null) throw new NotFoundException($"Department ID: {departmentId} is not found");
        if (dbDepartmentId is not null)
        {
            foreach (var employees in HRDbContext.Employees)
            {
                if (employees.Departmentİd == dbDepartmentId)
                {
                    Console.WriteLine($"Employee ID: {employees.Id}\n" +
                                      $"Employee name: {employees.Name}\n" +
                                      $"Employee surname: {employees.Surname}\n" +
                                      $"Employee department: {employees.Departmentİd} {employees.Departmentİd.Name}\n" +
                                      $"Employee age: {employees.Age}\n" +
                                      $"Employee gender: {employees.Gender}\n" +
                                      $"Employee salary: {employees.Salary}\n" +
                                      $"Employee role: {employees.Role}\n" +
                                      $"Employe joined time{employees.CreatedTime}");

                }
            }
        }
    }

    public void ShowAllInCompany(string companyName)
    {
        foreach (var department in HRDbContext.Departments)
        {
            if (department.Company.Name == companyName && department.IsActive == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Departments in {companyName} : \n" +
                                  $"Department ID: {department.Id}  Department Name: {department.Name}");
                Console.ResetColor();
            }
        }
    }

    public void UpdateDepartment(string newDepartmentName, string newDepartmentAbout, int newEmployeeLimit, int departmentId)
    {
        if (String.IsNullOrEmpty(newDepartmentName)) throw new ArgumentNullException();
        Department? dbDepartment =
            HRDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null) throw new NotFoundException("Department is not found");
        Department? dbNewDepartment =
            HRDbContext.Departments.Find(d => d.Name.ToLower() == newDepartmentName.ToLower());
        if (dbNewDepartment is not null && dbNewDepartment.Company == dbDepartment.Company)
            throw new AlreadyExistException($"{newDepartmentName} is already exist");
        if (newEmployeeLimit <= dbDepartment.EmployeeCount)
            throw new EmployeeLimitException($" New department employee limit cannot be less than old department current employee count: {dbDepartment.EmployeeCount}");
        dbDepartment.Name = newDepartmentName;
        dbDepartment.About = newDepartmentAbout;
        dbDepartment.EmployeeLimit = newEmployeeLimit;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Department {newDepartmentName} succesfully updated");
        Console.ResetColor();
    }
}
