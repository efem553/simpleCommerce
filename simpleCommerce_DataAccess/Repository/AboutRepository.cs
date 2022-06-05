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
    public class AboutRepository:Repository<About>,IAboutRepository
    {
        private readonly ApplicationDbContext _db;
        public AboutRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(About about)
        {
            if(about!=null)
            {
                _db.About.Update(about);
            }
        }
    }
}
