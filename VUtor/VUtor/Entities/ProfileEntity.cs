namespace VUtor.Models
{
    public class ProfileEntity
    {
        public int ProfileId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<TopicEntity> TopicsToTeach { get; set; }
        public List<TopicEntity> TopicsToLearn { get; set; }
    }
}
