using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Core.Models
{
    public class Buyer : User
    {
        [Key]
        public int BuyerId { get; set; }
        public bool IsInLoyaltyProgram { get; set; }

        public Buyer(string name, string surname, string email, string phoneNumber, bool isInLoyaltyProgram) : base(name, surname, email, phoneNumber)
        {
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
            IsInLoyaltyProgram = isInLoyaltyProgram;
        }

        public Buyer() { }
    }
}
