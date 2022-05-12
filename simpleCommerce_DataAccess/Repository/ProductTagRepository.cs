using simpleCommerce_DataAccess.Data;
using simpleCommerce_DataAccess.Repository.Interface;
using simpleCommerce_Models;

namespace simpleCommerce_DataAccess.Repository
{
    public class ProductTagRepository : Repository<ProductTag>, IProductTagRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductTagRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<ProductTag> AddRange(IEnumerable<ProductTag> entity)
        {
            _db.ProductTag.AddRange(entity);
            return entity;
        }

        public void Update(ProductTag entity)
        {
            _db.ProductTag.Update(entity);
        }
    }
}
