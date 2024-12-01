using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer;
using System.Threading.Tasks;

namespace MovieApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetSearchHistoryDbController : ControllerBase
    {
        private readonly GetSearchHistoryDbBusinessService _getSearchHistoryDbBusinessService;

        public GetSearchHistoryDbController(GetSearchHistoryDbBusinessService getSearchHistoryDbBusinessService)
        {
            _getSearchHistoryDbBusinessService = getSearchHistoryDbBusinessService;
        }

        // API-endpoint til at hente søgehistorik for en bruger baseret på user_id
        [HttpGet]
        public async Task<IActionResult> GetSearchHistory([FromQuery] int userId)
        {
            var searchHistory = await _getSearchHistoryDbBusinessService.GetSearchHistoryForUserAsync(userId);
            if (searchHistory == null || searchHistory.Count == 0)
            {
                return NotFound("No search history found for the given user.");
            }
            return Ok(searchHistory);
        }
    }
}
