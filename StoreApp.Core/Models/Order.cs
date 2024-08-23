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
        [Key]
        public int OrderId { get; set; }
        [ForeignKey("BuyerId")]
        public Buyer Buyer { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime DateTime { get; set; }
        [ForeignKey("SellerId")]
        public Seller Seller { get; set; }

        public Order(int orderId, Buyer buyer, Product product, int quantity, Seller seller)
        {
            OrderId = orderId;
            Buyer = buyer;
            Product = product;
            Quantity = quantity;
            DateTime = DateTime.Now;
            Seller = seller;
        }

        public Order() { }
    }
}
