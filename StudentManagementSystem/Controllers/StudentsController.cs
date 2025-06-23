using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.Controllers;

[Route("api/[controller]")]
class StudentsController : Controller
{
    readonly StudentService studentService = new();

    [HttpGet("{id}")]
    public IResult GetStudentById(int id)
    {
        try
        {
            var student = studentService.GetStudentById(id);
            return student == null
                ? Results.NotFound("Student not found")
                : Results.Ok(student);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public IResult CreateStudent([FromBody] StudentDto studentDto)
    {
        try
        {
            if (studentDto == null)
                return Results.BadRequest("Student data is required.");
            var created = studentService.CreateStudent(studentDto);
            return Results.Created($"/students/{created.Id}", created);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}