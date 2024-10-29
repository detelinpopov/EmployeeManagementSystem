using Domain.Entities.Results;

namespace Interfaces.Infrastructure.Queries
{
    public interface IGetEmployeesQuery
    {
        public Task<GetEmployeeResult> GetEmployeeAsync(int employeeId);

        public Task<GetCompanyHierarchyResult> GetAllEmployeesAsync();
    }
}
