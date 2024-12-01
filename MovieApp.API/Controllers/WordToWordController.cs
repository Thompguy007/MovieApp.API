using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer;

namespace MovieApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WordToWordController : ControllerBase
    {
        private readonly WordToWordBusinessService _businessService;

        public WordToWordController(WordToWordBusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpGet("Search")]
        public async Task<IActionResult> GetWords([FromQuery] string keyword)
        {
            var results = await _businessService.GetWordToWordAsync(keyword);
            return Ok(results);
        }
    }
}
