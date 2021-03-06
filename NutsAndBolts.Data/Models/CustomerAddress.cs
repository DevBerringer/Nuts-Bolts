using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutsAndBolts.Data.Models
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        [MaxLength(100)]
        public string AddressLine1 { get; set; }

        [MaxLength(100)]
        public string AddressLine2 { get; set; }
        
        [MaxLength(4)]
        public string AptNum { get; set; }
        
        [MaxLength(60)]
        public string City { get; set; }
        
        [MaxLength(2)]
        public string State { get; set; }
        
        [MaxLength(10)]
        public string PostalCode { get; set; }
        
        [MaxLength(50)]
        public string Country { get; set; }
    }
}
