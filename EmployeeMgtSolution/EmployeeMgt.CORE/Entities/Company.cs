using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeMgt.CORE.Entities
{
   public class Company : BaseClass
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar(100)"),Required]
        public string Name { get; set; }
        [Column(TypeName = "varchar(400)")]

        public string Email { get; set; }
        [Column(TypeName = "varbinary(100)")]
        public  byte[]? Logo { get; set; }
        [Column(TypeName = "varchar(1000)")]
        public string WebSite { get; set; }
        [NotMapped]
        public string CompanyLogoUrl { get; set; }

    }
}
