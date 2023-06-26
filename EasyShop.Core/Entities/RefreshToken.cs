using EasyShop.Core.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Core.Entities
{
    
        public class RefreshToken
        {
            public Guid Id { get; set; }
            public string Token { get; set; }
            public DateTime Expiration { get; set; }

            [ForeignKey("AppUser")]
            public int UserId { get; set; }
            public AppUser AppUser { get; set; }
        }

    
}
