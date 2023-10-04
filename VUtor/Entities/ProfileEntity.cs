using Microsoft.AspNetCore.Identity;

namespace VUtor.Entities
{
    public class ProfileEntity : IdentityUser
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<ProfileTopic>? TopicsToTeach { get; set; }
        public ICollection<ProfileTopic>? TopicsToLearn { get; set; }
    }
}
