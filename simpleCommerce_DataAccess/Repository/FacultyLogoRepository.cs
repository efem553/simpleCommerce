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
    public class FacultyLogoRepository : Repository<FacultyLogo>, IFacultyLogoRepository
    {
        private readonly ApplicationDbContext _db;
        public FacultyLogoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(FacultyLogo facultyLogo)
        {
            if(facultyLogo!=null)
            {
                _db.FacultyLogo.Update(facultyLogo);
            }
        }
        public IEnumerable<FacultyLogo> AddRange(IEnumerable<FacultyLogo> entity)
        {
            _db.FacultyLogo.AddRange(entity);
            return entity;
        }
    }
}
