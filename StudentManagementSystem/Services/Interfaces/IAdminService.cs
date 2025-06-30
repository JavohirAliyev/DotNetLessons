using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services.Interfaces;

public interface IAdminService
{
    Admin? GetByUsername(string username);
    Admin Register(Admin admin, string password);
    bool ValidatePassword(Admin admin, string password);
}
