namespace Domain.Entities.Results
{
    public class GetCompanyHierarchyResult : CommandResult
    {
        public IList<EmployeeModelResult> TopManagers { get; } = new List<EmployeeModelResult>();
    }
}
