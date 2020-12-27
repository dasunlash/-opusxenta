using EmployeeMgt.CORE;
using EmployeeMgt.CORE.Entities;
using EmployeeMgt.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMgt.BLL
{
    public class CompanyBLL : ICompanyBLL
    {
        private readonly ICompanyDAL _companyDAL;

        public CompanyBLL(ICompanyDAL companyDAL)
        {
            _companyDAL = companyDAL;
        }
        public Task<Company> AddCompany(Company company)
        {
            try
            {
                company.CreatedDate = DateTime.Now;
                return _companyDAL.AddCompany(company);
            }
            catch (Exception ex)
            {

                throw new Exception("unable to save company");
            }
        }

        public async Task<bool> DeleteCompany(int id)
        {
            try
            {
                return await _companyDAL.DeleteCompany(id);

            }
            catch (Exception ex)
            {

                throw new Exception("unable to delete company");
            }
        }

        public async Task<List<Company>> GetCompanies()
        {
            try
            {
                var companies= await _companyDAL.GetCompanies();
                 RetrieveCompanyLogo(companies);
                return companies;
            }
            catch (Exception ex)
            {

                throw new Exception("unable to get companies");
            }
        }

        private void  RetrieveCompanyLogo(List<Company>companies)
        {

            foreach (var company in   companies)
            {
                if (company.Logo!=null)
                {
                    string imageBase64Data = Convert.ToBase64String(company.Logo);
                    company.CompanyLogoUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                }
                
            }
        }

        
        public async Task<Company> GetCompanies(int id)
        {
            try
            {
                return await _companyDAL.GetCompanies(id);

            }
            catch (Exception ex)
            {

                throw new Exception("unable to get company");
            }
        }

        public async Task<List<Employee>> GetEmployees(int companyId)
        {
            try
            {
                return await _companyDAL.GetEmployees(companyId);
            }
            catch (Exception)
            {

                throw new Exception("unable to get company");
            }
        }

        public Task<Company> UpdateCompany(Company company)
        {
            try
            {
                company.ModifiedDate = DateTime.Now;
                return _companyDAL.UpdateCompany(company);
            }
            catch (Exception ex)
            {

                throw new Exception("unable to update company");
            }
        }
    }
}
