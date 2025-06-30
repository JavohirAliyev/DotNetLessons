using System.Text.Json;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services.Interfaces;
using StudentManagementSystem.Utils;

namespace StudentManagementSystem.Services;

public class StudentService(StudentManagementDbContext dbContext) : IStudentService
{
    private readonly StudentManagementDbContext _dbContext = dbContext;

    public Student CreateStudent(StudentDto studentDto)
    {
        Student student = new()
        {
            FirstName = studentDto.FirstName,
            LastName = studentDto.LastName
        };

        _dbContext.Students.Add(student);
        _dbContext.SaveChanges();
        return student;
    }

    public List<Student> GetAllStudents()
    {
        try
        {
            return _dbContext.Students.ToList();
        }
        catch
        {
            return [];
        }
    }

    public List<Student> FilterStudents(string? searchTerm)
    {
        var students = GetAllStudents();
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return students;
        }

        var validSearchTerm = searchTerm.RemoveDoubleSpaces();

        var result = students
            .Where(s => s.FirstName.Contains(validSearchTerm, StringComparison.OrdinalIgnoreCase)
            || s.LastName.Contains(validSearchTerm, StringComparison.OrdinalIgnoreCase));

        return result.ToList();
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
        _dbContext.SaveChanges();
        return student;
    }

    public bool DeleteStudent(int id)
    {
        var students = GetAllStudents();
        var student = students.FirstOrDefault(s => s.Id == id);
        if (student == null)
            return false;

        students.Remove(student);
        _dbContext.SaveChanges();
        return true;
    }

    public Student? MarkStudent(int id, string subject, double grade)
    {
        if (string.IsNullOrWhiteSpace(subject.Trim()) || grade < 0 || grade > 100)
        {
            throw new Exception("Invalid subject or grade.");
        }

        var students = GetAllStudents();
        var student = students.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return null;
        }

        var newGrade = new Grade
        {
            Subject = subject,
            Student = student,
            Value = grade
        };

        _dbContext.SaveChanges();
        return student;
    }
}