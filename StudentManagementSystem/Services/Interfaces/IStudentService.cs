using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services.Interfaces;

public interface IStudentService
{
    Student CreateStudent(StudentDto studentDto);
    Student? GetStudentById(int id);
    List<Student> GetAllStudents();
    Student? UpdateStudent(int id, StudentDto studentDto);
    bool DeleteStudent(int id);
    void SaveStudentsList(List<Student> students);
}