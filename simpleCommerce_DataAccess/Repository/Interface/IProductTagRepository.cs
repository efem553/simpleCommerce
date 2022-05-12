using simpleCommerce_Models;

namespace simpleCommerce_DataAccess.Repository.Interface
{
    public interface IProductTagRepository : IRepository<ProductTag>
    {
        public void Update(ProductTag entity);
        public IEnumerable<ProductTag> AddRange(IEnumerable<ProductTag> entity);
    }
}
