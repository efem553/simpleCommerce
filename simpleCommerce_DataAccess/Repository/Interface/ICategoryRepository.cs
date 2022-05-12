using simpleCommerce_Models;

namespace simpleCommerce_DataAccess.Repository.Interface
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
