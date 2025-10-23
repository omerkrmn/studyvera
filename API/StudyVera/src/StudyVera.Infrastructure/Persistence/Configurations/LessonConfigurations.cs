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
                new Lesson { Id = 1, Name = "Türkçe", ExamId = 1 },
                new Lesson { Id = 2, Name = "Türkçe", ExamId = 3 },
                new Lesson { Id = 3, Name = "Matematik", ExamId = 1 },
                new Lesson { Id = 4, Name = "Matematik", ExamId = 2 },
                new Lesson { Id = 5, Name = "Matematik", ExamId = 3 },
                new Lesson { Id = 6, Name = "Geometri", ExamId = 1 },
                new Lesson { Id = 7, Name = "Geometri", ExamId = 2 },
                new Lesson { Id = 8, Name = "Geometri", ExamId = 3 },
                new Lesson { Id = 9, Name = "Fizik", ExamId = 1 },
                new Lesson { Id = 10, Name = "Fizik", ExamId = 2 },
                new Lesson { Id = 11, Name = "Kimya", ExamId = 1 },
                new Lesson { Id = 12, Name = "Kimya", ExamId = 2 },
                new Lesson { Id = 13, Name = "Biyoloji", ExamId = 1 },
                new Lesson { Id = 14, Name = "Biyoloji", ExamId = 2 },
                new Lesson { Id = 15, Name = "Tarih", ExamId = 1 },
                new Lesson { Id = 16, Name = "Tarih", ExamId = 2 },
                new Lesson { Id = 17, Name = "Tarih", ExamId = 3 },
                new Lesson { Id = 18, Name = "Çoğrafya", ExamId = 1 },
                new Lesson { Id = 19, Name = "Çoğrafya", ExamId = 2 },
                new Lesson { Id = 20, Name = "Çoğrafya", ExamId = 3 },
                new Lesson { Id = 21, Name = "Felsefe", ExamId = 1 },
                new Lesson { Id = 22, Name = "Felsefe", ExamId = 2 },
                new Lesson { Id = 23, Name = "Din Kültürü ve Ahlak Bilgisi Konuları", ExamId = 1 },
                new Lesson { Id = 24, Name = "Din Kültürü ve Ahlak Bilgisi Konuları", ExamId = 2 },
                new Lesson { Id = 25, Name = "Edebiyat", ExamId = 2 },
                new Lesson { Id = 26, Name = "Vatandaşlık", ExamId = 3 }
            );
        }
    }

}
