using System.Threading.Tasks;
using Bit8.Students.Services;
using Bit8.Students.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Bit8.Students.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly IDisciplineService _disciplineService;
        
        public DisciplinesController(IDisciplineService disciplineService)
        {
            _disciplineService = disciplineService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscipline(CreateDisciplineDto dto)
        {
            var id = await _disciplineService.CreateDisciplineAsync(dto);
            return Ok(id);
        }
    }
}