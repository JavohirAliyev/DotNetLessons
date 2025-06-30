namespace StudentManagementSystem.Models;

public class TeacherDto
{
    public required string Email { get; set; }
    public required string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; } = string.Empty;
    public required string Department { get; set; }
    public int YearsOfExperience { get; set; }
    public List<string> SubjectsTaught { get; set; } = [];
    public required string Password { get; set; }
}
