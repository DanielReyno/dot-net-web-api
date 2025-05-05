using Microsoft.EntityFrameworkCore;
using WebAPITesting.Data;
using WebAPITesting.IRepository;

namespace WebAPITesting.Repository
{
    public class GenericRepositoryImpl<T> : IGenericRepository<T> where T : class
    {
        private readonly HotelsDbContext _context;

        //inyeccion del database context.
        public GenericRepositoryImpl(HotelsDbContext context)
        {
            this._context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            //La sentencia de abajo no lleva la palabra clave await debido a que el metodo 
            //Update no es asincrono.
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int? id)
        {
            if(id == null)
            {
                return null;
            }

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            //La sentencia de abajo no lleva la palabra clave await debido a que el metodo 
            //Update no es asincrono.
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

    }
}
