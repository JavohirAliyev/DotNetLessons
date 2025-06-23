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
        var students = GetAllStudents();
        var student = students.FirstOrDefault(s => s.Id == id);
        if (student == null)
            return false;

        students.Remove(student);
        SaveStudentsList(students);
        return true;
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
        public bool MarkStudent(int studentId, string subject, double grade)
    {
        var student = GetStudentById(studentId);
        if (student == null)
            return false;

        student.Grades[subject] = grade;
        var students = GetAllStudents();
        var index = students.FindIndex(s => s.Id == studentId);
        if (index != -1)
        {
            students[index] = student;
            SaveStudentsList(students);
            return true;
        }
        return false;
    }

    public Dictionary<string, double>? GetGrades(int studentId)
    {
        var student = GetStudentById(studentId);
        return student?.Grades;
    }

}