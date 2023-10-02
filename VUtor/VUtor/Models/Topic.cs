namespace VUtor.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Title { get; set; }

        public List<Profile> TeachingProfiles { get; set; }
        public List<Profile> LearningProfiles { get; set; }
    }
}
