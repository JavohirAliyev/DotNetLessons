using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.Controllers;

[Route("api/{Controller}")]
public class StudentsController : Controller
{
    readonly StudentService studentService = new();
    public IResult GetStudentById(int id)
    {
        try
        {
            var student = studentService.GetStudentById(id);
            return student == null ? Results.NotFound("Student not found")
            : Results.Ok(student);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    public IResult GetAllStudents()
    {
        try
        {
            var students = studentService.GetAllStudents();
            return students == null ? Results.NoContent()
            : Results.Ok(students);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}
