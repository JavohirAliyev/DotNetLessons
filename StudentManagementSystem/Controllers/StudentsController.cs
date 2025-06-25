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

    [HttpPut("/{id}")]
    public IResult UpdateStudent(int id, StudentDto studentDto)
    {
        try
        {
            var updated = studentService.UpdateStudent(id, studentDto);
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

    [HttpGet("search")]
    public IResult SearchByName(string name)
    {
        try
        {
            var students = studentService.GetAllStudents();

            if (string.IsNullOrWhiteSpace(name))
                return Results.BadRequest("Search term is required.");

            var results = students
            .Where(s =>
                s.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                s.LastName.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (results == null || results.Count == 0)
            return Results.NotFound("Student not found");

        return Results.Ok(results);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}