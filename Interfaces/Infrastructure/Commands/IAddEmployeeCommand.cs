using Domain.Entities;
using Domain.Entities.Results;

namespace Interfaces.Infrastructure.Commands
{
    public interface IAddEmployeeCommand
    {
        Task<AddEmployeeResult> ExecuteAsync(EmployeeData employeeData);
    }
}
