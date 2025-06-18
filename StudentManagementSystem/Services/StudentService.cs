using System.Text.Json;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services.Interfaces;

namespace StudentManagementSystem.Services;

public class StudentService : IStudentService
{
    public Student CreateStudent(string firstName, string lastName, List<Student> listOfStudents)
    {
        Student student = new()
        {
            FirstName = firstName,
            LastName = lastName,
            Id = listOfStudents.Count == 0 ? 1 : listOfStudents.Last().Id + 1
        };

        listOfStudents.Add(student);

        SaveHistory(listOfStudents);

        Console.WriteLine("The new student is succesfully created!");
        return student;
    }

    public void GetAllStudents(List<Student> listOfStudents)
    {
        if (listOfStudents.Count == 0)
        {
            Console.WriteLine("There is no student in list");
        }
        else
        {
            foreach (var student in listOfStudents)
            {
                Console.WriteLine($"{student.Id}: {student.FirstName} {student.LastName}");
            }
        }
    }

    public Student GetStudentById(int Id)
    {
        return null; 
    }

    public Student MarkStudent(int id, string subject, double grade, List<Student> listOfStudents)
    {
        if (listOfStudents.Count == 0 || listOfStudents.Count < id)
        {
            Console.WriteLine("There is no student with this Id");
            return null;
        }
        else
        {
            listOfStudents[id - 1].Grades.Add(subject, grade);
            SaveHistory(listOfStudents);

            Console.WriteLine($"You are succesfully mark the student");
            return listOfStudents[id - 1];
        }
    }

     public void SaveHistory(List<Student> listOfStudents)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(listOfStudents, options);

        File.WriteAllText("listOfStudents.json", json);
    }
}