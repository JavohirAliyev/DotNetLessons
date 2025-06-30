namespace StudentManagementSystem.Models;

public class Student
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public Dictionary<string, double> Grades { get; set; } = [];
    public List<Attendance> Attendances { get; set; } = [];
}