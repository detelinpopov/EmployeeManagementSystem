using Domain.Entities;
using Domain.Entities.Results;
namespace Interfaces.Infrastructure.Commands
{
    public interface IDeleteEmployeeCommand
    {
        Task<DeleteEmployeeResult> ExecuteAsync(int id);
    }
}
