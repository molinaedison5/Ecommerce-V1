using Ecommerce_V1.Data;
using Ecommerce_V1.Interfaces;
using Ecommerce_V1.Models;
using Ecommerce_V1.Models.DTO.Products;
using Microsoft.Data.SqlClient;
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
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.IsActive)
                .Select(p => new GetAllProducts
                {
                    Id = p.Id,
                    Sku = p.Sku,
                    Name = p.Name,
                    Description = p.Description,
                    PriceAmount = p.PriceAmount,
                    PriceCurrency = p.PriceCurrency,
                    IsActive = p.IsActive
                })
                .ToListAsync();
        }

        public async Task<GetByIdProduct?> GetProductById(Guid id)
        {
            var productById = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

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
            var skuAlreadyExists = await _context.Products
                .AnyAsync(p => p.Sku == product.Sku);

            if (skuAlreadyExists)
            {
                throw new DuplicateSkuException();
            }

            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                Sku = product.Sku,
                Name = product.Name,
                Description = product.Description,
                PriceAmount = product.PriceAmount,
                PriceCurrency = product.PriceCurrency,
                IsActive = true
            };

            _context.Products.Add(newProduct);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (IsDuplicateSku(ex))
            {
                throw new DuplicateSkuException(ex);
            }

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

        // 2601: duplicate key on a unique index. 2627: unique constraint violation.
        private static bool IsDuplicateSku(DbUpdateException ex)
        {
            return ex.InnerException is SqlException sqlException
                && (sqlException.Number == 2601 || sqlException.Number == 2627);
        }
    }
}
