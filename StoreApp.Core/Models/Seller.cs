using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Core.Enums;

namespace StoreApp.Core.Models
{
    public class Seller : User
    {
        [Key]
        public int SellerId { get; set; }
        public SellerPosition Position { get; set; }

        public Seller(string name, string surname, string email, string phoneNumber, SellerPosition position) : base(name, surname, email, phoneNumber)
        {
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
            Position = position;
        }

        public Seller() { }
    }
}
