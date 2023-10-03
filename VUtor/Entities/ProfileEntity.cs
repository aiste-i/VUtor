using Microsoft.AspNetCore.Identity;

namespace VUtor.Entities
{
    public class ProfileEntity : IdentityUser
    {
        public int ProfileId { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public List<TopicEntity>? TopicsToTeach { get; set; }
        public List<TopicEntity>? TopicsToLearn { get; set; }
    }
}
