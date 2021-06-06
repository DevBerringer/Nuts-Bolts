using NutsAndBolts.Data.Models;
using NutsAndBolts.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutsAndBolts.web.SerialIzation
{
    /// <summary>
    /// Handles mapping Order Data models to and from Related View Models
    /// </summary>
    public static class OrderMapper
    {
        /// <summary>
        /// Maps an InvoiceModel view model to a SalesOrder data model
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static SalesOrder SerializeInvoiceToOrder(InvoiceModel invoice)
        {
            var salesOrderItems = invoice.LineItems
                .Select(item => new SalesOrderItem
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Product = ProductMapper.SerializeProductModel(item.Product)
                }).ToList();

            return new SalesOrder
            {
                SalesOrderItems = salesOrderItems,
                CreateOn = DateTime.UtcNow,
                UpdateOn = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Maps a collection of SalesOrders to orderModels
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public static List<OrderModel> SerializeOrdersToViewModels(IEnumerable<SalesOrder> orders)
        {
            return orders.Select(order => new OrderModel 
            {
                Id = order.Id,
                CreatedOn = order.CreateOn,
                UpdatedOn = order.UpdateOn,
                SalesOrderItems = SerializesSalesOrderItems(order.SalesOrderItems),
                Customer = CustomerMapper.SerializeCustomer(order.Customer),
                IsPaid = order.IsPaid
            }).ToList();
        }

        /// <summary>
        /// Maps a collection of SalesOrderITems to a SaleOrderItemModels
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns></returns>
        private static List<SalesOrderItemModel> SerializesSalesOrderItems(IEnumerable<SalesOrderItem> orderItems)
        {
            return orderItems.Select(item => new SalesOrderItemModel
            {
                Id = item.Id,
                Quantity = item.Quantity,
                Product = ProductMapper.SerializeProductModel(item.Product)
            }).ToList();
        }

    }
}
