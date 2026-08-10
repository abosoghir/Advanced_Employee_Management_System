using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Employee_Management_System.Models;
public class Manager : Employee
{
    public List<Employee> TeamMembers { get; set; } = [];

    public Manager()
    {
        TeamMembers = new List<Employee>();
    }
    public void AddTeamMember(Employee employee)
    {
        TeamMembers.Add(employee);
    }

    public override string ToString()
    {
        string employeeInfo = base.ToString();

        return $"{employeeInfo}\nTeam Members : {TeamMembers.Count}";
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
                Console.WriteLine(member.ToString());
            }
        }
    }
    public bool RemoveTeamMember(int employeeId)
    {
        var memberToRemove = TeamMembers.FirstOrDefault(m => m.Id == employeeId);
        if (memberToRemove != null)
        {
            TeamMembers.Remove(memberToRemove);
            return true;
        }
        else
        {
            return false;
        }
    }

}
