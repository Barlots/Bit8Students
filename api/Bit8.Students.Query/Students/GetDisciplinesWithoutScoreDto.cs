using System.Collections.Generic;

namespace Bit8.Students.Services.Students
{
    public class GetDisciplinesWithoutScoreDto
    {
        public string StudentName { get; set; }
        public IEnumerable<string> DisciplineNames { get; set; }
    }
}