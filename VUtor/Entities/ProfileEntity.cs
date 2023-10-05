<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Identity;

namespace VUtor.Entities
=======
﻿namespace VUtor.Entities
>>>>>>> main
{
    public class ProfileEntity : IdentityUser
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<TopicEntity> TopicsToLearn { get; set; }
        public List<TopicEntity> TopicsToTeach { get; set; }
    }
}
