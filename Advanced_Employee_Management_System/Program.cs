using Advanced_Employee_Management_System.Models;
using Advanced_Employee_Management_System.Services;

public class Program
{
    public static void Main(string[] args)
    {
        Company company = new Company();
        company.EmployeeOnboarded += (e) => Console.WriteLine($"Employee onboarded: {e.Employee.ToString()}, {e.Message}");
        company.EmployeePromoted += (e) => Console.WriteLine($"Employee promoted: {e.Employee.ToString()}, {e.Message}");

        while (true)
        {
            int choice = company.menu();
            switch (choice)
            {
                case 1:
                    Console.WriteLine(company.AddEmployeeToOnboarding());
                    break;
                case 2:
                    Console.WriteLine(company.ProcessNextEmployee());
                    break;
                case 3:
                    Console.WriteLine(company.HandleSearchForEmployee());
                    break;
                case 4:
                    Console.WriteLine(company.PromoteEmployeetoManager());
                    break;
                case 5:
                    Console.WriteLine(company.DemoteManagerToEmployee());
                    break;
                case 6:
                    Console.WriteLine(company.RemoveEmployee());
                    break;
                case 7:
                    Console.WriteLine(company.UpdateEmployee());
                    break;
                case 8:
                    Console.WriteLine(company.AddSkillToEmployee());
                    break;
                case 9:
                    Console.WriteLine(company.RemoveSkillFromEmployee());
                    break;
                case 10:
                    Console.WriteLine(company.ShowEmployeeDetails());
                    break;
                case 11:
                    Console.WriteLine(company.AddMemeberToManager());
                    break;
                case 12:
                    Console.WriteLine(company.GetAllEmployees());
                    break;
                case 13:
                    Console.WriteLine(company.AddDepartment());
                    break;
                case 14:
                    Console.WriteLine(company.GetDepartmentById());
                    break;
                case 15:
                    Console.WriteLine(company.RemoveDepartment());
                    break;
                case 16:
                    Console.WriteLine(company.GetAllDepartments());
                    break;
                case 17:
                    Console.WriteLine(company.GetDepartmentEmployeeCount());
                    break;
                case 18:
                    Console.WriteLine(company.AddSkill());
                    break;
                case 19:
                    Console.WriteLine(company.RemoveSkill());
                    break;
                case 20:
                    Console.WriteLine(company.GetAllSkills());
                    break;
                case 21:
                    Console.WriteLine(company.GetActionHistory());
                    break;
                case 22:
                    Console.WriteLine(company.CalcAvarageSalary());
                    break;
                case 23:
                    HandleFilterEmployees(company);
                    break;
                case 24:
                    Console.WriteLine("Exiting the program...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

    }
    private static void HandleFilterEmployees(Company company)
    {
        Console.WriteLine("Filter by: (1) Managers only  (2) Salary above amount  (3) By this department");
        int choice = int.TryParse(Console.ReadLine(), out int result) ? result : 0;


        List<Employee> filtered;

        switch (choice)
        {
            case 1:
                // Passing a lambda directly as the EmployeeFilter delegate.
                filtered = company.FilterEmployees(emp => emp is Manager);
                break;
            case 2:
                Console.WriteLine("Enter the salary threshold: ");
                double threshold = double.TryParse(Console.ReadLine(), out double salary) ? salary : 0;
                filtered = company.FilterEmployees(emp => emp.Salary > threshold);
                break;
            case 3:
                Console.WriteLine("Enter the department ID: ");
                int departmentId = int.TryParse(Console.ReadLine(), out int deptId) ? deptId : 0;
                filtered = company.FilterEmployees(emp => emp.DepartmentId == departmentId);
                break;
            default:
                Console.WriteLine("Invalid filter option.");
                return;
        }

        if (filtered.Count == 0)
        {
            Console.WriteLine("No employees matched that filter.");
            return;
        }

        foreach (Employee employee in filtered)
            Console.WriteLine(employee);
    }
}
