using HR.Core.Entities;

namespace HR.Business.Interfaces;

public interface IDepartmentServices
{
    void Create(string name, string about, int employeeLimit, Company companyName);
    void AddEmployee(Employee employee,Department departmentId);
    void UpdateDepartment(string newName, string newAbout, int newEmployeeLimit);
    void GetDepartmentEmployees(Department departmentId);
    Department GetDepartmentById(int departmentId);
    void ActivateDepartment(Department departmentId);
    void DeactivateDepartment(Department departmentId);
    void ShowAllInCompany(Company companyName);
}

// department update zamani isci sayini yoxlamaq hal hazirkindan az olmasin
