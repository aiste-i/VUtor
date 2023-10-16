using Microsoft.EntityFrameworkCore;
using DataAccessLibrary.Data;
using DataAccessLibrary.Models;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccessLibrary
{
    public class ProfileRepository : IProfileRepository
    {
        readonly ApplicationDbContext _context;
        private Semaphore _pool;

        public ProfileRepository(ApplicationDbContext context)
        {
            _context = context;

            if (!Semaphore.TryOpenExisting("DbContextSemaphore", out _pool))
                _pool = new Semaphore(1, 1, "DbContextSemaphore");
        }
        public async Task<List<ProfileEntity>> GetProfiles()
        {
            List<ProfileEntity> profiles = new List<ProfileEntity>();

            while (!_pool.WaitOne(TimeSpan.FromTicks(1)))
                await Task.Delay(TimeSpan.FromSeconds(1));

            profiles = await _context.Profiles
                .OrderBy(profile => profile.Surname)
                .ToListAsync();
            _pool.Release();

            return profiles;
        }
        public async Task<List<ProfileEntity>> GetProfilesByNameAsync(string name, string surname)
        {
            List<ProfileEntity> profiles = await GetProfiles();

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

        public async Task<List<ProfileEntity>> GetProfilesByCourseAsync(int course)
        {
            List<ProfileEntity> profiles = await GetProfiles();

            if (!profiles.IsNullOrEmpty())
            {
                while (!_pool.WaitOne(TimeSpan.FromTicks(1)))
                    await Task.Delay(TimeSpan.FromSeconds(1));

                profiles = profiles
                    .Where(profile => course == 0 || profile.CourseInfo.courseName.Equals(course))
                    .ToList();
            }
            _pool.Release();

            return profiles;
        }


        public async Task<List<ProfileEntity>> GetProfilesByYearAsync(int year)
        {
            List<ProfileEntity> profiles = await GetProfiles();

            if (!profiles.IsNullOrEmpty())
            {
                while (!_pool.WaitOne(TimeSpan.FromTicks(1)))
                    await Task.Delay(TimeSpan.FromSeconds(1));

                profiles = profiles
                    .Where(profile => year == 0 || profile.CourseInfo.courseYear.Equals(year))
                    .ToList();
            }
            _pool.Release();

            return profiles;
        }

        public async Task<List<ProfileEntity>> GetProfilesByFilterAsync(string name, string surname, int courseName, int courseYear)
        {
            List<ProfileEntity> profiles = await GetProfiles();

            if (!profiles.IsNullOrEmpty())
            {
                while (!_pool.WaitOne(TimeSpan.FromTicks(1)))
                    await Task.Delay(TimeSpan.FromSeconds(1));

                profiles = (List<ProfileEntity>)profiles.FilterProfiles(name, surname, courseName, courseYear);
            }
            _pool.Release();

            return profiles;
        }

        public async Task AddProfile(ProfileEntity profile)
        {
            while (!_pool.WaitOne(TimeSpan.FromTicks(1)))
                await Task.Delay(TimeSpan.FromSeconds(1));

            await _context.Profiles.AddAsync(profile);
            _pool.Release();

            await SaveChanges();
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