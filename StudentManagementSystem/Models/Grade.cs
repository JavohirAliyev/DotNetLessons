namespace StudentManagementSystem.Models;

public class Grade
{
    public int Id { get; set; }
    public Student? Student { get; set; }
    public required string Subject { get; set; }
    public double Value { get; set; }
}