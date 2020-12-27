using EmployeeMgt.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMgt.CORE.Interfaces
{
   public interface IUserBLL
    {
        Task<List<User>> GetUsers();
        Task<User> GetUsers(int id);

        Task<User> AddUser(User user);

        Task<User> UpdateUser(User user);

        Task<bool> DeleteUser(int id);

        Task<User> ValidateUser(string userName, string password);

    }
}
