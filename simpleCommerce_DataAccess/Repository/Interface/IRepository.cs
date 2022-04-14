using System.Linq.Expressions;

namespace simpleCommerce_DataAccess.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        T Find(Guid guid);

        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
                              Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                              string includeProperties = null,
                              bool isTracking = true
            );


        T FirstOrDefault(Expression<Func<T, bool>> filter = null,
                         string includeProperties = null,
                         bool isTracking = true
            );

        void Add(T entity);

        void Remove(T entity);

        void Save();

        void RemoveRange(IEnumerable<T> entity);
    }
}
