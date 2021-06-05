using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NutsAndBolts.Data;
using NutsAndBolts.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutsAndBolts.Services.Inventory
{
    public class InventoryService : IInventroyService
    {
        private readonly NaBDBContext _db;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(NaBDBContext dbContext, ILogger<InventoryService> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Gets a Product Inventory instance by Product ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>ProductInventory</returns>
        public ProductInventory GetByProductId(int productId)
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .FirstOrDefault(pi => pi.Product.Id == productId);
        }

        /// <summary>
        /// Returns all current inventory from the database
        /// </summary>
        /// <returns>List of Product Inventory</returns>
        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .Where(pi => !pi.Product.IsArchived)
                .ToList();
        }

        /// <summary>
        /// Returns Snapshot history for the previus 6 hours
        /// </summary>
        /// <returns></returns>
        public List<ProductInventorySnapshot> GetSnapShotHistory()
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(6);
            return _db.ProductInventorySnapshots
                .Include(snap => snap.Product)
                .Where(snap 
                    => snap.SnapshotTime > earliest
                    && !snap.Product.IsArchived)
                .ToList();
        }

        /// <summary>
        /// Updated number of Units available of the provided product id
        /// Adjust the QuantityOnHand by adjustment value
        /// </summary>
        /// <param name="id">productId</param>
        /// <param name="adjustment">number of units added/removed from inventory</param>
        /// <returns>Service Response</returns>
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            var now = DateTime.UtcNow;

            try
            {
                var inventory = _db.ProductInventories
                    .Include(inv => inv.Product)
                    .First(inv => inv.Product.Id == id);

                inventory.QuantityOnHand += adjustment;

                try
                {
                    CreateSnapshot(inventory);
                }
                catch (Exception e)
                {
                    _logger.LogError("SnapShot Failed");
                    _logger.LogError(e.StackTrace);
                }

                _db.SaveChanges();

                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = true,
                    Message = "Units updated",
                    Time = now,
                    Data = inventory
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = now,
                    Data = null
                };
            }
        }

        /// <summary>
        /// Creates a snapshot record of the data using the Product Inventory provided
        /// </summary>
        /// <param name="inv"></param>
        private void CreateSnapshot(ProductInventory inv)
        {
            var now = DateTime.UtcNow;

            var snapshot = new ProductInventorySnapshot
            {
                SnapshotTime = now,
                Product = inv.Product,
                QuantityOnHand = inv.QuantityOnHand
            };

            _db.Add(snapshot);
        }
    }
}
