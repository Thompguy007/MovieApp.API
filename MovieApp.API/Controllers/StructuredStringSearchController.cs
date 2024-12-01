using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Services;

namespace MovieApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StructuredStringSearchController : ControllerBase
    {
        private readonly StructuredStringSearchBusinessService _businessService;

        public StructuredStringSearchController(StructuredStringSearchBusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(string titleOfMovie, string plotDesc, string characterName, string actorName)
        {
            var results = await _businessService.SearchAsync(titleOfMovie, plotDesc, characterName, actorName);
            return Ok(results);
        }
    }
}
