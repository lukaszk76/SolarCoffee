using System.Collections.Generic;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Product;
using SolarCoffee.Services.Inventory;
using System.Linq;
using System.Data;
using SolarCoffee.Data;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace SolarCoffee.Services.Order{
    public class OrderService : IOrderService{
        private readonly SolarDbContext _db;
        private readonly ILogger _logger;
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;

        public OrderService(SolarDbContext db, 
                            ILogger logger, 
                            IProductService productService, 
                            IInventoryService inventoryService){
            _db = db;
            _logger = logger;
            _productService = productService;
            _inventoryService = inventoryService;
        }

        public List<SalesOrder> GetOrders(){
            return _db.SalesOrders
                .Include(order => order.Customer)
                    .ThenInclude(customer => customer.PrimaryAddress)
                .Include(order => order.SalesOrderItems)
                    .ThenInclude(InsufficientMemoryException => InsufficientMemoryException.Product)
                .ToList();
        }

        public ServiceResponse<bool> GenerateInvoiceForOrder(SalesOrder order){

            foreach(var item in order.SalesOrderItems ){
                //var product = _productService.GetProductById(item.Product.Id); 
                //var inventoryId = _inventoryService.GetInventoryByProductId(product.Id).Id;
                _inventoryService.UpdateUnitsAvailable(item.Product.Id, -item.Quantity);
            }
            try{
                _db.SalesOrders.Add(order);
                _db.SaveChanges();
                return new ServiceResponse<bool>{
                    IsSuccess = true,
                    Message = "The Sales order was properly recorded",
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

        public ServiceResponse<bool> MarkFullfilled(int id){
            var now = DateTime.UtcNow;
            try{
                var order = _db.SalesOrders.Find(id);
                order.IsPaid = true;
                order.UpdatedOn = now;
                _db.SalesOrders.Update(order);
                _db.SaveChanges();
                return new ServiceResponse<bool>{
                    IsSuccess = true,
                    Message = $"Sales order {id} marked as paid",
                    Time = now,
                    Data = true
                };
            }
            catch(Exception e){
                return new ServiceResponse<bool>{
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = now,
                    Data = false
                };
            }
            
        }
    }
}