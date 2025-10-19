using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Entities;

public class Lesson
{

    public int Id { get; set; }
    // Matematik Türkçe Fizik Kimya Biyoloji Edebiyat Vatandaşlık Tarih Coğrafya
    public string Name { get; set; } = string.Empty;

    // 1(TYT) 2(AYT) 3(KPSS),
    public int ExamId { get; set; }
    public Exam Exam { get; set; } = null!;

    // Relationships
    // matematiğin alt konuları burada mevzu sadece ilgili sınava göre alt konuların filtrelenmesi.
    //Örneğin TYT Matematik konuları farklı AYT Matematik konuları farklı olabilir ama ortakta olabilir.
    public ICollection<Topic> Topics { get; set; } = [];

}
