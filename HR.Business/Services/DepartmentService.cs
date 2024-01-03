using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAcces.Contexts;

namespace HR.Business.Services;

public class DepartmentService : IDepartmentServices
{
    private ICompanyServices? companyService { get; }
    public DepartmentService()
    {
        companyService = new CompanyService();
    }
    public void Create(string? departmentName, string? departmentAbout, int employeeLimit, string? companyName)
    {
        if (String.IsNullOrEmpty(departmentName)) throw new EmptyNameException("Department name cannot be null or empty");
        if (String.IsNullOrEmpty(companyName)) throw new EmptyNameException("Company name cannot be null or empty");
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
        Console.WriteLine($"Department: ^{department.Name}^ succesfully created\n" +
                          $"And added to company: ^{department.Company.Name}^");
        Console.ResetColor();
    }
    public void AddEmployee(int employeeId, int departmentId)
    {
        Department? dbDepartment =
            HRDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null)
            throw new NotFoundException($"Department {departmentId} ID is not found");
        if (dbDepartment.EmployeeCount >= dbDepartment.EmployeeLimit)
            throw new EmployeeLimitException($"Employee can't be added. Because this department is already full.\n" +
                                             $"Try another department please.");
        Employee? dbEmployee = HRDbContext.Employees.Find(e => e.Id == employeeId);
        if (dbEmployee is null)
            throw new NotFoundException("Employee is not found");
        Department? employeeDepartment = dbDepartment;
        if (dbEmployee.DepartmentId != dbDepartment.Id && employeeDepartment.CompanyName == dbDepartment.CompanyName && employeeDepartment.EmployeeCount < employeeDepartment.EmployeeLimit)
        {
            dbEmployee.Department = dbDepartment;
            dbEmployee.DepartmentId = dbDepartment.Id;
            dbDepartment.EmployeeCount++;
            Console.WriteLine($"Employee succesfully added to {dbDepartment.Name}");
        }
        else if (dbEmployee.DepartmentId == dbDepartment.Id && employeeDepartment.CompanyName == dbDepartment.CompanyName)
        {
            throw new AlreadyExistException($"Employee {dbEmployee.Name} is already in {dbDepartment.Name} Department");
        }
        else throw new NotFoundException($"Employee {dbEmployee.Name} is not found in company");

    }
    public void ActivateDepartment(int departmentId)
    {
        Department? dbDepartment = HRDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null || dbDepartment.Id < 0) throw new NotFoundException($"Department ID: {departmentId} is not found");
        if (dbDepartment.IsActive == true)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Department is already active!");
        }
        else
        {
            dbDepartment.IsActive = true;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Department ID: {dbDepartment.Id} Name: {dbDepartment.Name} succesfully activated");
            Console.ResetColor();
        }
    }

    public void DeactivateDepartment(int departmentId)
    {
        Department? dbDepartment = HRDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null || dbDepartment.Id < 0) throw new NotFoundException($"Department ID: {departmentId} is not found");
        if (dbDepartment.IsActive == false)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Department is already deactive!");
        }
        else
        {
            dbDepartment.IsActive = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Department ID: {dbDepartment.Id} Name: {dbDepartment.Name} succesfully deactivated");
            Console.ResetColor();
        }
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
        Department? dbDepartment =
            HRDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null) throw new NotFoundException($"Department ID: {departmentId} is not found");
        if (dbDepartment is not null)
        {
            foreach (var employees in HRDbContext.Employees)
            {
                if (employees.DepartmentId == dbDepartment.Id)
                {
                    Console.WriteLine($"Employee ID: {employees.Id}\n" +
                                      $"Employee name: {employees.Name}\n" +
                                      $"Employee surname: {employees.Surname}\n" +
                                      $"Employee company: {employees.Company}\n" +
                                      $"Employee department: {employees.Department.Id} {employees.Department.Name}\n" +
                                      $"Employee age: {employees.Age}\n" +
                                      $"Employee gender: {employees.Gender}\n" +
                                      $"Employee salary: {employees.Salary}\n" +
                                      $"Employee role: {employees.Role}\n" +
                                      $"Employe joined time: {employees.CreatedTime}");

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
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Departments in {companyName} : \n" +
                                  $"Department ID: {department.Id}  Department Name: {department.Name}");
                Console.ResetColor();
            }
        }
    }

    public void UpdateDepartment(string? newDepartmentName, int newEmployeeLimit, int departmentId)
    {
        if (String.IsNullOrEmpty(newDepartmentName)) throw new EmptyNameException("Department name cannot be null or empty");
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
        dbDepartment.EmployeeLimit = newEmployeeLimit;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Department {newDepartmentName} succesfully updated");
        Console.ResetColor();
    }

    public void ShowAllDepartments()
    {
        foreach (var department in HRDbContext.Departments)
        {
            if (department.IsActive == true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Department ID: {department.Id}\n" +
                                  $"Department name: {department.Name}\n" +
                                  $"Department company: {department.Company.Name}\n" +
                                  $"Department employees: {department.EmployeeCount}\n" +
                                  $"Department employee limit: {department.EmployeeLimit}\n" +
                                  $"Department created time: {department.CreatedTime}\n" +
                                  $"   ");
                Console.ResetColor();
            }
        }
    }
    public bool DepExist()
    {
        foreach(var department in HRDbContext.Departments)
        {
            if(department.Id>=0 && department.IsActive==true)return true;
        }
        return false;
    }

}