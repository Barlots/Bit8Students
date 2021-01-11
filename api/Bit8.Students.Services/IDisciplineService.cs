using System.Threading.Tasks;
using Bit8.Students.Services.Dto;

namespace Bit8.Students.Services
{
    public interface IDisciplineService
    {
        Task<int> CreateDisciplineAsync(CreateDisciplineDto dto);
    }
}