using System.Collections.Generic;

namespace Bit8.Students.Query.Students
{
    public class GetAllWithSemestersQuery
    {
        public string StudentName { get; set; }
        public IEnumerable<string> SemesterNames { get; set; }
    }
}