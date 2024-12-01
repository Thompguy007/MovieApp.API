using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer;
using MovieApp.DataLayer.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StringSearchDbController : ControllerBase
    {
        private readonly StringSearchDbBusinessService _stringSearchDbBusinessService;

        public StringSearchDbController(StringSearchDbBusinessService stringSearchDbBusinessService)
        {
            _stringSearchDbBusinessService = stringSearchDbBusinessService;
        }

        // API-endpoint til at hente søgeresultater
        [HttpGet]
        public async Task<ActionResult<List<StringSearchResult>>> GetStringSearchResults([FromQuery] string searchName, [FromQuery] int userId)
        {
            var result = await _stringSearchDbBusinessService.GetStringSearchResultsAsync(searchName, userId);
            return Ok(result);
        }
    }
}
