using simpleCommerce_Models;

namespace simpleCommerce_DataAccess.Repository.Interface
{
    public interface IPropertyRepository : IRepository<Property>
    {
        void Update(Property property);
    }
}
