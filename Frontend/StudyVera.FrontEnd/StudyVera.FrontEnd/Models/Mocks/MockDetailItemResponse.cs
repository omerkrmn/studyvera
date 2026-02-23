using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.FrontEnd.Models.Mocks;

public class MockDetailItemResponse
{
    public string LessonName { get; set; } = string.Empty;
    public int Correct { get; set; }
    public int Wrong { get; set; }
    public int Empty { get; set; }
    public double Net { get; set; }
}