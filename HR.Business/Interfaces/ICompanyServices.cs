using HR.Core.Entities;

namespace HR.Business.Interfaces;

public interface ICompanyServices
{
    void Create(string companyName,string companyAbout);
    void Activate(string companyName);
    void Deactivate(string companyName);
    void GetAllDepartments(string companyName);
    Company? FindCompanyBy(string? companyName);
    void ShowAll();

}
