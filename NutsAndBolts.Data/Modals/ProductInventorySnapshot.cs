using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutsAndBolts.Data.Modals
{
    public class ProductInventorySnapshot
    {
        public int Id { get; set; }
        public DateTime SnapShotTime { get; set; }
        public int QuantityOnHand { get; set; }
        public Product Product { get; set; }
    }
}
