using System.Collections.Generic;
using System.Linq;
using SolarCoffe.Data;
using SolarCoffe.Data.Modules;
using System;

namespace SolarCoffe.Services.Product {
    public class ProductService : IProductService {
        private readonly SolarDbContext _db;

        public ProductService(SolarDbContext db){
            _db = db;
        }
        List<Product> GetAllProducts(){
            return _db.Products.ToList();
        }
        Product GetProductById(int id){
            return _db.Products.Find(id);

        }
        ServiceResponse<Product> CreateProduct(Product product){
            try{
                _db.Products.Add(product);
                
                var newInventory = new ProductInventory{
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity =10
                };
                _db.ProductInventories.Add(newInventory);

                _db.SafeChanges();

                return new ServiceResponse<Product>{
                    Data = product,
                    Time = DateTime.UtcNow,
                    IsSuccess = true,
                    Message = "new product added"
                };
            }
            catch(Exception e){
                return new ServiceResponse<Product>{
                    Data = product,
                    Time = DateTime.UtcNow,
                    IsSuccess = false,
                    Message = e.StackTrace
                };
            }
        }

        ServiceResponse<Product> ArchiveProduct(int id){
            try{
                var product = _db.Products.Find(id);
                product.IsArchived.set(true);
                _db.SaveChanges();
                
                return new ServiceResponse<Product>{
                    Data = product,
                    Time = DataTime.UtcNow,
                    IsSuccess = true,
                    Message = "the product was archived"
                
                };
            }
            catch(Exception e) {
                    return new ServiceResponse<Product>{
                    Data = null,
                    Time = DataTime.UtcNow,
                    IsSuccess = false,
                    Message = e.StackTrace
                
                };
            }
            }
        }
    }
}