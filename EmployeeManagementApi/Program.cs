using Infrastructure.Context;
using Infrastructure.Queries;
using Infrastructure.Repositories;
using Interfaces.Infrastructure.Commands;
using Interfaces.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;


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
builder.Services.AddScoped<IGetCompanyHierarchyQuery, GetCompanyHierarchyQuery>();
builder.Services.AddScoped<IGetEmployeeQuery, GetEmployeeQuery>();

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
