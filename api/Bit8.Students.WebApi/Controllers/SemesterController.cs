using System.Threading.Tasks;
using Bit8.Students.Query;
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
        private readonly ISemesterQuery _semesterQuery;
        
        public SemesterController(ISemesterService semesterService, ISemesterQuery semesterQuery)
        {
            _semesterService = semesterService;
            _semesterQuery = semesterQuery;
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
            var result = await _semesterQuery.GetAllWithDisciplinesAsync();
            return Ok(result);
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