using StoreApp.Core.Enums;

namespace StoreApp.API.Models
{
    public class CreateProductRequest
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public int AmountInStorage { get; set; }
    }
}
