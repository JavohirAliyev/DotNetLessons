using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services.Interfaces;

public interface IAuthService
{
    User Register(UserRegisterDto userRegisterDto);
    User? Authenticate(string username, string password);
}
