using simpleCommerce_Models;
using System.Linq.Expressions;

namespace simpleCommerce_DataAccess.Repository.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
        public IQueryable<Product> GetAllQuery(Expression<Func<Product, bool>>? filter = null, Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null, string? includeProperties = null, bool isTracking = true);
    }
}
