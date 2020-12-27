using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMgt.CORE;
using EmployeeMgt.CORE.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMgt.WebAPI.Controllers
{
    [Route("api/[controller]"),Authorize]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeBLL _employeeBLL;
        public EmployeeController(IEmployeeBLL employeeBLL)
        {
            _employeeBLL = employeeBLL;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get()
        {
            return Ok(await _employeeBLL.GetEmployees());
        }
        // POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee([FromBody] Employee employee)
        {
            employee.CreatedUser = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);
            return Ok(await _employeeBLL.AddEmployee(employee));
        }
        // GET: api/Employee/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var employee = await _employeeBLL.GetEmployees(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // PUT: api/Employee or PUT: api/Employee/5
        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee([FromBody] Employee employee)
        {
            employee .ModifiedUser= Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);
            var employeeObj = await _employeeBLL.UpdateEmployee(employee);
            if (employeeObj == null)
            {
                return StatusCode(500, $"Unable to update employee with id {employee.Id}");
            }
            return Ok(employeeObj);

        }
        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var isEmployeeDeleted = await _employeeBLL.DeleteEmployee(id);
            if (!isEmployeeDeleted)
            {
                return StatusCode(500, $"Unable to delete employee with id {id}");
            }
            return Ok(isEmployeeDeleted);
        }
    }
}
