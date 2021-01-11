using System.Threading.Tasks;
using Bit8.Students.Domain.Models;
using Bit8.Students.Persistence;
using Bit8.Students.Services.Dto;

namespace Bit8.Students.Services
{
    public class DisciplineService : IDisciplineService
    {
        private readonly IUnitOfWork _uow;
        
        public DisciplineService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        
        public async Task<int> CreateDisciplineAsync(CreateDisciplineDto dto)
        {
            var discipline = new Discipline {Name = dto.Name, ProfessorName = dto.ProfessorName};
            
            await _uow.DisciplineRepository.AddAsync(discipline);
            _uow.Commit();

            return discipline.Id;
        }
    }
}