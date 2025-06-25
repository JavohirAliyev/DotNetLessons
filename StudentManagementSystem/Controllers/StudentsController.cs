using System.Linq.Expressions;
using System.Xml.Schema;
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
    public IResult CreateStudent(StudentDto studentDto)
    {
        try
        {
            if (studentDto == null)
            {
                return Results.BadRequest("Student data is required");
            }
            var createdStudent = studentService.CreateStudent(studentDto);
            return Results.Created($"/students/{createdStudent.Id}", createdStudent);
        }
        catch (Exception ex)
        {
            return Results.Problem("Unexpected error happened while the processing of creating student.");
        }
    }
    public IResult UpdateStudent(int id, StudentDto studentDto)
    {
        try
        {
            var updated = studentService.UpdateStudent(id, studentDto);
            return updated == null ? Results.NotFound($"Student with id {id} not found.")
            : Results.Ok(updated);
        }
        catch (Exception ex)
        {
            return Results.Problem("Unexpected error happened with updating student");
        }
    }
    public IResult DeleteStudent(int id)
    {
        try
        {
            var deleted = studentService.DeleteStudent(id);
            if (!deleted)
            {
                return Results.NotFound($"Student with id {id} not found");
            }
            return Results.Ok($"Student with id {id} is deleted");
        }
        catch (Exception ex)
        {
            return Results.Problem("Unexpected error hapeened while deleting student");
        }
    }
    public void SaveStudentsList(List<Student> students)
    {
        try
        {
            studentService.SaveStudentsList(students);
        }
        catch (Exception ex)
        {
            Results.Problem("Unexpected error happened with saving student");
        }
    }
      public IResult MarkStudent(int id, MarkDto markDto)
    {
        try
        {
            if (markDto == null)
            {
                return Results.BadRequest("Mark data is required");
            }
            var student = studentService.GetStudentById(id);
            if (student == null)
            {
                return Results.NotFound($"Student with id {id} not found");
            }
            if (student.Marks == null)
            {
                student.Marks = new List<MarkDto>();
            }
            var mark = new MarkDto
            {
                Id = student.Marks.Count + 1,
                Subject = markDto.Subject,
                Value = markDto.Value
            };
            student.Marks.Add(mark);
            studentService.SaveStudentsList(studentService.GetAllStudents());
            return Results.Ok(mark);
        }
        catch (Exception ex)
         {
            return Results.Problem("Unexpected error happened while adding the mark");
        }
    }
}        