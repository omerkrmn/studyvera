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
    public class LessonConfigurations : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasData(
                new Lesson { Id = 2, Name = "Türkçe", ExamId = 1 },
                new Lesson { Id = 5, Name = "Matematik", ExamId = 1 },
                new Lesson { Id = 8, Name = "Geometri", ExamId = 1 },
                new Lesson { Id = 17, Name = "Tarih", ExamId = 1 },
                new Lesson { Id = 20, Name = "Çoğrafya", ExamId = 1 },
                new Lesson { Id = 26, Name = "Vatandaşlık", ExamId = 1 }
            );
        }
    }

}
