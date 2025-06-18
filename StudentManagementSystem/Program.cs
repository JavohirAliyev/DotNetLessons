using System.Text.RegularExpressions;
using StudentManagementSystem.Services;
using StudentManagementSystem.Utils;

StudentService studentService = new();
var students = studentService.GetAllStudents();

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
        if (students.Count() > 0)
        {
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Id}: {student.FirstName} {student.LastName}");
            }
        }
        else
            Console.WriteLine("There are not any students");
        break;

    case "2":
    case "mark":
        if (students.Count() > 0)
        {
            Console.Write("Enter Id of the student: ");
            var id = int.Parse(Console.ReadLine() ?? "");
            Console.Write("Enter subject of the student: ");
            var subject = Console.ReadLine()?.Trim().Capitalize().RemoveDoubleSpaces() ?? "";
            Console.Write("Enter grade of the student: ");
            var grade = int.Parse(Console.ReadLine() ?? "");
            studentService.MarkStudent(id, subject, grade, students);
        }
        else
            Console.WriteLine("There are not any students to mark");
        break;

    case "3":
    case "create":
        Console.Write("Input the first name: ");
        string firstName = Console.ReadLine()?.Trim().Capitalize() ?? "";
        Console.Write("Input the last name: ");
        string lastName = Console.ReadLine()?.Trim().Capitalize() ?? "";
        int id2 = students.Count == 0 ? 1 : students.Last().Id + 1;
        if (Regex.IsMatch(firstName, @"^[a-zA-Z]+$") && Regex.IsMatch(lastName, @"^[a-zA-Z]+$"))
            students.Add(studentService.CreateStudent(firstName, lastName, id2));
        else
            Console.WriteLine("Invalid name");
        break;

    case "4":
    case "clear":
        Console.Clear();
        break;

    case "5":
    case "exit":
    case "quit":
    case "kill":
        break;
}

studentService.SaveStudentsList(students);