using Bit8.Students.Common;

namespace Bit8.Students.Domain.Models
{
    public class Student : DatabaseEntity
    {
        public string Name { get; set; }
        public int SemesterId { get; set; }

        public void AssignToSemester(int semesterId)
        {
            SemesterId = semesterId;
        }
    }
}