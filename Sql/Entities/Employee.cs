using Interfaces.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class Employee : IEmployee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public string Title { get; set; }

        public int? ManagerId { get; set; }
      
        public virtual ICollection<Employee> ManagedEmployees { get; set; }

        IEnumerable<IEmployee> IEmployee.ManagedEmployees
        {
            get => ManagedEmployees;
            set => ManagedEmployees = value.Cast<Employee>().ToList();
        }
    }
}
