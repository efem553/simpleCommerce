using simpleCommerce_DataAccess.Data;
using simpleCommerce_DataAccess.Repository.Interface;
using simpleCommerce_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_DataAccess.Repository
{
    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        private readonly ApplicationDbContext _db;
        public PropertyRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(Property property)
        {
            _db.Property.Update(property);
        }
    }
}
