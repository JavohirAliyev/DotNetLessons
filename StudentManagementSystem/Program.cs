using StudentManagementSystem.Services;
using StudentManagementSystem.Services.Interfaces;
using StudentManagementSystem.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();