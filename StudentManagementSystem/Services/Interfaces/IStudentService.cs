using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services.Interfaces;

public interface IStudentService
{
    Student CreateStudent(string firstName, string lastName,  List<Student> listOfStudents);
    Student GetStudentById(int Id);
    void GetAllStudents(List<Student> listOfStudents);
    Student MarkStudent(int id, string subject, double grade, List<Student> listOfStudents);
    void SaveHistory(List<Student> listOfStudents);
}