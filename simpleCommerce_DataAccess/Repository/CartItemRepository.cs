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
    public class CartItemRepository:Repository<CartItem>,ICartItemRepository
    {
        private readonly ApplicationDbContext _db;
        public CartItemRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(CartItem cartItem)
        {
            if (cartItem!=null)
            {
                _db.CartItem.Update(cartItem);
            }
        }
    }
}
