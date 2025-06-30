namespace StudentManagementSystem.Models;

public class Student
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public List<Grade> Grades { get; set; } = [];
}