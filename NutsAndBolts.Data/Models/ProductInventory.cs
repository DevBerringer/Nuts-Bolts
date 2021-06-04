using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutsAndBolts.Data.Models
{
    public class ProductInventory
    {
        public int Id { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public int QuantityOnHand { get; set; }
        public int IdealQuantity { get; set; }

        public Product Product { get; set; }

    }
}
