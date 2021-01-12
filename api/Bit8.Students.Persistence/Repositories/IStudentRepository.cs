using System.Collections.Generic;
using System.Threading.Tasks;
using Bit8.Students.Domain.Models;

namespace Bit8.Students.Persistence.Repositories
{
    public interface IStudentRepository
    {
        Task AddAsync(Student entity);
        Task DeleteAsync(Student entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(Student entity);
        Task<Student> GetAsync(int id);

        Task<bool> ExistsAsync(int id);
    }
}