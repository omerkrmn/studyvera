using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Persistence.Configurations
{
    public class UserActivityHistoryConfigurations : IEntityTypeConfiguration<UserActivityHistory>
    {
        public void Configure(EntityTypeBuilder<UserActivityHistory> builder)
        {
            //builder.HasOne<AppUser>()
            //   .WithMany()                        
            //   .HasForeignKey(uah => uah.UserId)  
            //   .OnDelete(DeleteBehavior.Cascade); 
        }
    }

}
