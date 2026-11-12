using Ecommerce_V1.Data;
using Ecommerce_V1.Interfaces;
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

        public Task<GetByIdProduct> GetProductById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
