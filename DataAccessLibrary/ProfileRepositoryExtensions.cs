using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public static class ProfileExtensions
    {
        public static IEnumerable<ProfileEntity> FilterProfiles(this IEnumerable<ProfileEntity> profiles, string name, string surname, int courseName, int courseYear)
        {
            return profiles
                    .Where(profile =>
                    (string.IsNullOrWhiteSpace(name) || (profile.Name != null && profile.Name.Contains(name, StringComparison.OrdinalIgnoreCase))) &&
                    (string.IsNullOrWhiteSpace(surname) || (profile.Surname != null && profile.Surname.Contains(surname, StringComparison.OrdinalIgnoreCase))) &&
                    (courseName == 0 || profile.CourseInfo.courseName.Equals(courseName)) &&
                    (courseYear == 0 || profile.CourseInfo.courseYear.Equals(courseYear)))
                    .OrderBy(profile => profile.Surname)
                    .ToList();
        }
    }
}