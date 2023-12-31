using HR.Business.Interfaces;

namespace HR.Business.Services;

internal class EmployeeService : IEmployeeServices
{
    public void ChangeDepartment(int departmentId, string newDepartmentName)
    {
        throw new NotImplementedException();
    }

    public void ChangeRole(int employeeId, string newRole)
    {
        throw new NotImplementedException();
    }

    public void ChangeSalary(int employeeId, decimal newSalary)
    {
        throw new NotImplementedException();
    }

    public void Create(string name, string surname, int age, string gender, string role, decimal salary)
    {
        throw new NotImplementedException();
    }

    public void DeleteEmployee(int employeeId)
    {
        throw new NotImplementedException();
    }

    public void ShowAll()
    {
        throw new NotImplementedException();
    }
}
