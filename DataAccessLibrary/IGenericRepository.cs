namespace DataAccessLibrary
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task Delete(TEntity entity);
        Task<IQueryable<TEntity>> GetQueryable();
        Task Insert(TEntity entity);
        Task<List<TEntity>> LoadData();
        Task SaveChanges();
        Task Update(TEntity entity);
    }
}