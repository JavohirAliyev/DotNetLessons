namespace StudentManagementSystem.Models;
 class Lesson
 {
    public string Subject { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public byte RoomNumber { get; set; }
 }