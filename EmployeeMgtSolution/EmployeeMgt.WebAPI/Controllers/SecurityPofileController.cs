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
    public class SecurityPofileController : ControllerBase
    {
        private readonly ISecurityProfileBLL _securityProfileBLL;
        public SecurityPofileController(ISecurityProfileBLL securityProfileBLL)
        {
            _securityProfileBLL = securityProfileBLL;
        }

        [HttpGet]
        public async Task<ActionResult<List<SecurityProfile>>> Get()
        {
            return Ok(await _securityProfileBLL.GetSecurityProfiles());
        }
        // POST: api/SecurityPofile
        [HttpPost]
        public async Task<ActionResult<SecurityProfile>> AddSecurityProfile([FromBody] SecurityProfile securityProfile)
        {
            securityProfile.CreatedUser = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);
            return Ok(await _securityProfileBLL.AddSecurityProfile(securityProfile));
        }
        // GET: api/SecurityPofile/1
        [HttpGet("{id}")]
        public async Task<ActionResult<SecurityProfile>> Get(int id)
        {
            var securityProfile = await _securityProfileBLL.GetSecurityProfiles(id);
            if (securityProfile == null)
            {
                return NotFound();
            }
            return Ok(securityProfile);
        }

        // PUT: api/SecurityPofile or PUT: api/SecurityPofile/5
        [HttpPut]
        public async Task<ActionResult<SecurityProfile>> UpdateSecurityProfile([FromBody] SecurityProfile securityProfile)
        {
            securityProfile.ModifiedUser = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);
            var securityProfileObj = await _securityProfileBLL.UpdateSecurityProfile(securityProfile);
            if (securityProfileObj == null)
            {
                return StatusCode(500, $"Unable to update security profile with id {securityProfile.Id}");
            }
            return Ok(securityProfileObj);

        }
        // DELETE: api/SecurityPofile/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteStudent(int id)
        {
            var isSecurityProfileDeleted = await _securityProfileBLL.DeleteSecurityProfile(id);
            if (!isSecurityProfileDeleted)
            {
                return StatusCode(500, $"Unable to delete security profile with id {id}");
            }
            return Ok(isSecurityProfileDeleted);
        }
    }
}
