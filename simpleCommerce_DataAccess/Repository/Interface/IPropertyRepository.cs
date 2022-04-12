using simpleCommerce_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_DataAccess.Repository.Interface
{
    public interface IPropertyRepository:IRepository<Property>
    {
        void Update(Property property);
    }
}
