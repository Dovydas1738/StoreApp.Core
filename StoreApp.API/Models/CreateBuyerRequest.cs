namespace StoreApp.API.Models
{
    public class CreateBuyerRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsInLoyaltyProgram { get; set; }
    }
}
