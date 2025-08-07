using ComputerApi.API.Middleware;
using ComputerApi.Application.Mappings;
using ComputerApi.Application.Services;
using ComputerApi.Application.Validators;
using ComputerApi.Domain.Interfaces;
using ComputerApi.Infrastructure.Data;
using ComputerApi.Infrastructure.Repositories;
using ComputerApi.Infrastructure.UnitOfWork;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Computer Management API",
        Version = "v1",
        Description = "A RESTful API for managing computers and their installed software",
        Contact = new OpenApiContact
        {
            Name = "Computer Management Team",
            Email = "support@computermanagement.com"
        }
    });

    // Include XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // Add enum descriptions
    c.SchemaFilter<EnumSchemaFilter>();
});

// Configure Entity Framework
builder.Services.AddDbContext<ComputerDbContext>(options =>
    options.UseInMemoryDatabase("ComputerManagementDb"));

// Register repositories and unit of work
builder.Services.AddScoped<IComputerRepository, ComputerRepository>();
builder.Services.AddScoped<ISoftwareRepository, SoftwareRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register services
builder.Services.AddScoped<IComputerService, ComputerService>();
builder.Services.AddScoped<ISoftwareService, SoftwareService>();

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configure FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateComputerDtoValidator>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Computer Management API v1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

// Add custom middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ComputerDbContext>();
    context.Database.EnsureCreated();
}

app.Run();