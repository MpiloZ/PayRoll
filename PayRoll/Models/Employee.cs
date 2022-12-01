using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PayRoll.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Position { get; set; }

        public virtual Address HomeAddress { get; set; }
        [ForeignKey("HomeAddress")]
        public int? HomeAddressID { get; set; }

        public virtual Company Company { get; set; }
        [ForeignKey("Company")]
        public int? CompanyID { get; set; }
    }
}