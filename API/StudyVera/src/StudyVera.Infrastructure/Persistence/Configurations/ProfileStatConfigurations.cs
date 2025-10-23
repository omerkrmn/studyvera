using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyVera.Domain.Entities;
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

        }
    }

}
