using System.Collections.Generic;
using System.Threading.Tasks;
using Bit8.Students.Query.Students;
using Bit8.Students.Services.Students;

namespace Bit8.Students.Query
{
    public interface IStudentQuery
    {
        Task<IEnumerable<GetTopTenStudentsQuery>> GetTopTenAsync();
        Task<IEnumerable<GetDisciplinesWithoutScoreQuery>> GetDisciplinesWithoutScore();
        Task<IEnumerable<GetAllWithSemestersQuery>> GetAllWithSemestersAsync();
    }
}