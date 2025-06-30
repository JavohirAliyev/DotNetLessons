using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services.Interfaces;

namespace StudentManagementSystem.Controllers;

public interface IAuthController
{
    IActionResult Login(UserLoginDto dto);
    IActionResult Register(UserRegisterDto dto);
}

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase, IAuthController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register(UserRegisterDto dto)
    {
        try
        {
            var user = _authService.Register(dto);
            return Created($"/users/{user.Id}", user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public IActionResult Login(UserLoginDto dto)
    {
        try
        {
            var token = _authService.Login(dto);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}