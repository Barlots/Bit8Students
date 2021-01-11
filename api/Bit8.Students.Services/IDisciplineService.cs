using System.Collections.Generic;
using System.Threading.Tasks;
using Bit8.Students.Services.Disciplines;
using FluentResults;

namespace Bit8.Students.Services
{
    public interface IDisciplineService
    {
        Task<Result<int>> CreateAsync(CreateDisciplineDto dto);
        Task<Result<IEnumerable<GetAllDisciplinesDto>>> GetAllAsync();
        Task<Result> DeleteAsync(int id);
    }
}