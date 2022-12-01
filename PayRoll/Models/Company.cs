using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PayRoll.Models
{
    public class Company
    {
        public Company()
        {
            Employees = new List<Employee>();
        }

        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual List<Employee> Employees { get; set; }
        
        [ForeignKey("BusinessAddress")]
        public int BusinessAddressID { get; set; }

        public virtual Address BusinessAddress { get; set; }

    }
}