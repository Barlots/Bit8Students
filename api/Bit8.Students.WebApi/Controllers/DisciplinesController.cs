using System.Threading.Tasks;
using Bit8.Students.Services;
using Bit8.Students.Services.Disciplines;
using Bit8.Students.WebApi.Common;
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
            var result = await _disciplineService.CreateAsync(dto);
            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDisciplines()
        {
            var result = await _disciplineService.GetAllAsync();
            return Ok(result.Value);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDisciplines([FromBody] int id)
        {
            var result = await _disciplineService.DeleteAsync(id);
            
            if (result.IsFailed)
            {
                return UnprocessableEntity(result.ToErrorArray());
            }

            return Ok();
        }
    }
}