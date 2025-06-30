using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services.Interfaces;

public interface IUserService
{
    Admin Register(UserRegisterDto userDto);
    Admin? GetByUsername(string username);
    bool CheckPassword(Admin user, string password);

    List<Admin> GetAllUsers();
    bool ChangeUserRole(int userId, string newRole);
    bool DeleteUser(int userId);
}
