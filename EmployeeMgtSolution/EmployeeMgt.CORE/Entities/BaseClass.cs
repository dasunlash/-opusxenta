using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeMgt.CORE.Entities
{
   public class BaseClass
    {
        public bool IsActive { get; set; }
        public int CreatedUser { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime ? ModifiedDate { get; set; }
        public int? ModifiedUser { get; set; }
    }
}
