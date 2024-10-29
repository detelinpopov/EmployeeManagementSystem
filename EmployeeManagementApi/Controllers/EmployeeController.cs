using Domain.Entities;
using Interfaces.Infrastructure.Commands;
using Interfaces.Infrastructure.Queries;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementApi.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IAddEmployeeCommand _addEmployeeCommand;

        private readonly IGetCompanyHierarchyQuery _getCompanyHierarchyQuery;

        private readonly IGetEmployeeQuery _getEmployeeQuery;

        private readonly IDeleteEmployeeCommand _deleteEmployeeCommand;

        public EmployeeController(IAddEmployeeCommand addEmployeeCommand, IGetCompanyHierarchyQuery getCompanyHierarchyQuery, IGetEmployeeQuery getEmployeeQuery, IDeleteEmployeeCommand deleteEmployeeCommand)
        {
            _addEmployeeCommand = addEmployeeCommand;
            _getCompanyHierarchyQuery = getCompanyHierarchyQuery;
            _getEmployeeQuery = getEmployeeQuery;
            _deleteEmployeeCommand = deleteEmployeeCommand;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(EmployeeData employeeData)
        {
            var addEmployeeResult = await _addEmployeeCommand.ExecuteAsync(employeeData);

            if (addEmployeeResult.Success)
            {
                return Ok(addEmployeeResult);
            }

            return BadRequest(addEmployeeResult.GetErrorsAsString());
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCompanyHierarchy()
        {            
            var result = await _getCompanyHierarchyQuery.ExecuteAsync();        
            return Ok(result);
        }

        [HttpGet("employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var result = await _getEmployeeQuery.ExecuteAsync(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.GetErrorsAsString());
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _deleteEmployeeCommand.ExecuteAsync(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.GetErrorsAsString());
        }
    }
}
