using StudentManagementSystem.Services;
using StudentManagementSystem.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();