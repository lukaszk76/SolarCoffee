using System.Collections.Generic;

namespace SolarCoffee.Services.Product
{
    public interface IProductService{
        public List<Data.Modules.Product> GetAllProducts();
        public Data.Modules.Product GetProductById(int id);
        public ServiceResponse<Data.Modules.Product> CreateProduct(Data.Modules.Product product);
        public ServiceResponse<Data.Modules.Product> ArchiveProduct(int id);
    }   

}