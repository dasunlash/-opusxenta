using EmployeeMgt.CORE;
using EmployeeMgt.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmployeeMgt.CORE.Common.Common;

namespace EmployeeMgt.DAL
{
    public class UserDAL : IUserDAL
    {
        private readonly EmployeeContext _employeeContext;

        public UserDAL(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }
        public async Task<User> AddUser(User user)
        {
            _employeeContext.Users.Add(user);
            await _employeeContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUser(int id)
        {
            if (!_employeeContext.Users.Any(user => user.Id == id))
            {
                return false;
            }
            var user = await _employeeContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            _employeeContext.Users.Remove(user);
            await _employeeContext.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetUser(int id)
        {
            return await _employeeContext.Users.Include(x => x.SecurityProfile).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _employeeContext.Users.Include(x=>x.SecurityProfile).ToListAsync();
        }

        public async Task<User> UpdateUser(User user)
        {
            if (!_employeeContext.Users.Any(record => record.Id == user.Id))
            {
                return null;
            }
            _employeeContext.Entry(user).State = EntityState.Modified;
            _employeeContext.Entry(user).Property(x => x.CreatedDate).IsModified = false;
            _employeeContext.Entry(user).Property(x => x.CreatedUser).IsModified = false;
            await _employeeContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> ValidateUser(string userName,string password)
        {
            string pwd = CryptoEncrypt.Encrypt(password, true);
            var users=   await _employeeContext.Users?.Include(x=>x.SecurityProfile).Where(x => x.Email == userName && x.Password== pwd).FirstOrDefaultAsync();
            return users;
        }
    }
}
