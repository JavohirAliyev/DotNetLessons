namespace StudentManagementSystem.Models;

public class StudentDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}

public class MarkDto
{
    public string? Subject { get; set; }
    public double Grade { get; set; }
}