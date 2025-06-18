using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services.Interfaces;

public interface IStudentService
{
    Student CreateStudent(string firstName, string lastName, int id);
    Student GetStudentById(int Id);
    List<Student> GetAllStudents();
    Student MarkStudent(int id, string subject, double grade, List<Student> students);
    void SaveStudentsList(List<Student> students);
}