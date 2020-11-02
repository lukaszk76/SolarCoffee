using System.Collections.Generic;
using System.Linq;
using SolarCoffee.Data;
using SolarCoffee.Data.Modules;
using System;

namespace SolarCoffee.Services.Product {
    public class ProductService : IProductService {
        private readonly SolarDbContext _db;

        public ProductService(SolarDbContext db){
            _db = db;
        }
        public List<Data.Modules.Product> GetAllProducts(){
            return _db.Products.ToList();
        }
        public Data.Modules.Product GetProductById(int id){
            return _db.Products.Find(id);

        }
        public ServiceResponse<Data.Modules.Product> CreateProduct(Data.Modules.Product product){
            try{
                _db.Products.Add(product);
                
                var newInventory = new ProductInventory{
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity =10
                };
                _db.ProductInventories.Add(newInventory);

                _db.SaveChanges();

                return new ServiceResponse<Data.Modules.Product>{
                    Data = product,
                    Time = DateTime.UtcNow,
                    IsSuccess = true,
                    Message = "new product added"
                };
            }
            catch(Exception e){
                return new ServiceResponse<Data.Modules.Product>{
                    Data = product,
                    Time = DateTime.UtcNow,
                    IsSuccess = false,
                    Message = e.StackTrace
                };
            }
        }

        public ServiceResponse<Data.Modules.Product> ArchiveProduct(int id){
            try{
                var product = _db.Products.Find(id);
                product.IsArchived.set(true);
                _db.SaveChanges();
                
                return new ServiceResponse<Data.Modules.Product>{
                    Data = product,
                    Time = DataTime.UtcNow,
                    IsSuccess = true,
                    Message = "the product was archived"
                
                };
            }
            catch(Exception e) {
                    return new ServiceResponse<Data.Modules.Product>{
                    Data = null,
                    Time = DataTime.UtcNow,
                    IsSuccess = false,
                    Message = e.StackTrace
                
                };
            }
        }
    }
}
