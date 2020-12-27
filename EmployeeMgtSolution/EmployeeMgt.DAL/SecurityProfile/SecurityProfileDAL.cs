using EmployeeMgt.CORE.Entities;
using EmployeeMgt.CORE.Interfaces.SecuirityProfile;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMgt.DAL
{

   public class SecurityProfileDAL: ISecurityProfileDAL
    {
        private readonly EmployeeContext _employeeContext;

        public SecurityProfileDAL(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public async  Task<SecurityProfile> AddSecurityProfile(SecurityProfile securityProfile)
        {
            _employeeContext.SecurityProfiles.Add(securityProfile);
            await _employeeContext.SaveChangesAsync();
            return securityProfile;
        }

        public async Task<bool> DeleteSecurityProfile(int id)
        {
            if (!_employeeContext.SecurityProfiles.Any(user => user.Id == id))
            {
                return false;
            }
            var securityProfile = await _employeeContext.SecurityProfiles.FirstOrDefaultAsync(x => x.Id == id);
            _employeeContext.SecurityProfiles.Remove(securityProfile);
            await _employeeContext.SaveChangesAsync();
            return true;
        }

        public async Task<SecurityProfile> GetSecurityProfiles(int id)
        {
            return await _employeeContext.SecurityProfiles.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<SecurityProfile>> GetSecurityProfiles()
        {
            return await _employeeContext.SecurityProfiles.ToListAsync();
        }

        public async Task<SecurityProfile> UpdateSecurityProfile(SecurityProfile securityProfile)
        {
            if (!_employeeContext.SecurityProfiles.Any(record => record.Id == securityProfile.Id))
            {
                return null;
            }
            _employeeContext.Entry(securityProfile).State = EntityState.Modified;
            _employeeContext.Entry(securityProfile).Property(x => x.CreatedDate).IsModified = false;
            _employeeContext.Entry(securityProfile).Property(x => x.CreatedUser).IsModified = false;
            await _employeeContext.SaveChangesAsync();
            return securityProfile;
        }
    }
}
