namespace StudentManagementSystem.Models;

public class StudentIdAttendance
{
    public string? Date { get; set; }
    public Dictionary<string, string> Subjects { get; set; } = [];
}