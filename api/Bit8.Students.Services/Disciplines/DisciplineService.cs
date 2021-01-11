using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bit8.Students.Domain.Models;
using Bit8.Students.Persistence;

namespace Bit8.Students.Services.Disciplines
{
    public class DisciplineService : IDisciplineService
    {
        private readonly IUnitOfWork _uow;
        
        public DisciplineService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        
        public async Task<int> CreateAsync(CreateDisciplineDto dto)
        {
            var discipline = new Discipline {Name = dto.Name, ProfessorName = dto.ProfessorName};
            
            await _uow.DisciplineRepository.AddAsync(discipline);
            _uow.Commit();

            return discipline.Id;
        }

        public async Task<IEnumerable<GetAllDisciplinesDto>> GetAllAsync()
        {
            var disciplines = await _uow.DisciplineRepository.AllAsync();
            return disciplines.Select(x => new GetAllDisciplinesDto
            {
                Id = x.Id,
                Name = x.Name,
                ProfessorName = x.ProfessorName
            });
        }
    }
}