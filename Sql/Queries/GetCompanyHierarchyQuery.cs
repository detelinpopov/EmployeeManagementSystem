using Microsoft.EntityFrameworkCore;
using Infrastructure.Context;
using Interfaces.Infrastructure.Queries;
using Infrastructure.Entities;
using Domain.Entities;
using Infrastructure.Mappers;
using Domain.Entities.Results;

namespace Infrastructure.Queries
{
    public class GetCompanyHierarchyQuery : IGetCompanyHierarchyQuery
    {
        private readonly EmployeeDbContext _dbContext;

        public GetCompanyHierarchyQuery(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetCompanyHierarchyResult> ExecuteAsync()
        {
            var result = new GetCompanyHierarchyResult();
            var topManagers = await _dbContext.Employees.Include(nameof(Employee.ManagedEmployees)).Where(e => e.ManagerId == null).ToListAsync();

            foreach (var manager in topManagers)
            {
                var employeeResult = manager.ToEmployeeModelResult();
                result.TopManagers.Add(employeeResult);

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
