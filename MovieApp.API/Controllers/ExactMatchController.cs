using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer;
using System.Threading.Tasks;

namespace MovieApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExactMatchController : ControllerBase
    {
        private readonly ExactMatchBusinessService _exactMatchBusinessService;

        public ExactMatchController(ExactMatchBusinessService exactMatchBusinessService)
        {
            _exactMatchBusinessService = exactMatchBusinessService;
        }

        // API-endpoint til at hente film baseret på to søgeord
        [HttpGet("Search")]
        public async Task<IActionResult> GetExactMatchMovies([FromQuery] string keyword1, [FromQuery] string keyword2 = null)
        {
            // Kald BusinessLayer for at hente film
            var movies = await _exactMatchBusinessService.GetExactMatchMoviesAsync(keyword1, keyword2);
            if (movies == null || movies.Count == 0)
            {
                return NotFound("No movies found for the given keywords.");
            }
            return Ok(movies);
        }
    }
}
