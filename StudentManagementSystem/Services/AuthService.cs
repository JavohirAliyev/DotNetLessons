using StudentManagementSystem.Models;
using StudentManagementSystem.Services.Interfaces;

namespace StudentManagementSystem.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;

    public AuthService(IUserService userService)
    {
        _userService = userService;
    }

    public User? Authenticate(string username, string password)
    {
        var user = _userService.GetByUsername(username);
        if (user == null || !user.ValidatePassword(password))
        {
            return null;
        }

        return user;
    }

    public User Register(UserRegisterDto userRegisterDto)
    {
        var user = new User
        {
            Username = userRegisterDto.Username,
            PasswordHash = User.HashPassword(userRegisterDto.Password)
        };
        
        return _userService.Register(user);
    }
}