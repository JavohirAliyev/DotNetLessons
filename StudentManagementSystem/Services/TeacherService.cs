using System.Text.Json;
using System.Security.Cryptography;
using System.Text;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services.Interfaces;

namespace StudentManagementSystem.Services;

public class TeacherService : ITeacherService
{
    private readonly string _filePath = "teachersList.json";

    public Teacher CreateTeacher(TeacherDto teacherDto)
    {
        var teachers = GetAllTeachers();
        if (teachers.Any(t => t.Email == teacherDto.Email))
            throw new Exception("Email already in use.");

        int id = teachers.Count > 0 ? teachers.Last().Id + 1 : 1;
        var teacher = new Teacher
        {
            Id = id,
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

        teachers.Add(teacher);
        SaveTeachers(teachers);
        return teacher;
    }

    public Teacher? Login(LoginDto loginDto)
    {
        var teachers = GetAllTeachers();
        var teacher = teachers.FirstOrDefault(t => t.Email == loginDto.Email);

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
            SaveTeachers(teachers);
            return null;
        }

        SaveTeachers(teachers);
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

    private List<Teacher> GetAllTeachers()
    {
        if (!File.Exists(_filePath)) return new List<Teacher>();
        string json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Teacher>>(json) ?? [];
    }

    private void SaveTeachers(List<Teacher> teachers)
    {
        var json = JsonSerializer.Serialize(teachers, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}