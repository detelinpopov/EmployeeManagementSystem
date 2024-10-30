namespace Domain.Interfaces.Entities
{
    public interface IEmployee
    {
        public int Id { get; set; }
        
        public  string FullName { get; set; }

        public  string Title { get; set; }

        public int? ManagerId { get; set; }

        IEnumerable<IEmployee> ManagedEmployees { get; set; }
    }
}
