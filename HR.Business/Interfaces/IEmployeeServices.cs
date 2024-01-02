namespace HR.Business.Interfaces;

public interface IEmployeeServices
{
    void Create(string employeeName, string employeeSurname, int age, string gender, string role, decimal salary,int departmentId);
    void ChangeRole(int employeeId, string newRole);
    void ChangeSalary(int employeeId, decimal newSalary);
    void ChangeDepartment(int employeeId, int newDepartmentId);
    void DeleteEmployee(int employeeId);
    void ShowAll();
}
