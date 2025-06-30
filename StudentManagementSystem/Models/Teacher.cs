namespace StudentManagementSystem.Models;

public class Teacher
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; } = string.Empty;
    public required string Department { get; set; }
    public int YearsOfExperience { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime LastLogin { get; set; }
    public int LoginAttempts { get; set; } = 0;
    public bool IsLocked { get; set; } = false;
    public List<string> SubjectsTaught { get; set; } = [];
    public required string PasswordHash { get; set; }
}
