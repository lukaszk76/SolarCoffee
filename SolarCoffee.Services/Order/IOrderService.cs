using System.Collections.Generic;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Product;

namespace SolarCoffee.Services.Order{
    public interface IOrderService{
        public List<SalesOrder> GetOrders();
        public ServiceResponse<bool> GenerateInvoiceForOrder(SalesOrder order);
        public ServiceResponse<bool> MarkFullfilled(int id);
    }
}