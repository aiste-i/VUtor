namespace VUtor.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<Topic> TopicsToTeach { get; set; }
        public List<Topic> TopicsToLearn { get; set; }
    }
}
