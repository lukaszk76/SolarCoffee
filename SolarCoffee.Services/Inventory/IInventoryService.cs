using System.Collections.Generic;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Product;

namespace SolarCoffee.Services.Inventory {
    public interface IInventoryService {
        public List<ProductInventory> GetCurrentInventory(); 
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment);
        public ProductInventory GetInventoryByProductId(int id);
        public List<ProductInventorySnapshot> GetSnapshotHistory();
    }
}