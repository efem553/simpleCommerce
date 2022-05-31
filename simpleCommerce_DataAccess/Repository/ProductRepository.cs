using Microsoft.EntityFrameworkCore;
using simpleCommerce_DataAccess.Data;
using simpleCommerce_DataAccess.Repository.Interface;
using simpleCommerce_Models;
using System.Linq.Expressions;

namespace simpleCommerce_DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            _db.Product.Update(product);
        }

        public IQueryable<Product> GetAllQuery(Expression<Func<Product, bool>>? filter = null, Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null, string? includeProperties = null, bool isTracking = true)
        {
            IQueryable<Product> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }
    }
}
