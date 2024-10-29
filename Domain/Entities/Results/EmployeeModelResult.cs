namespace Domain.Entities.Results
{
    public class EmployeeModelResult
    {
        public int Id { get; set; }

        public required string FullName { get; set; }

        public required string Title { get; set; }

        public int? ManagerId { get; set; }

        public IList<EmployeeModelResult> DirectReports { get; set; } = new List<EmployeeModelResult>();
    }
}
