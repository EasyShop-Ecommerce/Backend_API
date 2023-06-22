using EasyShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Core.Specifications
{
    public class GetSubcategoriesOfCategory:BaseSpecification<Category>
    {
        public GetSubcategoriesOfCategory(int id):base(category=>category.Id==id)
        {
            AddIncludes(c => c.SubCategories);  
        }
    }
}
