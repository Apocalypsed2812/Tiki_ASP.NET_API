using System.Linq.Expressions;
using ToDoAPI.Data;

namespace ToDoAPI.Specifications.ProductSpecification
{
    public class ProductSpecification : Specification<Product>
    {
        private readonly int _price;

        public ProductSpecification(int price) {
            _price = price;
        }

        public override Expression<Func<Product, bool>> ToExpression()
        {
            return product => product.Price > _price;
        }
    }
}
