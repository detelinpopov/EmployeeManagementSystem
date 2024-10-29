using Domain.Entities;
using EmployeeManagementApi.Models;

namespace EmployeeManagementApi.Validators
{
    public static class EmployeeValidator
    {
        public static ValidateEmployeeDataResult Validate(EmployeeData employeeData)
        {
            var result = new ValidateEmployeeDataResult() { Success = true };

            if (string.IsNullOrWhiteSpace(employeeData.FullName))
            {
                result.Success = false;
                result.ErrorMessage = "Employee name is required.";
            }

            if (string.IsNullOrWhiteSpace(employeeData.Title))
            {
                result.Success = false;
                result.ErrorMessage = "Employee title is required.";
            }

            return result;
        }
    }
}
