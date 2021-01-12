using System.Threading.Tasks;
using Bit8.Students.Services.Semesters;
using FluentResults;

namespace Bit8.Students.Services
{
    public interface ISemesterService
    {
        Task<Result<int>> CreateAsync(CreateSemesterRequest request);
        Task<Result> DeleteAsync(int id);
    }
}