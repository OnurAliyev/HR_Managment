using HR.Business.Interfaces;
using HR.Core.Entities;

namespace HR.Business.Services;

public class DepartmentService : IDepartmentServices
{
    public void ActivateDepartment(Department departmentId)
    {
        throw new NotImplementedException();
    }

    public void AddEmployee(Employee employee, Department departmentId)
    {
        throw new NotImplementedException();
    }

    public void Create(string name, string about, int employeeLimit, Company companyName)
    {
        throw new NotImplementedException();
    }

    public void DeactivateDepartment(Department departmentId)
    {
        throw new NotImplementedException();
    }

    public Department GetDepartmentById(int departmentId)
    {
        throw new NotImplementedException();
    }

    public void GetDepartmentEmployees(Department departmentId)
    {
        throw new NotImplementedException();
    }

    public void ShowAllInCompany(Company companyName)
    {
        throw new NotImplementedException();
    }

    public void UpdateDepartment(string newName, string newAbout, int newEmployeeLimit)
    {
        throw new NotImplementedException();
    }
}
