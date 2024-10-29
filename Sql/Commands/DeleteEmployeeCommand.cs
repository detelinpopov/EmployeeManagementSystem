using Microsoft.EntityFrameworkCore;
using Infrastructure.Context;
using Interfaces.Infrastructure.Commands;
using Domain.Entities.Results;
using Interfaces.Infrastructure.Entities;
using Infrastructure.Mappers;

namespace Infrastructure.Repositories
{
    public class DeleteEmployeeCommand : IDeleteEmployeeCommand
    {
        private readonly EmployeeDbContext _dbContext;

        public DeleteEmployeeCommand(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
       
        public async Task<DeleteEmployeeResult> ExecuteAsync(int id)
        {
            var deleteEmployeeResult = new DeleteEmployeeResult();
            var employeeToDelete = await _dbContext.Employees.Include(nameof(IEmployee.ManagedEmployees)).FirstOrDefaultAsync(e => e.Id == id);

            if (employeeToDelete == null)
            {
                deleteEmployeeResult.Errors.Add(new ErrorModel { ErrorMessage = $"Employee with Id = '{id}' was not found." });
                return deleteEmployeeResult;
            }

            if(employeeToDelete.ManagerId == null)
            {
                foreach(var directReport in employeeToDelete.ManagedEmployees)
                {
                    directReport.ManagerId = null;
                    deleteEmployeeResult.ReassignedEmployees.Add(directReport.ToEmployeeModelResult());
                }
            }
            else
            {
                foreach (var directReport in employeeToDelete.ManagedEmployees)
                {
                    directReport.ManagerId = employeeToDelete.ManagerId;
                    deleteEmployeeResult.ReassignedEmployees.Add(directReport.ToEmployeeModelResult());
                }
            }

            _dbContext.Remove(employeeToDelete);
            await _dbContext.SaveChangesAsync();

            return deleteEmployeeResult;
        }      
    }
}
