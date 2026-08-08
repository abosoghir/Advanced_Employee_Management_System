using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advanced_Employee_Management_System.Models;

namespace Advanced_Employee_Management_System.Services;
public class Company
{
    private readonly List<Employee> employees = [];
    private readonly Dictionary<int, Department> departments = [];
    private readonly Queue<Employee> onboardingQueue = [];
    private readonly Stack<string> actionHistory = [];
    private readonly HashSet<string> companySkills = [];

}
