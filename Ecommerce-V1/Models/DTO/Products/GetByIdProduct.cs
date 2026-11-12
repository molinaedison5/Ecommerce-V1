namespace Ecommerce_V1.Models.DTO.Products
{
    public class GetByIdProduct
    {
        public Guid Id { get; set; }

        public string Sku { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal PriceAmount { get; set; }

        public string PriceCurrency { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
