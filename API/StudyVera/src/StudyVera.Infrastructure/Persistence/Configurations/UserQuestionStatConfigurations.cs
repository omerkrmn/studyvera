using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyVera.Domain.Entities;
using StudyVera.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Persistence.Configurations
{
    public class UserQuestionStatConfigurations : IEntityTypeConfiguration<UserQuestionStat>
    {
        public void Configure(EntityTypeBuilder<UserQuestionStat> builder)
        {
            builder.HasOne<AppUser>()
                   .WithMany()
                   .HasForeignKey(uqs => uqs.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
