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

    public IResult UpdateStudent(int id, StudentDto student)
    {
        try
        {
            var updated = studentService.UpdateStudent(id);
            return updated == null
                ? Results.NotFound("Student not found")
                : Results.Ok(updated);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}