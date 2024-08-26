using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Core.Models
{
    public class Order
    {
        [BsonId]
        [Key]
        public int OrderId { get; set; }
        public int BuyerId { get; set; }
        [ForeignKey("BuyerId")]
        public Buyer Buyer { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int SellerId { get; set; }
        [ForeignKey("SellerId")]
        public Seller Seller { get; set; }
        public int Quantity { get; set; }
        public DateTime DateTime { get; set; }
        public Order(Buyer buyer, Product product, int quantity, Seller seller)
        {
            BuyerId = buyer.BuyerId;
            ProductId = product.ProductId;
            SellerId = seller.SellerId;
            Quantity = quantity;
            DateTime = DateTime.Now;
        }

        public Order() { }
    }
}
