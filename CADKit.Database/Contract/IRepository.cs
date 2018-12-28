using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitDALDB.Contract
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, IList<Expression<Func<T, object>>> includedProperties = null, IList<ISortCriteria<T>> sortCriterias = null);
        PaginatedList<T> GetPaged(Expression<Func<T, bool>> filter = null, IList<Expression<Func<T, object>>> includedProperties = null, PagingOptions<T> pagingOptions = null);
        T Find(Expression<Func<T, bool>> filter, IList<Expression<Func<T, object>>> includedProperties = null);
        void Add(T t);
        void Remove(T t);
        void Remove(Expression<Func<T, bool>> filter);
    }
}
