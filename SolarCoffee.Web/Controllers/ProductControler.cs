using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Product;
using SolarCoffee.Web.Serialization;
using System.Linq;

namespace SolarCoffe.Web.Controlers {
    [ApiController]
    public class ProductControler : ControllerBase{
        private readonly ILogger<ProductControler> _logger;
        private readonly IProductService _productService;

        public ProductControler(ILogger<ProductControler> logger, IProductService productService) {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("/api/product")]
        public ActionResult GetProduct(){
            _logger.LogInformation("Getting all products");
            var products = _productService.GetAllProducts();
            var productViewModels = products.Select(product => ProductMapper.SerializeProductModel(product));
            return Ok(productViewModels);
        }
    }
}