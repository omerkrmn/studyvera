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
    public class ExamConfigurations : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            List<Exam> exams = new List<Exam>
            {
                new Exam
                {
                    Id=1,
                    Name = "TYT",
                    Description = "Temel Yeterlilik Testi",
                    ExamDate = new DateTime(DateTime.UtcNow.Year,6,14)  
                },
                new Exam
                {
                    Id=2,
                    Name = "AYT",
                    Description = "Alan Yeterlilik Testi",
                    ExamDate = new DateTime(DateTime.UtcNow.Year,6,15)
                }
                ,new Exam
                {
                    Id=3,
                    Name = "KPSS",
                    Description = "Kamu Personeli Seçme Sınavı",
                    ExamDate = new DateTime(DateTime.UtcNow.Year,6,16)
                }
            };
            builder.HasData(exams);
        }
    }

}
