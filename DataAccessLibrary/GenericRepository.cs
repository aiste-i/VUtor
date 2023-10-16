using DataAccessLibrary.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> 
        where TEntity : class
    {
        readonly ApplicationDbContext _context;
        private Semaphore _pool;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;

            if (!Semaphore.TryOpenExisting("DbContextSemaphore", out _pool))
                _pool = new Semaphore(1, 1, "DbContextSemaphore");
        }
        public async Task<List<TEntity>> LoadData()
        {
            List<TEntity> TList = new List<TEntity>();

            while (!_pool.WaitOne(TimeSpan.FromTicks(1)))
                await Task.Delay(TimeSpan.FromSeconds(1));

            TList = await _context.Set<TEntity>().ToListAsync();
            _pool.Release();

            return TList;
        }

        public async Task Insert(TEntity entity)
        {
            while (!_pool.WaitOne(TimeSpan.FromTicks(1)))
                await Task.Delay(TimeSpan.FromSeconds(1));

            _context.Set<TEntity>().Add(entity);
            _pool.Release();
        }
        public async Task<IQueryable<TEntity>> GetQueryable()
        {
            while (!_pool.WaitOne(TimeSpan.FromTicks(1)))
                await Task.Delay(TimeSpan.FromSeconds(1));

            var queryable = _context.Set<TEntity>();
            _pool.Release();

            return queryable;
        }
        public async Task Update(TEntity entity)
        {
            while (!_pool.WaitOne(TimeSpan.FromTicks(1)))
                await Task.Delay(TimeSpan.FromSeconds(1));

            _context.Set<TEntity>().Update(entity);
            _pool.Release();
        }
        public async Task Delete(TEntity entity)
        {
            while (!_pool.WaitOne(TimeSpan.FromTicks(1)))
                await Task.Delay(TimeSpan.FromSeconds(1));

            _context.Set<TEntity>().Remove(entity);
            _pool.Release();
        }
        public async Task SaveChanges()
        {
            while (!_pool.WaitOne(TimeSpan.FromTicks(1)))
                await Task.Delay(TimeSpan.FromSeconds(1));

            await _context.SaveChangesAsync();
            _pool.Release();
        }

    }
}
