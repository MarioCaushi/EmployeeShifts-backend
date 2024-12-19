using EmployeeShift_backend.Data;
using EmployeeShift_backend.Errors;
using EmployeeShift_backend.Services;
using EmployeeShift_backend.Services.ServicesInterfaces;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCors();

// Construct connection string using environment variables
var server = "localhost";
var database = "EmployeeShiftDB";
var username = builder.Configuration["Database:Username"];
var password = builder.Configuration["Database:Password"];

var connectionString = $"Server={server};Database={database};User Id={username};Password={password};";

builder.Services.AddDbContext<EmployeeShiftDbContext>(options =>
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(9, 1, 0)) // Specify your MySQL server version
    ));

// Register custom services
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IShiftService, ShiftService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IManagerService, ManagerService>();

// Register the Swagger generator
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // To serve the Swagger UI at the app's root
    });
}

// CORS Configuration
app.UseCors(options =>
    options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();

//Exception handling
app.ConfigureBuildInExceptionHandlers();

app.MapControllers();
app.Run();