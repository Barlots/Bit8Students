using System.Collections.Generic;
using System.Threading.Tasks;
using Bit8.Students.Domain.Models;

namespace Bit8.Students.Persistence.Repositories
{
    public interface IStudentRepository
    {
        Task AddAsync(Student entity);
        Task<IEnumerable<Student>> AllAsync();
        Task DeleteAsync(Student entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(Student entity);
        Task<Student> Get(int id);

        Task<bool> ExistsAsync(int id);
    }
}