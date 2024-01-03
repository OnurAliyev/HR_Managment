using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAcces.Contexts;
using System.Runtime.InteropServices.Marshalling;
using System.Xml.Linq;

namespace HR.Business.Services;

public class CompanyService : ICompanyServices
{
    public void Create(string? companyName, string? companyAbout)
    {
        if (String.IsNullOrEmpty(companyName)) throw new EmptyNameException("Company name cannot be null or empty");
        Company? dbCompany =
            HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is not null)
            throw new AlreadyExistException($"{companyName} is already exist");
        Company? company = new(companyName, companyAbout);
        HRDbContext.Companies.Add(company);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Company ^{companyName}^ succesfully created!");
    }
    public void Activate(string? companyName)
    {
        if (String.IsNullOrEmpty(companyName)) throw new EmptyNameException("Company name cannot be null or empty");
        Company? dbCompany =
            HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is null) throw new NotFoundException($"{companyName} is not found");
        if (dbCompany.IsActive == true)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Department is already active!");
        }
        else
        {
            dbCompany.IsActive = true;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Company {dbCompany.Name} succesfully activated");
            Console.ResetColor();
        }
    }


    public void Deactivate(string? companyName)
    {
        if (String.IsNullOrEmpty(companyName)) throw new EmptyNameException("Company name cannot be null or empty");
        Company? dbCompany =
            HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is null) throw new NotFoundException($"{companyName} is not found");
        if (dbCompany.IsActive == false)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Department is already deactive!");
        }
        else
        {
            dbCompany.IsActive = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Company {dbCompany.Name} succesfully deactivated");
            Console.ResetColor();
        }
    }

    public Company? FindCompanyBy(string? companyName)
    {
        if (String.IsNullOrEmpty(companyName)) throw new EmptyNameException("Company name cannot be null or empty");
        return HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
    }

    public void GetAllDepartments(string? companyName)
    {
        bool depExist = false;
        if (String.IsNullOrEmpty(companyName)) throw new EmptyNameException("Company name cannot be null or empty");
        Company? dbCompany =
            HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is not null)
        {
            foreach (var department in HRDbContext.Departments)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Departments of company: {companyName}");
                if (department.CompanyName.ToLower() == dbCompany.Name.ToLower())
                {
                    depExist = true;
                    Console.WriteLine($"Department ID: {department.Id}\n" +
                                      $"Department name: {department.Name}\n" +
                                      $"Department current employee count: {department.EmployeeCount}\n" +
                                      $"Department employee limit: {department.EmployeeLimit}\n" +
                                      $"Department created time: {department.CreatedTime}");
                    Console.ResetColor();
                }
            }
            if (depExist == false)
                Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"There is no any departments in company {dbCompany.Name}");
            Console.ResetColor();
        }
        else throw new NotFoundException($"{companyName} is not found");

    }

    public bool CompanyExist()
    {
        foreach (var company in HRDbContext.Companies)
        {
            if (company.IsActive == true) return true;
        }
        return false;
    }
    public void ShowAll()
    {
        foreach (var company in HRDbContext.Companies)
        {
            if (company.IsActive == true)
                Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Company ID: {company.Id}\n" +
                              $"Company Name: {company.Name}\n" +
                              $"Company created time: {company.CreatedTime}\n" +
                              $" ");
            Console.ResetColor();
        }
    }
}
