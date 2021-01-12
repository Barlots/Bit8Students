using System.Collections.Generic;
using System.Threading.Tasks;
using Bit8.Students.Services.Disciplines;

namespace Bit8.Students.Query
{
    public interface IDisciplineQuery
    {
        Task<IEnumerable<GetAllQuery>> GetAllAsync();
    }
}