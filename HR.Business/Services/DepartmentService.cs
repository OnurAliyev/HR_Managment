using HR.Business.Interfaces;
using HR.Core.Entities;

namespace HR.Business.Services;

public class DepartmentService : IDepartmentServices
{
    public void ActivateDepartment(int departmentId)
    {
        throw new NotImplementedException();
    }

    public void AddEmployee(Employee employee, Department departmentId)
    {
        throw new NotImplementedException();
    }

    public void Create(string departmentName, string departmentAbout, int employeeLimit, string companyName)
    {
        throw new NotImplementedException();
    }

    public void DeactivateDepartment(int departmentId)
    {
        throw new NotImplementedException();
    }

    public Department GetDepartmentById(int departmentId)
    {
        throw new NotImplementedException();
    }

    public void GetDepartmentEmployees(int departmentId)
    {
        throw new NotImplementedException();
    }

    public void ShowAllInCompany(string companyName)
    {
        throw new NotImplementedException();
    }

    public void UpdateDepartment(string newDepartmentName, string newDepartmentAbout, int newEmployeeLimit)
    {
        throw new NotImplementedException();
    }
}
