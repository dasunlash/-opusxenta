﻿using EmployeeMgt.CORE.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeMgt.CORE.Entities
{
    public class Employee: BaseClass
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar(100)"), Required]
        public string FirstName { get; set; }
        [Column(TypeName = "varchar(100)"), Required]
        public string LastName { get; set; }
        [Column(TypeName = "varchar(400)"), Required]
        public string Email { get; set; }
        [Column(TypeName = "varchar(12)"), Required]
        public string Phone { get; set; }
        public string Skills { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
        
    }
}
