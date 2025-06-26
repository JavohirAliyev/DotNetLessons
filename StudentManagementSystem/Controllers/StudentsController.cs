using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services.Interfaces;

namespace StudentManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    private readonly IStudentService _studentService;

    [HttpGet("{id}")]
    public IResult GetStudentById(int id)
    {
        try
        {
            var student = _studentService.GetStudentById(id);
            return student == null
                ? Results.NotFound("Student not found")
                : Results.Ok(student);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    [HttpPut("/{id}")]
    public IResult UpdateStudent(int id, StudentDto studentDto)
    {
        try
        {
            var updated = _studentService.UpdateStudent(id, studentDto);
            return updated == null
                ? Results.NotFound("Student not found")
                : Results.Ok(updated);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public IResult CreateStudent(StudentDto studentDto)
    {
        try
        {
            var createdStudent = _studentService.CreateStudent(studentDto);
            return Results.Created($"/students/{createdStudent.Id}", createdStudent);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    [HttpPut("/{id}/")]
    public IActionResult MarkStudent(int id, string subject, double grade)
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
}