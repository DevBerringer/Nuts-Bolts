using Microsoft.EntityFrameworkCore;
using NutsAndBolts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutsAndBolts.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly NaBDBContext _db;

        public CustomerService (NaBDBContext dbContext)
        {
            _db = dbContext;
        }

        /// <summary>
        /// Adds a new Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>ServiceResponce</returns>
        public ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer)
        {
            var now = DateTime.UtcNow;

            try
            {
                _db.Customers.Add(customer);
                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Customer>
                {
                    IsSuccess = true,
                    Message = "New customer added",
                    Time = now,
                    Data = customer
                };
            } 
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Customer>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = now,
                    Data = null
                };
            }
        }
        /// <summary>
        /// Deletes a customer record
        /// </summary>
        /// <param name="id">int customer primary key</param>
        /// <returns>ServiceResponce</returns>\
        public ServiceResponse<bool> DeleteCustomers(int id)
        {
            var customer = _db.Customers.Find(id);
            var now = DateTime.UtcNow;

            if (customer == null)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Customer Not Found",
                    Time = now,
                    Data = false
                };
            }
            try
            {
                _db.Customers.Remove(customer);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Message = "Customer Deleted",
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
        /// Returns a List of Customers from the database
        /// </summary>
        /// <returns>List of <Customer> including primary address</Customer></returns>
        public List<Data.Models.Customer> GetAllCustomers()
        {
            return _db.Customers
                .Include(customer => customer.PrimaryAddress)
                .OrderBy(customer => customer.LastName)
                .ToList();

        }

        /// <summary>
        /// Gets a customer recor by primary key
        /// </summary>
        /// <param name="id">customer primary key</param>
        /// <returns>Customer</returns>
        public Data.Models.Customer GetById(int id)
        {
            return _db.Customers.Find(id);

        }
    }
}
