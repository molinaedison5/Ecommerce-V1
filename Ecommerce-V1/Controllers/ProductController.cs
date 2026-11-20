using Ecommerce_V1.Interfaces;
using Ecommerce_V1.Models.DTO.Products;
using Ecommerce_V1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productService;

        public ProductController(IProduct productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProduct product)
        {
            try
            {
                var createdProduct = await _productService.CreateProduct(product);
                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (DuplicateSkuException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}
