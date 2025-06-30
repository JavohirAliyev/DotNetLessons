using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services.Interfaces;

public interface IAuthService
{
    string GenerateToken(Admin admin);

    Admin Register(UserRegisterDto userDto);
    Admin Login(UserLoginDto userDto);
}
