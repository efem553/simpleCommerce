using simpleCommerce_DataAccess.Data;
using simpleCommerce_DataAccess.Repository.Interface;
using simpleCommerce_Models;

namespace simpleCommerce_DataAccess.Repository
{
    public class PictureRepository : Repository<Picture>, IPictureRepository
    {
        private readonly ApplicationDbContext _db;
        public PictureRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Picture> AddRange(IEnumerable<Picture> entity)
        {
            _db.Picture.AddRange(entity);
            return entity;
        }

        public void Update(Picture picture)
        {
            _db.Picture.Update(picture);
        }
    }
}
