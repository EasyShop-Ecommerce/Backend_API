using EasyShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Core.Specifications
{
    public class GetOrderWithDetails:BaseSpecification<Order>
    {
        public GetOrderWithDetails()
        {
            AddIncludes(o => o.Customer);
            AddIncludes(o => o.Status);
            AddIncludes(o => o.PaymentMethod);
            AddIncludes(o => o.Product);
            AddIncludes(o => o.Seller);
        }
        public GetOrderWithDetails(int id):base(o=>o.Id==id)
        {
            AddIncludes(o => o.Customer);
            AddIncludes(o => o.Status);
            AddIncludes(o => o.PaymentMethod);
            AddIncludes(o => o.Product);
            AddIncludes(o => o.Seller);
        }
    }
}
