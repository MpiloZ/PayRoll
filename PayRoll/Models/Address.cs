using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PayRoll.Models
{

    public class Address
    {
        
        [Key]
        public int ID { get; set; }
        
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        //public virtual Employee Employee { get; set; }
        //public virtual Company Company { get; set; }
    }
}