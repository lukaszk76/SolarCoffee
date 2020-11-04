using System.Collections.Generic;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Product;
using SolarCoffee.Data;
using System;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace SolarCoffee.Services.Inventory {
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
                    CreateSnapshot(inventory);
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
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = null
                };
            }
        }
        public ProductInventory GetInventoryByProductId(int id){
            return _db.ProductInventories
                .Include(inv => inv.Product)
                .FirstOrDefault(inv => inv.Product.Id == id);
        }
        
        //returns a history of product inventory snapshots for last 6 hours and for products which are not archived
        public List<ProductInventorySnapshot> GetSnapshotHistory(){
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(6);
            
            return _db.ProductInventories
                .Include(inv => inv.Product)
                .Where(inv => inv.SnapshotTime > earliest 
                                && !inv.Product.IsArchived)
                .ToList();
        }

        private void CreateSnapshot(ProductInventory inventory){
        
            var snapshot = new ProductInventorySnapshot{
                SnapshotTime = DateTime.UtcNow,
                QuantityOnHand = inventory.QuantityOnHand,
                Product = inventory.Product
            };
            _db.ProductInventorySnapshots.Add(snapshot);
        }
    }
} 