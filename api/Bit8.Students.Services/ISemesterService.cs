using System.Collections.Generic;
using System.Threading.Tasks;
using Bit8.Students.Services.Disciplines;
using Bit8.Students.Services.Semesters;
using FluentResults;

namespace Bit8.Students.Services
{
    public interface ISemesterService
    {
        Task<Result<int>> CreateAsync(CreateSemesterRequest request);
        Task<Result<IEnumerable<GetAllSemestersDto>>> GetAllAsync();
        Task<Result> DeleteAsync(int id);
    }
}