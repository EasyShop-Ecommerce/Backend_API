using EasyShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Core.Specifications
{
    public class GetProductsOfSubcategory:BaseSpecification<SubCategory>
    {
        public GetProductsOfSubcategory(int id) :base(sub=>sub.Id==id)
        {
            AddIncludes(sub => sub.Products);
            AddIncludes(sub => sub.Category);
        }
    }
}
