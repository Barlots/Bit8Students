using System.Collections.Generic;

namespace Bit8.Students.Services.Students
{
    public class GetDisciplinesWithoutScoreDto
    {
        public string StudentName { get; set; }
        public ICollection<string> DisciplineNames { get; set; }
    }
}