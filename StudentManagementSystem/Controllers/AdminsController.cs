using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services.Interfaces;

namespace StudentManagementSystem.Controllers;

[ApiController]
[Route("api/admins")]
public class AdminsController : ControllerBase
{
    private readonly IUserService _userService;

    public AdminsController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("users")]
    public IActionResult GetAllUsers()
    {
        try
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("users/{id}/role")]
    public IActionResult ChangeUserRole(int id, [FromQuery] string role)
    {
        try
        {
            var result = _userService.ChangeUserRole(id, role);
            return result ? NoContent() : NotFound("User not found");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("users/{id}")]
    public IActionResult DeleteUser(int id)
    {
        try
        {
            var result = _userService.DeleteUser(id);
            return result ? NoContent() : NotFound("User not found");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
