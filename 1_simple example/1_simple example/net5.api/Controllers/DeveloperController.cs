using Microsoft.AspNetCore.Mvc;
using simple.shared.Interfaces;
using simple.shared.Models;
using System.Threading.Tasks;

namespace net5.api.Controllers
{
    [ApiController]
    [Route("api/developers")]
    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloperService _developerService;

        public DeveloperController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDeveloper()
        {
            return Ok(await _developerService.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult> CreateDeveloper(DeveloperDto request)
        {
            var result = await _developerService.CreateAsync(request);

            return result.HasErrors
                ? BadRequest(result)
                : NoContent();
        }
    }
}
