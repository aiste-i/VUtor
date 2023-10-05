<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VUtor.Entities
=======
﻿namespace VUtor.Entities
>>>>>>> main
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
