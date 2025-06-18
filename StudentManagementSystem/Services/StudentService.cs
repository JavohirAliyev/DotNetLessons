using System.Text.Json;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services.Interfaces;

namespace StudentManagementSystem.Services;

public class StudentService : IStudentService
{
    public Student CreateStudent(string firstName, string lastName, int id)
    {
        Student student = new()
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName
        };

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        string json = JsonSerializer.Serialize(student, options);

        File.WriteAllText("studentsList.json", json);

        return student;
    }

    public List<Student> GetAllStudents()
    {
        if (File.Exists("studentsList.json"))
        {
            string jsonString = File.ReadAllText("studentsList.json");

            var studentsList = JsonSerializer.Deserialize<List<Student>>(jsonString)!;

            return studentsList;
        }
        return [];
    }

    public Student GetStudentById(int Id)
    {
        throw new NotImplementedException();
    }

    public Student MarkStudent(int id, string subject, double grade, List<Student> students)
    {
        foreach (var student in students)
        {
            if (student.Id == id)
            {
                student.Grades.Add(subject, grade);
                return student;
            }
        }
        return null;
    }
    
    public void SaveStudentsList(List<Student> students)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        string json = JsonSerializer.Serialize(students, options);

        File.WriteAllText("studentsList.json", json);
    }

}