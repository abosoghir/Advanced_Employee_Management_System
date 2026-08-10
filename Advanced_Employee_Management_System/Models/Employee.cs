using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Employee_Management_System.Models;
public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } 

    public DateTime HireDate { get; set; }

    public int DepartmentId { get; set; }

    public double Salary { get; set; }

    public List<string> Skills { get; set; }
    public Employee()
    {
        Id = 0;
        Name = string.Empty;
        HireDate = DateTime.Now;
        DepartmentId = 0;
        Salary = 0.0;
        Skills = new List<string>();
    }


    public override string ToString()
    {
        return $"ID : {Id}\nName: {Name}\nDepartment : {DepartmentId}\nHire Date  : {HireDate:d}\nSalary     : {Salary}\nSkills     : {(Skills.Count == 0 ? "No Skills" : string.Join(", ", Skills))}";
    }                 
    public bool AddSkill(string skill)
    {
        if (HasSkill(skill))
        {
            return false; // Skill already exists
        }

        else
        {
            Skills.Add(skill);
            return true;
        }
    }
    public bool RemoveSkill(string skill)
    {
        if(HasSkill(skill))
        {
            Skills.Remove(skill);
            return true; // Skill removed successfully
        } 

        else     
            return false; // Skill not found
    }
    public bool HasSkill(string skill)
    {
        foreach (var existingSkill in Skills)
        {
            if (existingSkill == skill)
            {
                return true; // Skill exists
            }
        }
        return false;
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
    public bool HasName(string name)
    {
        if(Name == name)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool HasId(int id)
    {
        if(Id == id)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
