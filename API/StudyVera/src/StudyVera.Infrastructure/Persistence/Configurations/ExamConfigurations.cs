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
                    Description = "Yükseköğretim Kurumları Sınavı",
                    ExamDate = new DateTime(2026,6,20)
                },
                new Exam
                {
                    Id=2,
                    Name = "AYT",
                    Description = "Yükseköğretim Kurumları Sınavı",
                    ExamDate = new DateTime(2026,6,21)
                },
                new Exam
                {
                    Id=3,
                    Name = "DGS",
                    Description = "Dikey Geçiş Sınavı",
                    ExamDate = new DateTime(2026 ,7,19)
                }
                ,new Exam
                {
                    Id=4,
                    Name = "KPSS",
                    Description = "Kamu Personeli Seçme Sınavı",
                    ExamDate = new DateTime(2026,6,16)
                },new Exam
                {
                    Id=5,
                    Name = "ALES",
                    Description = "Akademik Personel ve Lisansüstü Eğitimi Giriş Sınavı",
                    ExamDate = new DateTime(2026,5,10)
                }
            };
            builder.HasData(exams);
        }
    }

}
