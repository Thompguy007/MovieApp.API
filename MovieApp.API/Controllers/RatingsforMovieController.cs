using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer;
using System.Threading.Tasks;

namespace MovieApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingsForMovieController : ControllerBase
    {
        private readonly RatingsForMovieBusinessService _ratingsForMovieBusinessService;

        public RatingsForMovieController(RatingsForMovieBusinessService ratingsForMovieBusinessService)
        {
            _ratingsForMovieBusinessService = ratingsForMovieBusinessService;
        }

        [HttpGet("CalculateRatingsForMovie")]
        public async Task<IActionResult> GetRatingsForMovie([FromQuery] string tconst)
        {
            // Hent ratings for film baseret på tconst
            var ratings = await _ratingsForMovieBusinessService.GetRatingsForMovieAsync(tconst);
            if (ratings == null || ratings.Count == 0)
            {
                return NotFound("No ratings found for the given movie.");
            }
            return Ok(ratings);
        }
    }
}
