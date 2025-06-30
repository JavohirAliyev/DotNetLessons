using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services.Interfaces;

public interface IUserService
{
    User Register(User user);
    User? GetByUsername(string username);
    bool ChangeUserRole(int userId, string newRole);
    bool DeleteUser(int userId);
    List<User> GetAllUsers();
}