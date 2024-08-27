using StoreApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StoreApp.Core.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProductCategory Category { get; set; }
        public int AmountInStorage { get; set; }

        public Product(string productName, decimal price, ProductCategory category, int amountInStorage)
        {
            ProductName = productName;
            Price = price;
            Category = category;
            AmountInStorage = amountInStorage;
        }

        public Product() { }

        public Product(int productId)
        {
            ProductId = productId;
        }
    }
}
