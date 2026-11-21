using System.ComponentModel.DataAnnotations;

namespace Ecommerce_V1.Models.DTO.Products
{
    public class UpdateProduct : IValidatableObject
    {
        [Required]
        [StringLength(64)]
        public string Sku { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(2000)]
        public string Description { get; set; } = null!;

        // decimal(12,2): greater than 0, at most 10 digits before the decimal point.
        [Range(typeof(decimal), "0.01", "9999999999.99",
            ParseLimitsInInvariantCulture = true,
            ConvertValueInInvariantCulture = true)]
        public decimal PriceAmount { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string PriceCurrency { get; set; } = null!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PriceAmount != decimal.Round(PriceAmount, 2))
            {
                yield return new ValidationResult(
                    "Price must have at most 2 decimal places.",
                    new[] { nameof(PriceAmount) });
            }
        }
    }
}
