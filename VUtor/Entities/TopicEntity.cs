using System.ComponentModel.DataAnnotations;

namespace VUtor.Entities
{
    public class TopicEntity
    {
        [Key]
        public int Id { get; init; }
        public required string Title { get; set; }
        public ICollection<ProfileTopic>? TeachingProfiles { get; set; }
        public ICollection<ProfileTopic>? LearningProfiles { get; set; }
    }
}
