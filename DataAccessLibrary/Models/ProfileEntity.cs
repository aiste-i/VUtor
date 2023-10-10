using Microsoft.AspNetCore.Identity;

namespace DataAccessLibrary.Models
{
    public class ProfileEntity : IdentityUser
    {
        // public Guid Id { get; set; } --- Inherited from IdentityUser
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int CourseName { get; set; }
        public int CourseYear { get; set; }
        public List<TopicEntity> TopicsToLearn { get; set; } = new List<TopicEntity>();
        public List<TopicEntity> TopicsToTeach { get; set; } = new List<TopicEntity>();
    }
}
