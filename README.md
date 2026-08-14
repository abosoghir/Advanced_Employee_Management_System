# Advanced_Employee_Management_System
# Employee Management System

A C# Console Application built as a practice project to demonstrate **Collections, Generics, Delegates, Events, Inheritance, and Manual Data Processing**.

The project simulates a simple employee management system where employees can be added to an onboarding queue, activated, promoted to managers, assigned skills, searched, filtered, and included in department reports.

The main goal of this project is to practice core C# concepts without relying on advanced frameworks or architectural patterns.

---

## Features

* Add and manage departments.
* Add employees to an onboarding queue.
* Process employees using **FIFO Queue** behavior.
* Promote employees to managers.
* Register unique employee skills.
* Search employees by ID or name.
* Display employees belonging to a department.
* Filter employees using a custom delegate.
* Calculate the average salary.
* Generate employee count reports by department.
* Display action history using a Stack.
* Display unique skills using a HashSet.
* Notify subscribers when:

  * An employee is onboarded.
  * An employee is promoted.
* Handle expected operation failures using a custom `Result<T>`.
* Seed the application with initial test data.
* Interactive console menu with input validation.

---

## Technologies & Concepts

* **C#**
* **.NET Console Application**
* Object-Oriented Programming
* Classes and Objects
* Inheritance
* Generics
* Delegates
* Events
* Lambda Expressions
* Collections
* Manual loops
* Exception-free expected error handling using `Result<T>`

---

## Collections Used

The project intentionally uses five different C# collection types.

### List<Employee>

Stores all active employees.

```csharp
List<Employee>
```

Used for:

* Searching employees.
* Filtering employees.
* Calculating average salary.
* Generating department reports.

---

### Dictionary<int, Department>

Stores departments using their IDs as keys.

```csharp
Dictionary<int, Department>
```

Example:

```text
1 → IT
2 → HR
3 → Finance
```

This allows departments to be accessed directly by ID.

---

### Queue<Employee>

Stores employees waiting for onboarding.

```csharp
Queue<Employee>
```

The Queue follows:

```text
FIFO
First In → First Out
```

When an employee is added:

```csharp
onboardingQueue.Enqueue(employee);
```

When the next employee is processed:

```csharp
onboardingQueue.Dequeue();
```

---

### Stack<string>

Stores the action history.

```csharp
Stack<string>
```

The Stack follows:

```text
LIFO
Last In → First Out
```

Therefore, the newest action is displayed first.

---

### HashSet<string>

Stores unique skills.

```csharp
HashSet<string>
```

For example:

```text
C#
SQL
Python
Excel
```

Adding `"C#"` again does not create a duplicate.

---

# Generic Result<T>

The project uses a custom generic class:

```csharp
Result<T>
```

It contains:

```csharp
public bool Success { get; set; }

public string Message { get; set; }

public T Data { get; set; }
```

This allows different operations to return different types while using the same result structure.

Examples:

```csharp
Result<Employee>
```

```csharp
Result<Department>
```

```csharp
Result<Manager>
```

```csharp
Result<string>
```

Instead of throwing exceptions for expected situations such as an employee not being found, the operation returns a failure result:

```csharp
Result<Employee>.Fail("Employee not found.");
```

A successful operation can return:

```csharp
Result<Employee>.Ok(
    "Employee added successfully.",
    employee);
```

This demonstrates **generic type safety** and avoids using `object` for unrelated result types.

---

# Custom Delegate

The project defines a custom delegate:

```csharp
public delegate bool EmployeeFilter(Employee employee);
```

The delegate represents a method that:

1. Receives an `Employee`.
2. Returns a `bool`.

It is used by:

```csharp
FilterEmployees()
```

Example:

```csharp
company.FilterEmployees(
    employee => employee.Salary > 10000);
```

Another example:

```csharp
company.FilterEmployees(
    employee => employee is Manager);
```

This allows the same filtering method to work with many different conditions.

---

# Events

The project demonstrates the Publisher/Subscriber model.

`Company` acts as the **Publisher**.

Two events are provided:

```csharp
EmployeeOnboarded
```

and:

```csharp
EmployeePromoted
```

Subscribers can register using:

```csharp
company.EmployeeOnboarded += HandleEmployeeOnboarded;

company.EmployeePromoted += HandleEmployeePromoted;
```

When an employee is onboarded, the company raises:

```csharp
EmployeeOnboarded
```

When an employee is promoted, the company raises:

```csharp
EmployeePromoted
```

The events allow other parts of the application to react to employee lifecycle changes without the `Company` class needing to know exactly what those subscribers do.

---

# Project Structure

