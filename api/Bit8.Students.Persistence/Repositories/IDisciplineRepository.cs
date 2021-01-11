using System.Collections.Generic;
using System.Threading.Tasks;
using Bit8.Students.Domain.Models;

namespace Bit8.Students.Persistence.Repositories
{
    public interface IDisciplineRepository
    {
        Task AddAsync(Discipline entity);
        Task<IEnumerable<Discipline>> AllAsync();
        Task DeleteAsync(Discipline entity);
        Task DeleteAsync(int id);

        Task<bool> HasScoresAsync(int id);
    }
}