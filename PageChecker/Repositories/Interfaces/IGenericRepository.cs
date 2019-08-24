using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PageCheckerAPI.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);
        Task Delete(T entity);
        Task<bool> Edit(T entity);
    }
}
