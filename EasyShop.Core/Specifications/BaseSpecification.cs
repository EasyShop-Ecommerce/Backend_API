using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace EasyShop.Core.Specifications
{
    public class BaseSpecification<T>:ISpecification<T>
    {
        public BaseSpecification() { }
       
        public BaseSpecification(Expression<Func<T,bool>> _criteria)
        {
            Criteria = _criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        protected void AddIncludes(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
