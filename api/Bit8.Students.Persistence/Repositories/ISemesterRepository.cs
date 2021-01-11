using System.Collections.Generic;
using System.Threading.Tasks;
using Bit8.Students.Domain.Models;

namespace Bit8.Students.Persistence.Repositories
{
    public interface ISemesterRepository
    {
        Task AddAsync(Semester entity);
        Task<IEnumerable<Semester>> AllAsync();
        Task DeleteAsync(Semester entity);
        Task DeleteAsync(int id);

        Task<bool> HasStudentsAsync(int id);
    }
}