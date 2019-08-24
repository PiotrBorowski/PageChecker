using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PageCheckerAPI.Repositories.Interfaces;

namespace PageCheckerAPI.Repositories
{
    public class GenericRepository<C, T> :
        IGenericRepository<T> where T : class where C : DbContext
    {
        private readonly C _context;

        public GenericRepository(C context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            IQueryable<T> query = _context.Set<T>();
            return await query.ToArrayAsync();
        }

        public virtual IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);
            return query;
        }

        public virtual async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            if (await _context.SaveChangesAsync() > 0)
                return entity;

            return null;
        }

        public virtual async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<bool> Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            if (await _context.SaveChangesAsync() > 0)
                return true;

            return false;
        }
    }
}
