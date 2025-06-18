using System.Transactions;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services;
using StudentManagementSystem.Utils;

StudentService studentService = new();
List<Student> listOfStudents = [];

Console.WriteLine("WELCOME TO STUDENT MANAGEMENT SYSTEM!");

Console.WriteLine("1. Students");
Console.WriteLine("2. Mark a student");
Console.WriteLine("3. Create a student");
Console.WriteLine("4. Clear");
Console.WriteLine("5. Exit");

while (true)
{
    Console.Write("Enter command : ");

    string input = Console.ReadLine() ?? "";
    string validInput = input.Trim().ToLower();

    switch (validInput)
    {
        case "1":
        case "students":
            studentService.GetAllStudents(listOfStudents);
            break;

        case "2":
        case "mark":
            Console.Write("Enter the Id of the student: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the Subject: ");
            string subject = Console.ReadLine()!.Trim().RemoveDoubleSpaces();
            Console.Write("Enter the grade : ");
            double grade = Convert.ToDouble(Console.ReadLine());

            studentService.MarkStudent(id, subject, grade, listOfStudents);
            break;

        case "3":
        case "create":
            Console.Write("Input the first name: ");
            string name = Console.ReadLine()?.Trim().Capitalize() ?? "";
            Console.Write("Input the last name: ");
            string surname = Console.ReadLine()?.Trim().Capitalize() ?? "";

            studentService.CreateStudent(name, surname, listOfStudents);
            break;

        case "4":
        case "clear":
            Console.Clear();
            Console.WriteLine("Console was cleared.");
            break;

        case "5":
        case "exit":
        case "quit":
        case "kill":
        Console.WriteLine("Good bye");
            return;
    }
}