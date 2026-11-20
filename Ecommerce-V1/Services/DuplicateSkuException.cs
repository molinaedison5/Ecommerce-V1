namespace Ecommerce_V1.Services
{
    public class DuplicateSkuException : Exception
    {
        public DuplicateSkuException(Exception? innerException = null)
            : base("A product with this SKU already exists.", innerException)
        {
        }
    }
}
