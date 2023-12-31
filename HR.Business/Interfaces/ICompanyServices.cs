using HR.Core.Entities;

namespace HR.Business.Interfaces;

public interface ICompanyServices
{
    void Create(string name,string about);
    void Activate(string name);
    void Deactivate(string name);
    void GetAllDepartmentsById(int id);
    Company FindCompanyById(int id);
    void ShowAll();

}
