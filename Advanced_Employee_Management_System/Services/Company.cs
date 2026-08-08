using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advanced_Employee_Management_System.Common;
using Advanced_Employee_Management_System.Models;

namespace Advanced_Employee_Management_System.Services;
public class Company
{
    private readonly List<Employee> _employees = [];
    private readonly Dictionary<int, Department> _departments = [];
    private readonly Queue<Employee> _onboardingQueue = [];
    private readonly Stack<string> _actionHistory = [];
    private readonly HashSet<string> _companySkills = [];

    private void Log(string action)
    {
        _actionHistory.Push($"{DateTime.Now:HH:mm:ss} - {action}");
    }
    // Employee Management
    public Result<Employee> AddEmployeeToOnboarding()
    {
        var employee = new Employee();
        if (employee.Read())
        {
            if (_departments.ContainsKey(employee.DepartmentId))
            {

                _onboardingQueue.Enqueue(employee);
                Log($"Employee with ID {employee.Id} added to onboarding queue.");
                return Result<Employee>.Success($"Employee with ID {employee.Id} added to onboarding queue.", employee);
            }
            else
            {
                return Result<Employee>.Failure($"Department with ID {employee.DepartmentId} does not exist.");
            }
        }
        else
        {
            return Result<Employee>.Failure("Failed to read employee information.");
        }
    }
    public Result<Employee> ProcessNextEmployee()
    {
        if (_onboardingQueue.Count == 0)
        {
            return Result<Employee>.Failure("No employees in the onboarding queue.");
        }
        var employee = _onboardingQueue.Dequeue();
        _employees.Add(employee);
        Log($"Employee with ID {employee.Id} processed and added to the company.");
        return Result<Employee>.Success($"Employee with ID {employee.Id} processed and added to the company.", employee);
    }
    public Result<Employee> HandleSearchForEmployee()
    {
        Console.WriteLine("Enter Employee 1) for ID or 2) for Name to search on Employee:");
        var searchChoice = Console.ReadLine();
        if (searchChoice == "1")
        {
            foreach (var emp in _employees)
            {
                if (emp.HasId(emp.Id))
                {
                    return Result<Employee>.Success($"Employee with ID {emp.Id} found.", emp);
                }
            }
            return Result<Employee>.Failure("Employee with the given ID not found.");
        }
        else if (searchChoice == "2")
        {
            foreach (var emp in _employees)
            {
                if (emp.HasName(emp.Name))
                {
                    return Result<Employee>.Success($"Employee with Name {emp.Name} found.", emp);
                }
            }
            return Result<Employee>.Failure("Employee with the given Name not found.");
        }
        else
        {
            return Result<Employee>.Failure("Invalid search choice.");
        }

    }
    //public Result<Employee> PromoteEmployeetoManager(int employeeId)
    //{
    //    var employee = _employees.FirstOrDefault(e => e.Id == employeeId);
    //    if (employee == null)
    //    {
    //        return Result<Employee>.Failure($"No employee found with ID {employeeId}.");
    //    }
    //    else
    //    {
    //        foreach (var emp in departmentEmployees)
    //        {
    //            emp.Display();
    //        }
    //        return Result<Employee>.Success($"Found {departmentEmployees.Count} employees in Department ID {departmentId}.", null);
    //    }
    //}

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



    public int menu()
    {
        int choice = -1;
        while (choice == -1)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("    Employee Management System");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Add Department");
            Console.WriteLine("2. Add Employee To Onboarding");
            Console.WriteLine("3. Process Next Employee");
            Console.WriteLine("4. Add Skill");
            Console.WriteLine("5. Search Employee By ID");
            Console.WriteLine("6. Search Employee By Name");
            Console.WriteLine("7. Show Department Employees");
            Console.WriteLine("8. Show Salary Average");
            Console.WriteLine("9. Department Report");
            Console.WriteLine("10. Show Action History");
            Console.WriteLine("11. Show Company Skills");
            Console.WriteLine("12. Show Active Employees");
            Console.WriteLine("13. Show Onboarding Queue");
            Console.WriteLine("14. Exit");
            Console.Write("Enter your menu choice [1 - 14]: ");

            string input = Console.ReadLine()!;

            if (int.TryParse(input, out choice))
            {
                if (choice < 1 || choice > 14)
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    choice = -1;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                choice = -1;
            }
        }
        return choice;
    }

}
