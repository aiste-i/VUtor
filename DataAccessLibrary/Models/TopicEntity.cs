using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccessLibrary.Models
{
    public class TopicEntity
    {
        public int Id { get; init; }
        public required string Title { get; set; }
        public List<ProfileEntity> LearningProfiles { get; set; } = new List<ProfileEntity>();
        public List<ProfileEntity> TeachingProfiles { get; set; } = new List<ProfileEntity>();
        public List<UserFile> UserFiles { get; set; } = new List<UserFile>();
    }
}
