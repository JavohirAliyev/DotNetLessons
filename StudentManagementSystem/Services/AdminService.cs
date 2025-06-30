using StudentManagementSystem.Models;
using StudentManagementSystem.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;
using StudentManagementSystem.Utils;

namespace StudentManagementSystem.Services;

public class AdminService : IAdminService
{
    private readonly List<Admin> _admins = [];

    public Admin Register(Admin admin, string password)
    {
        admin.PasswordHash = HashPassword(password);
        _admins.Add(admin);
        return admin;
    }

    public Admin? GetByUsername(string username)
    {
        return _admins.FirstOrDefault(a => a.Username == username);
    }

    public bool ValidatePassword(Admin admin, string password)
    {
        return admin.PasswordHash == HashPassword(password);
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}