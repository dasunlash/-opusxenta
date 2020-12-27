using EmployeeMgt.CORE;
using EmployeeMgt.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMgt.DAL
{
    public class EmployeeDAL : IEmployeeDAL
    {
        private readonly EmployeeContext _employeeContext;

        public EmployeeDAL(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            _employeeContext.Employees.Add(employee);
            await _employeeContext.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            if (!_employeeContext.Employees.Any(emp => emp.Id == id))
            {
                return false;
            }
            var employee = await _employeeContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            _employeeContext.Employees.Remove(employee);
            await _employeeContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _employeeContext.Employees.Include(x=>x.Company).ToListAsync();
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _employeeContext.Employees.Include(x => x.Company).FirstOrDefaultAsync(x => x.Id == id);
        }

       

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            if (!_employeeContext.Employees.Any(record => record.Id == employee.Id))
            {
                return null;
            }
            _employeeContext.Entry(employee).State = EntityState.Modified;
            _employeeContext.Entry(employee).Property(x => x.CreatedDate).IsModified = false;
            _employeeContext.Entry(employee).Property(x => x.CreatedUser).IsModified = false;
            await _employeeContext.SaveChangesAsync();
            return employee;
        }
    }
}
