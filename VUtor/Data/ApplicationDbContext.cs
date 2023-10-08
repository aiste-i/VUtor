using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.Sig;
using VUtor.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                .Property(e => e.Id);

            modelBuilder.Entity<ProfileEntity>()
                .Property(e => e.Name)
                .HasMaxLength(250);

            modelBuilder.Entity<ProfileEntity>()
                .Property(e => e.Surname)
                .HasMaxLength(250);

            modelBuilder.Entity<ProfileEntity>()
            .HasMany(p => p.TopicsToLearn)
            .WithMany(t => t.LearningProfiles)
            .UsingEntity(j => j.ToTable("ProfilesLearningTopics"));

            modelBuilder.Entity<ProfileEntity>()
                        .HasMany(p => p.TopicsToTeach)
                        .WithMany(t => t.TeachingProfiles)
                        .UsingEntity(j => j.ToTable("ProfilesTeachingTopics"));

            base.OnModelCreating(modelBuilder);

        }
    }
}