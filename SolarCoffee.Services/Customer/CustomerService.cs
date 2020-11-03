using System.Collections.Generic;
using SolarCoffee.Services.Product;
using System.Linq;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using System;


namespace SolarCoffee.Services.Customer {
    public class CustomerService : ICustomerService {
        private readonly SolarDbContext _db;

        public CustomerService(SolarDbContext db){
            _db = db;
        }
        public List<Data.Models.Customer> GetAllCustomers(){
            return _db.Customers
                .Include(customer => customer.PrimaryAddress)
                .OrderBy(customer => customer.LastName)
                .ToList();
        }
        public Data.Models.Customer GetCustomerById(int id) {
            return _db.Cusomers.Find(id);
        } 
        public ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer){
            try {
                _db.Customers.Add(customer);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Customer>{
                    IsSuccess = true,
                    Message = "Customer was created",
                    Time = DateTime.UtcNow,
                    Data = customer
                };
            } 
            catch (Exception e) {
                return new ServiceResponse<Data.Models.Customer>{
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = null
                };
            }
        }
        public ServiceResponse<bool> DeleteCustomer(int id){
            var customer = _db.Customers.Find(id);
            if (customer == null) {
                return new ServiceResponse<bool>{
                    IsSuccess = false,
                    Message = "Customer to delet not found",
                    Time = DateTime.UtcNow,
                    Data = false
                };
            } 

            try {
                _db.Cusomers.Remove(customer);
                _db.SaveChanges();
                return new ServiceResponse<bool>{
                    IsSuccess = true,
                    Message = "Customer deleted",
                    Time = DateTime.UtcNow,
                    Data = true
                };
            } 
            catch (Exception e) {
                return new ServiceResponse<bool>{
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = false
                };
            }
        }
    }
}