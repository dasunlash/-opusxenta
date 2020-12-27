using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmployeeMgt.CORE.Entities;


namespace EmployeeMgt.CORE
{
    public interface IUserDAL
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);

        Task<User> AddUser(User employee);

        Task<User> UpdateUser(User user);

        Task<bool> DeleteUser(int id);

        Task<User> ValidateUser(string userName, string password);
    }
}
