using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Employee_Management_System.Models;
public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } 

    public Department()
    {
        Id = 0;
        Name = string.Empty;
    }
    public void Display()
    {
        Console.WriteLine($"{Id} - {Name}");
    }
    public bool Read()
    {
        Console.Write("Enter department info: id & name");
        string[] input = Console.ReadLine()!.Split(' ');
        if (input.Length < 2)
        {
            return false;
        }
        else
        {
            Id = int.TryParse(input[0], out int id) ? id : 0;
            Name = input[1];
            if (id == 0)
            {
                return false;
            }
            return true;
        }
    }
}
