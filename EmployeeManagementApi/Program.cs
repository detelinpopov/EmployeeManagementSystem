using Infrastructure.Context;
using Infrastructure.Queries;
using Infrastructure.Repositories;
using Domain.Interfaces.Commands;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Queries;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

builder.Services.AddDbContext<EmployeeDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeConnectionString"));
});

builder.Services.AddScoped<IAddEmployeeCommand, AddEmployeeCommand>();
builder.Services.AddScoped<IDeleteEmployeeCommand, DeleteEmployeeCommand>();
builder.Services.AddScoped<IGetEmployeesQuery, GetEmployeesQuery>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
