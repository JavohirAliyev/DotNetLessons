using System.Text.Json;
using System.Security.Cryptography;
using System.Text;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services.Interfaces;

namespace StudentManagementSystem.Services;

public class TeacherService : ITeacherService
{
    private readonly StudentManagementDbContext _dbContext;

    public TeacherService(StudentManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Teacher CreateTeacher(TeacherDto teacherDto)
    {
        if (_dbContext.Teachers.Any(t => t.Email == teacherDto.Email))
            throw new Exception("Email already in use.");

        var teacher = new Teacher
        {
            Email = teacherDto.Email,
            Gender = teacherDto.Gender,
            DateOfBirth = teacherDto.DateOfBirth,
            Address = teacherDto.Address,
            Department = teacherDto.Department,
            YearsOfExperience = teacherDto.YearsOfExperience,
            SubjectsTaught = teacherDto.SubjectsTaught,
            LastLogin = DateTime.Now,
            IsLocked = false,
            LoginAttempts = 0,
            PasswordHash = HashPassword(teacherDto.Password)
        };
        _dbContext.Teachers.Add(teacher);
        _dbContext.SaveChanges();

        return teacher;
    }

    public Teacher? Login(LoginDto loginDto)
    {
        var teacher = _dbContext.Teachers.FirstOrDefault(t => t.Email == loginDto.Email);

        if (teacher == null || teacher.IsLocked)
            return null;

        if (VerifyPassword(loginDto.Password, teacher.PasswordHash))
        {
            teacher.LoginAttempts = 0;
            teacher.LastLogin = DateTime.Now;
        }
        else
        {
            teacher.LoginAttempts++;
            if (teacher.LoginAttempts >= 5)
            {
                teacher.IsLocked = true;
            }
            _dbContext.SaveChanges();
            return null;
        }

        _dbContext.SaveChanges();
        return teacher;
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private bool VerifyPassword(string password, string hash)
    {
        return HashPassword(password) == hash;
    }

    public List<Teacher> GetAllTeachers()
    {
        return _dbContext.Teachers.ToList();
    }

    // private void SaveTeachers(List<Teacher> teachers)
    // {
    //     var json = JsonSerializer.Serialize(teachers, new JsonSerializerOptions { WriteIndented = true });
    //     File.WriteAllText(_filePath, json);
    // }
}