```text
EmployeeManagementSystem/
│
├── Models/
│   ├── Employee.cs
│   ├── Manager.cs
│   └── Department.cs
│
├── Common/
│   └── Result.cs
│
├── Delegates/
│   └── EmployeeFilter.cs
│
├── Events/
│   └── EmployeeEventArgs.cs
│
├── Services/
│   └── Company.cs
│
└── Program.cs
```

---

# Class Responsibilities

## Employee

The base employee class contains:

```text
Id
Name
HireDate
DepartmentId
Salary
```

---

## Manager

`Manager` inherits from `Employee`:

```csharp
Manager : Employee
```

It adds:

```csharp
List<Employee> TeamMembers
```

---

## Department

Represents a company department.

Properties:

```text
Id
Name
```

---

## Result<T>

A generic result wrapper containing:

```text
Success
Message
Data
```

---

## EmployeeFilter

A custom delegate used to filter employees dynamically.

---

## EmployeeEventArgs

Contains information about the employee associated with an event.

---

## Company

The main service class.

It owns the five collections and contains the application's main business logic.

It is also responsible for raising the employee lifecycle events.

---

# Application Menu

The application provides the following operations:

```text
1. Add Employee ToOnboarding
2. Process Next Employee
3. Handle Search For Employee
4. Promote Employee to Manager
5. Demote Manager To Employee
6. Remove Employee
7. Update Employee
8. Add Skill To Employee
9. Remove Skill From Employee
10. Show Employee Details
11. Add Member To Manager
12. Get All Employees
13. Add Department
14. Get Department By Id
15. Remove Department
16. Get All Departments
17. Get Department Employee Count
18. Add Skill
19. Remove Skill
20. Get All Skills
21. Get Action History
22. Get Average Salary
23. Filter Employees
24. Exit
```

---

# Sample Workflow

A typical employee lifecycle looks like this:

```text
Add Department
      ↓
Add Employee
      ↓
Employee enters Onboarding Queue
      ↓
Process Onboarding
      ↓
Employee becomes Active
      ↓
Register Skills
      ↓
Search / Filter Employee
      ↓
Promote Employee
      ↓
Employee becomes Manager
```

During this process, the application also records actions in the `Stack` and raises the appropriate events.

---

# Example Filtering

The application supports different filters through the `EmployeeFilter` delegate.

### Managers only

```csharp
employee => employee is Manager
```

### Salary above 10,000

```csharp
employee => employee.Salary > 10000
```

### Salary below 10,000

```csharp
employee => employee.Salary < 10000
```

No LINQ is required.

---

# Error Handling

Expected errors are handled using `Result<T>` instead of exceptions.

Examples:

```text
Employee with this ID already exists.
Department does not exist.
Employee not found.
Employee is already a manager.
Onboarding queue is empty.
Skill cannot be empty.
```

Each operation returns a clear success/failure message.

---

# Restrictions

This project intentionally does **not** use:

* LINQ
* Async/Await
* File Handling
* Database
* Entity Framework Core
* ASP.NET Core
* Dependency Injection
* Clean Architecture
* CQRS

The application stores all data in memory.

Therefore:

> **All data is lost when the application closes.**

This is intentional because the primary goal is practicing C# language features and collections rather than persistence.

---

# Learning Objectives

This project demonstrates practical usage of:

### Collections

Understanding when to use:

```text
List
Dictionary
Queue
Stack
HashSet
```

### Generics

Understanding:

```csharp
Result<T>
```

and how the same generic class can work with different data types.

### Delegates

Understanding how a method or lambda expression can be passed as an argument.

### Lambda Expressions

Examples:

```csharp
employee => employee.Salary > 10000
```

and:

```csharp
employee => employee is Manager
```

### Events

Understanding:

```text
Publisher
    ↓
Event
    ↓
Subscriber
```

and event subscription using:

```csharp
+=
```

and unsubscription using:

```csharp
-=
```

### Object-Oriented Programming

Understanding:

```text
Employee
   ↑
Manager
```

through inheritance and polymorphism.

---

# How to Run

1. Clone or download the project.
2. Open the project using Visual Studio or another C# IDE.
3. Build the project.
4. Run the Console Application.
5. Use the menu to test the different features.

For a .NET CLI project:

```bash
dotnet build
```

Then:

```bash
dotnet run
```

---

# Conclusion

The **Employee Management System** is a practice project designed to combine several fundamental C# concepts into one practical application.

The project demonstrates how different collections solve different problems, while **Generics** provide reusable type-safe results, **Delegates** provide flexible filtering, and **Events** provide a way to notify other parts of the application about employee lifecycle changes.

The project intentionally keeps the architecture simple so the focus remains on learning and applying core C# concepts.

