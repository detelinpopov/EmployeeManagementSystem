using Microsoft.EntityFrameworkCore;
using Infrastructure.Context;
using Interfaces.Infrastructure.Queries;
using Infrastructure.Entities;
using Domain.Entities;
using Infrastructure.Mappers;
using Domain.Entities.Results;

namespace Infrastructure.Queries
{
    public class GetEmployeesQuery : IGetEmployeesQuery
    {
        private readonly EmployeeDbContext _dbContext;

        public GetEmployeesQuery(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetEmployeeResult> GetEmployeeAsync(int employeeId)
        {
            var result = new GetEmployeeResult();
            var employee = await _dbContext.Employees.Include(nameof(Employee.ManagedEmployees)).FirstOrDefaultAsync(e => e.Id == employeeId);
            if(employee == null)
            {
                result.Errors.Add(new ErrorModel { ErrorMessage = $"Employee with Id = '{employeeId}' was not found." });
                return result;
            }

            var employeeResult = employee.ToEmployeeModelResult();
            result.EmployeeHierarchy = employeeResult;

            employeeResult.DirectReports = await GetDirectReportsAsync(employee.Id);
            return result;
        }

        public async Task<GetCompanyHierarchyResult> GetAllEmployeesAsync()
        {
            var result = new GetCompanyHierarchyResult();
            var topManagers = await _dbContext.Employees.Include(nameof(Employee.ManagedEmployees)).Where(e => e.ManagerId == null).ToListAsync();

            foreach (var manager in topManagers)
            {
                var employeeResult = manager.ToEmployeeModelResult();
                result.CompanyHierarchy.Add(employeeResult);

                employeeResult.DirectReports = await GetDirectReportsAsync(manager.Id);
            }

            return result;
        }

        private async Task<IList<EmployeeModelResult>> GetDirectReportsAsync(int managerId)
        {
            var employeeHierarchy = new List<EmployeeModelResult>();

            // Get all employees who report directly to the specified manager.
            var directReports = await _dbContext.Employees.Include(nameof(Employee.ManagedEmployees)).Where(e => e.ManagerId == managerId).ToListAsync();

            var mappedReports = directReports.ToEmployeeResultList();
            employeeHierarchy.AddRange(mappedReports);

            // For each direct report get their direct reports recursively.
            foreach (var directReport in mappedReports)
            {
                directReport.DirectReports = await GetDirectReportsAsync(directReport.Id);
            }

            return employeeHierarchy;
        }      
    }
}
