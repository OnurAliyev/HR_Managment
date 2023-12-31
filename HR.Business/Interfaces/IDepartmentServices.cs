using HR.Core.Entities;

namespace HR.Business.Interfaces;

public interface IDepartmentServices
{
    void Create(string departmentName, string departmentAbout, int employeeLimit, string companyName);
    void AddEmployee(Employee employee,Department departmentId);
    void UpdateDepartment(string newDepartmentName, string newDepartmentAbout, int newEmployeeLimit);
    void GetDepartmentEmployees(int departmentId);
    Department GetDepartmentById(int departmentId);
    void ActivateDepartment(int departmentId);
    void DeactivateDepartment(int departmentId);
    void ShowAllInCompany(string companyName);
}

// department update zamani isci sayini yoxlamaq hal hazirkindan az olmasin
