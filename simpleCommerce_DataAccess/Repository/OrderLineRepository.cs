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
    public class OrderLineRepository:Repository<OrderLine>,IOrderLineRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderLineRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(OrderLine orderLine)
        {
            if(orderLine!=null)
            {
                _db.OrderLine.Update(orderLine);
            }
        }
        public IEnumerable<OrderLine> AddRange(IEnumerable<OrderLine> entity)
        {
            _db.OrderLine.AddRange(entity);
            return entity;
        }
    }
}
