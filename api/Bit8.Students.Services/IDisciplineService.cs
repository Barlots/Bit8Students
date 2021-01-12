using System.Collections.Generic;
using System.Threading.Tasks;
using Bit8.Students.Services.Disciplines;
using FluentResults;

namespace Bit8.Students.Services
{
    public interface IDisciplineService
    {
        Task<Result<int>> CreateAsync(CreateDisciplineRequest request);
        Task<Result> UpdateAsync(UpdateDisciplineRequest request);
        Task<Result> DeleteAsync(int id);
    }
}