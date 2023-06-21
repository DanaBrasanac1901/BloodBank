using BloodBankAPI.Model;
using BloodBankAPI.Settings;
using Castle.Components.DictionaryAdapter;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BloodBankAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly BloodBankDbContext _context;
        public GenericRepository(BloodBankDbContext context) {
            _context= context;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithIncludeAsync(string expression1, string expression2)
        {
            if(expression2 == null) return await _context.Set<T>().Include(expression1).ToListAsync();
            return await _context.Set<T>().Include(expression1).Include(expression2).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression){
           
           return await _context.Set<T>().Where(expression).ToListAsync();
        
        }

        public async Task<IEnumerable<T>> GetByConditionWithIncludeAsync(Expression<Func<T, bool>> expression, string include1, string include2)
        {
            if(include2 == null) return await _context.Set<T>().Where(expression).Include(include1).ToListAsync();
            return await _context.Set<T>().Where(expression).Include(include1).Include(include2).ToListAsync();

        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task InsertAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }


        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
