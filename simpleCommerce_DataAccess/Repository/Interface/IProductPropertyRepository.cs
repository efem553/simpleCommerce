using simpleCommerce_Models;

namespace simpleCommerce_DataAccess.Repository.Interface
{
    public interface IProductPropertyRepository : IRepository<ProductProperty>
    {
        void Update(ProductProperty productProperty);
        public IEnumerable<ProductProperty> AddRange(IEnumerable<ProductProperty> entity);
    }
}
