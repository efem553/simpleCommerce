using simpleCommerce_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_DataAccess.Repository.Interface
{
    public interface IOrderLineRepository:IRepository<OrderLine>
    {
        public void Update(OrderLine orderLine);
        IEnumerable<OrderLine> AddRange(IEnumerable<OrderLine> entity);
    }
}
