using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAcces.Contexts;

namespace HR.Business.Services;

public class CompanyService : ICompanyServices
{
    public void Create(string companyName, string companyAbout)
    {
        if (String.IsNullOrEmpty(companyName)) throw new ArgumentNullException();
        Company? dbCompany=
            HRDbContext.Companies.Find(c=> c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is not null)
            throw new AlreadyExistException($"{companyName} is already exist");
        Company company = new(companyName,companyAbout);
        HRDbContext.Companies.Add(company);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{companyName} successfully created");
        Console.ResetColor();
    }
    public void Activate(string companyName)
    {
        if(String.IsNullOrEmpty(companyName)) throw new ArgumentNullException();
        Company? dbCompany=
            HRDbContext.Companies.Find(c=>c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is null) throw new NotFoundException($"{companyName} is not found");
        dbCompany.IsActive = true;
    }


    public void Deactivate(string companyName)
    {
        if (String.IsNullOrEmpty(companyName)) throw new ArgumentNullException();
        Company? dbCompany =
            HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is null) throw new NotFoundException($"{companyName} is not found");
        dbCompany.IsActive = false;
    }

    public Company? FindCompanyBy(string? companyName)
    {
        if(String.IsNullOrEmpty(companyName)) throw new ArgumentNullException();
        return HRDbContext.Companies.Find(c=>c.Name.ToLower()==companyName.ToLower());
    }

    public void GetAllDepartments(string companyName)
    {
        if (String.IsNullOrEmpty(companyName))throw new ArgumentNullException();
        Company? dbCompany =
            HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is not null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Departments of {companyName} company: ");
            Console.ResetColor();
            foreach (var department in HRDbContext.Departments)
            {
                if (department.CompanyName.ToLower() == dbCompany.Name.ToLower())
                {
                    Console.WriteLine($"Department ID: {department.Id} Department name: {department.Name}");
                }
            }
        }
        else throw new NotFoundException($"{companyName} is not found");
        
    }

    public void ShowAll()
    {
        foreach(var company in HRDbContext.Companies)
        {
            if(company.IsActive==true) Console.WriteLine($"Company ID: {company.Id} Company Name: {company.Name}");
        }
    }
}

