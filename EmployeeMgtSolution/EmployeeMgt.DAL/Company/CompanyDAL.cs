using EmployeeMgt.CORE.Entities;
using EmployeeMgt.CORE.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMgt.DAL
{
    public class CompanyDAL : ICompanyDAL
    {
        private readonly EmployeeContext _employeeContext;

        public CompanyDAL(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public async Task<Company> AddCompany(Company company)
        {
            _employeeContext.Companies.Add(company);
            await _employeeContext.SaveChangesAsync();
            return company;
        }

        public async Task<bool> DeleteCompany(int id)
        {
            if (!_employeeContext.Companies.Any(comp => comp.Id == id))
            {
                return false;
            }
            var company = await _employeeContext.Companies.FirstOrDefaultAsync(x => x.Id == id);
            _employeeContext.Companies.Remove(company);
            await _employeeContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Company>> GetCompanies()
        {
            return await _employeeContext.Companies.ToListAsync();

        }

        public async Task<Company> GetCompanies(int id)
        {
            return await _employeeContext.Companies.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Employee>> GetEmployees(int companyId)
        {
            return await _employeeContext.Employees.Where(x => x.CompanyId == companyId).ToListAsync();
        }

        public async Task<Company> UpdateCompany(Company company)
        {
           
            if (!_employeeContext.Companies.Any(record => record.Id == company.Id))
            {
                return null;
            }
            _employeeContext.Entry(company).State = EntityState.Modified;
            _employeeContext.Entry(company).Property(x => x.CreatedDate).IsModified = false;
            _employeeContext.Entry(company).Property(x => x.CreatedUser).IsModified = false;
            await _employeeContext.SaveChangesAsync();
            return company;
        }
    }
}
