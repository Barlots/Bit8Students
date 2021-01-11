using System.Threading.Tasks;
using Bit8.Students.Services;
using Bit8.Students.Services.Disciplines;
using Microsoft.AspNetCore.Mvc;

namespace Bit8.Students.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var id = await _disciplineService.CreateAsync(dto);
            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDisciplines()
        {
            var result = await _disciplineService.GetAllAsync();
            return Ok(result);
        }
    }
}