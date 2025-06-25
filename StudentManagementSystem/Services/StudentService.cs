using System.Text.Json;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services.Interfaces;

namespace StudentManagementSystem.Services;

public class StudentService : IStudentService
{
    private readonly string _filePath = "studentsList.json";

    public Student CreateStudent(StudentDto studentDto)
    {
        var students = GetAllStudents();
        int id = students.Count > 0 ? students.Last().Id + 1 : 1;
        Student student = new()
        {
            Id = id,
            FirstName = studentDto.FirstName,
            LastName = studentDto.LastName
        };

        students.Add(student);
        SaveStudentsList(students);
        return student;
    }

    public List<Student> GetAllStudents()
    {
        if (File.Exists(_filePath))
        {
            string jsonString = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(jsonString))
                return [];
            try
            {
                return JsonSerializer.Deserialize<List<Student>>(jsonString) ?? [];
            }
            catch
            {
                return [];
            }
        }
        return [];
    }

    public Student? GetStudentById(int id)
    {
        var students = GetAllStudents();
        return students.FirstOrDefault(s => s.Id == id);
    }

    public Student? UpdateStudent(int id, StudentDto studentDto)
    {
        var students = GetAllStudents();
        var student = students.FirstOrDefault(s => s.Id == id);
        if (student == null)
            return null;

        student.FirstName = studentDto.FirstName;
        student.LastName = studentDto.LastName;
        SaveStudentsList(students);
        return student;
    }

    public bool DeleteStudent(int id)
    {
        var student = GetStudentById(id);
        if (student == null)
            return false;

        return RemoveStudent(student);
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

    public bool RemoveStudent(Student student)
    {
        var students = GetAllStudents();
        bool isRemoved = students.Remove(student);
        if (!isRemoved)
            return false;

        SaveStudentsList(students);
        return true;
    }
}