using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advanced_Employee_Management_System.Models;

namespace Advanced_Employee_Management_System.Events;

public delegate void EmployeeEvent(EmployeeEventArgs employeeEventArgs);
public class EmployeeEventArgs 
{
    public Employee Employee { get; }
    public string Message { get; }
    public EmployeeEventArgs(Employee employee, string message)
    {
        Employee = employee;
        Message = message;
    }
}