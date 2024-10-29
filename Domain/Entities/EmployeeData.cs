namespace Domain.Entities
{
    public class EmployeeData
    {
        public int Id { get; set; }

        public required string FullName { get; set; }

        public required string Title { get; set; }

        public int? ManagerId { get; set; }
    }
}
