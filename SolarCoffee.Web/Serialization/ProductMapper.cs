using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization {
    public class ProductMapper{
        public static ProductViewModel SerializeProductModel(Data.Models.Product product){
            return new ProductViewModel{
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }   

        public static Data.Models.Product SerializeProductModel(ProductViewModel product){
            return new Data.Models.Product {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        } 
    }
}