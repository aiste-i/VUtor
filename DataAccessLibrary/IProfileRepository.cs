using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public interface IProfileRepository
    {
        Task AddProfile(ProfileEntity profile);
        Task<List<ProfileEntity>> GetProfiles();
        Task<List<ProfileEntity>> GetProfilesByCourseAsync(int course);
        Task<List<ProfileEntity>> GetProfilesByFilterAsync(string name, string surname, int courseName, int courseYear);
        Task<List<ProfileEntity>> GetProfilesByNameAsync(string name, string surname);
        Task<List<ProfileEntity>> GetProfilesByYearAsync(int year);
        Task SaveChanges();
    }
}