using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffe.Services.Products;

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
            _productService.GetAllProducts();
            return Ok("");
        }
    }
}