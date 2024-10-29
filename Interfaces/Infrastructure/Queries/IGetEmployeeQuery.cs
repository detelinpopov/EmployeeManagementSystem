using Domain.Entities.Results;

namespace Interfaces.Infrastructure.Queries
{
    public interface IGetEmployeeQuery
    {
        public Task<GetEmployeeResult> ExecuteAsync(int employeeId);
    }
}
