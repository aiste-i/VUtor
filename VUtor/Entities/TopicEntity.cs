namespace VUtor.Models
{
    public class TopicEntity
    {
        public int TopicId { get; set; }
        public string Title { get; set; }

        public List<ProfileEntity> TeachingProfiles { get; set; }
        public List<ProfileEntity> LearningProfiles { get; set; }
    }
}
