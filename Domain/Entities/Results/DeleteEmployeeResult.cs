namespace Domain.Entities.Results
{
    public class DeleteEmployeeResult : CommandResult
    {
        public IList<EmployeeModelResult> ReassignedEmployees { get; } = new List<EmployeeModelResult>();
    }
}
