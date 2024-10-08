﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using StoreApp.Core.Enums;

namespace StoreApp.Core.Models
{
    public class Seller : User
    {
        [BsonId]
        [Key]
        public int SellerId { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
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

        public Seller(int sellerId)
        {
            SellerId = sellerId;
        }

    }
}
