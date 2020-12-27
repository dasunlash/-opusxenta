using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMgt.CORE;
using EmployeeMgt.CORE.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMgt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyBLL _companyBLL;
        public CompanyController(ICompanyBLL companyBLL)
        {
            _companyBLL = companyBLL;
        }

        [HttpGet]
        public async Task<ActionResult<List<Company>>> Get()
        {
            return Ok(await _companyBLL.GetCompanies());
        }
        // POST: api/Company
        [HttpPost]
        public async Task<ActionResult<Company>> AddCompany([FromForm] Company company)
        {
            var file = Request.Form.Files[0];
            company.Logo = GetCompanyLoGo();
            company.CreatedUser = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);
            return Ok(await _companyBLL.AddCompany(company));
        }
        // GET: api/Company/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> Get(int id)
        {
            var company = await _companyBLL.GetCompanies(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        // PUT: api/Company
        [HttpPut]
        public async Task<ActionResult<User>> UpdateCompany([FromForm] Company company)
        {
            company.ModifiedUser = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);
            company.Logo = GetCompanyLoGo();
            var companyObj = await _companyBLL.UpdateCompany(company);
            if (companyObj == null)
            {
                return StatusCode(500, $"Unable to update company with id {company.Id}");
            }
            return Ok(companyObj);

        }
        // DELETE: api/Company/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteCompnay(int id)
        {
            var employees= await _companyBLL.GetEmployees(id);
            if (employees.Count>0)
            {
                return StatusCode(400, $"Unable to delete company,There are employees under this company {id}");
            }
            var isCompanyDeleted = await _companyBLL.DeleteCompany(id);
            if (!isCompanyDeleted)
            {
                return StatusCode(500, $"Unable to delete company with id {id}");
            }
            return Ok(isCompanyDeleted);
        }

        [NonAction]
        public byte[] GetCompanyLoGo()
        {
            var file = Request.Form.Files[0];
            if (file.Length > 0)
            {
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                return ms.ToArray();
               
            }
            return null;

        }
    }
}
