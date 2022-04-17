using System.Linq.Expressions;

namespace simpleCommerce_DataAccess.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        T Find(Guid guid);

        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = default!,
                              Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = default!,
                              string includeProperties = default!,
                              bool isTracking = true
            );


        T FirstOrDefault(Expression<Func<T, bool>> filter = default!,
                         string includeProperties = default!,
                         bool isTracking = true
            );

        void Add(T entity);

        void Remove(T entity);

        void Save();

        void RemoveRange(IEnumerable<T> entity);
    }
}
