using simpleCommerce_DataAccess.Data;
using simpleCommerce_DataAccess.Repository;
using simpleCommerce_DataAccess.Repository.Interface;
using simpleCommerce_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_DataAccess.Repository
{

     public class CartRepository :Repository<Cart>,ICartRepository
    {
        private readonly ApplicationDbContext _db;

        public CartRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Cart cart)
        {
            if(cart !=null)
            {
                _db.Cart.Update(cart);
            }
        }
    }
}
