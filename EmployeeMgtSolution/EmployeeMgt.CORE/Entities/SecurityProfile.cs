using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeMgt.CORE.Entities
{
   public class SecurityProfile : BaseClass
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar(100)"), Required]
        public string Name { get; set; }
        [DefaultValue("false")]
        public bool IsCreate { get; set; }
        [DefaultValue("false")]
        public bool IsRead { get; set; }
        [DefaultValue("false")]
        public bool IsUpdate { get; set; }
        [DefaultValue("false")]
        public bool IsDelete { get; set; }
    }
}
