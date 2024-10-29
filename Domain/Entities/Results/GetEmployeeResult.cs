namespace Domain.Entities.Results
{
    public class GetEmployeeResult : CommandResult
    {
        public EmployeeModelResult EmployeeHierarchy { get; set; }
    }
}
