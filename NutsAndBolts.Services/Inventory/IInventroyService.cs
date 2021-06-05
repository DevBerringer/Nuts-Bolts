using NutsAndBolts.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutsAndBolts.Services.Inventory
{
    public interface IInventroyService
    {
        public List<ProductInventory> GetCurrentInventory();
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment);
        public ProductInventory GetByProductId(int productId);
        public void CreateSnapshot();
        public List<ProductInventorySnapshot> GetSnapShotHistory();
    }
}
