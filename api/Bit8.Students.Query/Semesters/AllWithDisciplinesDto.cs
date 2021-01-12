using System.Collections.Generic;

namespace Bit8.Students.Query.Semesters
{
    public class AllWithDisciplinesDto
    {
        public string SemesterName { get; set; }
        public IEnumerable<string> DisciplineNames { get; set; }
    }
}