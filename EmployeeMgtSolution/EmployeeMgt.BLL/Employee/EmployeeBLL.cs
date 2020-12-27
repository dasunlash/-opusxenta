using EmployeeMgt.CORE;
using EmployeeMgt.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMgt.BLL
{
    public class EmployeeBLL : IEmployeeBLL
    {
        private readonly IEmployeeDAL _employeeDAL;

        public EmployeeBLL(IEmployeeDAL employeeDAL)
        {
            _employeeDAL = employeeDAL;
        }

        public Task<Employee> AddEmployee(Employee employee)
        {
            try
            {
                employee.CreatedDate = DateTime.Now;
                return _employeeDAL.AddEmployee(employee);
            }
            catch (Exception ex)
            {

                throw new Exception("unable to save employee");
            }
        }
        private void RetrieveCompanyLogo(List<Company> companies)
        {

            foreach (var company in companies)
            {
                if (company.Logo != null)
                {
                    string imageBase64Data = Convert.ToBase64String(company.Logo);
                    company.CompanyLogoUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                }

            }
        }

        public Task<bool> DeleteEmployee(int id)
        {
            try
            {
                return _employeeDAL.DeleteEmployee(id);

            }
            catch (Exception ex)
            {

                throw new Exception("unable to delete employe");
            }
        }

        public async Task<List<Employee>> GetEmployees()
        {
            try
            {
                var employee= await _employeeDAL.GetEmployees();
                RetrieveCompanyLogo(employee);
                return employee;

            }
            catch (Exception ex)
            {

                throw new Exception("unable to get employees");
            }
        }

        private void RetrieveCompanyLogo(List<Employee> employee)
        {
            foreach (var company in employee)
            {
                if (company != null)
                {
                    string imageBase64Data = Convert.ToBase64String(company.Company.Logo);
                    company.Company.CompanyLogoUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                }

            }
        }

        public Task<Employee> GetEmployees(int id)
        {
            try
            {
                return _employeeDAL.GetEmployee(id);

            }
            catch (Exception ex)
            {

                throw new Exception("unable to get employee");
            }
        }

        public Task<Employee> GetEmployee(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> UpdateEmployee(Employee employee)
        {
            try
            {
                employee.ModifiedDate = DateTime.Now;
                return _employeeDAL.UpdateEmployee(employee);
            }
            catch (Exception ex)
            {

                throw new Exception("unable to update employee");
            }
        }
    }
}
