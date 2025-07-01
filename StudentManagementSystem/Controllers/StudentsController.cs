using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services.Interfaces;

namespace StudentManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    public StudentsController(IStudentService studentService, IUserService userService, IAuthService authService)
    {
        _studentService = studentService;
    }

    private readonly IStudentService _studentService;

    [HttpGet]
    public IActionResult GetAllStudents([FromQuery] string? searchTerm)
    {
        try
        {
            var students = _studentService.FilterStudents(searchTerm!);
            return Ok(students);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentById(int id)
    {
        try
        {
            var student = _studentService.GetStudentById(id);
            return student == null
                ? NotFound("Student not found")
                : Ok(student);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult CreateStudent(StudentDto studentDto)
    {
        try
        {
            var createdStudent = _studentService.CreateStudent(studentDto);
            return Created($"/students/{createdStudent.Id}", createdStudent);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStudent(int id, StudentDto studentDto)
    {
        try
        {
            var updated = _studentService.UpdateStudent(id, studentDto);
            return updated == null
                ? NotFound("Student not found")
                : Ok(updated);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}")]
    public IActionResult MarkStudent(int id, [FromQuery] string subject, [FromQuery] double grade)
    {
        try
        {
            var student = _studentService.MarkStudent(id, subject, grade);
            return Ok(student);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        try
        {
            var isDeleted = _studentService.DeleteStudent(id);
            return isDeleted ? NoContent() : NotFound($"No student was found with id {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}