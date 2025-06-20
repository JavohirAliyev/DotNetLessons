using System.Text.Json;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services.Interfaces;

namespace StudentManagementSystem.Services;

public class StudentService : IStudentService
{
    private readonly string _filePath = "studentsList.json";
    public Student CreateStudent(string firstName, string lastName)
    {
        var students = GetAllStudents();
        int id = students.Count > 0 ? students.Last().Id + 1 : 1;
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

        students.Add(student);
        string json = JsonSerializer.Serialize(students, options);

        File.WriteAllText(_filePath, json);

        return student;
    }

    public List<Student> GetAllStudents()
    {
        if (File.Exists(_filePath))
        {
            string jsonString = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return [];
            }
            try
            {
                List<Student> studentsList = JsonSerializer.Deserialize<List<Student>>(jsonString) ?? [];
                return studentsList;
            }
            catch (JsonException)
            {
                return [];
            }
        }
        return [];
    }

    public Student GetStudentById(int Id)
    {
        throw new NotImplementedException();
    }

    public Student MarkStudent(int id, string subject, double grade, List<Student> students)
    {
        if (string.IsNullOrWhiteSpace(subject) || grade < 0 || grade > 100)
        {
            throw new ArgumentException("Invalid subject or grade.");
        }

        var student = students.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            throw new KeyNotFoundException($"Student with ID {id} not found.");
        }

        student.Grades[subject] = grade;
        SaveStudentsList(students);
        return student;
    }

    public void DeleteStudent(int Id)
    {
        if (_filePath == null || !File.Exists(_filePath))
        {
            Console.WriteLine("There is no student.");
            return;
        }

        string json = File.ReadAllText(_filePath);
        List<Student>? students = JsonSerializer.Deserialize<List<Student>>(json);

        if (students == null || students.Count == 0)
        {
            Console.WriteLine("No students found.");
            return;
        }

        var student = students.FirstOrDefault(s => s.Id == Id);
        if (student != null)
        {
            students.Remove(student);
            Console.WriteLine($"{Id} removed");
            SaveStudentsList(students);
        }
        else
        {
            Console.WriteLine("There is no student with this Id");
        }
    }


    public void SaveStudentsList(List<Student> students)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        string json = JsonSerializer.Serialize(students, options);

        File.WriteAllText(_filePath, json);
    }

}