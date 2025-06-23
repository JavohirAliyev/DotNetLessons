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
    [HttpDelete("{id}")]
    public Task DeleteStudent(int id)
    {
        try
        {
            var students = GetAllStudents();
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return false;

            students.Remove(student);
            SaveStudentsList(students);
            return true;
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}