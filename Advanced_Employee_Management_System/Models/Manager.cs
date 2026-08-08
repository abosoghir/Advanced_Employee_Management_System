using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Employee_Management_System.Models;
public class Manager : Employee
{
    public List<Employee> TeamMembers { get; set; } = [];

    

    public void AddTeamMember(Employee employee)
    {
        TeamMembers.Add(employee);
    }

    public override void Display()
    {
        base.Display();

        Console.WriteLine($"Team Members : {TeamMembers.Count}");
    }
    public void DisplayTeamMembers()
    {
        Console.WriteLine("Team Members:");
        if (TeamMembers.Count == 0)
        {
            Console.WriteLine("No Team Members");
        }
        else
        {
            foreach (var member in TeamMembers)
            {
                member.Display();
            }
        }
    }
    public void RemoveTeamMember(int employeeId)
    {
        var memberToRemove = TeamMembers.FirstOrDefault(m => m.Id == employeeId);
        if (memberToRemove != null)
        {
            TeamMembers.Remove(memberToRemove);
            Console.WriteLine($"Employee with ID {employeeId} removed from the team.");
        }
        else
        {
            Console.WriteLine($"Employee with ID {employeeId} not found in the team.");
        }
    }

}
