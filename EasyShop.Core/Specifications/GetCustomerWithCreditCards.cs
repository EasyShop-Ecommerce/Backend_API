using EasyShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Core.Specifications
{
    public class GetCustomerWithCreditCards:BaseSpecification<Customer>
    {
        public GetCustomerWithCreditCards() 
        {
            AddIncludes(x => x.CreditCards);
            AddIncludes(x => x.Orders);
        }
        public GetCustomerWithCreditCards(int id):base(x=>x.Id==id)
        {
            AddIncludes(x => x.CreditCards);
            AddIncludes(x => x.Orders);
        }
    }
}
