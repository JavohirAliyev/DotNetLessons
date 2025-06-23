using StudentManagementSystem.Models;
using StudentManagementSystem.Services;

StudentService studentService = new();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Welcome to the Student Management System!");

/*app.MapGet("/students", () =>
{
    var students = studentService.GetAllStudents();
    return students == null || students.Count == 0
        ? Results.NoContent()
        : Results.Ok(students);
});*/

app.MapGet("/students/{id}", (int id) =>
{
    var student = studentService.GetStudentById(id);
    return student == null
        ? Results.NotFound("Student not found")
        : Results.Ok(student);
});

app.MapPost("/students", (StudentDto student) =>
{
    if (student == null)
        return Results.BadRequest("Student data is required.");
    var created = studentService.CreateStudent(student);
    return Results.Created($"/students/{created.Id}", created);
});

app.MapPut("/students/{id}", (int id, StudentDto student) =>
{
    var updated = studentService.UpdateStudent(id, student);
    return updated == null
        ? Results.NotFound("Student not found")
        : Results.Ok(updated);
});

app.MapDelete("/students/{id}", (int id) =>
{
    var deleted = studentService.DeleteStudent(id);
    return deleted
        ? Results.Ok("Student deleted")
        : Results.NotFound("Student not found");
});
app.MapGet("/students/{id}", (int id) =>
{
    try
    {
        var student = studentService.GetStudentById(id);
        if (student == null)
            return Results.NotFound($"Student with id {id} not found.");
        return Results.Ok(student);
    }
    catch
    {
        return Results.Problem("Unexpected error while getting student by id.");
    }
});

app.MapPost("/students", (StudentDto student) =>
{
    try
    {
        if (student == null)
            return Results.BadRequest("Student data is required.");
        var createdStudent = studentService.CreateStudent(student);
        return Results.Created($"/students/{createdStudent.Id}", createdStudent);
    }
    catch
    {
        return Results.Problem("Unexpected error while creating student.");
    }
});

// Обновление данных студента
app.MapPut("/students/{id}", (int id, StudentDto student) =>
{
    try
    {
        var updated = studentService.UpdateStudent(id, student);
        if (updated == null)
            return Results.NotFound($"Student with id {id} not found.");
        return Results.Ok(updated);
    }
    catch
    {
        return Results.Problem("Unexpected error while updating student.");
    }
});

// Удаление студента
app.MapDelete("/students/{id}", (int id) =>
{
    try
    {
        var deleted = studentService.DeleteStudent(id);
        if (!deleted)
            return Results.NotFound($"Student with id {id} not found.");
        return Results.Ok($"Student with id {id} deleted.");
    }
    catch
    {
        return Results.Problem("Unexpected error while deleting student.");
    }
});

app.MapPost("/students/{id}/mark", (int id, MarkDto mark) =>
{
    try
    {
        if (string.IsNullOrWhiteSpace(mark.Subject))
            return Results.BadRequest("Subject is required.");
        var result = studentService.MarkStudent(id, mark.Subject, mark.Grade);
        if (!result)
            return Results.NotFound($"Student with id {id} not found.");
        return Results.Ok($"Grade {mark.Grade} for {mark.Subject} set for student {id}.");
    }
    catch
    {
        return Results.Problem("Unexpected error while marking student.");
    }
});

app.Run();