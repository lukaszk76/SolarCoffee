using System.Collections.Generic;
using SolarCoffee.Services.Product;

namespace SolarCoffee.Services.Customer {
    public interface ICustomerService{
        public List<Data.Models.Customer> GetAllCustomers();
        public Data.Models.Customer GetCustomerById(int id); 
        public ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer);
        public ServiceResponse<bool> DeleteCustomer(int id);
    }
}