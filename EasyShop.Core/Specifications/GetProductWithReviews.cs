using EasyShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Core.Specifications
{
    public class GetProductWithReviews:BaseSpecification<Product>
    {
        public GetProductWithReviews() 
        {
            AddIncludes(p => p.Reviews);
        }

        public GetProductWithReviews(int id):base(p=>p.Id==id)
        {
            AddIncludes(p => p.Reviews);
        }
    }
}
