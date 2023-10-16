using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public static class ProfileExtensions
    {
        public static IEnumerable<ProfileEntity> FilterProfiles (this IEnumerable<ProfileEntity> profiles, string name, string surname, int courseName, int courseYear, int topicsLearn, int topicsTeach) 
        { 
            return profiles
                    .Where(profile =>
                    (string.IsNullOrWhiteSpace(name) || (profile.Name != null && profile.Name.Contains(name, StringComparison.OrdinalIgnoreCase))) &&
                    (string.IsNullOrWhiteSpace(surname) || (profile.Surname != null && profile.Surname.Contains(surname, StringComparison.OrdinalIgnoreCase))) &&
                    (courseName == 0 || profile.CourseInfo.courseName.Equals(courseName)) &&
                    (courseYear == 0 || profile.CourseInfo.courseYear.Equals(courseYear)) &&
                    (topicsLearn == 0 || profile.TopicsToLearn.Any(topic => topic.Id.Equals(topicsLearn))) &&
                    (topicsTeach == 0 || profile.TopicsToTeach.Any(topic => topic.Id.Equals(topicsTeach))))
                    .OrderBy(profile => profile.Surname)
                    .ToList();
        }
    }
}
