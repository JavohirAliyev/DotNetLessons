using StudentManagementSystem.Services;
using StudentManagementSystem.Utils;

StudentService studentService = new();

Console.WriteLine("WELCOME TO STUDENT MANAGEMENT SYSTEM!");

Console.WriteLine("1. Students");
Console.WriteLine("2. Mark a student");
Console.WriteLine("3. Create a student");
Console.WriteLine("4. Clear");
Console.WriteLine("5. Exit");

string input = Console.ReadLine() ?? "";
string validInput = input.Trim().ToLower();

switch (validInput)
{
    case "1":
    case "students":
        var students = studentService.GetAllStudents();
        foreach (var student in students)
        {
            Console.WriteLine($"{student.Id}: {student.FirstName} {student.LastName}");
        }
        break;

    case "2":
    case "mark":
        break;

    case "3":
    case "create":
        Console.Write("Input the first name: ");
        string name = Console.ReadLine()?.Trim().Capitalize() ?? "";
        Console.Write("Input the last name: ");
        string surname = Console.ReadLine()?.Trim().Capitalize() ?? "";

        studentService.CreateStudent(name, surname);
        break;

    case "4":
    case "clear":
        break;

    case "5":
    case "exit":
    case "quit":
    case "kill":
        break;
}