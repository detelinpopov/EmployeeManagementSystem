using Domain.Entities.Results;

namespace Domain.Interfaces.Queries
{
    public interface IGetEmployeesQuery
    {
        public Task<GetEmployeeResult> GetEmployeeAsync(int employeeId);

        public Task<GetCompanyHierarchyResult> GetAllEmployeesAsync();
    }
}
