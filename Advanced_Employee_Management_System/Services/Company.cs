using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advanced_Employee_Management_System.Common;
using Advanced_Employee_Management_System.Delegates;
using Advanced_Employee_Management_System.Events;
using Advanced_Employee_Management_System.Models;

namespace Advanced_Employee_Management_System.Services;
public class Company
{
    private readonly List<Employee> _employees = [];
    private readonly Dictionary<int, Department> _departments = [];
    private readonly Queue<Employee> _onboardingQueue = [];
    private readonly Stack<string> _actionHistory = [];
    private readonly HashSet<string> _companySkills = [];

    public event EmployeeEvent? EmployeeOnboarded;
    public event EmployeeEvent? EmployeePromoted;
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
        if(EmployeeOnboarded != null)
        {
            var employeeEventArgs = new EmployeeEventArgs(employee, $"{employee.Name} has completed onboarding and is now active.");
            EmployeeOnboarded(employeeEventArgs);
        }
        return Result<Employee>.Success($"Employee with ID {employee.Id} processed and added to the company.", employee);
    }
    public Result<Employee> HandleSearchForEmployee()
    {
        Console.WriteLine("Enter Employee 1) for ID or 2) for Name to search on Employee:");
        var searchChoice = Console.ReadLine();
        if (searchChoice == "1")
        {
            Console.WriteLine("Enter Employee ID to search: ");
            int employeeId = 0;
            if(int.TryParse(Console.ReadLine(), out int id))
            {
                employeeId = id;
            }
            else
            {
                return Result<Employee>.Failure("Invalid Employee ID.");
            }
            foreach (var emp in _employees)
            {
                if (emp.HasId(employeeId))
                {
                    return Result<Employee>.Success($"Employee with ID {emp.Id} found.", emp);
                }
            }
            return Result<Employee>.Failure("Employee with the given ID not found.");
        }
        else if (searchChoice == "2")
        {
            Console.WriteLine("Enter Employee Name to search: ");
            string employeeName = Console.ReadLine()!;

            foreach (var emp in _employees)
            {
                if (emp.HasName(employeeName))
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
    public Result<Employee> PromoteEmployeetoManager()
    {
        Console.WriteLine("Enter Employee ID to promote: ");
        var employeeId = 0;
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            employeeId = id;
        }
        else
        {
            return Result<Employee>.Failure("Invalid Employee ID.");
        }
        var employee = new Employee();
        int indx = -1;
        for (int i = 0; i < _employees.Count; i++)
        {
            if (_employees[i].HasId(employeeId))
            {
                employee = _employees[i];
                indx = i;
                break;
            }
        }
        if (employee == null)
        {
            return Result<Employee>.Failure($"No employee found with ID {employeeId}.");
        }
        else
        {
            if(employee is Manager)
            {
                return Result<Employee>.Failure($"Employee with ID {employeeId} is already a Manager.");
            }
            else 
            {
                var manager = new Manager
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    HireDate = employee.HireDate,
                    DepartmentId = employee.DepartmentId,
                    Salary = employee.Salary,
                    Skills = employee.Skills
                };
                _employees[indx] = manager;
                Log($"Employee with ID {employeeId} promoted to Manager.");
                if(EmployeePromoted != null)
                {
                    var employeeEventArgs = new EmployeeEventArgs(manager, $"{manager.Name} has been promoted to Manager.");
                    EmployeePromoted(employeeEventArgs);
                }
                return Result<Employee>.Success($"Employee with ID {employeeId} promoted to Manager.", manager);
            }
        }
    }
    public Result<Employee> DemoteManagerToEmployee()
    {
        Console.WriteLine("Enter Employee ID to demote: ");
        var employeeId = 0;
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            employeeId = id;
        }
        else
        {
            return Result<Employee>.Failure("Invalid Employee ID.");
        }
        var employee = new Employee();
        int indx = -1;
        for (int i = 0; i < _employees.Count; i++)
        {
            if (_employees[i].HasId(employeeId))
            {
                employee = _employees[i];
                indx = i;
                break;
            }
        }
        if (employee == null)
        {
            return Result<Employee>.Failure($"No employee found with ID {employeeId}.");
        }
        else
        {
            if(employee is not Manager)
            {
                return Result<Employee>.Failure($"Employee with ID {employeeId} is not a Manager.");
            }
            else 
            {
                var regularEmployee = new Employee
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    HireDate = employee.HireDate,
                    DepartmentId = employee.DepartmentId,
                    Salary = employee.Salary,
                    Skills = employee.Skills
                };
                _employees[indx] = regularEmployee;
                Log($"Manager with ID {employeeId} demoted to Employee.");
                return Result<Employee>.Success($"Manager with ID {employeeId} demoted to Employee.", regularEmployee);
            }
        }
    }
    public Result<Employee> RemoveEmployee()
    {
        Console.WriteLine("Enter Employee ID to remove: ");
        var employeeId = 0;
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            employeeId = id;
        }
        else
        {
            return Result<Employee>.Failure("Invalid Employee ID.");
        }
        var employee = new Employee();
        for (int i = 0; i < _employees.Count; i++)
        {
            if (_employees[i].HasId(employeeId))
            {
                employee = _employees[i];
                break;
            }
        }
        if (employee == null)
        {
            return Result<Employee>.Failure($"No employee found with ID {employeeId}.");
        }
        else
        {
            _employees.Remove(employee);
            Log($"Employee with ID {employeeId} removed from the company.");
            return Result<Employee>.Success($"Employee with ID {employeeId} removed from the company.", employee);
        }
    }
    public Result<Employee> UpdateEmployee()
    {
        Console.WriteLine("Enter Employee ID to update: ");
        var employeeId = 0;
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            employeeId = id;
        }
        else
        {
            return Result<Employee>.Failure("Invalid Employee ID.");
        }
        var employee = new Employee();
        int indx = -1;
        for (int i = 0; i < _employees.Count; i++)
        {
            if (_employees[i].HasId(employeeId))
            {
                employee = _employees[i];
                indx = i;
                break;
            }
        }
        if (employee == null)
        {
            return Result<Employee>.Failure($"No employee found with ID {employeeId}.");
        }
        else
        {
            Console.WriteLine("Enter new details for the employee:");
            var updatedEmployee = new Employee();
            if (updatedEmployee.Read())
            {
                bool departmentExists = _departments.ContainsKey(updatedEmployee.DepartmentId);
                if (!departmentExists)
                {
                    return Result<Employee>.Failure($"Department with ID {updatedEmployee.DepartmentId} does not exist.");
                }
                _employees[indx] = updatedEmployee;
                Log($"Employee with ID {employeeId} updated.");
                return Result<Employee>.Success($"Employee with ID {employeeId} updated.", updatedEmployee);
            }
            else
            {
                return Result<Employee>.Failure("Failed to read updated employee information.");
            }
        }
    }
    public Result<Employee> AddSkillToEmployee()
    {
        Console.WriteLine("Enter the Employee ID: ");
        int employeeId = 0;
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            employeeId = id;
        }
        else 
            return Result<Employee>.Failure("Invalid Employee ID.");
        Console.WriteLine("Enter the skill ");
        
        string skill = Console.ReadLine()!;
        var employee = new Employee();
        int indx = -1;
        for (int i = 0; i < _employees.Count; i++)
        {
            if (_employees[i].HasId(employeeId))
            {
                employee = _employees[i];
                indx = i;
                break;
            }
        }
        if (employee == null)
        {
            return Result<Employee>.Failure($"No employee found with ID {employeeId}.");
        }
        else
        {
            bool skillExists = false;
            foreach (var sk in _companySkills)
            {
                if (skill == sk)
                {
                    skill = sk;
                    skillExists = true;
                    break;
                }
            }
            if (employee.AddSkill(skill) && skillExists)
            {
                Log($"Skill '{skill}' added to employee with ID {employeeId}.");
                return Result<Employee>.Success($"Skill '{skill}' added to employee with ID {employeeId}.", employee);
            }
            else if(!skillExists)
            {
                return Result<Employee>.Failure($"Skill '{skill}' does not exist in the company.");
            }
            else
            {
                return Result<Employee>.Failure($"Employee with ID {employeeId} already has the skill '{skill}'.");
            }
        }
    }
    public Result<Employee> RemoveSkillFromEmployee()
    {
        Console.WriteLine("Enter the Employee ID: ");
        int employeeId = 0;
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            employeeId = id;
        }
        else
            return Result<Employee>.Failure("Invalid Employee ID.");
        Console.WriteLine("Enter the skill ");

        string skill = Console.ReadLine()!;
        var employee = new Employee();
        int indx = -1;
        for (int i = 0; i < _employees.Count; i++)
        {
            if (_employees[i].HasId(employeeId))
            {
                employee = _employees[i];
                indx = i;
                break;
            }
        }
        if (employee == null)
        {
            return Result<Employee>.Failure($"No employee found with ID {employeeId}.");
        }
        else
        {
            bool skillExists = false;
            foreach (var sk in _companySkills)
            {
                if (skill == sk)
                {
                    skill = sk;
                    skillExists = true;
                    break;
                }
            }
            if (!skillExists)
            {
                return Result<Employee>.Failure($"Skill '{skill}' does not exist in the company.");
            }
            else if (employee.RemoveSkill(skill) && skillExists)
            {
                Log($"Skill '{skill}' removed from employee with ID {employeeId}.");
                return Result<Employee>.Success($"Skill '{skill}' removed from employee with ID {employeeId}.", employee);
            }
            else
            {
                return Result<Employee>.Failure($"Employee with ID {employeeId} does not have the skill '{skill}'.");
            }
        }
    }
    public Result<Employee> ShowEmployeeDetails()
    {
        Console.WriteLine("Enter the Employee ID: ");
        int employeeId = 0;
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            employeeId = id;
        }
        else
            return Result<Employee>.Failure("Invalid Employee ID.");
        var employee = new Employee();
        for (int i = 0; i < _employees.Count; i++)
        {
            if (_employees[i].HasId(employeeId))
            {
                employee = _employees[i];
                break;
            }
        }
        if (employee == null)
        {
            return Result<Employee>.Failure($"No employee found with ID {employeeId}.");
        }
        else
        {
            return Result<Employee>.Success($"Employee with ID {employeeId} details displayed.", employee);
        }
    }
    public Result<Employee> AddMemeberToManager()
    {
        Console.WriteLine("Enter the Employee ID: ");
        int employeeId = 0;
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            employeeId = id;
        }
        else
            return Result<Employee>.Failure("Invalid Employee ID.");
        Console.WriteLine("Enter the Manager ID: ");
        int managerId = 0;
        if (int.TryParse(Console.ReadLine(), out int mid))
        {
            managerId = mid;
        }
        else
            return Result<Employee>.Failure("Invalid Employee ID.");
        var manager = new Manager();
        var employee = new Employee();
        int managerIndx = -1;
        int employeeIndx = -1;
        for (int i = 0; i < _employees.Count; i++)
        {
            if (_employees[i].HasId(managerId) && _employees[i] is Manager)
            {
                manager = (Manager)_employees[i];
                managerIndx = i;
            }
            if (_employees[i].HasId(employeeId))
            {
                employee = _employees[i];
                employeeIndx = i;
            }
        }
        if (managerIndx == -1)
        {
            return Result<Employee>.Failure($"No manager found with ID {managerId}.");
        }
        if (employeeIndx == -1)
        {
            return Result<Employee>.Failure($"No employee found with ID {employeeId}.");
        }
        manager.AddTeamMember(employee);
        Log($"Employee with ID {employeeId} added to manager with ID {managerId}.");
        return Result<Employee>.Success($"Employee with ID {employeeId} added to manager with ID {managerId}.", employee);
    }
    public Result<List<Employee>> GetAllEmployees()
    {
        if(_employees.Count == 0)
        {
            return Result<List<Employee>>.Failure("No employees found in the company.");
        }
        else 
            return Result<List<Employee>>.Success("All employees retrieved.", _employees);
    }
    // Department Management
    public Result<Department> AddDepartment()
    {
        var department = new Department();
        if (department.Read())
        {
            if (_departments.ContainsKey(department.Id))
            {
                return Result<Department>.Failure($"Department with ID {department.Id} already exists.");
            }
            else
            {
                _departments.Add(department.Id, department);
                Log($"Department with ID {department.Id} added.");
                return Result<Department>.Success($"Department with ID {department.Id} added.", department);
            }
        }
        else
        {
            return Result<Department>.Failure("Failed to read department information.");
        }
    }
    public Result<Department> GetDepartmentById()
    {
        Console.WriteLine("Enter the Department ID: ");
        int departmentId = 0;
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            departmentId = id;
        }
        else
            return Result<Department>.Failure("Invalid Department ID.");
        var department = new Department();
        foreach (var dept in _departments.Values)
        {
            if (dept.Id == departmentId)
            {
                department = dept;
                break;
            }
        }
        if (department.Id == 0)
        {
            return Result<Department>.Failure($"No department found with ID {departmentId}.");
        }
        else
        {
            return Result<Department>.Success($"Department with ID {departmentId} found.", department);
        }
    } 
    public Result<Department> RemoveDepartment()
    {
        Console.WriteLine("Enter the Department ID: ");
        int departmentId = 0;
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            departmentId = id;
        }
        else
            return Result<Department>.Failure("Invalid Department ID.");
        if (_departments.ContainsKey(departmentId))
        {
            var department = _departments[departmentId];
            _departments.Remove(departmentId);
            Log($"Department with ID {departmentId} removed.");
            return Result<Department>.Success($"Department with ID {departmentId} removed.", department);
        }
        else
        {
            return Result<Department>.Failure($"No department found with ID {departmentId}.");
        }
    }
    public Result<List<Department>> GetAllDepartments()
    {
        if (_departments.Count == 0)
        {
            return Result<List<Department>>.Failure("No departments found in the company.");
        }
        else
        {
            var departmentList = new List<Department>();
            foreach (var dept in _departments.Values)
            {
                departmentList.Add(dept);
            }
            return Result<List<Department>>.Success("All departments retrieved.", departmentList);
        }
    }
    public Result<Dictionary<int,int>> GetDepartmentEmployeeCount()
    {
        var departmentEmployeeCount = new Dictionary<int, int>();
        foreach (var employee in _employees)
        {
            if (departmentEmployeeCount.ContainsKey(employee.DepartmentId))
            {
                departmentEmployeeCount[employee.DepartmentId]++;
            }
            else
            {
                departmentEmployeeCount[employee.DepartmentId] = 1;
            }
        }
        return Result<Dictionary<int,int>>.Success("Department employee counts retrieved.", departmentEmployeeCount);
    }
    // Skill Management
    public Result<string> AddSkill()
    {
        Console.WriteLine("Enter the skill to add: ");
        string skill = Console.ReadLine()!;
        if (_companySkills.Contains(skill))
        {
            return Result<string>.Failure($"Skill '{skill}' already exists in the company.");
        }
        else
        {
            _companySkills.Add(skill);
            Log($"Skill '{skill}' added to the company.");
            return Result<string>.Success($"Skill '{skill}' added to the company.", skill);
        }
    }
    public Result<string> RemoveSkill()
    {
        Console.WriteLine("Enter the skill to remove: ");
        string skill = Console.ReadLine()!;
        if (_companySkills.Contains(skill))
        {
            _companySkills.Remove(skill);
            Log($"Skill '{skill}' removed from the company.");
            return Result<string>.Success($"Skill '{skill}' removed from the company.", skill);
        }
        else
        {
            return Result<string>.Failure($"Skill '{skill}' does not exist in the company.");
        }
    }
    public Result<HashSet<string>> GetAllSkills()
    {
        if(_companySkills.Count == 0)
        {
            return Result<HashSet<string>>.Failure("No skills found in the company.");
        }
        else 
            return Result<HashSet<string>>.Success("All skills retrieved.", _companySkills);
    }
    // History Management
    public Result<List<string>> GetActionHistory()
    {
        if (_actionHistory.Count == 0)
        {
            return Result<List<string>>.Failure("No actions found in the history.");
        }
        else
        {
            var historyList = new List<string>();
            foreach (var item in _actionHistory)
            {
                historyList.Add(item);
            }
            historyList.Reverse(); // To show the most recent action first
            return Result<List<string>>.Success("Action history retrieved.", historyList);
        }
    }
    // Salary Management
    public Result<double> CalcAvarageSalary()
    {
        double totalSalary = 0;
        foreach (var employee in _employees)
        {
            totalSalary += employee.Salary;
        }
        double averageSalary = _employees.Count > 0 ? totalSalary / _employees.Count : 0;
        Log($"Total Average salary calculated: {averageSalary}");
        return Result<double>.Success($"Total Average salary calculated: {averageSalary}", averageSalary);
    }
    // Filtering Employees Managment 
    public List<Employee> FilterEmployees(FilterEmployee filter)
    {
        List<Employee> result = new List<Employee>();
        if (filter == null)
            return result;

        foreach (Employee employee in _employees)
        {
            if (filter(employee))
                result.Add(employee);
        }
        return result;
    }
    public int menu()
    {
        int choice = -1;
        while (choice == -1)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("    Employee Management System");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Add Employee ToOnboarding");
            Console.WriteLine("2. Process Next Employee");
            Console.WriteLine("3. Handle Search For Employee");
            Console.WriteLine("4. Promote Employee to Manager");
            Console.WriteLine("5. Demote Manager To Employee");
            Console.WriteLine("6. Remove Employee");
            Console.WriteLine("7. Update Employee");
            Console.WriteLine("8. Add Skill To Employee");
            Console.WriteLine("9. Remove Skill From Employee");
            Console.WriteLine("10. Show Employee Details");
            Console.WriteLine("11. Add Member To Manager");
            Console.WriteLine("12. Get All Employees");
            Console.WriteLine("13. Add Department");
            Console.WriteLine("14. Get Department By Id");
            Console.WriteLine("15. Remove Department");
            Console.WriteLine("16. Get All Departments");
            Console.WriteLine("17. Get Department Employee Count");
            Console.WriteLine("18. Add Skill");
            Console.WriteLine("19. Remove Skill");
            Console.WriteLine("20. Get All Skills");
            Console.WriteLine("21. Get Action History");
            Console.WriteLine("22. Get Average Salary");
            Console.WriteLine("23. Filter Employees");
            Console.WriteLine("24. Exit");
            Console.Write("Enter your menu choice [1 - 24]: ");
            string input = Console.ReadLine()!;

            if (int.TryParse(input, out choice))
            {
                if (choice < 1 || choice > 24)
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
