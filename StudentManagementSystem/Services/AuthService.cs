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

    public User Register(UserRegisterDto dto)
    {
        var user = new User
        {
            Username = dto.Username,
            PasswordHash = User.HashPassword(dto.Password),
            Role = dto.Role
        };

        return _userService.Register(user);
    }

    public string Login(UserLoginDto dto)
    {
        var user = _userService.GetByUsername(dto.Username);
        if (user == null || !user.ValidatePassword(dto.Password))
            throw new Exception("Invalid username or password.");

        return $"fake-jwt-token-for-{user.Username}";
    }

    public User? Authenticate(string username, string password)
    {
        throw new NotImplementedException();
    }
}
