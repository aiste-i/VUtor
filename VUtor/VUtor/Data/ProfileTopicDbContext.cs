using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using VUtor.Models;

namespace VUtor.Data
{
    public class ProfileTopicDbContext : DbContext 
    {
        private readonly string _connString;

        public ProfileTopicDbContext(IConfiguration config) => _connString = config.GetConnectionString("AzureSQL");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connString);
        }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Topic> Topics { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>()
              .HasMany(p => p.TopicsToTeach)
              .WithMany(t => t.TeachingProfiles);

            modelBuilder.Entity<Profile>()
              .HasMany(p => p.TopicsToLearn)
              .WithMany(t => t.LearningProfiles);
        }
    }
}
