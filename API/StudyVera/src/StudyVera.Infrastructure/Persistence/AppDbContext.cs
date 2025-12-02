using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Entities.Identity;

namespace StudyVera.Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{

    public DbSet<Exam> Exams { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<ProfileStat> ProfileStats { get; set; }
    public DbSet<UserActivityHistory> UserActivityHistories { get; set; }
    public DbSet<UserLessonProgress> UserLessonProgresses { get; set; }
    public DbSet<UserQuestionStat> UserQuestionStats { get; set; }
    public DbSet<LessonSchedule> LessonSchedules { get; set; }
    public DbSet<UserSettings> UserSettings { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    
}
