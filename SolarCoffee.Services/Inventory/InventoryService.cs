using System.Collections.Generic;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Product;
using System.Linq;
using SolarCoffee.Data;
using System;
using Microsoft.Extensions.Logging;

namespace SolarCoffe.Services.Inventory {
    public class InventoryService : IInventoryService {
        private readonly SolarDbContext _db;
        private readonly ILogger<InventoryService> _logger;
        public InventoryService(SolarDbContext db, ILogger<InventoryService> logger){
            _db = db;
            _logger = logger;
        }

        public List<ProductInventory> GetCurrentInventory(){
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .Where(pi => !pi.Product.IsArchived )
                .ToList();
        }
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment){
            try{
                var inventory = _db.ProductInventories
                    .Include(inv => inv.Product)
                    .First(inv => inv.Product.Id == id);
                inventory.QuantityOnHand += adjustment;

                try {
                    CreateSnapshot();
                } catch (Exception e){
                    _logger.LogError("Error creating inventory snapshot");
                    _logger.LogError(e.StackTrace);
                }


                _db.SaveChanges();
                return new ServiceResponse<Data.Models.ProductInventory>{
                    IsSuccess = true,
                    Message = $"Product {id} inventory adjusted",
                    Time = DateTime.UtcNow,
                    Data = inventory
                };
            } catch (Exception e) {
                return new ServiceResponse<Data.Models.ProductInventory>{
                    IsSuccess = false,
                    Message = "Error while updating the inventory",
                    Time = DateTime.UtcNow,
                    Data = null
                };
            }
        }
        public ProductInventory GetInventoryByProductId(int id){

        }
        public void CreateSnapshot(){

        }
        public List<ProductInventorySnapshot> GetSnapshotHistory(){

        }
    }
}