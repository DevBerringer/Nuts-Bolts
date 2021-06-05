using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutsAndBolts.Services.Customer
{
    public interface ICustomerService
    {
        List<Data.Models.Customer> GetAllCustomers();

        ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer);

        ServiceResponse<bool> DeleteCustomers(int id);

        Data.Models.Customer GetById(int id);
    }
}
