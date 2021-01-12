using System.Collections.Generic;
using System.Threading.Tasks;
using Bit8.Students.Query.Students;
using Bit8.Students.Services.Students;

namespace Bit8.Students.Query
{
    public interface IStudentQuery
    {
        Task<IEnumerable<GetTopTenStudentsDto>> GetTopTenAsync();
        Task<IEnumerable<GetDisciplinesWithoutScoreDto>> GetDisciplinesWithoutScore();
        Task<IEnumerable<GetAllWithSemestersDto>> GetAllWithSemestersAsync();
    }
}