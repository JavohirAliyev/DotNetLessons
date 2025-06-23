using StudentManagementSystem.Models;
using StudentManagementSystem.Services;

StudentService studentService = new();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Welcome to the Student Management System!");


// team 1
app.MapGet("/students", () =>
{
    var students = studentService.GetAllStudents();
    return students == null || students.Count == 0
        ? Results.NoContent()
        : Results.Ok(students);
});

// app.MapGet("/students/{id}", (int id) =>
// {
//     var student = studentService.GetStudentById(id);
//     return student == null
//         ? Results.NotFound("Student not found")
//         : Results.Ok(student);
// });

// team 2
app.MapPost("/students", (StudentDto student) =>
{
    if (student == null)
        return Results.BadRequest("Student data is required.");
    var created = studentService.CreateStudent(student);
    return Results.Created($"/students/{created.Id}", created);
});

// team 3
app.MapPut("/students/{id}", (int id, StudentDto student) =>
{
    var updated = studentService.UpdateStudent(id, student);
    return updated == null
        ? Results.NotFound("Student not found")
        : Results.Ok(updated);
});


// team 4
app.MapDelete("/students/{id}", (int id) =>
{
    var deleted = studentService.DeleteStudent(id);
    return deleted
        ? Results.Ok("Student deleted")
        : Results.NotFound("Student not found");
});

app.Run();
