using simpleCommerce_Models;

namespace simpleCommerce_DataAccess.Repository.Interface
{
    public interface ITagRepository : IRepository<Tag>
    {
        void Update(Tag tag);
    }
}
