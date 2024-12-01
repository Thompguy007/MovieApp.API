using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer;
using MovieApp.DataLayer.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SimilarMoviesDbController : ControllerBase
    {
        private readonly SimilarMoviesDbBusinessService _similarMoviesDbBusinessService;

        public SimilarMoviesDbController(SimilarMoviesDbBusinessService similarMoviesDbBusinessService)
        {
            _similarMoviesDbBusinessService = similarMoviesDbBusinessService;
        }

        // API-endpoint til at hente lignende film
        [HttpGet]
        public async Task<ActionResult<List<SimilarMoviesResult>>> GetSimilarMovies([FromQuery] string movieTitle)
        {
            var result = await _similarMoviesDbBusinessService.GetSimilarMoviesAsync(movieTitle);
            return Ok(result);
        }
    }
}
