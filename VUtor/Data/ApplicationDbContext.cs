using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.Sig;
using VUtor.Entities;

namespace VUtor.Data
{
    public class ApplicationDbContext : IdentityDbContext<ProfileEntity>
    {
        public DbSet<ProfileEntity> Profiles { get; set; }
        public DbSet<TopicEntity> Topics { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:vutor.database.windows.net,1433;Initial Catalog=VUtor;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;Authentication=Active Directory Default;", 
                options => options.EnableRetryOnFailure());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfileEntity>()
                .Property(e => e.Name)
                .HasMaxLength(250);

            modelBuilder.Entity<ProfileEntity>()
                .Property(e => e.Surname)
                .HasMaxLength(250);

            modelBuilder.SharedTypeEntity<ProfileTopic>("ProfileTopicToLearn", builder =>
            {
                builder.ToTable("ProfileTopicToLearn");
                builder.HasKey(e => new { e.ProfileId, e.TopicId });
                builder.HasOne(e => e.Profile).WithMany(e => e.TopicsToLearn);
                builder.HasOne(e => e.Topic).WithMany(e => e.LearningProfiles);
            });

            modelBuilder.SharedTypeEntity<ProfileTopic>("ProfileTopicToTeach", builder =>
            {
                builder.ToTable("ProfileTopicToTeach");
                builder.HasKey(e => new { e.ProfileId, e.TopicId });
                builder.HasOne(e => e.Profile).WithMany(e => e.TopicsToTeach);
                builder.HasOne(e => e.Topic).WithMany(e => e.TeachingProfiles);
            });
           
            base.OnModelCreating(modelBuilder);

        }
    }
}