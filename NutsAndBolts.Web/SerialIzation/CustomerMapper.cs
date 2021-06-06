using NutsAndBolts.Data.Models;
using NutsAndBolts.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutsAndBolts.web.SerialIzation
{
    public class CustomerMapper
    {
        /// <summary>
        /// Serialized a Customer data model into a CustomerModel view model
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static CustomerModel SerializeCustomer(Customer customer)
        {
            return new CustomerModel
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = mapCustomerAddress(customer.PrimaryAddress)
            };
        }

        /// <summary>
        /// Serializes a CustomerModel view model into a Customer data model
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static Customer SerializeCustomer(CustomerModel customer)
        {
            return new Customer
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = mapCustomerAddress(customer.PrimaryAddress)
            };
        }

        public static List<CustomerModel> SerializeCustomers(IEnumerable<Customer> customers)
        {
            return customers.Select(customer => new CustomerModel
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PrimaryAddress = CustomerMapper
                    .mapCustomerAddress(customer.PrimaryAddress),
                    CreatedOn = customer.CreatedOn,
                    UpdatedOn = customer.UpdatedOn
                })
                .OrderByDescending(customer => customer.CreatedOn)
                .ToList();
        }

        /// <summary>
        /// Maps a Customer Address data model to a CustomerAddressModel view model
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static CustomerAddressModel mapCustomerAddress(CustomerAddress address)
        {
            return new CustomerAddressModel
            {
                Id = address.Id,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                AptNum = address.AptNum,
                City = address.City,
                State = address.State,
                Country = address.Country,
                CreatedOn = address.CreatedOn,
                UpdatedOn = address.UpdatedOn
            };
        }
        /// <summary>
        /// Maps a CustomerAddressModel view model to a CustomerAddress data model
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static CustomerAddress mapCustomerAddress(CustomerAddressModel address)
        {
            return new CustomerAddress
            {
                Id = address.Id,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                AptNum = address.AptNum,
                City = address.City,
                State = address.State,
                Country = address.Country,
                CreatedOn = address.CreatedOn,
                UpdatedOn = address.UpdatedOn
            };
        }
    }
}
