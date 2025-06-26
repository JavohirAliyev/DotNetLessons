using StudentManagementSystem.Models;
using StudentManagementSystem.Services;
using StudentManagementSystem.Services.Interfaces;

StudentService studentService = new();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddScoped<IStudentService, StudentService>();

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

// team 4
app.MapDelete("/students/{id}", (int id) =>
{
    var deleted = studentService.DeleteStudent(id);
    return deleted
        ? Results.Ok("Student deleted")
        : Results.NotFound("Student not found");
});

// Example PUT request to test in Postman:
// URL: http://localhost:5000/students/1?subject=Math&grade=91.5
app.MapPut("/students/{id}", (int id, string subject, double grade) =>
{
    try
    {
        var student = studentService.MarkStudent(id, subject, grade);
        return Results.Ok(student);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});


app.UseHttpsRedirection();
app.MapControllers();

app.Run();