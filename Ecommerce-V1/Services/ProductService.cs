using Ecommerce_V1.Data;
using Ecommerce_V1.Interfaces;
using Ecommerce_V1.Models;
using Ecommerce_V1.Models.DTO.Products;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_V1.Services
{
    public class ProductService : IProduct
    {
        private readonly EcommerceDbContext _context;

        public ProductService(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllProducts>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products.Select(p => new GetAllProducts
            {
                Id = p.Id,
                Sku = p.Sku,
                Name = p.Name,
                Description = p.Description,
                PriceAmount = p.PriceAmount,
                PriceCurrency = p.PriceCurrency,
                IsActive = p.IsActive
            }).ToList();
        }

        public async Task<GetByIdProduct> GetProductById(Guid id)
        {
            var productById = await _context.Products.FindAsync(id);
            if (productById == null)
            {
                return null;
            }

            return new GetByIdProduct
            {
                Id = productById.Id,
                Sku = productById.Sku,
                Name = productById.Name,
                Description = productById.Description,
                PriceAmount = productById.PriceAmount,
                PriceCurrency = productById.PriceCurrency,
                IsActive = productById.IsActive
            };


        }

        public async Task<GetByIdProduct> CreateProduct(CreateProduct product)
        {
            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                Sku = product.Sku,
                Name = product.Name,
                Description = product.Description,
                PriceAmount = product.PriceAmount,
                PriceCurrency = product.PriceCurrency,
                IsActive = product.IsActive
            };
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return new GetByIdProduct
            {
                Id = newProduct.Id,
                Sku = newProduct.Sku,
                Name = newProduct.Name,
                Description = newProduct.Description,
                PriceAmount = newProduct.PriceAmount,
                PriceCurrency = newProduct.PriceCurrency,
                IsActive = newProduct.IsActive
            };
        }
    }
}
