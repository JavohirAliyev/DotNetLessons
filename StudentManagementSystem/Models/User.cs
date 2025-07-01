namespace StudentManagementSystem.Models;
using System.Text;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Role { get; set; } = "student";

    public bool ValidatePassword(string password)
    {
        return HashPassword(password) == PasswordHash;
    }

    public static string HashPassword(string password)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
    }
}