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
    public class ProfileStatConfigurations : IEntityTypeConfiguration<ProfileStat>
    {
        public void Configure(EntityTypeBuilder<ProfileStat> builder)
        {
            builder.HasOne<AppUser>()
                .WithOne()
                   .HasForeignKey<ProfileStat>(ps => ps.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
