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
                    Name = "KPSS",
                    Description = "Kamu Personeli Seçme Sınavı",
                    ExamDate = new DateTime(2026,9,6)
                }
                
            };
            builder.HasData(exams);
        }
    }

}
