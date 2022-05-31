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
    public class ProvinceRepository:Repository<Province>,IProvinceRepository
    {
        private readonly ApplicationDbContext _db;
        public ProvinceRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Province province)
        {
           if(province!=null)
            {
                _db.Province.Update(province);
            }
        }
    }
}
