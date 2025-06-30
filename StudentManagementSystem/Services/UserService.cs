using System.Security.Cryptography;
using System.Text;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services.Interfaces;

namespace StudentManagementSystem.Services;

public class UserService : IUserService
{
    private readonly List<User> _users = new();
    private int _nextId = 1;

    public User Register(User user)
    {
        user.Id = _nextId++;
        _users.Add(user);
        return user;
    }

    public User? GetByUsername(string username)
    {
        return _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }

    public bool ChangeUserRole(int userId, string newRole)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
            return false;

        user.Role = newRole;
        return true;
    }

    public bool DeleteUser(int userId)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
            return false;

        _users.Remove(user);
        return true;
    }

    public List<User> GetAllUsers()
    {
        return _users;
    }
}