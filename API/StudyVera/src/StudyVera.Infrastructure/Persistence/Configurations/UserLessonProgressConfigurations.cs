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
    public class UserLessonProgressConfigurations : IEntityTypeConfiguration<UserLessonProgress>
    {
        public void Configure(EntityTypeBuilder<UserLessonProgress> builder)
        {

        }
    }

}
