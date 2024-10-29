using Domain.Entities.Results;

namespace Interfaces.Infrastructure.Queries
{
    public interface IGetCompanyHierarchyQuery
    {
        public Task<GetCompanyHierarchyResult> ExecuteAsync();
    }
}
