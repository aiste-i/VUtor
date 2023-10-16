using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public interface IProfileRepository
    {
        Task<List<ProfileEntity>> GetProfilesByNameAsync(string name, string surname);
        Task<List<ProfileEntity>> GetProfilesByFilterAsync(string name, string surname, int courseName, int courseYear);
        Task Delete(ProfileEntity entity);
        Task<IQueryable<ProfileEntity>> GetQueryable();
        Task Insert(ProfileEntity entity);
        Task<List<ProfileEntity>> LoadData();
        Task SaveChanges();
        Task Update(ProfileEntity entity);
    }
}