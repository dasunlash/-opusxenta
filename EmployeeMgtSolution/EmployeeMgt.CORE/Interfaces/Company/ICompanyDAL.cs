using EmployeeMgt.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMgt.CORE.Interfaces
{
    public interface ICompanyDAL
    {
        Task<List<Company>> GetCompanies();
         Task<Company> GetCompanies(int id);
        Task<Company> AddCompany(Company company);
        Task<Company> UpdateCompany(Company company);
        Task<bool> DeleteCompany(int id);
        Task<List<Employee>> GetEmployees(int companyId);
    }
}
