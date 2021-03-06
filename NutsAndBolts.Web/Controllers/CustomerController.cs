using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutsAndBolts.Services.Customer;
using NutsAndBolts.web.SerialIzation;
using NutsAndBolts.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutsAndBolts.web.Controllers
{
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpPost("/api/customer")]
        public ActionResult CreateCustomer([FromBody] CustomerModel customer)
        {
            _logger.LogInformation("Creating a new customer");
            customer.CreatedOn = DateTime.UtcNow;
            customer.UpdatedOn = DateTime.UtcNow;
            var customerData = CustomerMapper.SerializeCustomer(customer);
            var newCustomer = _customerService.CreateCustomer(customerData);
            return Ok(newCustomer);
        }

        [HttpGet("/api/customer")]
        public ActionResult GetCustomers()
        {
            _logger.LogInformation("Getting customers");
            var customers = _customerService.GetAllCustomers();
            var customersModels = CustomerMapper.SerializeCustomers(customers);

            return Ok(customersModels);
        }

        [HttpDelete("/api/customer/{id}")]
        public ActionResult DeleteCusomer(int id)
        {
            _logger.LogInformation("Deleting a customer");
            var response = _customerService
                .DeleteCustomers(id);
            return Ok(response);
        }
    }
}
