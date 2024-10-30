using Domain.Entities.Results;
using Domain.Interfaces.Entities;

namespace Infrastructure.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeModelResult ToEmployeeModelResult(this IEmployee model)
        {
            var employee = new EmployeeModelResult
            {
                FullName = model.FullName,
                Id = model.Id,
                Title = model.Title,
                ManagerId = model.ManagerId
            };

            return employee;
        }

        public static IEnumerable<EmployeeModelResult> ToEmployeeResultList(this IEnumerable<IEmployee> employees)
        {
            var mappedEntities = new List<EmployeeModelResult>();
            foreach (var employee in employees)
            {
                mappedEntities.Add(ToEmployeeModelResult(employee));
            }

            return mappedEntities;
        }
    }
}
