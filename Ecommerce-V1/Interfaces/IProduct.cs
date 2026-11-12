using Ecommerce_V1.Models.DTO.Products;

namespace Ecommerce_V1.Interfaces
{
    public interface IProduct
    {
        Task<List<GetAllProducts>> GetAllProducts();
        Task<GetByIdProduct> GetProductById(Guid id);

    }
}
