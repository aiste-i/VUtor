using Microsoft.EntityFrameworkCore;
using DataAccessLibrary.Data;
using DataAccessLibrary.Models;
using Microsoft.IdentityModel.Tokens;

namespace DataAccessLibrary
{
    public class ProfileRepository : IProfileRepository
    {
        readonly ApplicationDbContext _context;

        public ProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ProfileEntity>> GetProfiles()
        {
            List<ProfileEntity> profiles = new List<ProfileEntity>();
            profiles = await _context.Profiles
                .OrderBy(profile => profile.Surname)
                .ToListAsync();

            return profiles;
        }
        public async Task<List<ProfileEntity>> GetProfilesByNameAsync(string name, string surname)
        {
            List<ProfileEntity> profiles = await GetProfiles();
            if (!profiles.IsNullOrEmpty())
            {
                profiles = profiles.Where(profile =>
                    (string.IsNullOrWhiteSpace(name) || profile.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrWhiteSpace(surname) || profile.Surname.Contains(surname, StringComparison.OrdinalIgnoreCase))).ToList();
            }

            return profiles;
        }

        public async Task<List<ProfileEntity>> GetProfilesByCourseAsync(int course)
        {
            List<ProfileEntity> profiles = await GetProfiles();
            if (!profiles.IsNullOrEmpty())
            {
                profiles = profiles
                .Where(profile => course == 0 || profile.CourseYear.Equals(course))
                .ToList();
            }

            return profiles;
        }


        public async Task<List<ProfileEntity>> GetProfilesByYearAsync(int year)
        {
            List<ProfileEntity> profiles = await GetProfiles();
            if (!profiles.IsNullOrEmpty())
            {
                profiles = profiles
                .Where(profile => year == 0 || profile.CourseYear.Equals(year))
                .ToList();
            }

            return profiles;
        }

        public async Task<List<ProfileEntity>> GetProfilesByFilterAsync(string name, string surname, int courseName, int courseYear)
        {
            List<ProfileEntity> profiles = await GetProfiles();
            if (!profiles.IsNullOrEmpty())
            {
                profiles = profiles
                .Where(profile =>
                (string.IsNullOrWhiteSpace(name) || profile.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(surname) || profile.Surname.Contains(surname, StringComparison.OrdinalIgnoreCase)) &&
                (courseName == 0 || profile.CourseYear.Equals(courseName)) &&
                (courseYear == 0 || profile.CourseYear.Equals(courseYear)))
                .OrderBy(profile => profile.Surname)
                .ToList();
            }

            return profiles;
        }

        public async Task AddStudent(ProfileEntity student)
        {
            await _context.Profiles.AddAsync(student);
            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}