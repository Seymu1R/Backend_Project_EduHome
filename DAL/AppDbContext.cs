using EduHomeProject.DAL.Entities;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherSosialMedia> TeacherSosialMedias { get; set; }
        public DbSet<TeacherSkill> TeacherSkills { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<SpeakerEvent> SpeakerEvents { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UseFullLink> UseFullLinks { get; set; }
        public DbSet<Information> Informations { get; set; }
        public DbSet<HeaderLogo> HeaderLogos { get; set; }
        public DbSet<GetInTouch> GetInTouches { get; set; }
        public DbSet<FooterLeftSide> FooterLeftSides { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
           .Entries()
           .Where(e => e.Entity is BaseEntity && (
               e.State == EntityState.Added
               || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }

            return base.SaveChangesAsync();
        }
    }
}
