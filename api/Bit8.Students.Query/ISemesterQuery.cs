using System.Collections.Generic;
using System.Threading.Tasks;
using Bit8.Students.Query.Semesters;

namespace Bit8.Students.Query
{
    public interface ISemesterQuery
    {
        Task<IEnumerable<AllWithDisciplinesDto>> GetAllWithDisciplinesAsync();
    }
}