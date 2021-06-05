using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NutsAndBolts.Data;
using NutsAndBolts.Data.Models;
using NutsAndBolts.Services.Product;
using NutsAndBolts.Services.Inventory;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutsAndBolts.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly NaBDBContext _db;
        private readonly ILogger<OrderService> _logger;
        private readonly IProductService _productService;
        private readonly IInventroyService _inventoryService;

        public OrderService(
            NaBDBContext dbContext, 
            ILogger<OrderService> logger,
            IProductService productService,
            IInventroyService inventoryService)
        {
            _db = dbContext;
            _logger = logger;
            _productService = productService;
            _inventoryService = inventoryService;
        }
        /// <summary>
        /// Creates an open SalesOrder
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ServiceResponse<bool> GenerateOpenOrder(SalesOrder order)
        {
            var now = DateTime.UtcNow;

            foreach(var item in order.SalesOrderItems) {
                item.Product = _productService
                    .GetProductById(item.Product.Id);
                
                var inventoryId = _inventoryService
                    .GetByProductId(item.Product.Id).Id;

                _inventoryService
                    .UpdateUnitsAvailable(inventoryId, -item.Quantity);
            }

            try
            {
                _db.SalesOrders.Add(order);
                _db.SaveChanges();

                _logger.LogInformation("Generating new order");

                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Message = "Order opened",
                    Time = now,
                    Data = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = now,
                    Data = false
                };
            }
        }

        /// <summary>
        /// Gets all sales order from the data base
        /// </summary>
        /// <returns>List of orders </returns>
        public List<SalesOrder> GetOrders()
        {
            return _db.SalesOrders
                .Include(so => so.Customer)
                    .ThenInclude(customer => customer.PrimaryAddress)
                .Include(so => so.SalesOrderItems)
                    .ThenInclude(item => item.Product)
                .ToList();
        }

        /// <summary>
        /// Marks and opren SalesOrder as paid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            var now = DateTime.UtcNow;

            var order = _db.SalesOrders.Find(id);
            order.UpdateOn = now;
            order.IsPaid = true;

            try
            {
                _db.SalesOrders.Update(order);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Message = $"Order {order.Id} closed invoice paid in full",
                    Time = now,
                    Data = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = now,
                    Data = false
                };
            }

        }
    }
}
