using HR.Business.Interfaces;
using HR.Business.Services;
using HR.Business.Utilities.Helpers;
using HR.Core.Entities;
using HR.DataAcces.Contexts;

Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("Application started...");
Console.ResetColor();
CompanyService companyService = new();
DepartmentService departmentService = new();
EmployeeService employeeService = new();
bool AppRun = true;
while (AppRun)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("_______\n" +
                      "Company\n" +
                      "-------");
    Console.ResetColor();
    Console.WriteLine("1) Create company\n" +
                      "2) Activate company\n" +
                      "3) Deactivate company\n" +
                      "4) Show departments of company\n" +
                      "5) Show all companies");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("__________\n" +
                      "Department\n" +
                      "----------");
    Console.ResetColor();
    Console.WriteLine("6) Create department\n" +
                      "7) Add employee to department\n" +
                      "8) Show employees of department\n" +
                      "9) Activate department\n" +
                      "10) Deactivate department\n" +
                      "11) Update department\n" +
                      "12) Show all departments");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("________\n" +
                      "Employee\n" +
                      "--------");
    Console.ResetColor();
    Console.WriteLine("13) Create employee\n" +
                      "14) Change employee department\n" +
                      "15) Change employee role\n" +
                      "16) Change employee salary\n" +
                      "17) Delete employee\n" +
                      "18) Show all employees\n" +
                      "0) Close App");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write("Choose the option: ");
    Console.ResetColor();
    string? option = Console.ReadLine();
    int IntOption;
    bool IsInt = int.TryParse(option, out IntOption);
    if (IsInt)
    {
        if (IntOption >= 0 && IntOption <= 18)
        {
            switch (IntOption)
            {
                case (int)Menu.CreateComp:
                    try
                    {
                        Console.Write("Enter company name: ");
                        string? companyName = Console.ReadLine();
                        Console.WriteLine("Write about company");
                        string? companyAbout = Console.ReadLine();
                        companyService.Create(companyName, companyAbout);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto case (int)Menu.CreateComp;
                    }
                    break;
                case (int)Menu.ActivateComp:
                    if (companyService.CompanyExist() == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no any created company");
                        Console.ResetColor();
                        goto case (int)Menu.CreateComp;
                    }
                    else
                    {
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("All Companies: ");
                            foreach (var company in HRDbContext.Companies)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"Company ID: {company.Id}\n" +
                                                  $"Company name: {company.Name}\n" +
                                                  $"Company created time: {company.CreatedTime}\n" +
                                                  $"Company active status: {company.IsActive}\n" +
                                                  $" ");
                                Console.ResetColor();
                            }
                            Console.ResetColor();
                            Console.Write("Enter company name: ");
                            string? companyName = (Console.ReadLine());
                            companyService.Activate(companyName);
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            goto case (int)Menu.ActivateComp;
                        }
                        break;
                    }
                case (int)Menu.DeactivateComp:
                    if (companyService.CompanyExist() == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no any created company");
                        Console.ResetColor();
                        goto case (int)Menu.CreateComp;
                    }
                    else
                    {
                        try
                        {
                            Console.Write("Enter company name: ");
                            string? companyName = Console.ReadLine();
                            companyService.Deactivate(companyName);
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            goto case (int)Menu.DeactivateComp;
                        }
                        break;
                    }
                case (int)Menu.ShowDepComp:
                    if (departmentService.DepExist() == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no any departments created!");
                        Console.ResetColor();
                        goto case (int)Menu.CreateDep;
                    }
                    else
                    {
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("All companies :");
                            companyService.ShowAll();
                            Console.ResetColor();
                            Console.Write("Enter company name: ");
                            string? companyName = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            companyService.GetAllDepartments(companyName);
                            Console.ResetColor();

                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            goto case (int)Menu.ShowDepComp;
                        }
                    }
                    break;
                case (int)Menu.ShowAllComp:
                    if (companyService.CompanyExist() == true)
                    {
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("All companies: ");
                            companyService.ShowAll();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            goto case (int)Menu.ShowAllComp;
                        }
                        break;

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no any created company");
                        Console.ResetColor();
                        goto case (int)Menu.CreateComp;
                    }
                case (int)Menu.CreateDep:
                    if (companyService.CompanyExist() == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no any created company!\n" +
                                          $"First create company please");
                        Console.ResetColor();
                        goto case (int)Menu.CreateComp;
                    }
                    else
                    {
                        try
                        {
                            Console.Write("Enter department name: ");
                            string? departmentName = Console.ReadLine();
                            Console.WriteLine("Write about department:");
                            string? departmentAbout = Console.ReadLine();
                            Console.Write("Enter employee limit: ");
                            int employeeLimit = Convert.ToInt32(Console.ReadLine());
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("All companies:");
                            companyService.ShowAll();
                            Console.ResetColor();
                            Console.Write("Enter company name: ");
                            string? companyName = Console.ReadLine();
                            departmentService.Create(departmentName, departmentAbout, employeeLimit, companyName);
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            goto case (int)Menu.CreateDep;
                        }
                    }
                    break;
                case (int)Menu.AddEmployee:
                    if (employeeService.EmpExist() == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no any employee created!");
                        Console.ResetColor();
                        goto case (int)Menu.CreateEmp;
                    }
                    else
                    {
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("All employees:");
                            employeeService.ShowAll();
                            Console.ResetColor();
                            Console.Write("Enter employee ID: ");
                            int employeeId = Convert.ToInt32(Console.ReadLine());
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("All departments: ");
                            departmentService.ShowAllDepartments();
                            Console.ResetColor();
                            Console.Write("Enter department ID :");
                            int departmentId = Convert.ToInt32(Console.ReadLine());
                            departmentService.AddEmployee(employeeId, departmentId);

                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            goto case (int)Menu.AddEmployee;
                        }
                    }

                    break;
                case (int)Menu.ShowEmpOfDep:
                    if (departmentService.DepExist() == false || employeeService.EmpExist() == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no any departments or employees created");
                        Console.ResetColor();
                    }
                    else
                    {
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("All departments: ");
                            departmentService.ShowAllDepartments();
                            Console.ResetColor();
                            Console.Write("Enter department ID: ");
                            int departmentId = Convert.ToInt32(Console.ReadLine());
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Employees of department");
                            Console.ResetColor();
                            departmentService.GetDepartmentEmployees(departmentId);
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            goto case (int)Menu.ShowEmpOfDep;
                        }
                    }
                    break;
                case (int)Menu.ActivateDep:
                    if (departmentService.DepExist() == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no any department created!");
                        Console.ResetColor();
                        goto case (int)Menu.CreateDep;
                    }
                    else
                    {
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("All departments: ");
                            foreach (var department in HRDbContext.Departments)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"Department ID: {department.Id}\n" +
                                                  $"Department name: {department.Name}\n" +
                                                  $"Department company: {department.Company.Name}\n" +
                                                  $"Department employees: {department.EmployeeCount}\n" +
                                                  $"Department employee limit: {department.EmployeeLimit}\n" +
                                                  $"Department created time: {department.CreatedTime}\n" +
                                                  $"Department active status: {department.IsActive}\n" +
                                                  $" ");
                                Console.ResetColor();
                            }
                            Console.ResetColor();
                            Console.Write("Enter department ID: ");
                            int departmentId = Convert.ToInt32(Console.ReadLine());
                            departmentService.ActivateDepartment(departmentId);
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            goto case (int)Menu.ActivateDep;
                        }
                    }
                    break;
                case (int)Menu.DeactivateDep:
                    if (departmentService.DepExist() == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no any department created!");
                        Console.ResetColor();
                        goto case (int)Menu.CreateDep;
                    }
                    else
                    {
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("All departments: ");
                            departmentService.ShowAllDepartments();
                            Console.ResetColor();
                            Console.Write("Enter department ID: ");
                            int departmentId = Convert.ToInt32(Console.ReadLine());
                            departmentService.DeactivateDepartment(departmentId);
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            goto case (int)Menu.DeactivateDep;
                        }
                    }
                    break;
                case (int)Menu.UpdateDep:
                    if (departmentService.DepExist() == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no any department created!");
                        Console.ResetColor();
                        goto case (int)Menu.CreateDep;
                    }
                    else
                    {
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("All departments: ");
                            departmentService.ShowAllDepartments();
                            Console.ResetColor();
                            Console.Write("Enter department ID: ");
                            int departmentId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter new department name: ");
                            string? newDepartmentName = Console.ReadLine();
                            Console.Write("Enter new department employee limit: ");
                            int newEmployeeLimit = Convert.ToInt32(Console.ReadLine());
                            departmentService.UpdateDepartment(newDepartmentName, newEmployeeLimit, departmentId);
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            goto case (int)Menu.UpdateDep;
                        }
                    }
                    break;
                case (int)Menu.ShowAllDep:
                    if (departmentService.DepExist() == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no any created department");
                        Console.ResetColor();
                        goto case (int)Menu.CreateDep;
                    }
                    else
                    {
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("All departments: ");
                            departmentService.ShowAllDepartments();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            goto case (int)Menu.ShowAllDep;
                        }
                        break;
                    }
                case (int)Menu.CreateEmp:
                    if (departmentService.DepExist() == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no any department created!\n" +
                                          $"First create department please");
                        Console.ResetColor();
                        goto case (int)Menu.CreateDep;
                    }
                    else
                    {
                        try
                        {
                            Console.Write("Enter employee name: ");
                            string? employeeName = Console.ReadLine();
                            Console.Write("Enter employee surname: ");
                            string? employeeSurname = Console.ReadLine();
                            Console.Write("Enter employee age: ");
                            int employeeAge = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter employee gender: ");
                            string? gender = Console.ReadLine();
                            Console.Write("Enter employee role: ");
                            string? role = Console.ReadLine();
                            Console.Write("Enter employee salary: ");
                            decimal salary = Convert.ToDecimal(Console.ReadLine());
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("All departments: ");
                            Console.ResetColor();
                            departmentService.ShowAllDepartments();
                            Console.Write("Enter department ID: ");
                            int departmentId = Convert.ToInt32(Console.ReadLine());
                            employeeService.Create(employeeName, employeeSurname, employeeAge, gender, role, salary, departmentId);

                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            goto case (int)Menu.CreateEmp;
                        }
                    }
                    break;
                case (int)Menu.ChangeEmpDep:
                    if (employeeService.EmpExist() == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no any employee created!");
                        Console.ResetColor();
                        goto case (int)Menu.CreateEmp;
                    }
                    else
                    {
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("All employees: ");
                            Console.ResetColor();
                            employeeService.ShowAll();
                            Console.Write("Enter employee ID: ");
                            int employeeId = Convert.ToInt32(Console.ReadLine());
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("All departments: ");
                            departmentService.ShowAllDepartments();
                            Console.ResetColor();
                            Console.Write("Enter new department ID: ");
                            int newDepartmentId = Convert.ToInt32(Console.ReadLine());
                            employeeService.ChangeDepartment(employeeId, newDepartmentId);
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            goto case (int)Menu.ChangeEmpDep;
                        }
                    }
                    break;
                case (int)Menu.ChangeEmpR:
                    if (employeeService.EmpExist() == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no any employee created!");
                        Console.ResetColor();
                        goto case (int)Menu.CreateEmp;
                    }
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("All employees: ");
                        Console.ResetColor();
                        employeeService.ShowAll();
                        Console.Write("Enter employee ID: ");
                        int employeeId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter employee's new role: ");
                        string? newRole = Console.ReadLine();
                        employeeService.ChangeRole(employeeId, newRole);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto case (int)Menu.ChangeEmpR;
                    }
                    break;
                case (int)Menu.ChangeEmpSal:
                    if (employeeService.EmpExist() == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no any employee created!");
                        Console.ResetColor();
                        goto case (int)Menu.CreateEmp;
                    }
                    else
                    {
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("All employees: ");
                            Console.ResetColor();
                            employeeService.ShowAll();
                            Console.Write("Enter employee ID: ");
                            int employeeId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter employee's new salary: ");
                            decimal newSalary = Convert.ToDecimal(Console.ReadLine());
                            employeeService.ChangeSalary(employeeId, newSalary);
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            goto case (int)Menu.ChangeEmpSal;
                        }
                    }
                    break;
                case (int)Menu.DeleteEmp:
                    if (employeeService.EmpExist() == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no any employee created!");
                        Console.ResetColor();
                        goto case (int)Menu.CreateEmp;
                    }
                    else
                    {
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("All employees: ");
                            Console.ResetColor();
                            employeeService.ShowAll();
                            Console.Write("Enter employee ID: ");
                            int employeeId = Convert.ToInt32(Console.ReadLine());
                            employeeService.DeleteEmployee(employeeId);
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            goto case (int)Menu.DeleteEmp;
                        }
                    }
                    break;
                case (int)Menu.ShowAllEmp:
                    try
                    {
                        if (employeeService.EmpExist() == false)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("There is no any employee created!");
                            Console.ResetColor();
                            goto case (int)Menu.CreateEmp;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("All employees:");
                            Console.ResetColor();
                            employeeService.ShowAll();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto case (int)Menu.ShowAllEmp;
                    }
                    break;
                case 0:
                    AppRun = false;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Application closed!\n" +
                                      $"Press any key to close window...");
                    Console.ResetColor();
                    break;
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please enter correct number (0-18)");
            Console.ResetColor();
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Please enter only numbers (0-18)");
        Console.ResetColor();
    }
}


