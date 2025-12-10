using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CustomerTask.Core.Entites;
using CustomerTask.Core.Interfaces;
using CustomerTask.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerTask.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync();
        }
       
        public async Task<T?> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);

        public void Update(T entity) => _context.Set<T>().Update(entity);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);
        public async Task<int> GetBiggestNumber() {

            var sql = @"
                 SELECT TOP 1 n.Number
                FROM Numbers n
                 left JOIN ReservedNumbers r 
                    ON n.Number = r.ReservedNumber
                WHERE r.ReservedNumber IS NULL
                ORDER BY n.Number DESC";

           return await _context.Numbers
                .FromSqlRaw(sql)
                .Select(n => n.Number)
                .FirstOrDefaultAsync();

        }

        public IQueryable<T> GetAllAsQuery(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }
    }
}
