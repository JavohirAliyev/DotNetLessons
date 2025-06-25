using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
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

    [HttpPost]
    public IResult CreateStudent(StudentDto studentDto)
    {
        try
        {
            var createdStudent = studentService.CreateStudent(studentDto);
            return Results.Created($"/students/{createdStudent.Id}", createdStudent);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}