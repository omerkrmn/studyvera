using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Dtos.ProfileSummary;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Entities.Identity;
using System.Reflection.Emit;

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
    public DbSet<UserProfile> UserSettings { get; set; }
    public DbSet<QuestionStatDetail> QuestionStatDetails { get; set; }
    public DbSet<UserRankResult> RankResults { get; set; }
    public DbSet<UserWeeklyGoal> UserWeeklyGoals { get; set; }
    public DbSet<Friendship> Friendships { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<UserRankResult>(entity =>
        {
            entity.HasNoKey();
            entity.ToView(null);
        });
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        builder.Entity<Friendship>(entity =>
        {
            entity.HasKey(f => f.Id);

            entity.HasOne(f => f.Requestor)
                .WithMany(u => u.SentFriendRequests)
                .HasForeignKey(f => f.RequestorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(f => f.Receiver)
                .WithMany(u => u.ReceivedFriendRequests)
                .HasForeignKey(f => f.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(f => new { f.RequestorId, f.ReceiverId }).IsUnique();
        });
    }
    
}
