using EasyShop.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Core.Identity
{
    public class AppUser:IdentityUser
    {
        
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Government { get; set; }

        public string BusinessName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string Token { get; set; }


        [ForeignKey("customer")]
        public int? CustomerID { get; set; }
        public Customer customer { get; set; }

        [ForeignKey("seller")]
        public int? SellerID { get; set; }
        public Seller seller { get; set; }
    }
}
