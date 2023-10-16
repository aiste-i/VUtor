using Microsoft.EntityFrameworkCore;
using DataAccessLibrary.Data;
using DataAccessLibrary.Models;
using Microsoft.IdentityModel.Tokens;
using DataAccessLibrary;
using static Dapper.SqlMapper;

namespace DataAccessLibrary
{
    public class ProfileRepository : GenericRepository<ProfileEntity>, IProfileRepository
    {
        readonly ApplicationDbContext _context;
        private Semaphore _pool;
        public ProfileRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;

            if (!Semaphore.TryOpenExisting("DbContextSemaphore", out _pool))
                _pool = new Semaphore(1, 1, "DbContextSemaphore");
        }

        public async Task<List<ProfileEntity>> GetProfilesByNameAsync(string name, string surname)
        {
            List<ProfileEntity> profiles = await LoadData();

            if (!profiles.IsNullOrEmpty())
            {
                while (!_pool.WaitOne(TimeSpan.FromTicks(1)))
                    await Task.Delay(TimeSpan.FromSeconds(1));
                profiles = profiles.Where(profile =>
                    (string.IsNullOrWhiteSpace(name) || profile.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrWhiteSpace(surname) || profile.Surname.Contains(surname, StringComparison.OrdinalIgnoreCase))).ToList();
            }
            _pool.Release();

            return profiles;
        }

        public async Task<List<ProfileEntity>> GetProfilesByFilterAsync(string name, string surname, int courseName, int courseYear)
        {
            List<ProfileEntity> profiles = await LoadData();

            if (!profiles.IsNullOrEmpty())
            {
                while (!_pool.WaitOne(TimeSpan.FromTicks(1)))
                    await Task.Delay(TimeSpan.FromSeconds(1));

                profiles = (List<ProfileEntity>)profiles.FilterProfiles(name, surname, courseName, courseYear);
            }
            _pool.Release();

            return profiles;
        }
    }
}