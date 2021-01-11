using System.Collections.Generic;
using System.Threading.Tasks;
using Bit8.Students.Services.Disciplines;

namespace Bit8.Students.Services
{
    public interface IDisciplineService
    {
        Task<int> CreateAsync(CreateDisciplineDto dto);
        Task<IEnumerable<GetAllDisciplinesDto>> GetAllAsync();
    }
}