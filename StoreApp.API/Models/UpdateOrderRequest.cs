namespace StoreApp.API.Models
{
    public class UpdateOrderRequest
    {
        public int OrderId { get; set; }
        public int BuyerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int SellerId { get; set; }
    }
}
