using simpleCommerce_DataAccess.Data;
using simpleCommerce_DataAccess.Repository.Interface;
using simpleCommerce_Models;

namespace simpleCommerce_DataAccess.Repository
{
    public class ProductPropertyRepository : Repository<ProductProperty>, IProductPropertyRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductPropertyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<ProductProperty> AddRange(IEnumerable<ProductProperty> entity)
        {
            _db.ProductProperty.AddRange(entity);
            return entity;
        }
        public void Update(ProductProperty productProperty)
        {
            if (productProperty is not null)
            {
                _db.ProductProperty.Update(productProperty);
            }
        }
    }
}
