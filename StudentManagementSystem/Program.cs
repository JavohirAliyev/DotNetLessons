// using System.Text.RegularExpressions;
using StudentManagementSystem.Services;
// using StudentManagementSystem.Utils;

StudentService studentService = new();
// var students = studentService.GetAllStudents();
// Console.WriteLine("WELCOME TO STUDENT MANAGEMENT SYSTEM!");

// while (true)
// {
//     Console.WriteLine("1. Students");
//     Console.WriteLine("2. Mark a student");
//     Console.WriteLine("3. Create a student");
//     Console.WriteLine("4. Clear");
//     Console.WriteLine("5. Exit");

//     string input = Console.ReadLine() ?? "";
//     string validInput = input.Trim().ToLower();

//     switch (validInput)
//     {
//         case "1":
//         case "students":
//             if (students.Count > 0)
//             {
//                 foreach (var student in students)
//                 {
//                     if (student.Grades.Count > 0)
//                     {
//                         Console.WriteLine($"{student.Id}: {student.FirstName} {student.LastName} - Grades: {string.Join(", ", student.Grades.Select(g => $"{g.Key}: {g.Value}"))}");
//                     }
//                     else
//                     {
//                         Console.WriteLine($"{student.Id}: {student.FirstName} {student.LastName} - No grades recorded.");
//                     }
//                 }
//             }
//             else
//             {
//                 Console.WriteLine("There are not any students");
//             }

//             Console.WriteLine("Press any key to continue...");
//             Console.ReadKey(true);
//             break;

//         case "2":
//         case "mark":
//             if (students.Count > 0)
//             {
//                 Console.Write("Enter Id of the student: ");
//                 var id = int.Parse(Console.ReadLine() ?? "");
//                 Console.Write("Enter subject of the student: ");
//                 var subject = Console.ReadLine()?.Trim().Capitalize().RemoveDoubleSpaces() ?? "";
//                 Console.Write("Enter grade of the student: ");
//                 var grade = double.Parse(Console.ReadLine() ?? "");
//                 studentService.MarkStudent(id, subject, grade, students);
//             }
//             else
//             {
//                 Console.WriteLine("There are no students to mark");
//             }
//             break;

//         case "3":
//         case "create":
//             Console.Write("Input the first name: ");
//             string firstName = Console.ReadLine()?.Trim().Capitalize() ?? "";
//             Console.Write("Input the last name: ");
//             string lastName = Console.ReadLine()?.Trim().Capitalize() ?? "";

//             if (MyRegex().IsMatch(firstName) && Regex.IsMatch(lastName, @"^[a-zA-Z]+$"))
//             {
//                 students.Add(studentService.CreateStudent(firstName, lastName));
//                 Console.WriteLine($"Student {firstName} {lastName} created successfully.");
//             }
//             else
//             {
//                 Console.WriteLine("Invalid name or surname. Please use only letters.");
//             }

//             Console.WriteLine("Press any key to continue...");
//             Console.ReadKey();
//             break;

//         case "4":
//         case "clear":
//             Console.Clear();
//             break;

//         case "5":
//         case "exit":
//         case "quit":
//         case "kill":
//             Console.WriteLine("Exiting the program...");
//             return;
//         default:
//             Console.WriteLine("Invalid input, please try again.");
//             break;
//     }
//     studentService.SaveStudentsList(students);
// }
// partial class Program
// {
//     [GeneratedRegex(@"^[a-zA-Z]+$")]
//     private static partial Regex MyRegex();
// }

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Welcome to the Student Management System!");

app.MapGet("/students", () => studentService.GetAllStudents());

app.MapGet("/students/{id}", (int id) =>
{
    try
    {
        return Results.Ok(studentService.GetStudentById(id));
    }
    catch (Exception ex)
    {
        return Results.NotFound(ex.Message);
    }
});

app.Run();