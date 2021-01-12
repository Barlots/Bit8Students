using System.Collections.Generic;
using System.Threading.Tasks;
using Bit8.Students.Services.Students;
using FluentResults;

namespace Bit8.Students.Services
{
    public interface IStudentService
    {
        Task<Result<int>> CreateAsync(CreateStudentRequest request);
        Task<Result> AssignToSemesterAsync(AssignToSemesterRequest request);
        Task<Result> UpdateAsync(UpdateStudentRequest request);
    }
}