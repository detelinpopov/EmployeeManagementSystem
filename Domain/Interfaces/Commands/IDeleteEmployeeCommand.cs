using Domain.Entities.Results;

namespace Domain.Interfaces.Commands
{
    public interface IDeleteEmployeeCommand
    {
        Task<DeleteEmployeeResult> ExecuteAsync(int id);
    }
}
