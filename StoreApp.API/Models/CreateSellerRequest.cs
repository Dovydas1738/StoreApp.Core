using StoreApp.Core.Enums;

namespace StoreApp.API.Models
{
    public class CreateSellerRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public SellerPosition Position { get; set; }
    }
}
