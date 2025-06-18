using StudentManagementSystem.Models;
using StudentManagementSystem.Services.Interfaces;

namespace StudentManagementSystem.Services;

public class StudentService : IStudentService
{
    public Student CreateStudent(string firstName, string lastName)
    {
        throw new NotImplementedException();
    }

    public List<Student> GetAllStudents()
    {
        throw new NotImplementedException();
    }

    public Student GetStudentById(int Id)
    {
        throw new NotImplementedException();
    }

    public Student MarkStudent(int id, string subject, double grade)
    {
        throw new NotImplementedException();
    }
}