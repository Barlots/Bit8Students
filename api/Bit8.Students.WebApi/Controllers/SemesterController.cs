using System.Threading.Tasks;
using Bit8.Students.Services;
using Bit8.Students.Services.Semesters;
using Bit8.Students.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Bit8.Students.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SemesterController : Controller
    {
        private readonly ISemesterService _semesterService;
        
        public SemesterController(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscipline(CreateSemesterRequest request)
        {
            var result = await _semesterService.CreateAsync(request);
            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDisciplines()
        {
            var result = await _semesterService.GetAllAsync();
            return Ok(result.Value);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDisciplines([FromBody] int id)
        {
            var result = await _semesterService.DeleteAsync(id);
            
            if (result.IsFailed)
            {
                return UnprocessableEntity(result.ToErrorArray());
            }

            return Ok();
        }
    }
}