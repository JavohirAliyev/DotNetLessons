using Microsoft.AspNetCore.Mvc;
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
    public IResult CreateStudent([FromBody] StudentDto student)
    {
        if (student == null)
            return Results.BadRequest("Student data is required.");

        try
        {
            var created = studentService.CreateStudent(student);
            return Results.Created($"/students/{created.Id}", created);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}