using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Employee_Management_System.Models;
public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime HireDate { get; set; }

    public int DepartmentId { get; set; }

    public double Salary { get; set; }

    public List<string> Skills { get; set; } = [];

  
    public virtual void Display()
    {
        Console.WriteLine("------------------------------------");
        Console.WriteLine($"ID         : {Id}");
        Console.WriteLine($"Name       : {Name}");
        Console.WriteLine($"Department : {DepartmentId}");
        Console.WriteLine($"Hire Date  : {HireDate:d}");
        Console.WriteLine($"Salary     : {Salary}");

        Console.Write("Skills : ");

        if (Skills.Count == 0)
        {
            Console.WriteLine("No Skills");
        }
        else
        {
            for (int i = 0; i < Skills.Count; i++)
            {
                Console.Write(Skills[i]);

                if (i != Skills.Count - 1)
                    Console.Write(", ");
            }

            Console.WriteLine();
        }
    }
    public bool AddSkill(string skill)
    {
        if (Skills.Contains(skill))
        {
            return false;
        }
        Skills.Add(skill);
        return true;
    }
    public bool RemoveSkill(string skill)
    {
        if (!Skills.Contains(skill))
        {
            return false;
        }
        Skills.Remove(skill);
        return true;
    }
    public bool HasSkill(string skill)
    {
        return Skills.Contains(skill);
    }
    public bool HasSkills()
    {
        return Skills.Count > 0;
    }
    public void ClearSkills()
    {
        Skills.Clear();
    }
    public void UpdateSalary(double newSalary)
    {
        Salary = newSalary;
    }
    public void UpdateDepartment(int newDepartmentId)
    {
        DepartmentId = newDepartmentId;
    }
    public bool Read()
    {
        Console.Write("Enter Employee info: id & name & departmentId & salary ");
        string[] input = Console.ReadLine()!.Split(' ');
        if (input.Length < 4)
        {
            return false;
        }
        else
        {
            Id = int.TryParse(input[0], out int id) ? id : 0;
            Name = input[1];
            DepartmentId = int.TryParse(input[2], out int departmentId) ? departmentId : 0;
            Salary = double.TryParse(input[3], out double salary) ? salary : 0.0;
            if (id == 0 || departmentId == 0 || salary == 0.0)
            {
                return false;
            }
            return true;
        }
    }
}
