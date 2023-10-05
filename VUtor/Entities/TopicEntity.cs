using System.ComponentModel.DataAnnotations;

namespace VUtor.Entities

{
    public class TopicEntity
    {
        [Key]
        public int Id { get; init; }
        public string Title { get; set; }
        public List<ProfileEntity> LearningProfiles { get; set; }
        public List<ProfileEntity> TeachingProfiles { get; set; }
    }
}
