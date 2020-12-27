using EmployeeMgt.CORE;
using EmployeeMgt.CORE.Entities;
using EmployeeMgt.CORE.Interfaces;
using EmployeeMgt.CORE.Interfaces.SecuirityProfile;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMgt.BLL
{
  public  class SecurityProfileBLL: ISecurityProfileBLL
    {
        private readonly ISecurityProfileDAL _securityProfileDAL;

        public SecurityProfileBLL(ISecurityProfileDAL securityProfileDAL)
        {
            _securityProfileDAL = securityProfileDAL;
        }

        public Task<SecurityProfile> AddSecurityProfile(SecurityProfile securityProfile)
        {
            try
            {
                securityProfile.CreatedDate = DateTime.Now;
                return _securityProfileDAL.AddSecurityProfile(securityProfile);
            }
            catch (Exception ex)
            {

                throw new Exception("unable to save security profile");
            }
        }

        public Task<bool> DeleteSecurityProfile(int id)
        {
            try
            {
                return _securityProfileDAL.DeleteSecurityProfile(id);

            }
            catch (Exception ex)
            {

                throw new Exception("unable to delete security profile");
            }
        }

        public Task<SecurityProfile> GetSecurityProfiles(int id)
        {
            try
            {
                return _securityProfileDAL.GetSecurityProfiles(id);

            }
            catch (Exception ex)
            {

                throw new Exception("unable to get security profile");
            }
        }

        public Task<List<SecurityProfile>> GetSecurityProfiles()
        {
            try
            {
                return _securityProfileDAL.GetSecurityProfiles();

            }
            catch (Exception ex)
            {

                throw new Exception("unable to get security profiles");
            }
        }

        public Task<SecurityProfile> UpdateSecurityProfile(SecurityProfile securityProfile)
        {
            try
            {

                securityProfile.ModifiedDate = DateTime.Now;
                return _securityProfileDAL.UpdateSecurityProfile(securityProfile);
            }
            catch (Exception ex)
            {

                throw new Exception("unable to update security profile");
            }
        }
    }
}
