using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutsAndBolts.Services.Product;
using NutsAndBolts.web.SerialIzation;
using System.Linq;

namespace NutsAndBolts.web.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("/api/product")]
        public ActionResult GetProduct()
        {
            _logger.LogInformation("Getting all Products");
            var products = _productService.GetAllProducts();
            var productViewModels = products
                .Select(product => ProductMapper.SerializeProductModel(product));

            return Ok(productViewModels);
        }
    }
}
