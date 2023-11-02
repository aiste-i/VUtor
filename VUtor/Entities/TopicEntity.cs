﻿using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VUtor.Entities
{
    public class TopicEntity
    {
        [Key]
        public int Id { get; init; }
        public required string Title { get; set; }
        public List<ProfileEntity> LearningProfiles { get; set; }
        public List<ProfileEntity> TeachingProfiles { get; set; }
    }
}