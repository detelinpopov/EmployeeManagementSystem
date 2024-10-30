using Domain.Entities;
using Domain.Entities.Results;

namespace Domain.Interfaces.Commands
{
    public interface IAddEmployeeCommand
    {
        Task<AddEmployeeResult> ExecuteAsync(EmployeeData employeeData);
    }
}
