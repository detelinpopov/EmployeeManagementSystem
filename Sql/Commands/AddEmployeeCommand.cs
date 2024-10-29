using Microsoft.EntityFrameworkCore;
using Infrastructure.Context;
using Infrastructure.Entities;
using Interfaces.Infrastructure.Commands;
using Domain.Entities;
using Domain.Entities.Results;

namespace Infrastructure.Repositories
{
    public class AddEmployeeCommand : IAddEmployeeCommand
    {
        private readonly EmployeeDbContext _dbContext;

        public AddEmployeeCommand(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
       
        public async Task<AddEmployeeResult> ExecuteAsync(EmployeeData employeeData)
        {
            var addEmployeeResult = employeeData.Id == 0 ?
                                    await CreateEmployeeAsync(employeeData) :
                                    await UpdateEmployeeAsync(employeeData);

            return addEmployeeResult;
        }

        private async Task<AddEmployeeResult> CreateEmployeeAsync(EmployeeData employeeData)
        {
            var addEmployeeResult = new AddEmployeeResult();
            var employee = new Employee
            {
                Id = employeeData.Id,
                FullName = employeeData.FullName,
                Title = employeeData.Title
            };
         
            _dbContext.Employees.Add(employee);

            if (employeeData.ManagerId > 0)
            {
                await UpdateEmployeeManagerAsync(employeeData, employee, addEmployeeResult);
            }

            await _dbContext.SaveChangesAsync();
            return addEmployeeResult;
        }

        private async Task<AddEmployeeResult> UpdateEmployeeAsync(EmployeeData employeeData)
        {
            var addEmployeeResult = new AddEmployeeResult();
            var existingEmployee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == employeeData.Id);
            if (existingEmployee == null)
            {
                addEmployeeResult.Errors.Add(new ErrorModel { ErrorMessage = $"Employee with Id = '{employeeData.Id}' was not found." });
                return addEmployeeResult;
            }
           
            existingEmployee.FullName = employeeData.FullName;
            existingEmployee.Title = employeeData.Title;
            existingEmployee.ManagerId = employeeData.ManagerId;

            await UpdateEmployeeManagerAsync(employeeData, existingEmployee, addEmployeeResult);

            await _dbContext.SaveChangesAsync();
            return addEmployeeResult;
        }

        private async Task UpdateEmployeeManagerAsync(EmployeeData employeeData, Employee employee, AddEmployeeResult result)
        {
            if (employeeData.ManagerId > 0)
            {
                var manager = await _dbContext.Employees.Include(nameof(Employee.ManagedEmployees)).FirstOrDefaultAsync(e => e.Id == employeeData.ManagerId);
                if (manager == null)
                {
                    result.Errors.Add(new ErrorModel { ErrorMessage = $"Manager with Id = '{employeeData.ManagerId}' was not found." });
                    return;
                }

                employee.ManagerId = manager.Id;
                manager.ManagedEmployees.Add(employee);
            }
        }
    }
}
