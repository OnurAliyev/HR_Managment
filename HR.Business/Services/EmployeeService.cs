using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAcces.Contexts;

namespace HR.Business.Services;

public class EmployeeService : IEmployeeServices
{
    public IDepartmentServices? departmentService { get; }
    public IEmployeeServices? employeeService { get; }
    public EmployeeService()
    {
        departmentService = new DepartmentService();
    }
    public void Create(string? employeeName, string? employeeSurname, int age, string? gender, string? role, decimal salary, int departmentId)
    {
        if (String.IsNullOrEmpty(employeeName)) throw new EmptyNameException("Employee name cannot be null or empty");
        if (String.IsNullOrEmpty(employeeSurname)) throw new EmptyNameException("Employee surname cannot be null or empty");
        if (String.IsNullOrEmpty(gender)) throw new EmptyNameException("Employee gender cannot be null or empty");
        if (String.IsNullOrEmpty(role)) throw new EmptyNameException("Employee role cannot be null or empty");
        if (age < 18) throw new MinAgeException("Employee should be over 18 years of age");
        if (salary < 0 || salary < 300) throw new MinSalaryException("Employee salary should be minimum 300 USD");
        Department? dbDepartment=
            HRDbContext.Departments.Find(d=>d.Id==departmentId);
        if (dbDepartment is null) throw new NotFoundException("Department is not found");
        if (dbDepartment.EmployeeCount >= dbDepartment.EmployeeLimit)
            throw new EmployeeLimitException($"{dbDepartment.Name.ToLower()} is already full");
        Employee employee = new(employeeName, employeeSurname, age, gender, role, salary, departmentId);
        employee.Department = dbDepartment;
        employee.Company=dbDepartment.Company;
        HRDbContext.Employees.Add(employee);
        dbDepartment.EmployeeCount++;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Employee: {employee.Name} {employee.Surname} successfully created\n"+
                          $"And added to department with {employee.DepartmentId} ID and {employee.Department.Name} name");
        Console.ResetColor();
    }
    public void ChangeDepartment(int employeeId, int newDepartmentId)
    {
       Employee? dbEmployee=HRDbContext.Employees.Find(e=>e.Id==employeeId);
        if (dbEmployee is null) throw new NotFoundException($"Employee ID: {employeeId} is not found");
        dbEmployee.DepartmentId = newDepartmentId;
    }

    public void ChangeRole(int employeeId, string? newRole)
    {
        if (employeeId < 0) throw new ArgumentOutOfRangeException();
        if (String.IsNullOrEmpty(newRole)) throw new ArgumentException();
        Employee? dbEmployee=
            HRDbContext.Employees.Find(e=> e.Id==employeeId);
        if (dbEmployee is null) throw new NotFoundException("Employee is not found");
        if (newRole == dbEmployee.Role) throw new AlreadyExistException($"Employee is already in {newRole} role");
        dbEmployee.Role = newRole;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Employee role successfully changed to: ^{newRole}^");
        Console.ResetColor();
    }

    public void ChangeSalary(int employeeId, decimal newSalary)
    {
        if(employeeId < 0)throw new ArgumentOutOfRangeException();
        if (newSalary <= 0 || newSalary < 300) throw new MinSalaryException("Employee salary should be minimum 300 USD");
        Employee? employee=HRDbContext.Employees.Find(e=> e.Id==employeeId);
        if (employee is null) throw new NotFoundException("Employee cannot be found");
        employee.Salary = newSalary;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Employee salary successfully changed to ^{newSalary}^");
        Console.ResetColor();
    }


    public void DeleteEmployee(int employeeId)
    {
        if(employeeId < 0) throw new ArgumentOutOfRangeException();
        Employee? dbEmployee=HRDbContext.Employees.Find(e=>e.Id==employeeId);
        if (dbEmployee is null) throw new NotFoundException("Employee is not found");
        HRDbContext.Employees.Remove(dbEmployee);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Employee successfully deleted");
        Console.ResetColor();
        
    }

    public void ShowAll()
    {
       foreach(var employee in HRDbContext.Employees)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Employee ID: {employee.Id}\n" +
                              $"Employee company: {employee.Company}\n" +
                              $"Employee department: {employee.Department}\n" +
                              $"Employee name:{employee.Name}\n" +
                              $"Employee surname: {employee.Surname}\n" +
                              $"Employee age : {employee.Age}\n" +
                              $"Employee gender: {employee.Gender}\n" +
                              $"Employee role: {employee.Role}\n" +
                              $"Employee joined time: {employee.CreatedTime}\n"+
                              $" ");
            Console.ResetColor();


        }
    }
    public bool EmpExist()
    {
        foreach(var employees in HRDbContext.Employees)
        {
            if(employees.Id>=0) return true;
        }
        return false;
    }
}
