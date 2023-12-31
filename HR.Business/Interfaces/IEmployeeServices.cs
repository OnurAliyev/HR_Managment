namespace HR.Business.Interfaces;

public interface IEmployeeServices
{
    void Create(string name, string surname, int age, string gender, string role, decimal salary);
    void ChangeRole(int employeeId, string newRole);
    void ChangeSalary(int employeeId, decimal newSalary);
    void ChangeDepartment(int departmentId, string newDepartmentName);
    void DeleteEmployee(int employeeId);
    void ShowAll();
}
