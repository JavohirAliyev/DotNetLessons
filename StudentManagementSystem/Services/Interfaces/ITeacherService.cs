using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services.Interfaces;

public interface ITeacherService
{
    Teacher CreateTeacher(TeacherDto teacherDto);
    Teacher? Login(LoginDto loginDto);
}