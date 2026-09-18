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
                new Lesson { Id = 1, Name = "Türkçe", ExamId = 1, ExamQuestionCount = 30 },
                new Lesson { Id = 2, Name = "Matematik", ExamId = 1, ExamQuestionCount = 27 },
                new Lesson { Id = 3, Name = "Geometri", ExamId = 1 ,ExamQuestionCount =3},
                new Lesson { Id = 4, Name = "Tarih", ExamId = 1 ,ExamQuestionCount = 27 },
                new Lesson { Id = 5, Name = "Çoğrafya", ExamId = 1 ,ExamQuestionCount = 18},
                new Lesson { Id = 6, Name = "Vatandaşlık", ExamId = 1 ,ExamQuestionCount = 9 },
                new Lesson { Id = 7, Name = "Güncel Olaylar", ExamId = 1 ,ExamQuestionCount = 6}
            );
        }
    }

}
