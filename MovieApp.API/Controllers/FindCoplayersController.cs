using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer;
using System.Threading.Tasks;

namespace MovieApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FindCoplayersController : ControllerBase
    {
        private readonly FindCoplayersBusinessService _findCoplayersBusinessService;

        public FindCoplayersController(FindCoplayersBusinessService findCoplayersBusinessService)
        {
            _findCoplayersBusinessService = findCoplayersBusinessService;
        }

        // API-endpoint til at finde medspillere baseret på skuespillerens navn
        [HttpGet("Search")]
        public async Task<IActionResult> GetCoplayers([FromQuery] string actorName)
        {
            // Kald BusinessLayer for at finde medspillere
            var coplayers = await _findCoplayersBusinessService.GetCoplayersAsync(actorName);
            if (coplayers == null || coplayers.Count == 0)
            {
                return NotFound("No coplayers found for the given actor name.");
            }
            return Ok(coplayers);
        }
    }
}
