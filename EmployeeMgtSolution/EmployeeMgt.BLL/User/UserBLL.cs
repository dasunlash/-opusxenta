using EmployeeMgt.CORE;
using EmployeeMgt.CORE.Entities;
using EmployeeMgt.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static EmployeeMgt.CORE.Common.Common;

namespace EmployeeMgt.BLL
{
    public class UserBLL : IUserBLL
    {
        private readonly IUserDAL _userDAL;

        public UserBLL(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }
        public Task<User> AddUser(User user)
        {
            try
            {
                user.CreatedDate = DateTime.Now;
                user.Password= CryptoEncrypt.Encrypt(user.Password, true);
                return _userDAL.AddUser(user);
            }
            catch (Exception ex)
            {

                throw new Exception("unable to save user");
            }
        }

        public Task<bool> DeleteUser(int id)
        {
            try
            {
                return _userDAL.DeleteUser(id);
            }
            catch (Exception ex)
            {

                throw new Exception("unable to save user");
            }
        }

        public Task<User> GetUsers(int id)
        {
            try
            {
                return _userDAL.GetUser(id);
            }
            catch (Exception ex)
            {

                throw new Exception("unable to get user");
            }
        }

        public Task<List<User>> GetUsers()
        {
            try
            {
                return _userDAL.GetUsers();
            }
            catch (Exception ex)
            {

                throw new Exception("unable to get users");
            }
        }

        public Task<User> UpdateUser(User user)
        {
            try
            {
                user.ModifiedDate = DateTime.Now;
                user.Password = CryptoEncrypt.Encrypt(user.Password, true);
                return _userDAL.UpdateUser(user);
            }
            catch (Exception ex)
            {

                throw new Exception("unable to update user");
            }
        }

        public async Task<User> ValidateUser(string userName, string password)
        {
            try
            {
                return await _userDAL.ValidateUser(userName, password);
            }
            catch (Exception)
            {

                throw new Exception("unable to validate user");
            }
        }

    }
}
