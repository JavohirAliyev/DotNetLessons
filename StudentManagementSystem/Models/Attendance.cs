namespace StudentManagementSystem.Models;

public class Attendance
{
    public Student Student { get; set; }
    public bool IsPresent { get; set; }
    public Lesson Lesson { get; set; }
}