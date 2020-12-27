using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMgt.CORE.Entities
{
   public interface IEmployeeDAL
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int id);

        Task<Employee> AddEmployee(Employee employee);

        Task<Employee> UpdateEmployee(Employee employee);

        Task<bool> DeleteEmployee(int id);
    }
}
