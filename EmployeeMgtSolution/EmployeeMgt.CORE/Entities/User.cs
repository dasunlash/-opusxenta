using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeMgt.CORE.Entities
{
   public class User : BaseClass
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar(100)"), Required]
        public string Email { get; set; }
        [Column(TypeName = "varchar(100)"), Required]
        public string Password { get; set; }
        [Column(TypeName = "varchar(400)"), Required]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(100)"), Required]
        public string LastName { get; set; }
        public int SecurityProfileId { get; set; }
        [ForeignKey("SecurityProfileId")]
        public SecurityProfile SecurityProfile { get; set; }
    }
}
