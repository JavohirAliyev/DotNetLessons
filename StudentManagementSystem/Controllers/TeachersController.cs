using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services.Interfaces;

namespace StudentManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeachersController(ITeacherService teacherService) : ControllerBase
{
    private readonly ITeacherService _teacherService = teacherService;

    [HttpPost("register")]
    public IActionResult Register([FromBody] TeacherDto teacherDto)
    {
        try
        {
            var teacher = _teacherService.CreateTeacher(teacherDto);
            return Created($"/api/teachers/{teacher.Id}", teacher);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var teacher = _teacherService.Login(loginDto);
            return teacher == null
                ? Unauthorized("Invalid credentials or account locked.")
                : Ok(teacher);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); 
        }
    }
}