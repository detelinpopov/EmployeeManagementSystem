using Domain.Entities;
using Domain.Interfaces.Commands;
using Domain.Interfaces.Queries;
using EmployeeManagementApi.Validators;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementApi.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IAddEmployeeCommand _addEmployeeCommand;

        private readonly IGetEmployeesQuery _getEmployeesQuery;

        private readonly IDeleteEmployeeCommand _deleteEmployeeCommand;

        public EmployeeController(IAddEmployeeCommand addEmployeeCommand, IGetEmployeesQuery getEmployeesQuery, IDeleteEmployeeCommand deleteEmployeeCommand)
        {
            _addEmployeeCommand = addEmployeeCommand;
            _getEmployeesQuery = getEmployeesQuery;
            _deleteEmployeeCommand = deleteEmployeeCommand;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(EmployeeData employeeData)
        {
            var validatedEmployeeResult = EmployeeValidator.Validate(employeeData);
            if (!validatedEmployeeResult.Success)
            {
                return BadRequest(validatedEmployeeResult.ErrorMessage);
            }

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
        public async Task<IActionResult> GetAll()
        {            
            var result = await _getEmployeesQuery.GetAllEmployeesAsync();        
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _getEmployeesQuery.GetEmployeeAsync(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.GetErrorsAsString());
        }

        [HttpDelete("{id:int}")]
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
