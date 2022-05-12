using simpleCommerce_Models;

namespace simpleCommerce_DataAccess.Repository.Interface
{
    public interface IPictureRepository : IRepository<Picture>
    {
        void Update(Picture picture);

        IEnumerable<Picture> AddRange(IEnumerable<Picture> entity);
    }
}
