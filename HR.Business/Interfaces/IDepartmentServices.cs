using HR.Core.Entities;

namespace HR.Business.Interfaces;

public interface IDepartmentServices
{
    void Create(string departmentName, string departmentAbout, int employeeLimit, string companyName);
    void AddEmployee(int employeeId,int departmentId);
    void UpdateDepartment(string newDepartmentName, int newEmployeeLimit,int departmentId);
    void GetDepartmentEmployees(int departmentId);
    Department GetDepartmentById(int departmentId);
    void ActivateDepartment(int departmentId);
    void DeactivateDepartment(int departmentId);
    void ShowAllInCompany(string companyName);
    void ShowAllDepartments();
}

// department update zamani isci sayini yoxlamaq hal hazirkindan az olmasin
