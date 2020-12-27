using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmployeeMgt.CORE;
using EmployeeMgt.CORE.Entities;

namespace EmployeeMgt.CORE
{
   public interface IEmployeeBLL
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployees(int id);

        Task<Employee> AddEmployee(Employee employee);

        Task<Employee> UpdateEmployee(Employee employee);

        Task<bool> DeleteEmployee(int id);
        Task<Employee> GetEmployee(string firstName, string lastName);
    }
}
