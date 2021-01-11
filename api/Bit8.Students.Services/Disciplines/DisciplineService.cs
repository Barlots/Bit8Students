using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bit8.Students.Domain.Models;
using Bit8.Students.Persistence;
using FluentResults;

namespace Bit8.Students.Services.Disciplines
{
    public class DisciplineService : IDisciplineService
    {
        private readonly IUnitOfWork _uow;
        
        public DisciplineService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        
        public async Task<Result<int>> CreateAsync(CreateDisciplineRequest request)
        {
            var discipline = new Discipline {Name = request.Name, ProfessorName = request.ProfessorName};
            
            await _uow.DisciplineRepository.AddAsync(discipline);
            _uow.Commit();

            return Result.Ok(discipline.Id);
        }

        public async Task<Result<IEnumerable<GetAllDisciplinesDto>>> GetAllAsync()
        {
            var disciplines = await _uow.DisciplineRepository.AllAsync();
            var mapped = disciplines.Select(x => new GetAllDisciplinesDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ProfessorName = x.ProfessorName
                });
            return Result.Ok(mapped);
        }

        public async Task<Result> DeleteAsync(int id)
        {
            if (id == 0)
            {
                return Result.Fail("Id must be provided");
            }
            
            var hasAssignedScores = await _uow.DisciplineRepository.HasScoresAsync(id);

            if (hasAssignedScores)
            {
                return Result.Fail("Discipline has scores assigned");
            }
                
            await _uow.DisciplineRepository.DeleteAsync(id);
            _uow.Commit();

            return Result.Ok();
        }
    }
}