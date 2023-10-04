using System.ComponentModel.DataAnnotations;

namespace VUtor.Entities
{
    public class ProfileTopic
    {
        [Key]
        public int ProfileId { get; set; }
        public required ProfileEntity Profile { get; set; }
        [Key] 
        public int TopicId { get; set; }
        public required TopicEntity Topic { get; set; }
    }
}
