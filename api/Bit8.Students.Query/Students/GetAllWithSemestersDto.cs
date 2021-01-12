using System.Collections.Generic;

namespace Bit8.Students.Query.Students
{
    public class GetAllWithSemestersDto
    {
        public string StudentName { get; set; }
        public IEnumerable<string> SemesterNames { get; set; }
    }
}