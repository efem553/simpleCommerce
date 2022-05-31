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
    public class OrderRepository:Repository<Order>,IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Order order)
        {
            if(order!=null)
            {
                _db.Order.Update(order);
            }
        }
    }
}
