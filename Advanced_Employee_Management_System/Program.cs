using Advanced_Employee_Management_System.Models;
using Advanced_Employee_Management_System.Services;

public class Program
{
    public static void Main(string[] args)
    {
        //public void run()
        //{
        //    while (true)
        //    {
        //        int choice = menu();

        //        if (choice == 1)
        //            AddDepartment();
        //        else if (choice == 2)
        //            AddEmployeeToOnboarding();
        //        else if (choice == 3)
        //            ProcessNextEmployee();
        //        else if (choice == 4)
        //            AddSkill();
        //        else if (choice == 5)
        //            SearchById();
        //        else if (choice == 6)
        //            SearchByName();
        //        else if (choice == 7)
        //            ShowDepartmentEmployees();
        //        else if (choice == 8)
        //            CalculateAverageSalaryInDepartment();
        //        else if (choice == 9)
        //            DepartmentReport();
        //        else if (choice == 10)
        //            ShowHistory();
        //        else if (choice == 11)
        //            ShowCompanySkills();
        //        else if (choice == 12)
        //            ShowEmployees();
        //        else if (choice == 13)
        //            ShowWaitingEmployees();
        //        else
        //            break;
        //    }
        //}



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
                double threshold = double.TryParse(Console.ReadLine(), out double salary) ? salary : 0;
                filtered = company.FilterEmployees(emp => emp.Salary > threshold);
                break;
            case 3:
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
