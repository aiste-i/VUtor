using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLibrary.Data
{
    public class ApplicationDbContext : IdentityDbContext<ProfileEntity>
    {
        public DbSet<ProfileEntity> Profiles { get; set; }
        public DbSet<TopicEntity> Topics { get; set; }
        public DbSet<UserFile> UserFiles { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<UserItem> UserItems { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:vutor.database.windows.net,1433;Initial Catalog=vutorapp;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;Authentication=Active Directory Default;Pooling=True;",
                options => options.EnableRetryOnFailure().MaxBatchSize(100));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserItem>()
                .HasOne(e => e.Profile)
                .WithMany(e => e.UserItems)
                .HasForeignKey(e => e.ProfileId);

            modelBuilder.Entity<Folder>()
                .HasMany(e => e.SubFolders)
                .WithMany()
                .UsingEntity(join => join.ToTable("FolderHierarchy"));

            modelBuilder.Entity<UserFile>()
                .HasOne(e => e.Topic)
                .WithMany(e => e.UserFiles)
                .HasForeignKey(e => e.TopicId)
                .IsRequired();

            modelBuilder.Entity<TopicEntity>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<TopicEntity>()
                .Property(e => e.Title)
                .HasMaxLength(250);

            modelBuilder.Entity<ProfileEntity>()
                .Property(e => e.Name)
                .HasMaxLength(250);

            modelBuilder.Entity<ProfileEntity>()
                .Property(e => e.Surname)
                .HasMaxLength(250);

            modelBuilder.Entity<ProfileEntity>()
                .Property(e => e.CourseInfo)
                .HasConversion(
                    v => v.ToString(),
                    v => new CourseData(v))
                .HasMaxLength(250);

            modelBuilder.Entity<ProfileEntity>()
                .Property(e => e.CreationDate)
                .HasConversion(
                    v => v.ToString(),
                    v => new profileCreationDate(v))
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