using System.Threading.Tasks;
using Bit8.Students.Domain.Models;
using Bit8.Students.Persistence;
using FluentResults;

namespace Bit8.Students.Services.Semesters
{
    public class SemesterService : ISemesterService
    {
        private readonly IUnitOfWork _uow;
        
        public SemesterService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        
        public async Task<Result<int>> CreateAsync(CreateSemesterRequest request)
        {
            var semester = new Semester { Name = request.Name };
            
            await _uow.SemesterRepository.AddAsync(semester);

            foreach (var disciplineId in request.DisciplineIds)
            {
                await _uow.SemesterRepository.AddRelationToDisciplineAsync(semester.Id, disciplineId);
            }
            
            _uow.Commit();
            return Result.Ok(semester.Id);
        }

        public async Task<Result> DeleteAsync(int id)
        {
            if (id == 0)
            {
                return Result.Fail("Id must be provided");
            }
            
            var hasAssignedScores = await _uow.SemesterRepository.HasStudentsAsync(id);

            if (hasAssignedScores)
            {
                return Result.Fail("Semester has students assigned");
            }
                
            await _uow.SemesterRepository.DeleteAsync(id);
            _uow.Commit();

            return Result.Ok();
        }
    }
}