using EmployeeMgt.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMgt.CORE.Interfaces.SecuirityProfile
{
   public interface ISecurityProfileDAL
    {
        Task<List<SecurityProfile>> GetSecurityProfiles();
        Task<SecurityProfile> GetSecurityProfiles(int id);

        Task<SecurityProfile> AddSecurityProfile(SecurityProfile securityProfile);

        Task<SecurityProfile> UpdateSecurityProfile(SecurityProfile securityProfile);

        Task<bool> DeleteSecurityProfile(int id);
    }
}
