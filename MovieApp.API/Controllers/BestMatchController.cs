using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer;
using System.Threading.Tasks;

namespace MovieApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BestMatchController : ControllerBase
    {
        private readonly BestMatchBusinessService _bestMatchBusinessService;

        public BestMatchController(BestMatchBusinessService bestMatchBusinessService)
        {
            _bestMatchBusinessService = bestMatchBusinessService;
        }

        [HttpGet("Search")]
        public async Task<IActionResult> GetBestMatch([FromQuery] string keyword1, [FromQuery] string? keyword2 = null)
        {
            var results = await _bestMatchBusinessService.GetBestMatchAsync(keyword1, keyword2);
            if (results == null || results.Count == 0)
            {
                return NotFound("No matching movies found.");
            }
            return Ok(results);
        }
    }
}
