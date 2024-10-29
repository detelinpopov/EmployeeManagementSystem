namespace Domain.Entities.Results
{
    public class GetCompanyHierarchyResult : CommandResult
    {
        public IList<EmployeeModelResult> CompanyHierarchy { get; } = new List<EmployeeModelResult>();
    }
}
